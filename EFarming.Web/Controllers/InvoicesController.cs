using EFarming.DTO.TraceabilityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Web.Coocentral;
using EFarming.Web.Models;
using PagedList;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for manage all the invoices of the farms
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability,Reader")]
    public class InvoicesController : BaseController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IInvoiceManager _manager;
        /// <summary>
        /// The _farm manager
        /// </summary>
        private IFarmManager _farmManager;
        /// <summary>
        /// The _lot manager
        /// </summary>
        private ILotManager _lotManager;

        /// <summary>
        /// The perpage
        /// </summary>
        public const int PERPAGE = 1000000;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoicesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="farmManager">The farm manager.</param>
        /// <param name="lotManager">The lot manager.</param>
        public InvoicesController(
            InvoiceManager manager,
            FarmManager farmManager,
            LotManager lotManager)
        {
            _manager = manager;
            _farmManager = farmManager;
            _lotManager = lotManager;
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm</returns>
        public async Task<ActionResult> Details(Guid id, DateTime? start, DateTime? end, Guid? lotId, int? page = 1)
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

            //ViewBag.Lots = new SelectList(_lotManager.GetAll(), "Id", "Code", lotId);

            var farm = _farmManager.Details(id);
            var PagedInvoices = _manager.GetAllByFarm(id, start, end, lotId).OrderByDescending(o => o.Date).ToPagedList(page.Value, PERPAGE);

            var PagedInvoicesWet = PagedInvoices.Where(t => t.CoffeeTypeId == 6).Select( o => new InvoiceDTO
            {
                Id = o.Id,
                Cash = o.Cash,
                CoffeeTypeId = o.CoffeeTypeId,
                CreatedAt = o.CreatedAt,
                Date = o.Date,
                DateInvoice = o.DateInvoice,
                FarmId = o.FarmId,
                Hold = o.Hold,
                Identification = o.Identification,
                InvoiceNumber = o.InvoiceNumber,
                IsNew = o.IsNew,
                Ubication = o.Ubication,
                UpdatedAt = o.UpdatedAt,
                BaseKg = o.BaseKg,
                Value = o.Value,
                Weight = o.Weight/2
            });
            var PagedInvoicesDry = PagedInvoices.Where(t => t.CoffeeTypeId != 6).Select(o => new InvoiceDTO
            {
                Id = o.Id,
                Cash = o.Cash,
                CoffeeTypeId = o.CoffeeTypeId,
                CreatedAt = o.CreatedAt,
                Date = o.Date,
                DateInvoice = o.DateInvoice,
                FarmId = o.FarmId,
                Hold = o.Hold,
                Identification = o.Identification,
                InvoiceNumber = o.InvoiceNumber,
                IsNew = o.IsNew,
                Ubication = o.Ubication,
                UpdatedAt = o.UpdatedAt,
                BaseKg = o.BaseKg,
                Value = o.Value,
                Weight = o.Weight
            });

            IPagedList<InvoiceDTO> PagedInovicesForView = PagedInvoicesWet.Union(PagedInvoicesDry).OrderByDescending(o => o.Date).ToPagedList(page.Value, PERPAGE);

            ViewBag.PagedInvoices = PagedInovicesForView;


            var groupedInvoices = PagedInvoicesWet.Union(PagedInvoicesDry)
                .GroupBy(y => y.Date.Year)
                .Select(g => new groupedInvoice
                {
                    Year = g.Key,
                    Totalkg = g.Sum(i => i.Weight),
                    TotalValue = g.Sum(i => i.Value),
                    AverageValue = g.Sum(i => i.Value)/ g.Sum(i => i.Weight)
                }).OrderByDescending(y => y.Year)
                .ToList();

            ViewBag.groupedInvoices = groupedInvoices;
            //GetInvoicesData Update = new GetInvoicesData();
            //ViewBag.PagedInvoices = await Update.GetInvoicesInformation(farm.Code);
            return PartialView("~/Views/Invoices/Details.cshtml", farm);
        }

        /// <summary>
        /// Creates the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with ivoice</returns>
        public ActionResult Create(Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            ViewBag.Lots = new SelectList(_lotManager.GetAll(), "Id", "Code");
            var invoice = new InvoiceDTO { FarmId = farmId };
            return PartialView("~/Views/Invoices/Create.cshtml", invoice);
        }

        /// <summary>
        /// Creates the specified invoice.
        /// </summary>
        /// <param name="invoice">The invoice.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm</returns>
        [HttpPost]
        public ActionResult Create(InvoiceDTO invoice, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            try
            {
                _manager.Add(invoice);
                var farm = _farmManager.Details(invoice.FarmId);
                ViewBag.PagedInvoices = _manager.GetAllByFarm(invoice.FarmId, null, null, null).ToPagedList(page.Value, PERPAGE);
                return PartialView("~/Views/Invoices/Index.cshtml", farm);
            }
            catch
            {
                ViewBag.Lots = new SelectList(_lotManager.GetAll(), "Id", "Code", null);
                return PartialView("~/Views/Invoices/Create.cshtml", invoice);
            }
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>Partialview with invoice</returns>
        public ActionResult Edit(Guid id, Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var invoice = _manager.Get(id);
            ViewBag.Lots = new SelectList(_lotManager.GetAll(), "Id", "Code", null);
            return PartialView("~/Views/Invoices/Edit.cshtml", invoice);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="invoice">The invoice.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm or invoice</returns>
        [HttpPost]
        public ActionResult Edit(Guid id, InvoiceDTO invoice, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            try
            {
                _manager.Edit(invoice);
                var farm = _farmManager.Details(invoice.FarmId);
                ViewBag.PagedInvoices = _manager.GetAllByFarm(id, null, null, null).ToPagedList(page.Value, PERPAGE);
                return PartialView("~/Views/Invoices/Index.cshtml", farm);
            }
            catch
            {
                ViewBag.Lots = new SelectList(_lotManager.GetAll(), "Id", "Code", null);
                return PartialView("~/Views/Invoices/Edit.cshtml", invoice);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with invoice</returns>
        public ActionResult Delete(Guid id, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var invoice = _manager.Get(id);
            return PartialView("~/Views/Invoices/Delete.cshtml", invoice);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="invoice">The invoice.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView with farm or invoice</returns>
        [HttpPost]
        public ActionResult Delete(Guid id, InvoiceDTO invoice, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            try
            {
                _manager.Remove(invoice);
                var farm = _farmManager.Details(invoice.FarmId);
                ViewBag.PagedInvoices = _manager.GetAllByFarm(id, null, null, null).ToPagedList(page.Value, PERPAGE);
                return PartialView("~/Views/Invoices/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/Invoices/Delete.cshtml", invoice);
            }
        }
        public class groupedInvoice
        {
            public int Year { get; set; }
            public double Totalkg { get; set; }
            public double TotalValue { get; set; }
            public double AverageValue { get; set; }
        }
    }
}