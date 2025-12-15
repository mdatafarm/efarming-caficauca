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
//using System.Web.Routing;
//using EFarming.Web.Models;

//namespace EFarming.Web.Areas.Comercial.Controllers
//{
//    /// <summary>
//    /// ContractLots Controller
//    /// </summary>
//    [CustomAuthorize(Roles = "Commercial")]
//    public class ContractLotsController : Controller
//    {
//        private UnitOfWork db = new UnitOfWork();

//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Index()
//        {
//            var contractLots = db.ContractLot.Include(c => c.Agreement).Include(c => c.Shipment);
//            return View(contractLots.ToList());
//        }

//        /// <summary>
//        /// Detailses the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Details(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

//            ContractLot contractLot = db.ContractLot.Find(id);

//            if (contractLot == null)
//                return HttpNotFound();

//            return View(contractLot);
//        }

//        /// <summary>
//        /// Element used to save all the information to ber rendered into the view
//        /// </summary>
//        public class ContractAndLots
//        {
//	  /// <summary>
//	  /// Gets or sets the contract.
//	  /// </summary>
//	  /// <value>
//	  /// The contract.
//	  /// </value>
//	  public Agreement Contract { get; set; }

//	  /// <summary>
//	  /// Gets or sets the contract lots.
//	  /// </summary>
//	  /// <value>
//	  /// Igrouping og Id and ContractLot
//	  /// </value>
//	  public List<IGrouping<Guid, ContractLot>> ContractLots { get; set; }

//	  /// <summary>
//	  /// Gets or sets the shipments.
//	  /// </summary>
//	  /// <value>
//	  /// List of Contratracts.
//	  /// </value>
//            public List<ContractLot> Shipments { get; set; }
//        }

//        /// <summary>
//        /// Creates the specified contracts for lots.
//        /// </summary>
//        /// <param name="ContractsForLots">The contracts for lots.</param>
//        /// <returns></returns>
//        public ActionResult Create(Guid?[] ContractsForLots)
//        {
//            Guid?[] ContracstForLotsTemp = (Guid?[])TempData["ContractsForLotsTemp"];

//            if (ContracstForLotsTemp != null)
//                ContractsForLots = ContracstForLotsTemp;

//            List<ContractAndLots> ContractAndLots = new List<ContractLotsController.ContractAndLots>();
//            List<ContractLot> shipmentslist = new List<ContractLot>();

//            ViewBag.ContractsForLots = ContractsForLots;

//            if(ContractsForLots != null)
//            {
//                foreach (var item in ContractsForLots)
//                {
//                    Agreement agreement = db.Agreements.Find(item);

//                    if (agreement == null)
//                        return HttpNotFound();

//                    else
//                    {
//                        int LotsQuantity = db.ContractLot.Where(C => C.AgreementId == item).Count();
//                        IOrderedQueryable<ContractLot> contractLots = null;
//                        ViewBag.agreement = agreement;

//                        if (LotsQuantity != 0)
//                        {
//                            var shipments = db.ContractLot.Include(c => c.Agreement).Include(c => c.Shipment).Where(c => c.AgreementId == item).GroupBy(l => l.ShipmentId).Select(grp => grp.FirstOrDefault());
//                            contractLots = db.ContractLot.Include(c => c.Agreement).Include(c => c.Shipment).Where(c => c.AgreementId == item).OrderBy(l => l.LotReference);
//                            var contractLots1 = contractLots.GroupBy(l => l.ShipmentId).ToList();
//                            contractLots = contractLots.OrderBy(l => l.LotReference);

//                            foreach (var lot in shipments.ToList())
//                                shipmentslist.Add(lot);

//                            ContractAndLots.Add(new ContractAndLots
//                            {
//                                Contract = agreement,
//                                ContractLots = contractLots.OrderBy(l => l.LotReference).GroupBy(l => l.ShipmentId).ToList(),
//                                Shipments = shipments.ToList()
//                            });
//                        }
//                        else
//                        {
//                            ContractAndLots.Add(new ContractAndLots
//                            {
//                                Contract = agreement,
//                                ContractLots = null,
//                                Shipments = null
//                            });
//                        }
//                    }
//                }
//                var GuidShipmentslist = shipmentslist.Select(s => s.ShipmentId).Distinct().ToList();
//                ViewBag.shipments = GuidShipmentslist;
//                return View(ContractAndLots);
//            }
//            return RedirectToAction("Index", "Agreements");
//        }

