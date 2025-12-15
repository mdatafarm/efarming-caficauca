using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for manage the Fowering Period
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability")]
    public class FloweringPeriodsController : Controller
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmManager _manager;
        private IFloweringPeriodQualificationManager _floweringperiodqualificationmanager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FloweringPeriodsController" /> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="floweringperiodqualificationmanager">The floweringperiodqualificationmanager.</param>
        public FloweringPeriodsController(FarmManager manager, FloweringPeriodQualificationManager floweringperiodqualificationmanager)
        {
            _manager = manager;
            _floweringperiodqualificationmanager = floweringperiodqualificationmanager;
        }

        /// <summary>
        /// Indexes the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="plantationId">The plantation identifier.</param>
        /// <returns>PartialView</returns>
        public ActionResult Index(Guid farmId, Guid plantationId)
        {
            var farm = _manager.Details(farmId);
            var plantation = farm.Productivity.Plantations.First(p => p.Id.Equals(plantationId));
            return PartialView("~/Views/FloweringPeriods/Index.cshtml", plantation);
        }

        /// <summary>
        /// Creates the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="plantationId">The plantation identifier.</param>
        /// <returns>PartialView</returns>
        public ActionResult Create(Guid farmId, Guid plantationId)
        {
            var period = new FloweringPeriodDTO { PlantationId = plantationId, FarmId = farmId };
            return PartialView("~/Views/FloweringPeriods/Create.cshtml", period);
        }

        /// <summary>
        /// Creates the specified period.
        /// </summary>
        /// <param name="period">The period.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Create(FloweringPeriodDTO period)
        {
            try
            {
                var farm = _manager.Details(period.FarmId);
                var plantation = farm.Productivity.Plantations.First(p => p.Id.Equals(period.PlantationId));
                plantation.FloweringPeriods.Add(period);
                _manager.Edit(farm.Id, farm, FarmManager.FLOWERING_PERIODS);
                farm = _manager.Details(period.FarmId);
                plantation = farm.Productivity.Plantations.First(p => p.Id.Equals(period.PlantationId));
                return PartialView("~/Views/FloweringPeriods/Index.cshtml", plantation);
            }
            catch
            {
                return PartialView("~/Views/FloweringPeriods/Create.cshtml", period);
            }
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="plantationId">The plantation identifier.</param>
        /// <returns>PartialView</returns>
        public ActionResult Edit(Guid id, Guid farmId, Guid plantationId)
        {
            var farm = _manager.Details(farmId);
            var period = farm.Productivity
                .Plantations.First(p => p.Id.Equals(plantationId))
                .FloweringPeriods.First(fp => fp.Id.Equals(id));
            return PartialView("~/Views/FloweringPeriods/Edit.cshtml", period);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="period">The period.</param>
        /// <returns>c</returns>
        [HttpPost]
        public ActionResult Edit(Guid id, FloweringPeriodDTO period)
        {
            try
            {
                var farm = _manager.Details(period.FarmId);
                var plantation = farm.Productivity.Plantations.First(p => p.Id.Equals(period.PlantationId));
                var toRemove = plantation.FloweringPeriods.First(fp => fp.Id.Equals(id));
                plantation.FloweringPeriods.Remove(toRemove);
                plantation.FloweringPeriods.Add(period);
                _manager.Edit(farm.Id, farm, FarmManager.FLOWERING_PERIODS);
                farm = _manager.Details(period.FarmId);
                plantation = farm.Productivity.Plantations.First(p => p.Id.Equals(period.PlantationId));
                return PartialView("~/Views/FloweringPeriods/Index.cshtml", plantation);
            }
            catch
            {
                return PartialView("~/Views/FloweringPeriods/Edit.cshtml", period);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="plantationId">The plantation identifier.</param>
        /// <returns>+41763692322</returns>
        public ActionResult Delete(Guid id, Guid farmId, Guid plantationId)
        {
            var period = _manager.Details(farmId).Productivity
                .Plantations.First(p => p.Id.Equals(plantationId))
                .FloweringPeriods.First(fp => fp.Id.Equals(id));
            return PartialView("~/Views/FloweringPeriods/Delete.cshtml", period);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="period">The period.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Delete(Guid id, FloweringPeriodDTO period)
        {
            try
            {
                var farm = _manager.Details(period.FarmId);
                var plantation = farm.Productivity.Plantations.First(p => p.Id.Equals(period.PlantationId));
                var toRemove = plantation.FloweringPeriods.First(fp => fp.Id.Equals(id));
                plantation.FloweringPeriods.Remove(toRemove);
                _manager.Edit(farm.Id, farm, FarmManager.FLOWERING_PERIODS);
                farm = _manager.Details(period.FarmId);
                plantation = farm.Productivity.Plantations.First(p => p.Id.Equals(period.PlantationId));
                return PartialView("~/Views/FloweringPeriods/Index.cshtml", plantation);
            }
            catch
            {
                return PartialView("~/Views/FloweringPeriods/Delete.cshtml", period);
            }
        }
    }
}
