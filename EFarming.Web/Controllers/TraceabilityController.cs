using EFarming.Manager.Contract;
using EFarming.Web.Models;
using System;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for the Traceability
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability")]    
    public class TraceabilityController : BaseController
    {
        /// <summary>
        /// The _invoice manager
        /// </summary>
        private IInvoiceManager _invoiceManager;
        /// <summary>
        /// The _lot manager
        /// </summary>
        private ILotManager _lotManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceabilityController"/> class.
        /// </summary>
        /// <param name="invoiceManager">The invoice manager.</param>
        /// <param name="lotManager">The lot manager.</param>
        public TraceabilityController(IInvoiceManager invoiceManager,ILotManager lotManager){
            _invoiceManager = invoiceManager;
            _lotManager = lotManager;
        }

        /// <summary>
        /// Indexes the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns>The View</returns>
        public ActionResult Index(DateTime? start, DateTime? end, Guid? lotId)
        {
            if (start.HasValue)
                ViewBag.SelectedStart = string.Format("{0:yyyy-MM-dd}", start.Value);
            else
                ViewBag.SelectedStart = string.Empty;

            if (end.HasValue)
                ViewBag.SelectedEnd = string.Format("{0:yyyy-MM-dd}", end.Value);
            else
                ViewBag.SelectedEnd = string.Empty;

            if (lotId.HasValue)
                ViewBag.SeletedLotId = lotId.Value;
            else
                ViewBag.SeletedLotId = string.Empty;

            ViewBag.Lots = new SelectList(_lotManager.GetAll(), "Id", "Code", lotId);
            var data = _invoiceManager.Sellers(start, end, lotId);
            ViewBag.TopSellers = data;
            return View();
        }
    }
}