//        /// <summary>
//        /// Creates the specified contract identifier.
//        /// </summary>
//        /// <param name="ContractId">The contract identifier.</param>
//        /// <param name="LotsNumberInput">The lots number input.</param>
//        /// <param name="Unit">The unit.</param>
//        /// <param name="ContractsForLotsPost">The contracts for lots post.</param>
//        /// <returns></returns>
//        [HttpPost]
//        public ActionResult Create(Guid? ContractId, int? LotsNumberInput, int? Unit, Guid?[] ContractsForLotsPost)
//        {
//            ViewBag.ContractsForLots = ContractsForLotsPost;
//            Agreement agreement = null;
//            if (ContractId != null && LotsNumberInput != null)
//            {
//                int LotsQuantity = db.ContractLot.Where(C => C.AgreementId == ContractId).Count();
//                int volumeReference = new int();
//                if (LotsQuantity == 0)
//                {
//                    agreement = db.Agreements.Find(ContractId);
//                    for (int i = 1; i <= LotsNumberInput; i++)
//                    {
//                        string LotName = agreement.OurRef.ToString() + "-" + i;
//                        ContractLot lot = new ContractLot();
//                        volumeReference = db.DocumentReference.Where(r => r.Type == "lot" && r.Name == "Volume" && r.ClientId == agreement.ClientId).FirstOrDefault().Id;
//                        lot.AgreementId = agreement.Id;
//                        lot.LotReference = LotName;
//                        lot.ShipmentId = new Guid("3144ea78-e766-42a3-b40b-3c5f1bab7d98");
//                        lot.ReferenceRelationShip = new List<ReferenceRelationShip>();
//                        lot.ReferenceRelationShip.Add(new ReferenceRelationShip
//                        {
//                            DocumentId = lot.Id,
//                            DocumentReferenceId = volumeReference,
//                            Value = Unit.ToString(),
//                            Id = lot.Id,
//                            CreatedAt = DateTime.Now
//                        });
//                        db.ContractLot.Add(lot);
//                        db.SaveChanges();
//                    }
//                }
//                else
//                {
//                    // Create the necesary variables to show the lots and then edit them
//                    agreement = db.Agreements.Find(ContractId);
//                }
//            }
//            else
//                return RedirectToAction("Index", "Agreements");

//            var contract = ContractId;
//            var LotsNumber1 = LotsNumberInput;
//            ViewBag.agreement = agreement;
//            //return View();
//            TempData["ContractsForLotsTemp"] = ContractsForLotsPost;
//            return RedirectToAction("Create", "ContractLots");
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="ContractsForLots">The contracts for lots.</param>
//        /// <returns></returns>
//        public ActionResult Edit(Guid? id, string ContractsForLots)
//        {
//            string[] ContractsForLotsPost = ContractsForLots.Split('|');
//            List<Guid?> ContractsForLotsSend = new List<Guid?>();
//            int i = 0;

//            foreach (var item in ContractsForLotsPost)
//            {
//                if (item != "")
//                    ContractsForLotsSend.Add(new Guid("{" + item + "}"));
//                i++;
//            }
//            ViewBag.ContractsForLots = ContractsForLotsSend;
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

//            ContractLot contractLot = db.ContractLot.Find(id);
//            if (contractLot == null)
//                return HttpNotFound();

//            ViewBag.SavedReferences = db.ReferenceRelationShip.Where(l => l.DocumentId == contractLot.Id);

//            //SelecList for the Countries
//            var Countries = db.Countries.OrderBy(c => c.Name).ToList().Select(s => new SelectListItem { Value = s.code.ToString(), Text = s.code + " - " + s.Name, });

//            SelectList CountriesList = new SelectList(Countries, "Value", "Text", contractLot.IcoMark != null ? contractLot.IcoMark.Split('-')[1] : "3");
//            ViewBag.Countries = CountriesList;

