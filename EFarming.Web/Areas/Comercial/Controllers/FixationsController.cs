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
//    [CustomAuthorize(Roles = "Commercial")]
//    public class FixationsController : Controller
//    {
//        private UnitOfWork db = new UnitOfWork();

//        // GET: Comercial/Fixations
//        /// <summary>
//        /// Show the contracts by no fixed, recently fixed and fixed.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Index()
//        {
//            var fixations = db.Fixation.Include(f => f.Agreement).Include(f => f.FixationType).OrderByDescending(f => f.UpdatedAt);
//            ViewBag.FixationTypeId = new SelectList(db.FixationType, "Id", "Type");
//            return View(fixations.ToList());
//        }

//        /// <summary>
//        /// Shows the Fixations to be sended to the client
//        /// </summary>
//        /// <param name="sendFixations">The send fixations.</param>
//        /// <returns></returns>
//        //[HttpPost, ActionName("Index")]
//        public ActionResult validateFixations(Guid[] sendFixations)
//        {

//            IEnumerable<Fixation> fixations = null;
//            List<Fixation> fixationsToSend = new List<Fixation>();
//            //var fixations = db.Fixation.Include(f => f.Agreement).Include(f => f.FixationType).OrderByDescending(f => f.UpdatedAt);

//            if (sendFixations == null)
//            {

//                ModelState.AddModelError("", "None of the reconds has been selected for delete action !");
//                return View(fixations);
//            }

//            foreach (var item in sendFixations)
//            {

//                try
//                {
//                    Fixation fixation = db.Fixation.Find(item);
//                    if (fixation.Agreement.Client.Name != "Nestlé Nespresso S.A.")
//                    {
//                        int Month = fixation.Agreement.ShipmentDate.Month;
//                        int Year = fixation.Agreement.ShipmentDate.Year;
//                        if (Month < 10)
//                            fixation.Agreement.Quality = fixation.Agreement.Quality.ToString().Replace("(Crop)", " " + (Year - 1).ToString().Substring(2, 2) + "/" + (Year).ToString().Substring(2, 2));
//                        else
//                            fixation.Agreement.Quality = fixation.Agreement.Quality.ToString().Replace("(Crop)", " " + (Year).ToString().Substring(2, 2) + "/" + (Year + 1).ToString().Substring(2, 2));
//                    }
//                    fixationsToSend.Add(fixation);
//                }
//                catch (Exception err)
//                {

//                    ModelState.AddModelError("", "Failed On Id " + item.ToString() + " : " + err.Message);
//                    return View(fixations);
//                }
//            }

//            //Showing the pdf View
//            return new PdfActionResult(fixationsToSend, (writer, document) => {
//                document.SetPageSize(new Rectangle(612f, 792f, 90));
//                document.NewPage();
//            });

//            //return View(fixationsToSend);
//        }

//        // GET: Comercial/Fixations/Details/5
//        /// <summary>
//        /// Details for the specific fixation
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Fixation fixation = db.Fixation.Find(id);
//            if (fixation == null)
//            {
//                return HttpNotFound();
//            }
//            return View(fixation);
//        }

//        // POST: Comercial/Fixation/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        /// <summary>
//        /// Edit the value and date for the specific fixation.
//        /// </summary>
//        /// <param name="fixation">The fixation.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,FixationDate,FixationLevel,Fixed,FixationTypeId,CreatedAt,UpdatedAt,DeletedAt")] Fixation fixation)
//        {
//            Agreement agreement = db.Agreements.Find(fixation.Id);
//            if (ModelState.IsValid)
//            {
//                fixation.Fixed = true;
//                fixation.UpdatedAt = DateTime.Now;
//                agreement.Fixation = fixation.FixationLevel;
//                agreement.FixationDate =  fixation.FixationDate;
//                db.Entry(agreement).State = EntityState.Modified;
//                db.Entry(fixation).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.Id = new SelectList(db.Agreements, "Id", "YourRef", fixation.Id);
//            ViewBag.FixationTypeId = new SelectList(db.FixationType, "Id", "Type", fixation.FixationTypeId);
//            return RedirectToAction("Index");
//        }

//        // GET: Comercial/Fixation/Delete/5
//        /// <summary>
//        /// Shows the information of the fixation to be deleted.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Fixation fixation = db.Fixation.Find(id);
//            if (fixation == null)
//            {
//                return HttpNotFound();
//            }
//            return PartialView(fixation);
//        }

//        // POST: Comercial/Fixation/Delete/5
//        /// <summary>
//        /// Deletes the information of the fixation selected.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(Guid id)
//        {
//            Agreement agreement = db.Agreements.Find(id);
//            Fixation fixation = db.Fixation.Find(id);
//            fixation.FixationDate = null;
//            fixation.FixationLevel = null;
//            fixation.FixationTypeId = 4;
//            fixation.Fixed = false;
//            agreement.Fixation = null;
//            agreement.FixationDate = null;
//            db.Entry(agreement).State = EntityState.Modified;
//            db.Entry(fixation).State = EntityState.Modified;
//            db.SaveChanges();
//            //db.Fixation.Remove(fixation);
//            //db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        /// <summary>
//        /// Releases unmanaged and - optionally - managed resources.
//        /// </summary>
//        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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
