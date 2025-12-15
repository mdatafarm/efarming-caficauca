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
//    /// Sallers Controller
//    /// </summary>
//    [CustomAuthorize(Roles = "Commercial")]
//    public class SellersController : Controller
//    {
//        /// <summary>
//        /// The database
//        /// </summary>
//        private UnitOfWork db = new UnitOfWork();

//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns>Listo Of Sellers</returns>
//        public ActionResult Index()
//        {
//            return View(db.Sellers.ToList());
//        }

//        /// <summary>
//        /// Detailses the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The View with Sellers</returns>
//        public ActionResult Details(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

//            Seller seller = db.Sellers.Find(id);
            
//	  if (seller == null)
//                return HttpNotFound();
            
//	  return View(seller);
//        }

//        /// <summary>
//        /// Creates this instance.
//        /// </summary>
//        /// <returns>The View</returns>
//        public ActionResult Create()
//        {
//            return View();
//        }

//        /// <summary>
//        /// Creates the specified seller.
//        /// </summary>
//        /// <param name="seller">The seller.</param>
//        /// <returns>Redirecto to Index</returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,Name,Footer,Header,SubHeader,CreatedAt,UpdatedAt,DeletedAt")] Seller seller)
//        {
//            if (ModelState.IsValid)
//            {
//                seller.Id = Guid.NewGuid();
//                db.Sellers.Add(seller);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(seller);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The view with seller</returns>
//        public ActionResult Edit(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        
//	  Seller seller = db.Sellers.Find(id);
            
//	  if (seller == null)
//                return HttpNotFound();
            
//	  return View(seller);
//        }

//        /// <summary>
//        /// Edits the specified seller.
//        /// </summary>
//        /// <param name="seller">The seller.</param>
//        /// <returns>Redirect to Index</returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,Name,Footer,Header,SubHeader,CreatedAt,UpdatedAt,DeletedAt")] Seller seller)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(seller).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(seller);
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The view with seller</returns>
//        public ActionResult Delete(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        
//	  Seller seller = db.Sellers.Find(id);
            
//	  if (seller == null)
//                return HttpNotFound();
            
//	  return View(seller);
//        }

//        /// <summary>
//        /// Deletes the confirmed.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>Redirecto to Index</returns>
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(Guid id)
//        {
//            Seller seller = db.Sellers.Find(id);
//            db.Sellers.Remove(seller);
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
