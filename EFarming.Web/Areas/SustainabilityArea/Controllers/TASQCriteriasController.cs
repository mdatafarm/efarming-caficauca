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
using EFarming.Core.AdminModule.AssessmentAggregate;

namespace EFarming.Web.Areas.SustainabilityArea.Controllers
{
    public class TASQCriteriasController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        //// GET: SustainabilityArea/TASQCriterias
        public ActionResult Index(int? SubModuleId, Guid? assesmentId)
        {
            var tASQCriterias = db.TASQCriteria.Include(t => t.FlagIndicator).Include(t => t.SubModule);

            AssessmentTemplate assessmentTemplate = db.AssessmentTemplates.Find(assesmentId);
            var asMod = assessmentTemplate.Modules.ToList();
            var modId = asMod.Select(m => m.Id).ToList();
            ViewBag.mod = asMod.FirstOrDefault();

            if (SubModuleId != null && assesmentId != null)
            {
                //subModules = db.SubModule.Include(s => s.Module).Where(m => m.Module.AssessmentTemplateId == assesmentId);

                tASQCriterias = db.TASQCriteria.Include(s => s.SubModule).Include(s => s.SubModule.Module).Where(m => m.SubModule.Module.AssessmentTemplateId == assesmentId).OrderBy(s => s.Id);
                //tASQCriterias = db.TASQCriteria.Include(t => t.FlagIndicator).Include(t => t.SubModule).Where(s => s.SubModuleId == SubModuleId);
            }
            return View(tASQCriterias.ToList());
        }

        // GET: SustainabilityArea/TASQCriterias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TASQCriteria tASQCriteria = db.TASQCriteria.Find(id);
            if (tASQCriteria == null)
            {
                return HttpNotFound();
            }
            return View(tASQCriteria);
        }

        // GET: SustainabilityArea/TASQCriterias/Create
        public ActionResult Create(Guid? assesmentId)
        {
            ViewBag.FlagIndicatorId = new SelectList(db.FlagIndicator, "Id", "Name");
            ViewBag.SubModuleId = new SelectList(db.SubModule.Include(i => i.Module).Where(m => m.Module.AssessmentTemplateId == assesmentId).ToList(), "Id", "Name");
            ViewBag.CriteriaTypeId = new SelectList(db.TASQCriteriaType, "Id", "QuestionType");
            return View();
        }

        // POST: SustainabilityArea/TASQCriterias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Description,SubModuleId,FlagIndicatorId,Short,CriteriaOrder,Options,CreatedAt,CriteriaTypeId,UpdatedAt,DeletedAt")] TASQCriteria tASQCriteria)
        {
            if (ModelState.IsValid)
            {
                //db.TASQCriteria.Add(tASQCriteria);
                //db.SaveChanges();
                //return RedirectToAction("Index");

                db.TASQCriteria.Add(tASQCriteria);
                db.SaveChanges();

                var assementid = db.TASQCriteria.Include(s => s.SubModule).Include(s => s.SubModule.Module).Where(m => m.Id == tASQCriteria.Id).Select(x => x.SubModule.Module.AssessmentTemplateId).FirstOrDefault();

                return RedirectToAction("Index", new { SubModuleId = tASQCriteria.SubModuleId, assesmentId = assementid });
            }

            ViewBag.FlagIndicatorId = new SelectList(db.FlagIndicator, "Id", "Name", tASQCriteria.FlagIndicatorId);
            ViewBag.SubModuleId = new SelectList(db.SubModule, "Id", "Name", tASQCriteria.SubModuleId);
            ViewBag.CriteriaTypeId = new SelectList(db.TASQCriteriaType, "Id", "QuestionType");
            return View(tASQCriteria);
        }

        // GET: SustainabilityArea/TASQCriterias/Edit/5
        public ActionResult Edit(int? id, Guid? assesmentId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TASQCriteria tASQCriteria = db.TASQCriteria.Find(id);
            ViewBag.mod = tASQCriteria.SubModule.Module;
            if (tASQCriteria == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlagIndicatorId = new SelectList(db.FlagIndicator, "Id", "Name", tASQCriteria.FlagIndicatorId);
            //ViewBag.SubModuleId = new SelectList(db.SubModule, "Id", "Name", tASQCriteria.SubModuleId);
            ViewBag.SubModuleId = new SelectList(db.SubModule.Include(i => i.Module).Where(m => m.Module.AssessmentTemplateId == assesmentId).ToList(), "Id", "Name", tASQCriteria.SubModuleId);
            ViewBag.CriteriaTypeId = new SelectList(db.TASQCriteriaType, "Id", "QuestionType");
            return View(tASQCriteria);
        }

        // POST: SustainabilityArea/TASQCriterias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Description,SubModuleId,FlagIndicatorId,Short,CriteriaOrder,Options,CreatedAt,CriteriaTypeId,UpdatedAt,DeletedAt")] TASQCriteria tASQCriteria)
        {
            ViewBag.mod = db.SubModule.Where(sm => sm.Id == tASQCriteria.SubModuleId).Select(sm => sm.Module);
            if (ModelState.IsValid)
            {
                db.Entry(tASQCriteria).State = EntityState.Modified;
                db.SaveChanges();

                var assementid = db.TASQCriteria.Include(s => s.SubModule).Include(s => s.SubModule.Module).Where(m => m.Id == tASQCriteria.Id).Select(x => x.SubModule.Module.AssessmentTemplateId).FirstOrDefault();

                return RedirectToAction("Index", new { SubModuleId = tASQCriteria.SubModuleId, assesmentId = assementid });

                //return RedirectToAction("Index", new { SubModuleId = tASQCriteria.SubModuleId });
            }
            ViewBag.FlagIndicatorId = new SelectList(db.FlagIndicator, "Id", "Name", tASQCriteria.FlagIndicatorId);
            ViewBag.SubModuleId = new SelectList(db.SubModule, "Id", "Name", tASQCriteria.SubModuleId);
            ViewBag.CriteriaTypeId = new SelectList(db.TASQCriteriaType, "Id", "QuestionType");
            return View(tASQCriteria);
        }

        // GET: SustainabilityArea/TASQCriterias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TASQCriteria tASQCriteria = db.TASQCriteria.Find(id);
            ViewBag.mod = tASQCriteria.SubModule.Module;
            if (tASQCriteria == null)
            {
                return HttpNotFound();
            }
            return View(tASQCriteria);
        }

        // POST: SustainabilityArea/TASQCriterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var assementid = db.TASQCriteria.Include(s => s.SubModule).Include(s => s.SubModule.Module).Where(m => m.Id == id).Select(x => x.SubModule.Module.AssessmentTemplateId).FirstOrDefault();

            TASQCriteria tASQCriteria = db.TASQCriteria.Find(id);
            db.TASQCriteria.Remove(tASQCriteria);
            db.SaveChanges();

            return RedirectToAction("Index", new { SubModuleId = tASQCriteria.SubModuleId, assesmentId = assementid });

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
