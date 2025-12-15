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
//    /// Clients Controller
//    /// </summary>
//    [CustomAuthorize(Roles = "Commercial")]
//    public class ClientsController : Controller
//    {
//        /// <summary>
//        /// The database
//        /// </summary>
//        private UnitOfWork db = new UnitOfWork();

//        /// <summary>
//        /// Indexes this instance.
//        /// </summary>
//        /// <returns>The listo of Clients</returns>
//        public ActionResult Index()
//        {
//            return View(db.Clients.ToList());
//        }

//        /// <summary>
//        /// Detailses the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>the Client</returns>
//        public ActionResult Details(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        
//	  Client client = db.Clients.Find(id);
            
//	  if (client == null)
//                return HttpNotFound();
            
//	  return View(client);
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
//        /// Creates the specified client.
//        /// </summary>
//        /// <param name="client">The client.</param>
//        /// <returns>The view with client</returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,Name,Address,ZipCode,City,Country,CreatedAt,UpdatedAt,DeletedAt")] Client client)
//        {
//            if (ModelState.IsValid)
//            {
//                client.Id = Guid.NewGuid();
//                db.Clients.Add(client);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(client);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The view with client</returns>
//        public ActionResult Edit(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        
//	  Client client = db.Clients.Find(id);
            
//	  if (client == null)
//                return HttpNotFound();
            
//	  return View(client);
//        }

//        /// <summary>
//        /// Edits the specified client.
//        /// </summary>
//        /// <param name="client">The client.</param>
//        /// <returns>Redirecto to Index</returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,Name,Address,ZipCode,City,Country,CreatedAt,UpdatedAt,DeletedAt")] Client client)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(client).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(client);
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The View with client</returns>
//        public ActionResult Delete(Guid? id)
//        {
//            if (id == null)
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        
//	  Client client = db.Clients.Find(id);
            
//	  if (client == null)
//                return HttpNotFound();
            
//	  return View(client);
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
//            Client client = db.Clients.Find(id);
//            db.Clients.Remove(client);
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
        
//	  base.Dispose(disposing);
//        }
//    }
//}
