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
//    /// <summary>
//    /// DocumentReferences Controller
//    /// </summary>
//    [CustomAuthorize(Roles = "Commercial")]
//    public class DocumentReferencesController : Controller
//    {
//        private UnitOfWork db = new UnitOfWork();

//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Index()
//        {
//            var DocumentReference = db.DocumentReference.Include(d => d.Client);
//            return View(DocumentReference.ToList());
//        }

//        /// <summary>
//        /// Detailses the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            DocumentReference documentReference = db.DocumentReference.Find(id);
//            if (documentReference == null)
//            {
//                return HttpNotFound();
//            }
//            return View(documentReference);
//        }

//        /// <summary>
//        /// Creates this instance.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Create()
//        {
//            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name");
//            return View();
//        }

//        /// <summary>
//        /// Creates the specified document reference.
//        /// </summary>
//        /// <param name="documentReference">The document reference.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,Name,Type,ClientId,CreatedAt,UpdatedAt,DeletedAt")] DocumentReference documentReference)
//        {
//            if (ModelState.IsValid)
//            {
//                db.DocumentReference.Add(documentReference);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", documentReference.ClientId);
//            return View(documentReference);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            DocumentReference documentReference = db.DocumentReference.Find(id);
//            if (documentReference == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", documentReference.ClientId);
//            return View(documentReference);
//        }

//        /// <summary>
//        /// Edits the specified document reference.
//        /// </summary>
//        /// <param name="documentReference">The document reference.</param>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,Name,Type,ClientId,CreatedAt,UpdatedAt,DeletedAt")] DocumentReference documentReference)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(documentReference).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", documentReference.ClientId);
//            return View(documentReference);
//        }

//        // GET: Comercial/DocumentReference/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            DocumentReference documentReference = db.DocumentReference.Find(id);
//            if (documentReference == null)
//            {
//                return HttpNotFound();
//            }
//            return View(documentReference);
//        }

//        // POST: Comercial/DocumentReference/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            DocumentReference documentReference = db.DocumentReference.Find(id);
//            db.DocumentReference.Remove(documentReference);
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
