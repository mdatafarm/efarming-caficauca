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
    /// Controller for the Sensory Profile 
    /// </summary>
    [CustomAuthorize(Roles = "User,Quality,Taster")]
    public class SensoryProfileController : BaseController
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
        /// The _farm manager
        /// </summary>
        private IFarmManager _farmManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SensoryProfileController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="farmManager">The farm manager.</param>
        /// <param name="qualityManager">The quality manager.</param>
        public SensoryProfileController(
            SensoryProfileManager manager,
            FarmManager farmManager,
            QualityAttributeManager qualityManager)
        {
            _manager = manager;
            _farmManager = farmManager;
            _qualityManager = qualityManager;
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The View with farm</returns>
        public ActionResult Details(Guid id)
        {
            var farm = _farmManager.Details(id);
            ViewBag.Farm = farm;
            return View(farm);
        }

        /// <summary>
        /// Creates the specified assessment identifier.
        /// </summary>
        /// <param name="assessmentId">The assessment identifier.</param>
        /// <returns>The View with attributes</returns>
        public ActionResult Create(Guid assessmentId)
        {
            var attributes = GetAssessmentAttributes(assessmentId);

            return View(attributes);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="qualityAttributes">The quality attributes.</param>
        /// <returns>The View Create with attributes</returns>
        [HttpPut]
        public ActionResult Edit(Guid id, FormCollection qualityAttributes)
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
            return View("Create", attributes);
        }

        /// <summary>
        /// Gets the assessment attributes.
        /// </summary>
        /// <param name="assessmentId">The assessment identifier.</param>
        /// <returns>attributes</returns>
        private ICollection<QualityAttributeDTO> GetAssessmentAttributes(Guid assessmentId)
        {
            ViewBag.AssessmentId = assessmentId;
            var assessment = _manager.Get(assessmentId);
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