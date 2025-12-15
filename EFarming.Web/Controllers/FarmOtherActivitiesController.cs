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
    /// Controller for manage the others activities of the farm
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability")]
    public class FarmOtherActivitiesController : Controller
    {
        public const int PERPAGE = 6;
        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmOtherActivitiesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FarmOtherActivitiesController(FarmManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        public ActionResult Index(Guid farmId, int? page = 1)
        {
            var farm = _manager.Details(farmId);
            ViewBag.PagedActivities = farm.OtherActivities.ToPagedList(page.Value, PERPAGE);
            return PartialView("~/Views/FarmOtherActivities/Index.cshtml", farm);
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
            var OtherActivities = new FarmOtherActivityDTO { FarmId = farmId };
            return PartialView("~/Views/FarmOtherActivities/Create.cshtml", OtherActivities);
        }

        /// <summary>
        /// Creates the specified other activity.
        /// </summary>
        /// <param name="OtherActivity">The other activity.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Create(FarmOtherActivityDTO OtherActivity, int? page = 1)
        {
            try
            {
                //OtherActivity.Percentage = OtherActivity.InputPercentage / 100d;
                var farm = _manager.Details(OtherActivity.FarmId);
                farm.OtherActivities.Add(OtherActivity);
                _manager.Edit(farm.Id, farm, FarmManager.OTHER_ACTIVITIES);
                farm = _manager.Details(OtherActivity.FarmId);
                ViewBag.PagedActivities = farm.OtherActivities.ToPagedList(page.Value, PERPAGE);
                return PartialView("~/Views/FarmOtherActivities/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/FarmOtherActivities/Create.cshtml", OtherActivity);
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
            var OtherActivity = _manager.Details(farmId).OtherActivities.Find(sa => sa.Id.Equals(id));
            //OtherActivity.InputPercentage = Convert.ToInt32(OtherActivity.Percentage * 100);
            return PartialView("~/Views/FarmOtherActivities/Edit.cshtml", OtherActivity);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="OtherActivity">The other activity.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Edit(Guid id, FarmOtherActivityDTO OtherActivity, int? page = 1)
        {
            try
            {
                //OtherActivity.Percentage = OtherActivity.InputPercentage / 100d;
                var farm = _manager.Details(OtherActivity.FarmId);
                var toRemove = farm.OtherActivities.First(sa => sa.Id.Equals(id));
                farm.OtherActivities.Remove(toRemove);
                farm.OtherActivities.Add(OtherActivity);
                _manager.Edit(farm.Id, farm, FarmManager.OTHER_ACTIVITIES);
                farm = _manager.Details(OtherActivity.FarmId);
                ViewBag.PagedActivities = farm.OtherActivities.ToPagedList(page.Value, PERPAGE);
                return PartialView("~/Views/FarmOtherActivities/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/FarmOtherActivities/Edit.cshtml", OtherActivity);
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
            var OtherActivities = _manager.Details(farmId).OtherActivities.Find(sa => sa.Id.Equals(id));
            return PartialView("~/Views/FarmOtherActivities/Delete.cshtml", OtherActivities);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="OtherActivity">The other activity.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Delete(Guid id, FarmOtherActivityDTO OtherActivity, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(OtherActivity.FarmId);
                var toRemove = farm.OtherActivities.First(sa => sa.Id.Equals(id));
                farm.OtherActivities.Remove(toRemove);
                _manager.Edit(farm.Id, farm, FarmManager.OTHER_ACTIVITIES);
                farm = _manager.Details(OtherActivity.FarmId);
                ViewBag.PagedActivities = farm.OtherActivities.ToPagedList(page.Value, 6);
                return PartialView("~/Views/FarmOtherActivities/Index.cshtml", farm);
            }
            catch
            {
                return View();
            }
        }
    }
}