//            //SelecList for the Sellers
//            var Sellers = db.Sellers.OrderBy(c => c.Name).ToList().Select(s => new SelectListItem
//            {
//                Value = s.Code.ToString(),
//                Text = s.Code + " - " + s.Name,
//            });
//            SelectList SellersList = new SelectList(Sellers, "Value", "Text", contractLot.IcoMark != null ? contractLot.IcoMark.Split('-')[2] : "63");
//            ViewBag.Sellers = SellersList;

//            ViewBag.MDType = db.Moreinformation.Where(c => c.ClientId == contractLot.Agreement.ClientId && c.InformationType == "Quality" && c.Text == contractLot.Agreement.Quality).First();
//            ViewBag.MDTypes = new SelectList(db.Moreinformation.Where(c => c.ClientId == contractLot.Agreement.ClientId && c.InformationType == "Quality"), "Id", "InvoiceReference", ViewBag.MDTypes);
//            ViewBag.MDOrigins = new SelectList(db.MDOrigin.Where(c => c.ClientId == contractLot.Agreement.ClientId), "Id", "Name", ViewBag.MDOrigins);
//            ViewBag.References = new SelectList(db.DocumentReference.Where(c => c.ClientId == contractLot.Agreement.ClientId && c.Type == "Lot"), "Id", "Name", ViewBag.References);
//            ViewBag.AgreementId = new SelectList(db.Agreements, "Id", "YourRef", contractLot.AgreementId);
//            return View(contractLot);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,LotReference,ShipmentId,AgreementId,CreatedAt,UpdatedAt,DeletedAt,IcoMark,MerchandiseDescription")] ContractLot contractLot, Guid?[] ContractsForLots)
//        {
//            if (ModelState.IsValid)
//            {
//                contractLot.ReferenceRelationShip = new List<ReferenceRelationShip>();
//                //ContractLot contractLot1 = db.ContractLot.Find(contractLot.Id);
//                //contractLot.ShipmentId = contractLot1.ShipmentId;
//                //contractLot.AgreementId = contractLot.Agreement.Id;
//                foreach (string key in Request.Form.AllKeys)
//                {
//                    if (key.StartsWith("dynamic"))
//                    {
//                        string refid = key.ToString().Replace("dynamic ", "");
//                        contractLot.ReferenceRelationShip.Add(new ReferenceRelationShip
//                        {
//                            DocumentId = contractLot.Id,
//                            DocumentReferenceId = Int32.Parse(refid),
//                            Value = Request.Form[key],
//                            Id = contractLot.Id,
//                            CreatedAt = DateTime.Now
//                        });
//                    }
//                }
//                foreach (var item in contractLot.ReferenceRelationShip)
//                {
//                    var documentReference = db.ReferenceRelationShip.Find(item.DocumentId, item.DocumentReferenceId);
//                    if (documentReference != null)
//                    {
//                        db.ReferenceRelationShip.Remove(documentReference);
//                        db.SaveChanges();
//                        db.ReferenceRelationShip.Add(item);
//                        db.SaveChanges();
//                    }
//                    else
//                    {
//                        db.ReferenceRelationShip.Add(item);
//                        db.SaveChanges();
//                    }
//                }
//                db.Entry(contractLot).State = EntityState.Modified;
//                db.SaveChanges();
//                TempData["ContractsForLotsTemp"] = ContractsForLots;
//                return RedirectToAction("Create", "ContractLots", new { id = contractLot.AgreementId });
//            }
//            ViewBag.AgreementId = new SelectList(db.Agreements, "Id", "YourRef", contractLot.AgreementId);
//            ViewBag.ShipmentId = new SelectList(db.Shipments, "Id", "DocumentBL", contractLot.ShipmentId);
//            return View(contractLot);
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

//            ContractLot contractLot = db.ContractLot.Find(id);
//            if (contractLot == null)
//                return HttpNotFound();

//            return View(contractLot);
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
//            ContractLot contractLot = db.ContractLot.Find(id);
//            db.ContractLot.Remove(contractLot);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        /// <summary>
//        /// Releases unmanaged and - optionally - managed resources.
//        /// </summary>
//        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//                db.Dispose();

//            base.Dispose(disposing);
//        }
//    }
//}
