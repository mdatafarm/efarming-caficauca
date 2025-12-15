//using EFarming.Core.SustainabilityModule.DashboardAggregate;
//using EFarming.Manager.Contract;
//using EFarming.Manager.Implementation;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace EFarming.Web.Controllers
//{
//    /// <summary>
//    /// Controller for the Impact module
//    /// All the information provider by the survey application
//    /// This controller provide information for the response surveys
//    /// </summary>
//    public class ImpactPCC2015Controller : Controller
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        private IFarmManager _manager;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="ImpactPCC2015Controller"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        public ImpactPCC2015Controller(FarmManager manager)
//        {
//            _manager = manager;
//        }
//        /// <summary>
//        /// Pcs the C2015.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="SurveyID">The survey identifier.</param>
//        /// <returns>PartialView</returns>
//        public async Task<ActionResult> PCC2015(Guid id, int SurveyID)
//        {
//            var farm = _manager.Details(id);
//            string FarmCode = farm.Code.ToString();
//            if (farm != null)
//            {
//                var client = new HttpClient();
//                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["SurveysAPI"] + "TASQ/=?IdFarm=" + FarmCode + "&SurveyID=" + SurveyID);
//                response.EnsureSuccessStatusCode();
//                string result = await response.Content.ReadAsStringAsync();
//                List<TASQByFarm> items = JsonConvert.DeserializeObject<List<TASQByFarm>>(result);
//                ViewBag.TASQTable = items;

//                ViewBag.Farm = farm;
//                return PartialView(farm);
//            }
//            return PartialView(farm);
//        }
//    }
//}