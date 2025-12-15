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
//using System.Threading;
//using System.Globalization;
//using EFarming.Web.Models;

//namespace EFarming.Web.Areas.Comercial.Controllers
//{
    
//    /// <summary>
//    /// Agreements Controller
//    /// Use for add the agreements to the contract
//    /// </summary>
//    [CustomAuthorize(Roles = "Commercial")]
//    public class AgreementsController : Controller
//    {
//        /// <summary>
//        /// The database
//        /// </summary>
//        private UnitOfWork db = new UnitOfWork();

//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Index(DateTime? start, DateTime? end)
//        {
//            if (start == null)
//            {
//                start = DateTime.Now.AddDays(-30);
//                ViewBag.SelectedStart = start;
//            }
//            else
//            {
//                ViewBag.SelectedStart = start;
//            }

//            if (end == null)
//            {
//                end = DateTime.Now;
//                ViewBag.SelectedEnd = end;
//            }
//            else
//            {
//                ViewBag.SelectedEnd = end;
//            }

//            ViewBag.QualityList = new List<MoreInformation>(from x in db.Moreinformation
//                                                            where x.InformationType == "Quality"
//                                                            select x).ToList();
//            var agreements = db.Agreements.Include(a => a.Client).Include(a => a.Seller).Where(a => a.ShipmentDate >= start && a.ShipmentDate <= end).OrderByDescending(a => a.ShipmentDate);
//            return View(agreements.ToList());
//        }

//        /// <summary>
//        /// Detail of the contract identifying the quality classification for some parameters
//        /// </summary>
//        /// <param name="id">Contract identifier.</param>
//        /// <returns></returns>
//        public ActionResult Details(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         
//	  Agreement agreement = db.Agreements.Find(id);
            
//	  if (agreement == null)
//                return HttpNotFound();
            
//	  else
//            {
//                string Quality = (from x in db.Moreinformation.Where(w => w.Text.Equals(agreement.Quality.ToString()))
//                                  where x.ClientId == agreement.ClientId
//                                  select x.Short).FirstOrDefault();

//                // Identifying quality for Nespresso and adding the special conditions for the model.
//	      // De acuerdo a la solicitud de Jhonnatan se quema el nombre del cliente para definir la logica y el comportamiento
//	      // Si el cliente es NN Nestlé Nespresso S.A. se le adicionan por defecto la siguiente informacion
//                if (agreement.Client.Name == "Nestlé Nespresso S.A.")
//                {
//                    if (Quality == "AAA+FT")
//                    {
//                        agreement.Client.Name = agreement.Client.Name + " FLO ID 31044";
//                        agreement.Seller.Name = agreement.Seller.Name + " FLO ID 29527";
//                        agreement.Others = agreement.Others + " Price includes 10 USD ct-lb of FT Premium Additional 10 USD ct-lb of FT Premium to be sent separately FLO FT regulations to apply";
//                    };
//                }
//                else
//                {
//                    // Identifying quality for other clients and adding the special conditions for the model.
//                    if (Quality.Length > 3)
//                    {
//                        string FloId = "FLO ID ";
//                        switch (agreement.Client.Name.ToString())
//                        {
//                            case "Starbucks Coffee Trading Company S.A.R.L.":
//                                FloId = FloId + "1622";
//                                break;
//                            case "Industria Colombiana de Café S.A.S. Colcafé":
//                                FloId = FloId + "1656";
//                                break;
//                            default:
//                                agreement.Client.Name = agreement.Client.Name;
//                                break;
//                        };
//                        agreement.Client.Name = agreement.Client.Name + " " + FloId;
//                        agreement.Seller.Name = agreement.Seller.Name + " FLO ID 2957";
//                        agreement.Others = agreement.Others + " Price includes 20 USD ct-lb of FT Premium FLO FT Regulations to apply";
//                        int Month = agreement.ShipmentDate.Month;
//                        int Year = agreement.ShipmentDate.Year;
//                        if(Month < 10)                        
//                            agreement.Quality = agreement.Quality.ToString().Replace("(Crop)", " " + (Year - 1).ToString().Substring(2, 2) + "/" + (Year).ToString().Substring(2, 2));                        
//                        else                        
//                            agreement.Quality = agreement.Quality.ToString().Replace("(Crop)", " " + (Year).ToString().Substring(2, 2) + "/" + (Year + 1).ToString().Substring(2, 2));                        
//                    }
//                    else                    
//                        agreement.Seller.Name = agreement.Seller.Name + " C.A.F.E. Practices ID 99057";
//                };

//                //Showing the pdf View
//                return new PdfActionResult(agreement, (writer, document) => {
//                    document.SetPageSize(new Rectangle(612f, 792f, 90));
//                    document.NewPage();
//                });
//            }            
//        }

//        /// <summary>
//        /// Creates this instance.
//        /// </summary>
//        /// <returns>Instance the Create form</returns>
//        public ActionResult Create()
//        {
//            var Refs = db.Agreements.ToList().OrderByDescending(x => x.OurRef).ToList();

