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
//    /// Agents Controller
//    /// </summary>
//    [CustomAuthorize(Roles = "Commercial")]
//    public class AgentsController : Controller
//    {
//        /// <summary>
//        /// The database
//        /// </summary>
//        private UnitOfWork db = new UnitOfWork();

//        /// <summary>
//        /// The list of the Agent created 
//        /// </summary>
//        /// <returns>List of Agents</returns>
//        public ActionResult Index()
//        {
//	  var agents = db.Agents.Include(a => a.Client);
//	  return View(agents.ToList());
//        }

//        /// <summary>
//        /// Details of specific Agent
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The Agent</returns>
//        public ActionResult Details(Guid? id)
//        {
//	  if (id == null)
//	      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	  
//	  Agent agent = db.Agents.Find(id);
	  
//	  if (agent == null)
//	      return HttpNotFound();
	  
//	  return View(agent);
//        }

//        /// <summary>
//        /// From create
//        /// </summary>
//        /// <returns>The View</returns>
//        public ActionResult Create()
//        {
//	  ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name");
//	  return View();
//        }

//        /// <summary>
//        /// Creates the specified agent.
//        /// </summary>
//        /// <param name="agent">The agent.</param>
//        /// <returns>Redirecto to Index</returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,ClientId,CreatedAt,UpdatedAt,DeletedAt")] Agent agent)
//        {
//	  if (ModelState.IsValid)
//	  {
//	      agent.Id = Guid.NewGuid();
//	      db.Agents.Add(agent);
//	      db.SaveChanges();
//	      return RedirectToAction("Index");
//	  }

//	  ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", agent.ClientId);
//	  return View(agent);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>The Edit View</returns>
//        public ActionResult Edit(Guid? id)
//        {
//	  if (id == null)
//	      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	  
//	  Agent agent = db.Agents.Find(id);
	  
//	  if (agent == null)
//	      return HttpNotFound();
	  
//	  ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", agent.ClientId);
//	  return View(agent);
//        }

//        /// <summary>
//        /// Edits the specified agent.
//        /// </summary>
//        /// <param name="agent">The agent.</param>
//        /// <returns>Redirecto to index</returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,ClientId,CreatedAt,UpdatedAt,DeletedAt")] Agent agent)
//        {
//	  if (ModelState.IsValid)
//	  {
//	      db.Entry(agent).State = EntityState.Modified;
//	      db.SaveChanges();
//	      return RedirectToAction("Index");
//	  }
//	  ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", agent.ClientId);
//	  return View(agent);
//        }

//        /// <summary>
//        /// Deletes the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Delete(Guid? id)
//        {
//	  if (id == null)
//	      return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	  
//	  Agent agent = db.Agents.Find(id);
	  
//	  if (agent == null)
//	      return HttpNotFound();

//	  return View(agent);
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
//	  Agent agent = db.Agents.Find(id);
//	  db.Agents.Remove(agent);
//	  db.SaveChanges();
//	  return RedirectToAction("Index");
//        }

//        /// <summary>
//        /// Releases unmanaged resources and optionally releases managed resources.
//        /// </summary>
//        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
//        protected override void Dispose(bool disposing)
//        {
//	  if (disposing)
//	      db.Dispose();
//	  base.Dispose(disposing);
//        }
//    }
//}