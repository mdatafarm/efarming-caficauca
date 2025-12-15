using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for manage the Quality Assessments
    /// </summary>
    [CustomAuthorize(Roles = "User,Quality,Manager,Taster,Admin,Reader")]
    public class QualityAssessmentsController : BaseController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private ISensoryProfileManager _manager;

        /// <summary>
        /// The _quality manager
        /// </summary>
        private IQualityAttributeManager _qualityManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="QualityAssessmentsController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="qualityManager">The quality manager.</param>
        public QualityAssessmentsController(SensoryProfileManager manager, QualityAttributeManager qualityManager)
        {
            _manager = manager;
            _qualityManager = qualityManager;
        }

        /// <summary>
        /// Indexes the specified description.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="buscar">if set to <c>true</c> [buscar].</param>
        /// <returns>Tha view with assessments</returns>
        [HttpGet]
        public ActionResult Index(string description, bool buscar = false)
        {
            IEnumerable<SensoryProfileAssessmentDTO> assessments;
            Guid? userId = Guid.Empty;

            if (buscar)
            {
                if (!User.IsTaster())
                    userId = User.UserId;
                else
                    userId = null;
            }

            assessments = _manager.Filter(userId, description);

            return View("Index", assessments);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The View</returns>
        //[HttpGet]
        public ActionResult Edit(Guid id)
        {
            var attributes = GetAssessmentAttributes(id);
            if (attributes.Any())
                return View("Edit", attributes);
            else
                return View("Index");
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="qualityAttributes">The quality attributes.</param>
        /// <returns>The View Edit</returns>
        [HttpPost]
        public ActionResult Update(Guid id, FormCollection qualityAttributes)
        {
            var assessment = _manager.Get(id);
            assessment.SensoryProfileAnswers.Clear();
            Dictionary<string, string> form = qualityAttributes.AllKeys.ToDictionary(k => k, v => qualityAttributes[v]);
            foreach (var qa in form)
            {
                var qaId = qa.Key.Replace("[]", string.Empty);
                var attr = _qualityManager.Find(Guid.Parse(qaId));
                var answers = qa.Value.Split(',');
                foreach (var answer in answers)
                {
                    assessment.SensoryProfileAnswers.Add(new SensoryProfileAnswerDTO
                    {
                        Answer = answer,
                        QualityAttributeId = attr.Id,
                        SensoryProfileAssessmentId = assessment.Id
                    });
                }
            }
            _manager.Edit(assessment);
            var attributes = GetAssessmentAttributes(id);
            return View("Edit", attributes);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The reports view</returns>
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _manager.Destroy(id);
                return RedirectToAction("Index", "Reports");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Reports");
                throw;
            }
        }

        /// <summary>
        /// Gets the assessment attributes.
        /// </summary>
        /// <param name="assessmentId">The assessment identifier.</param>
        /// <returns>icollection of qualityattributeDTO</returns>
        private ICollection<QualityAttributeDTO> GetAssessmentAttributes(Guid assessmentId)
        {
            ViewBag.AssessmentId = assessmentId;
            var assessment = _manager.Get(assessmentId);
            ViewBag.Description = assessment.Description;
            ViewBag.Date = assessment.Date.ToShortDateString();
            ViewBag.User = assessment.User.FullName;
            var attributes = _qualityManager.Get(assessment.AssessmentTemplateId);

            foreach (var attribute in attributes)
            {
                var res = assessment.SensoryProfileAnswers
                    .Where(spa => spa.QualityAttributeId.Equals(attribute.Id));
                var answers = string.Empty;
                if (res.Any())
                    answers = res.Select(ans => ans.Answer).Aggregate((current, next) => current + "," + next);
                attribute.Answer = answers != null ? answers : string.Empty;
            }
            return attributes;
        }
    }
}