//            int LastRef = Int32.Parse(Refs.First().OurRef.Substring(2,5));
//            LastRef = LastRef + 1;
//            ViewBag.NewRef = "EX" + LastRef;

//            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name");
//            ViewBag.SellerId = new SelectList(db.Sellers, "Id", "Name");
//            ViewBag.AgentId = new SelectList(db.Agents, "Id", "Name");

//            return View();
//        }

//        /// <summary>
//        /// Creates the specified agreement.
//        /// </summary>
//        /// <param name="agreement">The agreement.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,OurRef,Date,ClientId,Volume,ShipmentDate,Quality,PriceDate,Terms,Weights,Payment,Samples,Arbitration,Others,CreatedAt,UpdatedAt,DeletedAt,SellerId,AgentId,LotsNumber,PriceType,PriceDifferential,Fixation,FixationDate")] Agreement agreement)
//        {
//            if (ModelState.IsValid || !ModelState.IsValid)
//            {
//                Fixation fixation = new Fixation();
//                agreement.Id = Guid.NewGuid();

//                if (agreement.Fixation != null)
//                {
//                    agreement.FixationDate = DateTime.Now;

//                    fixation.Id = agreement.Id;
//                    fixation.FixationDate = DateTime.Now;
//                    fixation.UpdatedAt = DateTime.Now;
//                    fixation.FixationLevel = agreement.Fixation;
//                    fixation.Fixed = true;
//                    fixation.FixationTypeId = 3;
//                    fixation.CreatedAt = DateTime.Now;
//                }
//                else
//                {
//                    fixation.Id = agreement.Id;
//                    fixation.Fixed = false;
//                    fixation.FixationTypeId = 4;
//                    fixation.CreatedAt = DateTime.Now;
//                }

//                db.Agreements.Add(agreement);
//                db.Fixation.Add(fixation);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", agreement.ClientId);
//            ViewBag.SellerId = new SelectList(db.Sellers, "Id", "Name", agreement.SellerId);
//            ViewBag.AgentId = new SelectList(db.Agents, "Id", "Name");
//            return View(agreement);
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
            
//	  Agreement agreement = db.Agreements.Find(id);
            
//	  if (agreement == null)
//                return HttpNotFound();
        
//	  ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", agreement.ClientId);
//            ViewBag.SellerId = new SelectList(db.Sellers, "Id", "Name", agreement.SellerId);
            
//	  return View(agreement);
//        }

//        /// <summary>
//        /// Edits the specified agreement.
//        /// </summary>
//        /// <param name="agreement">The agreement.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id, YourRef, SecondClientRef")] Agreement agreement)
//        {
//            Agreement SavedAgreement = db.Agreements.Find(agreement.Id);
//            SavedAgreement.YourRef = agreement.YourRef;
//            SavedAgreement.SecondClientRef = agreement.SecondClientRef;
//            if (ModelState.IsValid)
//            {
//                db.Entry(SavedAgreement).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", agreement.ClientId);
//            ViewBag.SellerId = new SelectList(db.Sellers, "Id", "Name", agreement.SellerId);
//            return View(SavedAgreement);
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Delete(Guid? id)
//        {
//            if (id == null)
//               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
//	  Agreement agreement = db.Agreements.Find(id);
            
//	  if (agreement == null)
//                return HttpNotFound();
            
//	  return View(agreement);
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
//            Agreement agreement = db.Agreements.Find(id);
//            Fixation fixation = db.Fixation.Find(id);
//            List<ContractLot> Lots = db.ContractLot.Where(l => l.AgreementId == id).ToList();
//            List<ContractInvoice> invoices = new List<ContractInvoice>();
//            List<Shipment> shipments = new List<Shipment>();

//            foreach (var lot in Lots)
//            {
//                List<ReferenceRelationShip> references = db.ReferenceRelationShip.Where(r => r.DocumentId == lot.Id).ToList();
//                foreach(var reference in references)
//                {
//                    db.ReferenceRelationShip.Remove(reference);
//                    db.SaveChanges();
//                }
//                Shipment shipment = db.Shipments.Find(lot.ShipmentId);
//                ContractInvoice invoice = db.ContractInvoice.Find(lot.ShipmentId);
//                db.ContractLot.Remove(lot);
//                db.SaveChanges();
//                if(invoice != null && invoice.Id != new Guid("{3144EA78-E766-42A3-B40B-3C5F1BAB7D98}"))
//                {
//                    invoices.Add(invoice);
//                }
//                if(shipment != null && shipment.Id != new Guid("{3144EA78-E766-42A3-B40B-3C5F1BAB7D98}"))
//                {
//                    shipments.Add(shipment);
//                }
//            }
//            foreach(var todeleteinvoice in invoices.Distinct())
//            {
//                db.ContractInvoice.Remove(todeleteinvoice);
//                db.SaveChanges();
//            }

//            foreach (var todeleteshipment in shipments.Distinct())
//            {
//                db.Shipments.Remove(todeleteshipment);
//                db.SaveChanges();
//            }

//            db.Fixation.Remove(fixation);
//            db.Agreements.Remove(agreement);
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
