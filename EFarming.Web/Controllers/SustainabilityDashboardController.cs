using EFarming.Core.SustainabilityModule.DashboardAggregate;
using EFarming.DAL;
using EFarming.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    [CustomAuthorize(Roles = "Sustainability")]
    /// <summary>
    /// Controller for the Sustainability
    /// </summary>
    public class SustainabilityDashboardController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        private string result;

        public ActionResult TasqResults()
        {
            ViewBag.cooperatives = db.Cooperatives.ToList();
            var General_TASQ = db.ExecuteQuery<TASQResults>("SELECT * FROM TASQResults order by SubModuleOrder, CriteriaOrder");
            return View(General_TASQ);
        }

        public ActionResult TasqResultsByCoop(Guid? CoopId)
        {
            ViewBag.cooperatives = db.Cooperatives.ToList();
            ViewBag.cooperativeId = CoopId;
            ViewBag.cooperative = db.Cooperatives.Where(c => c.Id == CoopId).Select(c => c.Name).First().ToString();

            var General_TASQ = db.ExecuteQuery<TASQResults>("Sustainability_TASQ_By_Cooperative @CooperativeId", new SqlParameter("CooperativeId", CoopId.ToString()));
            return View(General_TASQ);
        }

        /// <summary>
        /// Tasqs the clasification.
        /// </summary>
        /// <returns>The View</returns>
        public ActionResult Sustainability()
        {   
            return View();
        }
    }
}