using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Web.Models;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for the Soil Analysis of the farm
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability")]
    public class SoilAnalysisController : Controller
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="SoilAnalysisController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public SoilAnalysisController(FarmManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm</returns>
        public ActionResult Index(Guid farmId, int? page = 1)
        {
            var farm = _manager.Details(farmId);
            ViewBag.PagedSoilAnalysis = farm.SoilAnalysis.ToPagedList(page.Value, 6);
            return PartialView("~/Views/SoilAnalysis/Index.cshtml", farm);
        }

        /// <summary>
        /// Creates the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with solAnalysis</returns>
        public ActionResult Create(Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var soilAnalysis = new SoilAnalysisDTO { FarmId = farmId };
            return PartialView("~/Views/SoilAnalysis/Create.cshtml", soilAnalysis);
        }

        /// <summary>
        /// Creates the specified soil analysis.
        /// </summary>
        /// <param name="soilAnalysis">The soil analysis.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm or soilAnalysis</returns>
        [HttpPost]
        public ActionResult Create(SoilAnalysisDTO soilAnalysis, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(soilAnalysis.FarmId);
                farm.SoilAnalysis.Add(soilAnalysis);
                _manager.Edit(farm.Id, farm, FarmManager.SOIL_ANALYSIS);
                ViewBag.PagedSoilAnalysis = farm.SoilAnalysis.ToPagedList(page.Value, 6);
                return PartialView("~/Views/SoilAnalysis/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/SoilTypes/Create.cshtml", soilAnalysis);
            }
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with solAnalysis</returns>
        public ActionResult Edit(Guid id, Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var soilAnalysis = _manager.Details(farmId).SoilAnalysis.Find(sa => sa.Id.Equals(id));
            return PartialView("~/Views/SoilAnalysis/Edit.cshtml", soilAnalysis);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="soilAnalysis">The soil analysis.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm or soilAnalysis</returns>
        [HttpPost]
        public ActionResult Edit(Guid id, SoilAnalysisDTO soilAnalysis, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(soilAnalysis.FarmId);
                var toRemove = farm.SoilAnalysis.First(sa => sa.Id.Equals(id));
                farm.SoilAnalysis.Remove(toRemove);
                farm.SoilAnalysis.Add(soilAnalysis);
                _manager.Edit(farm.Id, farm, FarmManager.SOIL_ANALYSIS);
                ViewBag.PagedSoilAnalysis = farm.SoilAnalysis.ToPagedList(page.Value, 6);
                return PartialView("~/Views/SoilAnalysis/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/SoilTypes/Edit.cshtml", soilAnalysis);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>partialview with solAnalysis</returns>
        public ActionResult Delete(Guid id, Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var soilAnalysis = _manager.Details(farmId).SoilAnalysis.Find(sa => sa.Id.Equals(id));
            return PartialView("~/Views/SoilAnalysis/Delete.cshtml", soilAnalysis);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="soilAnalysis">The soil analysis.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm or The View</returns>
        [HttpPost]
        public ActionResult Delete(Guid id, SoilAnalysisDTO soilAnalysis, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(soilAnalysis.FarmId);
                var toRemove = farm.SoilAnalysis.First(sa => sa.Id.Equals(id));
                farm.SoilAnalysis.Remove(toRemove);
                _manager.Edit(farm.Id, farm, FarmManager.SOIL_ANALYSIS);
                ViewBag.PagedSoilAnalysis = farm.SoilAnalysis.ToPagedList(page.Value, 6);
                return PartialView("~/Views/SoilAnalysis/Index.cshtml", farm);
            }
            catch
            {
                return View();
            }
        }
    }
}
