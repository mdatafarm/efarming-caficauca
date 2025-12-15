//using EFarming.DAL;
//using EFarming.Manager.Contract;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;

//namespace EFarming.Web.Controllers
//{
//    /// <summary>
//    /// Controller for the Commercial Reports
//    /// </summary>
//    public class CommercialReportsController : Controller
//    {
//        /// <summary>
//        /// The _lot manager
//        /// </summary>
//        private ILotManager _lotManager;

//        private UnitOfWork db = new UnitOfWork();

//        /// <summary>
//        /// Initializes a new instance of the <see cref="CommercialReportsController"/> class.
//        /// </summary>
//        /// <param name="lotManager">The lot manager.</param>
//        public CommercialReportsController(ILotManager lotManager){
//	  _lotManager = lotManager;
//        }

//        /// <summary>
//        /// Comertials the dashboard.
//        /// </summary>
//        /// <returns>the lots</returns>
//        public ActionResult CommercialDashboard()
//        {  
//	  IEnumerable<DTO.TraceabilityModule.LotDTO> _lots = _lotManager.GetAll();
//	  return (View(_lots));
//        }

//        /// <summary>
//        /// Shows the dashboard for the contracts information
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult CommercialContractsDashboard()
//        {
//            ViewBag.Clients = db.Clients.ToList();
//            return View();
//        }

//        /// <summary>
//        /// Shows the dashboard for the contracts information by Client
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult CommercialContractsDashboardByClient(string id, string Name)
//        {
//            ViewBag.ClientId = id;
//            ViewBag.ClientName = Name;
//            ViewBag.Clients = db.Clients.ToList();
//            return View();
//        }
//    }
//}