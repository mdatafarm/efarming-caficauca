//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using EFarming.Core.ComercialModule;
//using EFarming.DAL;
//using MvcRazorToPdf;
//using iTextSharp.text;
//using EFarming.Web.Models;

//namespace EFarming.Web.Areas.Comercial.Controllers
//{
//    /// <summary>
//    /// Contract Controller
//    /// The principal controller for manage the contracts
//    /// </summary>
//    [CustomAuthorize(Roles = "Commercial")]
//    public class ContractInvoicesController : Controller
//    {
//        private UnitOfWork db = new UnitOfWork();

//        /// <summary>
//        /// Get the invoice contracts.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Index()
//        {
//            var contractInvoices = db.ContractInvoice.Where(i => i.Id != new Guid("{3144EA78-E766-42A3-B40B-3C5F1BAB7D98}")).Include(c => c.Shipment);
//            return View(contractInvoices.ToList());
//        }

//        /// <summary>
//        /// Detailses the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            ContractInvoice contractInvoice = db.ContractInvoice.Find(id);
//            if (contractInvoice == null)
//            {
//                return HttpNotFound();
//            }

//            //Set a default InvoiceId for search the associated lots
//            Guid InvoiceId = (id.HasValue) ? id.Value : new Guid("{00000000-0000-0000-0000-000000000000}");
//            List<ContractLot> Lotslist = db.ContractLot.Where(s => s.ShipmentId == InvoiceId).ToList();
//            ViewBag.LotsList = Lotslist;

//            //Obtaining the Document references and all the references for the lots depending of the client
//            List<ReferenceRelationShip> lotRefererencesRelationship = new List<ReferenceRelationShip>();
//            List<ReferenceRelationShip> lotRefererencesRelationshipAll = new List<ReferenceRelationShip>();

//            //Adding all the lot references and saving it in lists
//            foreach (var lot in Lotslist)
//            {
//                var referencesByLot = db.ReferenceRelationShip.Where(l => l.DocumentId == lot.Id && l.DocumentReference.Type == "Lot");
//                var DocumentReferencesByLot = referencesByLot.Where(l => l.DocumentId == lot.Id && l.DocumentReference.Type == "Lot" && l.DocumentReference.RefType == "Document");
//                lotRefererencesRelationship.AddRange(DocumentReferencesByLot);
//                lotRefererencesRelationshipAll.AddRange(referencesByLot);
//            }
//            ViewBag.lotRefererencesRelationshipAll = lotRefererencesRelationshipAll;
//            ViewBag.lotRefererencesRelationship = lotRefererencesRelationship;

//            //Obtaining the Agreement for the lots
//            ViewBag.Agreement = Lotslist.First().Agreement;
//            var clientId = Lotslist.First().Agreement.ClientId;

//            //Creating the Merchandise description lists
//            List<MoreInformation> MDTypes = new List<MoreInformation>(); 
//            MDTypes = db.Moreinformation.Where(c => c.ClientId == clientId && c.InformationType == "Quality").ToList();
//            ViewBag.MDTypes = MDTypes;

//            List<MDOrigin> MDOrigins = new List<MDOrigin>();
//            MDOrigins = db.MDOrigin.ToList();
//            ViewBag.MDOrigins = MDOrigins;

//            List<DocumentReference> References = new List<DocumentReference>();
//            List<DocumentReference> ReferencesAll = new List<DocumentReference>();
//            ReferencesAll = db.DocumentReference.Where(c => c.ClientId == clientId && c.Type == "Lot").ToList();
//            References = ReferencesAll.Where(c => c.ClientId == clientId && c.Type == "Lot" && c.RefType == "Document").ToList();
//            ViewBag.References = References;
//            ViewBag.ReferencesAll = ReferencesAll;

//            return new PdfActionResult(contractInvoice, (writer, document) => {
//                document.SetPageSize(new Rectangle(792f, 612f, 90));
//                document.NewPage();
//            });
//        }

//        // GET: Comercial/ContractInvoices/Create
//        /// <summary>
//        /// Creates the specified Invoice with the same shipment id.
//        /// </summary>
//        /// <param name="Shipmentid">The shipmentid.</param>
//        /// <returns></returns>
//        public ActionResult Create(Guid Shipmentid)
//        {
//            ContractInvoice Invoice = new ContractInvoice();
//            Invoice.Id = Shipmentid;
//            ViewBag.Id = new SelectList(db.Shipments, "Id", "DocumentBL");
//            Shipment Shipment = db.Shipments.Find(Shipmentid);
//            ViewBag.Shipment = Shipment;
//            return View(Invoice);
//        }

//        /// <summary>
//        /// Creates the specified contract invoice.
//        /// </summary>
//        /// <param name="contractInvoice">The contract invoice.</param>
//        /// <param name="Shipmentid">The shipmentid.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,Number,CafexportReceiptDate,CafexportPaymentDeadline,ClientPaymentDeadLine,CafexportPaymentDate,ClientPaymentDate,CreatedAt,UpdatedAt,DeletedAt,ShipmentId")] ContractInvoice contractInvoice, Guid? Shipmentid)
//        {
//            if (ModelState.IsValid)
//            {
//                contractInvoice.Id = (Shipmentid.HasValue)?Shipmentid.Value:new Guid("{00000000-0000-0000-0000-000000000000}");
//                var InvoicesList = db.ContractInvoice.ToList();
//                int lastInvoice = 1;
//                if(InvoicesList.Count() > 0)
//                {
//                    lastInvoice = InvoicesList.OrderByDescending(x => x.Number).ToList().First().Number + 1;
//                }
//                contractInvoice.Number = lastInvoice;
//                db.ContractInvoice.Add(contractInvoice);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            Shipment Shipment = db.Shipments.Find(Shipmentid);
//            ViewBag.Shipment = Shipment;
//            ViewBag.Id = new SelectList(db.Shipments, "Id", "DocumentBL", contractInvoice.Id);
//            return View(contractInvoice);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Edit(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
//	  ContractInvoice contractInvoice = db.ContractInvoice.Find(id);

//	  if (contractInvoice == null)
//                return HttpNotFound();
            
//	  ViewBag.Id = new SelectList(db.Shipments, "Id", "DocumentBL", contractInvoice.Id);
//            return View(contractInvoice);
//        }

//        /// <summary>
//        /// Edits the specified contract invoice.
//        /// </summary>
//        /// <param name="contractInvoice">The contract invoice.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,Number,CafexportReceiptDate,CafexportPaymentDeadline,ClientPaymentDeadLine,CafexportPaymentDate,ClientPaymentDate,CreatedAt,UpdatedAt,DeletedAt")] ContractInvoice contractInvoice)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(contractInvoice).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.Id = new SelectList(db.Shipments, "Id", "DocumentBL", contractInvoice.Id);
//            return View(contractInvoice);
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Delete(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
//	  ContractInvoice contractInvoice = db.ContractInvoice.Find(id);
            
//	  if (contractInvoice == null)
//                return HttpNotFound();
            
//	  return View(contractInvoice);
//        }

//        /// <summary>
//        /// Deletes the confirmed.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(Guid id)
//        {
//            ContractInvoice contractInvoice = db.ContractInvoice.Find(id);
//            db.ContractInvoice.Remove(contractInvoice);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        /// <summary>
//        /// Releases unmanaged resources and optionally releases managed resources.
//        /// </summary>
//        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//                db.Dispose();
//            base.Dispose(disposing);
//        }
//    }
//}
