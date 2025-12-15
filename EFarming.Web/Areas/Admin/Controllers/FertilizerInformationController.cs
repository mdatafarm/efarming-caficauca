using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFarming.DAL;
using EFarming.DTO.FertilizersCalculatorModule;
using EFarming.Core.FerilizersCalculatorModule;

namespace EFarming.Web.Areas.Admin.Controllers
{
    public class FertilizerInformationController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: Admin/FertilizerInformationDTOes
        public ActionResult Index()
        {
            return View(db.FertilizerInformation.ToList());
        }

        // GET: Admin/FertilizerInformationDTOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FertilizerInformation fertilizerInformation = db.FertilizerInformation.Find(id);
            if (fertilizerInformation == null)
            {
                return HttpNotFound();
            }
            return View(fertilizerInformation);
        }

        // GET: Admin/FertilizerInformationDTOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/FertilizerInformationDTOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,kg,Price,N,P2O5,K20,CaO,MgO,SO4,B,Zn,Cu,Fe,Mn,Mo,SiO,CreatedAt,UpdatedAt,DeletedAt")] FertilizerInformationDTO fertilizerInformationDTO)
        {
            if (ModelState.IsValid)
            {
                FertilizerInformation fertilizerInformation = new FertilizerInformation();

                fertilizerInformation.Name = fertilizerInformationDTO.Name;
                fertilizerInformation.Price = Convert.ToDecimal(fertilizerInformationDTO.Price);
                fertilizerInformation.kg = Convert.ToDecimal(fertilizerInformationDTO.kg);
                fertilizerInformation.N = Convert.ToDecimal(fertilizerInformationDTO.N);
                fertilizerInformation.P2O5 = Convert.ToDecimal(fertilizerInformationDTO.P2O5);
                fertilizerInformation.K20 = Convert.ToDecimal(fertilizerInformationDTO.K20);
                fertilizerInformation.CaO = Convert.ToDecimal(fertilizerInformationDTO.CaO);
                fertilizerInformation.MgO = Convert.ToDecimal(fertilizerInformationDTO.MgO);
                fertilizerInformation.SO4 = Convert.ToDecimal(fertilizerInformationDTO.SO4);
                fertilizerInformation.B = Convert.ToDecimal(fertilizerInformationDTO.B);
                fertilizerInformation.Zn = Convert.ToDecimal(fertilizerInformationDTO.Zn);
                fertilizerInformation.Cu = Convert.ToDecimal(fertilizerInformationDTO.Cu);
                fertilizerInformation.Fe = Convert.ToDecimal(fertilizerInformationDTO.Fe);
                fertilizerInformation.Mn = Convert.ToDecimal(fertilizerInformationDTO.Mn);
                fertilizerInformation.Mo = Convert.ToDecimal(fertilizerInformationDTO.Mo);
                fertilizerInformation.SiO = Convert.ToDecimal(fertilizerInformationDTO.SiO);

                db.FertilizerInformation.Add(fertilizerInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fertilizerInformationDTO);
        }

        // GET: Admin/FertilizerInformationDTOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FertilizerInformation fertilizerInformation = db.FertilizerInformation.Find(id);
            if (fertilizerInformation == null)
            {
                return HttpNotFound();
            }
            return View(fertilizerInformation);
        }

        // POST: Admin/FertilizerInformationDTOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,kg,Price,N,P2O5,K20,CaO,MgO,SO4,B,Zn,Cu,Fe,Mn,Mo,SiO,CreatedAt,UpdatedAt,DeletedAt")] FertilizerInformationDTO fertilizerInformationDTO)
        {
            if (ModelState.IsValid)
            {
                FertilizerInformation fertilizerInformation = db.FertilizerInformation.Find(fertilizerInformationDTO.Id);

                fertilizerInformation.Name = fertilizerInformationDTO.Name;
                fertilizerInformation.Price = Convert.ToDecimal(fertilizerInformationDTO.Price);
                fertilizerInformation.kg = Convert.ToDecimal(fertilizerInformationDTO.kg);
                fertilizerInformation.N = Convert.ToDecimal(fertilizerInformationDTO.N);
                fertilizerInformation.P2O5 = Convert.ToDecimal(fertilizerInformationDTO.P2O5);
                fertilizerInformation.K20 = Convert.ToDecimal(fertilizerInformationDTO.K20);
                fertilizerInformation.CaO = Convert.ToDecimal(fertilizerInformationDTO.CaO);
                fertilizerInformation.MgO = Convert.ToDecimal(fertilizerInformationDTO.MgO);
                fertilizerInformation.SO4 = Convert.ToDecimal(fertilizerInformationDTO.SO4);
                fertilizerInformation.B = Convert.ToDecimal(fertilizerInformationDTO.B);
                fertilizerInformation.Zn = Convert.ToDecimal(fertilizerInformationDTO.Zn);
                fertilizerInformation.Cu = Convert.ToDecimal(fertilizerInformationDTO.Cu);
                fertilizerInformation.Fe = Convert.ToDecimal(fertilizerInformationDTO.Fe);
                fertilizerInformation.Mn = Convert.ToDecimal(fertilizerInformationDTO.Mn);
                fertilizerInformation.Mo = Convert.ToDecimal(fertilizerInformationDTO.Mo);
                fertilizerInformation.SiO = Convert.ToDecimal(fertilizerInformationDTO.SiO);
                fertilizerInformation.UpdatedAt = DateTime.Now;

                db.Entry(fertilizerInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fertilizerInformationDTO);
        }

        // GET: Admin/FertilizerInformationDTOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FertilizerInformation fertilizerInformation = db.FertilizerInformation.Find(id);
            if (fertilizerInformation == null)
            {
                return HttpNotFound();
            }
            return View(fertilizerInformation);
        }

        // POST: Admin/FertilizerInformationDTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FertilizerInformation fertilizerInformation = db.FertilizerInformation.Find(id);
            db.FertilizerInformation.Remove(fertilizerInformation);
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
