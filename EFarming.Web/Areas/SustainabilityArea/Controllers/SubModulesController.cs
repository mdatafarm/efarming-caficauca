using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFarming.Core.TasqModule;
using EFarming.DAL;

namespace EFarming.Web.Areas.SustainabilityArea.Controllers
{
    public class SubModulesController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: SustainabilityArea/SubModules
        public ActionResult Index(int? ModuleId, Guid? assesmentId)
        {
            var subModules = db.SubModule.Include(s => s.Module);
            ViewBag.mod = db.Module.Where(m => m.Id == ModuleId).FirstOrDefault();

            if (ModuleId != null && assesmentId != null)
            {
                subModules = db.SubModule.Include(s => s.Module).Where(m => m.Module.AssessmentTemplateId == assesmentId).OrderBy(s => s.Name);
            }

            //SubModules = db.SubModule.Where(sub => modId.Contains(sub.ModuleId)).ToList();
            //ViewBag.SubModules = SubModules;
            //var subId = SubModules.Select(s => s.Id).ToList();

            return PartialView(subModules.OrderBy(x => x.Name).ThenBy(x => x.SubModuleOrder).ToList());
        }

        // GET: SustainabilityArea/SubModules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubModule subModule = db.SubModule.Find(id);
            if (subModule == null)
            {
                return HttpNotFound();
            }
            return View(subModule);
        }

        // GET: SustainabilityArea/SubModules/Create
        public ActionResult Create(Guid? assesmentId)
        {
            var modules = db.Module.Where(m => m.AssessmentTemplateId == assesmentId).ToList();
            ViewBag.ModuleId = new SelectList(modules, "Id", "Name");
            return View();
        }

        public SubModule ChangeName(SubModule sub) {

            SubModule sb = new SubModule();

            //sb.Id = db.SubModule.Select(x=> x.Id).Max() + 1;
            sb.Id = sub.Id;
            sb.CreatedAt = sub.CreatedAt;

            if (sub.SubModuleOrder.ToString().Count() == 1)
            {
                sb.Name = "00"+sub.SubModuleOrder + "-" + sub.Name.Replace("-", " ");
            }
            else if (sub.SubModuleOrder.ToString().Count() == 2)
            {
                sb.Name = "0" + sub.SubModuleOrder + "-" + sub.Name.Replace("-", " ");
            }
            else if (sub.SubModuleOrder.ToString().Count() == 3)
            {
                sb.Name = sub.SubModuleOrder + "-" + sub.Name.Replace("-", " ");
            }
            sb.SubModuleOrder = sub.SubModuleOrder;
            sb.ModuleId = sub.ModuleId;

            return sb;

        }

        // POST: SustainabilityArea/SubModules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,SubModuleOrder,ModuleId,CreatedAt,UpdatedAt,DeletedAt")] SubModule subModule)
        {
            SubModule sn = ChangeName(subModule);
            if (ModelState.IsValid)
            {
                db.SubModule.Add(sn);
                db.SaveChanges();

                var assementid = db.SubModule.Include(s => s.Module).Where(m => m.Id == sn.Id).Select(x => x.Module.AssessmentTemplateId).FirstOrDefault();
                return RedirectToAction("Index", new { ModuleId = sn.ModuleId, assesmentId = assementid });

                //return RedirectToAction("Index");
            }
            ViewBag.ModuleId = new SelectList(db.Module, "Id", "Name", subModule.ModuleId);
            return View(subModule);
        }

        // GET: SustainabilityArea/SubModules/Edit/5
        public ActionResult Edit(int? id, Guid? assesmentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubModule sb = new SubModule();
            SubModule subModule = db.SubModule.Find(id);
            sb.Id = subModule.Id;
            sb.CreatedAt = subModule.CreatedAt;
            sb.ModuleId = subModule.ModuleId;
            string[] Separados;
            Separados = subModule.Name.Split('-');
            if (Separados.Length > 1)
            {
                sb.Name = Separados[1];
            }
            else {
                sb.Name = Separados[0];
            }
            sb.SubModuleOrder = subModule.SubModuleOrder;
            sb.TASQCriterias = subModule.TASQCriterias;
           

            
            ViewBag.mod = subModule.Module;
            if (subModule == null)
            {
                return HttpNotFound();
            }

            var modules = db.Module.Where(m => m.AssessmentTemplateId == assesmentId).ToList();
            ViewBag.ModuleId = new SelectList(modules, "Id", "Name", subModule.ModuleId);
            return View(sb);
        }

        // POST: SustainabilityArea/SubModules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,SubModuleOrder,ModuleId,CreatedAt,UpdatedAt,DeletedAt")] SubModule subModule)
        {
            SubModule sn = ChangeName(subModule);
            if (ModelState.IsValid)
            {
                db.Entry(sn).State = EntityState.Modified;
                db.SaveChanges();

                var assementid = db.SubModule.Include(s => s.Module).Where(m => m.Id == sn.Id).Select(x => x.Module.AssessmentTemplateId).FirstOrDefault();

                return RedirectToAction("Index", new { ModuleId = sn.ModuleId, assesmentId = assementid });
            }
            ViewBag.mod = subModule.Module;
            ViewBag.ModuleId = new SelectList(db.Module, "Id", "Name", subModule.ModuleId);
            return View(subModule);
        }

        // GET: SustainabilityArea/SubModules/Delete/5
        public ActionResult Delete(int? id, Guid? assesmentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubModule subModule = db.SubModule.Find(id);
            if (subModule == null)
            {
                return HttpNotFound();
            }

            var modules = db.Module.Where(m => m.AssessmentTemplateId == assesmentId).ToList();
            ViewBag.ModuleId = new SelectList(modules, "Id", "Name", subModule.ModuleId);
            return View(subModule);
        }

        // POST: SustainabilityArea/SubModules/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SubModule subModule = db.SubModule.Find(id);
            var TasqCriterias = subModule.TASQCriterias.ToList();
            foreach (var TasqCriteria in TasqCriterias)
            {
                db.TASQCriteria.Remove(TasqCriteria);
            }
            db.SubModule.Remove(subModule);
            db.SaveChanges();

            var Module_Id = subModule.ModuleId;
            var assementid = db.Module.Where(m => m.Id == Module_Id).Select(x => x.AssessmentTemplateId).FirstOrDefault();

            return RedirectToAction("Index", new { ModuleId = subModule.ModuleId, assesmentId = assementid });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
