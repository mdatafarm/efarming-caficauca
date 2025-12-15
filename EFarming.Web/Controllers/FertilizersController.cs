using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Web.Coocentral;
using EFarming.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for see the fertilizers
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability,Reader")]
    public class FertilizersController : Controller
    {
        public const int PERPAGE = 10;

        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FertilizersController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FertilizersController(FarmManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        public async Task<ActionResult> Index(Guid farmId, int? page = 1)
        {
            var farm = _manager.Details(farmId);
            ViewBag.PagedFertilizers = farm.Fertilizers.OrderByDescending(o => o.Date).ToPagedList(page.Value, PERPAGE);
            var groupedFertilizers = farm.Fertilizers.GroupBy(f => f.Date.Year)
                                                           .Select(g => new GroupedFertilizer {Year = g.Key, Quantity = g.Sum(f => f.Quantity), TotalValue = g.Sum(f => f.Value), Average = g.Average(f => f.UnitPrice) })
                                                           .OrderByDescending(y => y.Year)
                                                           .ToList();

            //GetFertilizersData Update = new GetFertilizersData();
            //ViewBag.PagedFertilizers = await Update.GetFertilizersInformation(farm.Code);
            ViewBag.groupedFertilizers = groupedFertilizers;
            return PartialView("~/Views/Fertilizers/Index.cshtml", farm);
        }

        /// <summary>
        /// Creates the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        public ActionResult Create(Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var Fertilizers = new FertilizerDTO { FarmId = farmId };
            return PartialView("~/Views/Fertilizers/Create.cshtml", Fertilizers);
        }

        /// <summary>
        /// Creates the specified fertilizers.
        /// </summary>
        /// <param name="Fertilizers">The fertilizers.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Create(FertilizerDTO Fertilizers, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(Fertilizers.FarmId);
                farm.Fertilizers.Add(Fertilizers);
                _manager.Edit(farm.Id, farm, FarmManager.FERTILIZERS);
                ViewBag.PagedFertilizers = farm.Fertilizers.ToPagedList(page.Value, PERPAGE);
                return PartialView("~/Views/Fertilizers/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/Fertilizers/Create.cshtml", Fertilizers);
            }
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        public ActionResult Edit(Guid id, Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var Fertilizers = _manager.Details(farmId).Fertilizers.Find(sa => sa.Id.Equals(id));
            return PartialView("~/Views/Fertilizers/Edit.cshtml", Fertilizers);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="Fertilizers">The fertilizers.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Edit(Guid id, FertilizerDTO Fertilizers, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(Fertilizers.FarmId);
                var toRemove = farm.Fertilizers.First(sa => sa.Id.Equals(id));
                farm.Fertilizers.Remove(toRemove);
                farm.Fertilizers.Add(Fertilizers);
                _manager.Edit(farm.Id, farm, FarmManager.FERTILIZERS);
                ViewBag.PagedFertilizers = farm.Fertilizers.ToPagedList(page.Value, PERPAGE);
                return PartialView("~/Views/Fertilizers/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/Fertilizers/Edit.cshtml", Fertilizers);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        public ActionResult Delete(Guid id, Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var Fertilizers = _manager.Details(farmId).Fertilizers.Find(sa => sa.Id.Equals(id));
            return PartialView("~/Views/Fertilizers/Delete.cshtml", Fertilizers);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="Fertilizers">The fertilizers.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Delete(Guid id, FertilizerDTO Fertilizers, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(Fertilizers.FarmId);
                var toRemove = farm.Fertilizers.First(sa => sa.Id.Equals(id));
                farm.Fertilizers.Remove(toRemove);
                _manager.Edit(farm.Id, farm, FarmManager.FERTILIZERS);
                ViewBag.PagedFertilizers = farm.Fertilizers.ToPagedList(page.Value, 6);
                return PartialView("~/Views/Fertilizers/Index.cshtml", farm);
            }
            catch
            {
                return View();
            }
        }
        public class GroupedFertilizer
        {
            public int Year { get; set; }
            public double Quantity { get; set; }
            public double TotalValue { get; set; }
            public double Average { get; set; }
        }
    }
}
