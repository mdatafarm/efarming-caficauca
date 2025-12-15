using EFarming.DAL;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    public class ProductivityController : BaseController
    {
        private IFarmManager _farmManager;
        private UnitOfWork db = new UnitOfWork();
        public ProductivityController(FarmManager farmManager)
        {
            _farmManager = farmManager;
        }
        // GET: Productivity
        public ActionResult Index(Guid farmId)
        {
            var farm = _farmManager.Details(farmId);
            var productivitiesChange = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
            ViewBag.opeColombia = productivitiesChange.percentageColombia;
            ViewBag.opeCaturra = productivitiesChange.percentageCaturra;
            ViewBag.opeCastillo = productivitiesChange.percentageCastillo;
            ViewBag.opeOtro = productivitiesChange.percentageotra;
            ViewBag.total = null;
            if (Math.Round((ViewBag.opeColombia + ViewBag.opeCaturra + ViewBag.opeCastillo + ViewBag.opeOtro), 2) >= 100)
            {
                ViewBag.total = 100;
            }
            else
            {
                ViewBag.total = Math.Round(ViewBag.opeColombia + ViewBag.opeCaturra + ViewBag.opeCastillo + ViewBag.opeOtro);
            }

            farm.Productivity.percentageColombia = productivitiesChange.percentageColombia;
            farm.Productivity.percentageCaturra = productivitiesChange.percentageCaturra;
            farm.Productivity.percentageCastillo = productivitiesChange.percentageCastillo;
            farm.Productivity.percentageotra = productivitiesChange.percentageotra;
            return PartialView("~/Views/Productivity/_Productivity.cshtml", farm);
        }
    }
}