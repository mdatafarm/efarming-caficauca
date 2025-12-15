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
//using EFarming.Web.Models;

//namespace EFarming.Web.Areas.Comercial.Controllers
//{
//    [CustomAuthorize(Roles = "Commercial")]
//    public class ShipmentsController : Controller
//    {
//        private UnitOfWork db = new UnitOfWork();

//        // GET: Comercial/Shipments
//        public ActionResult Index()
//        {
//            //Shows the shipments without the Not shipment yet
//            return View(db.Shipments.Where(s => s.Id != new Guid("{3144EA78-E766-42A3-B40B-3C5F1BAB7D98}")).ToList());
//        }

//        // GET: Comercial/Shipments/Details/5
//        public ActionResult Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Shipment shipment = db.Shipments.Find(id);
//            if (shipment == null)
//            {
//                return HttpNotFound();
//            }
//            return View(shipment);
//        }

//        /// <summary>
//        /// Class for the objects to be passed to the shipment
//        /// </summary>
//        public class LotAndReferences
//        {
//            public ContractLot lot { get; set; }
//            public List<ReferenceRelationShip> references { get; set; }
//        }

//        // GET: Comercial/Shipments/Create
//        public ActionResult Create(Guid[] lotsForShipment)
//        {
//            Guid[] ContracstForLotsTemp = (Guid[])TempData["ContractsForLotsTemp"];

//            if (ContracstForLotsTemp != null)
//                lotsForShipment = ContracstForLotsTemp;

//            List<ContractLot> lotsList = new List<ContractLot>();
//            List<LotAndReferences> LotsWithReferences = new List<LotAndReferences>();
//            List<ReferenceRelationShip> ReferencesByLot = new List<ReferenceRelationShip>();
//            List<DocumentReference> referencesList = new List<DocumentReference>();
//            List<Guid> lotsId = new List<Guid>();

//            if(lotsForShipment != null)
//            {
//                foreach (Guid id in lotsForShipment)
//                {
//                    ContractLot lot = db.ContractLot.Find(id);
//                    lotsId.Add(lot.Id);
//                    ViewBag.Contract = lot.Agreement;
//                    referencesList = db.DocumentReference.Where(r => r.ClientId == lot.Agreement.ClientId && r.Type == "Lot" && r.RefType == "Document").ToList();
//                    var referencesByLot = db.ReferenceRelationShip.Where(l => l.DocumentId == id && l.DocumentReference.Type == "Lot");
//                    var DocumentReferencesByLot = referencesByLot.Where(l => l.DocumentId == id && l.DocumentReference.Type == "Lot" && l.DocumentReference.RefType == "Document").ToList();
//                    lotsList.Add(lot);
//                    LotsWithReferences.Add(new LotAndReferences { lot = lot, references = DocumentReferencesByLot });
//                }
//                ViewBag.Lots = LotsWithReferences;
//                ViewBag.referencesList = referencesList;
//                ViewBag.LotsId = lotsId;

//                return View();
//            }
//            return View();
//        }

//        // POST: Comercial/Shipments/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,DocumentBL,ShippingDate,PortOfLanding,PortOfDestination,Vessel,ShippingLine,ExpocafeInvoice,CreatedAt,UpdatedAt,DeletedAt")] Shipment shipment, Guid[] Lots, DateTime? invoiceReceipt)
//        {
//            ViewBag.Lots = new SelectList(db.ContractLot.Where(l => l.IcoMark != null && l.ShipmentId == new Guid("3144EA78-E766-42A3-B40B-3C5F1BAB7D98")), "Id", "IcoMark", ViewBag.Lots);
//            if (ModelState.IsValid)
//            {
//                DateTime InvoiceReceiptNotNull = invoiceReceipt ?? DateTime.Now;
//                Agreement Contract = new Agreement();
//                shipment.Id = Guid.NewGuid();
//                db.Shipments.Add(shipment);
//                db.SaveChanges();
//                if (Lots != null)
//                {
//                    foreach (Guid id in Lots)
//                    {
//                        ContractLot lot = db.ContractLot.Find(id);
//                        Contract = lot.Agreement;
//                        lot.ShipmentId = shipment.Id;
//                        db.Entry(lot).State = EntityState.Modified;
//                        db.SaveChanges();
//                    }
//                }
//                if(Contract.SellerId == new Guid("{9FD591FB-D3E4-49C8-A7EE-458472AECCB7}"))
//                {
//                    ContractInvoice Invoice = new ContractInvoice();
//                    Invoice.Id = shipment.Id;
//                    Invoice.CafexportReceiptDate = InvoiceReceiptNotNull;
//                    var InvoicesList = db.ContractInvoice.ToList();
//                    int lastInvoice = 1;
//                    if (InvoicesList.Count() > 0)
//                    {
//                        lastInvoice = InvoicesList.OrderByDescending(x => x.Number).ToList().First().Number + 1;
//                    }
//                    Invoice.Number = lastInvoice;
//                    db.ContractInvoice.Add(Invoice);
//                    db.SaveChanges();
//                }
//                return RedirectToAction("Index");
//            }
//            TempData["ContractsForLotsTemp"] = Lots;
//            return RedirectToAction("Create", "Shipments");
//            //return View(shipment);
//        }

//        // GET: Comercial/Shipments/Edit/5
//        public ActionResult Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Shipment shipment = db.Shipments.Find(id);
//            if (shipment == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.Lots = new SelectList(db.ContractLot.Where(l => l.IcoMark != null && (l.ShipmentId == new Guid("3144EA78-E766-42A3-B40B-3C5F1BAB7D98") || l.ShipmentId == shipment.Id)), "Id", "IcoMark", ViewBag.Lots);
//            //ViewBag.SelectedLots = new SelectList(db.ContractLot.Where(l => l.IcoMark != null && l.ShipmentId == shipment.Id).Select(c => c.Id), "Id", ViewBag.SelectedLots);
//            //ViewBag.CompleteList = new MultiSelectList(db.ContractLot.Where(l => l.IcoMark != null && (l.ShipmentId == new Guid("3144EA78-E766-42A3-B40B-3C5F1BAB7D98") || l.ShipmentId == shipment.Id)), "Value", "Text", "hola", db.ContractLot.Where(l => l.IcoMark != null && l.ShipmentId == shipment.Id).Select(c => c.Id), new { multiple = "multiple" });
//            return View(shipment);
//        }

//        // POST: Comercial/Shipments/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,DocumentBL,ShippingDate,PortOfLanding,PortOfDestination,Vessel,ShippingLine,ExpocafeInvoice,CreatedAt,UpdatedAt,DeletedAt")] Shipment shipment)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(shipment).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(shipment);
//        }

//        // GET: Comercial/Shipments/Delete/5
//        public ActionResult Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Shipment shipment = db.Shipments.Find(id);
//            if (shipment == null)
//            {
//                return HttpNotFound();
//            }
//            return View(shipment);
//        }

//        // POST: Comercial/Shipments/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(Guid id)
//        {
//            Shipment shipment = db.Shipments.Find(id);
//            ContractInvoice invoice = db.ContractInvoice.Find(id);
//            var lots = db.ContractLot.Where(s => s.ShipmentId == id).ToList();
//            foreach (var lot in lots)
//            {
//                lot.ShipmentId = new Guid("{3144EA78-E766-42A3-B40B-3C5F1BAB7D98}");
//                db.Entry(lot).State = EntityState.Modified;
//                db.SaveChanges();
//            }
//            db.Shipments.Remove(shipment);
//            if (invoice != null)
//            {
//                db.ContractInvoice.Remove(invoice);
//            }
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
