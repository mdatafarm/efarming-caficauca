using EFarming.Core.FerilizersCalculatorModule;
using EFarming.DAL;
using EFarming.DTO.FertilizersCalculatorModule;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Areas.Admin.Controllers
{
    public class AverageExtractionController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: AverageExtractionDTOes
        public ActionResult Index()
        {
            return View(db.AverageExtraction.ToList());
        }

        // GET: AverageExtractionDTOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AverageExtraction averageExtraction = db.AverageExtraction.Find(id);
            if (averageExtraction == null)
            {
                return HttpNotFound();
            }
            return View(averageExtraction);
        }

        // GET: AverageExtractionDTOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AverageExtractionDTOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,N,P2O5,K20,CaO,MgO,SO4,B,Zn,Cu,Fe,Mn,Mo,SiO,CreatedAt,UpdatedAt,DeletedAt")] AverageExtractionDTO averageExtractionDTO)
        {
            if (ModelState.IsValid)
            {
                AverageExtraction averageExtraction = new AverageExtraction();

                averageExtraction.Name = averageExtractionDTO.Name;
                averageExtraction.N = Convert.ToDecimal(averageExtractionDTO.N);
                averageExtraction.P2O5 = Convert.ToDecimal(averageExtractionDTO.P2O5);
                averageExtraction.K20 = Convert.ToDecimal(averageExtractionDTO.K20);
                averageExtraction.CaO = Convert.ToDecimal(averageExtractionDTO.CaO);
                averageExtraction.MgO = Convert.ToDecimal(averageExtractionDTO.MgO);
                averageExtraction.SO4 = Convert.ToDecimal(averageExtractionDTO.SO4);
                averageExtraction.B = Convert.ToDecimal(averageExtractionDTO.B);
                averageExtraction.Zn = Convert.ToDecimal(averageExtractionDTO.Zn);
                averageExtraction.Cu = Convert.ToDecimal(averageExtractionDTO.Cu);
                averageExtraction.Fe = Convert.ToDecimal(averageExtractionDTO.Fe);
                averageExtraction.Mn = Convert.ToDecimal(averageExtractionDTO.Mn);
                averageExtraction.Mo = Convert.ToDecimal(averageExtractionDTO.Mo);
                averageExtraction.SiO = Convert.ToDecimal(averageExtractionDTO.SiO);

                db.AverageExtraction.Add(averageExtraction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(averageExtractionDTO);
        }

        // GET: AverageExtractionDTOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AverageExtraction averageExtraction = db.AverageExtraction.Find(id);
            if (averageExtraction == null)
            {
                return HttpNotFound();
            }
            return View(averageExtraction);
        }

        // POST: AverageExtractionDTOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,N,P2O5,K20,CaO,MgO,SO4,B,Zn,Cu,Fe,Mn,Mo,SiO,CreatedAt,UpdatedAt,DeletedAt")] AverageExtractionDTO averageExtractionDTO)
        {
            if (ModelState.IsValid)
            {
                AverageExtraction averageExtraction = db.AverageExtraction.Find(averageExtractionDTO.Id);

                averageExtraction.Name = averageExtractionDTO.Name;
                averageExtraction.N = Convert.ToDecimal(averageExtractionDTO.N);
                averageExtraction.P2O5 = Convert.ToDecimal(averageExtractionDTO.P2O5);
                averageExtraction.K20 = Convert.ToDecimal(averageExtractionDTO.K20);
                averageExtraction.CaO = Convert.ToDecimal(averageExtractionDTO.CaO);
                averageExtraction.MgO = Convert.ToDecimal(averageExtractionDTO.MgO);
                averageExtraction.SO4 = Convert.ToDecimal(averageExtractionDTO.SO4);
                averageExtraction.B = Convert.ToDecimal(averageExtractionDTO.B);
                averageExtraction.Zn = Convert.ToDecimal(averageExtractionDTO.Zn);
                averageExtraction.Cu = Convert.ToDecimal(averageExtractionDTO.Cu);
                averageExtraction.Fe = Convert.ToDecimal(averageExtractionDTO.Fe);
                averageExtraction.Mn = Convert.ToDecimal(averageExtractionDTO.Mn);
                averageExtraction.Mo = Convert.ToDecimal(averageExtractionDTO.Mo);
                averageExtraction.SiO = Convert.ToDecimal(averageExtractionDTO.SiO);
                averageExtraction.UpdatedAt = DateTime.Now;

                db.Entry(averageExtraction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(averageExtractionDTO);
        }

        // GET: AverageExtractionDTOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AverageExtraction averageExtraction = db.AverageExtraction.Find(id);
            if (averageExtraction == null)
            {
                return HttpNotFound();
            }
            return View(averageExtraction);
        }

        // POST: AverageExtractionDTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AverageExtraction averageExtraction = db.AverageExtraction.Find(id);
            db.AverageExtraction.Remove(averageExtraction);
            db.SaveChanges();
            return RedirectToAction("Index");
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