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
    public class ModulesController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: SustainabilityArea/Modules
        public ActionResult Index(Guid? assesmentId)
        {
            var modules = db.Module.Include(m => m.AssessmentTemplate).Where(m => m.AssessmentTemplateId == assesmentId).OrderBy(s => s.Name);
            //ViewBag.mod2 = db.Module.Where(m => m.AssessmentTemplateId == assesmentId).FirstOrDefault();
            //return PartialView(modules.ToList());

            //-----------NUEVAS LINEAS--------------
            ViewBag.mod2 = assesmentId;
            //---------------------------------------

            return View(modules);
        }

        // GET: SustainabilityArea/Modules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: SustainabilityArea/Modules/Create
        public ActionResult Create(Guid? assesmentId)
        {
            var modules = db.AssessmentTemplates.Where(m => m.Id == assesmentId).ToList();
            ViewBag.AssessmentTemplateId = new SelectList(modules, "Id", "Name");
            //ViewBag.AssessmentTemplateId = new SelectList(modules, "Id", "Name");
            return View();
        }

        // POST: SustainabilityArea/Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ModuleOrder,AssessmentTemplateId,CreatedAt,UpdatedAt,DeletedAt")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Module.Add(module);
                db.SaveChanges();

                //-----------NUEVAS LINEAS--------------
                var asMod2 = module.AssessmentTemplateId;
                ViewBag.mod2 = asMod2;
                //---------------------------------------

                var assessment = db.AssessmentTemplates.Find(module.AssessmentTemplateId);
                var Modules = assessment.Modules;
                return PartialView("~/Areas/SustainabilityArea/Views/Modules/Index.cshtml", Modules);
            }

            ViewBag.AssessmentTemplateId = new SelectList(db.AssessmentTemplates, "Id", "Name", module.AssessmentTemplateId);
            return View(module);
        }

        // GET: SustainabilityArea/Modules/Edit/5
        public ActionResult Edit(int? id, Guid? assesmentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }

            var modules = db.AssessmentTemplates.Where(m => m.Id == assesmentId).ToList();
            ViewBag.AssessmentTemplateId = new SelectList(modules, "Id", "Name", module.AssessmentTemplateId);
            return View(module);
        }

        // POST: SustainabilityArea/Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,ModuleOrder,AssessmentTemplateId,CreatedAt,UpdatedAt,DeletedAt")] Module module, Guid? assesmentId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();

                //-----------NUEVAS LINEAS--------------
                var asMod2 = module.AssessmentTemplateId;
                ViewBag.mod2 = asMod2;
                //---------------------------------------

                var assessment = db.AssessmentTemplates.Find(module.AssessmentTemplateId);
                var Modules = assessment.Modules;
                return PartialView("~/Areas/SustainabilityArea/Views/Modules/Index.cshtml", Modules);
            }
            ViewBag.AssessmentTemplateId = new SelectList(db.AssessmentTemplates, "Id", "Name", module.AssessmentTemplateId);
            return View(module);
        }

        // GET: SustainabilityArea/Modules/Delete/5
        public ActionResult Delete(int? id, Guid? assesmentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }

            var modules = db.AssessmentTemplates.Where(m => m.Id == assesmentId).ToList();
            ViewBag.AssessmentTemplateId = new SelectList(modules, "Id", "Name", module.AssessmentTemplateId);
            return View(module);
        }

        // POST: SustainabilityArea/Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Module module = db.Module.Find(id);
            var subModules = module.SubModule.ToList();
            foreach (var subModule in subModules)
            {
                var tasqCriterias = subModule.TASQCriterias.ToList();
                foreach (var tasqCriteria in tasqCriterias)
                {
                    db.TASQCriteria.Remove(tasqCriteria);
                }
                db.SubModule.Remove(subModule);
            }
            db.Module.Remove(module);
            db.SaveChanges();

            //-----------NUEVAS LINEAS--------------
            var asMod2 = module.AssessmentTemplateId;
            ViewBag.mod2 = asMod2;
            //---------------------------------------

            return RedirectToAction("Index", new { assesmentId = asMod2 });
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
