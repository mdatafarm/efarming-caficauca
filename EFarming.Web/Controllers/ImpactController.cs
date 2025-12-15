using EFarming.Core.SustainabilityModule.DashboardAggregate;
using EFarming.DAL;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using EFarming.Core.TasqModule;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for manage the impact information
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability,Reader")]
    public class ImpactController : BaseController
    {
        private UnitOfWork db = new UnitOfWork();
        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="ImpactController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public ImpactController(FarmManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>View With farm</returns>
        public ActionResult Details(Guid id)
        {
            var farm = _manager.Details(id);
            if (farm != null)
            {
                ViewBag.Farm = farm;
                return View(farm);
            }
            return View("~/Views/Shared/NotFound.cshtml");
        }

        /// <summary>
        /// Tasqs the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Partialview with farm</returns>
        public async Task<ActionResult> TASQ(Guid id)
        {
            var farm = _manager.Details(id);
            string FarmCode = farm.Code.ToString();
            if (farm != null)
            {
                var listAssessments = db.TASQAssessmentAnswer
                    .Where(a => a.TASQAssessment.FarmId == id)
                    .GroupBy(a => a.TASQAssessmentId)
                    .Select(g => new groupedAssessments
                    {
                        Answers = g.OrderBy(a => a.Criteria.SubModule.Module.ModuleOrder)
                            .ThenBy(a => a.Criteria.SubModule.SubModuleOrder)
                            .ThenBy(a => a.Criteria.CriteriaOrder)
                            .ToList()
                    })
                    .ToList();
                return PartialView(listAssessments);
            }

            return PartialView();
        }

        /// <summary>
        /// Pcs the C2015.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="SurveyID">The survey identifier.</param>
        /// <returns>PartialView with farm</returns>
        public async Task<ActionResult> PCC2015(Guid id, int SurveyID)
        {
            var farm = _manager.Details(id);
            string FarmCode = farm.Code.ToString();
            if (farm != null)
            {
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["SurveysAPI"] + "TASQ/=?IdFarm=" + FarmCode + "&SurveyID=" + SurveyID);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                List<TASQByFarm> items = JsonConvert.DeserializeObject<List<TASQByFarm>>(result);
                ViewBag.TASQTable = items;

                ViewBag.Farm = farm;
                return PartialView(farm);
            }
            return PartialView(farm);
        }
        public class groupedAssessments
        {
            public List<TASQAssessmentAnswer> Answers { get; set; }
        }
    }
}
