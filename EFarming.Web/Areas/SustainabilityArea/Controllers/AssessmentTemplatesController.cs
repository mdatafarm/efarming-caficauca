using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.DAL;
using EFarming.Core.TasqModule;
using System.Collections.ObjectModel;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using System.Data.SqlClient;

namespace EFarming.Web.Areas.SustainabilityArea.Controllers
{
    public class AssessmentTemplatesController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: SustainabilityArea/AssessmentTemplates
        public ActionResult Index()
        {
            return View(db.AssessmentTemplates.Where(a => a.DeletedAt == null).OrderBy(X=> X.Name).ToList());
        }
        //(a.Name.Equals("ENCUESTA COOCENTRAL") || a.Name.Equals("MR ESPRESSO KIT CATACION") || a.Name.Equals(" PREMIUM SHARING REWARDS "))
        public ActionResult DashboardHome()
        {
            return View(db.AssessmentTemplates.Where(a => a.DeletedAt == null ).ToList());
        }

        public ActionResult Prueba()
        {
            var hol="";
            return View();
        }
        public ActionResult Dashboard(DateTime? start, DateTime? end,Guid? id, string currentName, string searchName, string currentDepartment,
            string searchDepartment, string currentMunicipality, string searchMunicipality,
            string searchVillage, string currentVillage, string currentCode, string searchCode,
            string searchFarmerIdentification, string currentFarmerIdentification,
            string searchFarmerName, string currentFarmerName, int? page)
        {
            var withFilter = (!this.isNullorEmpty(searchName)
                                || !this.isNullorEmpty(searchDepartment)
                                || !this.isNullorEmpty(searchMunicipality)
                                || !this.isNullorEmpty(searchVillage)
                                || !this.isNullorEmpty(searchCode)
                                || !this.isNullorEmpty(searchFarmerIdentification)
                                || !this.isNullorEmpty(searchFarmerName));
            if (withFilter)
            {
                page = 1;
            }
            else
            {
                searchName = currentName;
                searchCode = currentCode;
                searchMunicipality = currentMunicipality;
                searchDepartment = currentDepartment;
                searchVillage = currentVillage;
                searchFarmerName = currentFarmerName;
                searchFarmerIdentification = currentFarmerIdentification;
            }
            var dep = db.Departments.OrderBy(d => d.Name).ToList();
            SelectList ld = new SelectList(dep, "Id", "Name");
            ViewBag.Departments = ld;

            ViewBag.CurrentName = searchName;
            ViewBag.CurrentCode = searchCode;
            ViewBag.CurrentFarmerName = searchFarmerName;
            ViewBag.CurrentFarmerIdentification = searchFarmerIdentification;
            ViewBag.CurrentVillage = searchVillage;
            ViewBag.CurrentMunicipality = searchMunicipality;
            ViewBag.CurrentDepartment = searchDepartment;
            var villageId = this.isNullorEmpty(searchVillage) ? Guid.Empty : Guid.Parse(searchVillage);
            var municipalityId = this.isNullorEmpty(searchMunicipality) ? Guid.Empty : Guid.Parse(searchMunicipality);
            var departmentId = this.isNullorEmpty(searchDepartment) ? Guid.Empty : Guid.Parse(searchDepartment);

            if (start.HasValue == true)
                ViewBag.start = start;
            else
                ViewBag.start = DateTime.Now.AddDays(-30);

            if (end.HasValue == true)
                ViewBag.end = end;
            else
                ViewBag.end = DateTime.Now;

            ViewBag.start = start;
            ViewBag.end = end;
            ViewBag.id = id;
            AssessmentTemplate assessmentTemplate = db.AssessmentTemplates.Find(id);
            ViewBag.name = assessmentTemplate.Name;
            return View();
        }
        public ActionResult Summary(Guid? id, DateTime? start, DateTime? end, string currentName, string searchName, string currentDepartment,
            string searchDepartment, string currentMunicipality, string searchMunicipality,
            string searchVillage, string currentVillage, string currentCode, string searchCode,
            string searchFarmerIdentification, string currentFarmerIdentification,
            string searchFarmerName, string currentFarmerName, int? page)
        {
            try
            {
                var withFilter = (!this.isNullorEmpty(searchName)
                                || !this.isNullorEmpty(searchDepartment)
                                || !this.isNullorEmpty(searchMunicipality)
                                || !this.isNullorEmpty(searchVillage)
                                || !this.isNullorEmpty(searchCode)
                                || !this.isNullorEmpty(searchFarmerIdentification)
                                || !this.isNullorEmpty(searchFarmerName));
                if (withFilter)
                {
                    page = 1;
                }
                else
                {
                    searchName = currentName;
                    searchCode = currentCode;
                    searchMunicipality = currentMunicipality;
                    searchDepartment = currentDepartment;
                    searchVillage = currentVillage;
                    searchFarmerName = currentFarmerName;
                    searchFarmerIdentification = currentFarmerIdentification;
                }
                var dep = db.Departments.OrderBy(d => d.Name).ToList();
                SelectList ld = new SelectList(dep, "Id", "Name");
                ViewBag.Departments = ld;
                string guid = id.ToString();
                ViewBag.id = guid;
                ViewBag.CurrentName = searchName;
                ViewBag.CurrentCode = searchCode;
                ViewBag.CurrentFarmerName = searchFarmerName;
                ViewBag.CurrentFarmerIdentification = searchFarmerIdentification;
                ViewBag.CurrentVillage = searchVillage;
                ViewBag.CurrentMunicipality = searchMunicipality;
                ViewBag.CurrentDepartment = searchDepartment;
                var villageId = this.isNullorEmpty(searchVillage) ? Guid.Empty : Guid.Parse(searchVillage);
                var municipalityId = this.isNullorEmpty(searchMunicipality) ? Guid.Empty : Guid.Parse(searchMunicipality);
                var departmentId = this.isNullorEmpty(searchDepartment) ? Guid.Empty : Guid.Parse(searchDepartment);

                if (start.HasValue == true)
                    ViewBag.start = start;
                else
                    ViewBag.start = DateTime.Now.AddDays(-30);

                if (end.HasValue == true)
                    ViewBag.end = end;
                else
                    ViewBag.end = DateTime.Now;

                if (start != null && end != null)
                    ViewBag.TASQ = this.TasqResume(id, Convert.ToDateTime(start), Convert.ToDateTime(end), searchDepartment, searchMunicipality, searchVillage);
                else
                    ViewBag.TASQ = new List<TASQAssessments>();

                ViewBag.start = start;
                ViewBag.end = end;
                AssessmentTemplate assessmentTemplate = db.AssessmentTemplates.Find(id);
                ViewBag.name = assessmentTemplate.Name;
            }
            catch (Exception ex)
            {

            }
            
            return View();
        }
        public ActionResult GAP(Guid? id, DateTime? start, DateTime? end, string currentName, string searchName, string currentDepartment,
            string searchDepartment, string currentMunicipality, string searchMunicipality,
            string searchVillage, string currentVillage, string currentCode, string searchCode,
            string searchFarmerIdentification, string currentFarmerIdentification,
            string searchFarmerName, string currentFarmerName, int? page)
        {
            try
            {
                var withFilter = (!this.isNullorEmpty(searchName)
                                || !this.isNullorEmpty(searchDepartment)
                                || !this.isNullorEmpty(searchMunicipality)
                                || !this.isNullorEmpty(searchVillage)
                                || !this.isNullorEmpty(searchCode)
                                || !this.isNullorEmpty(searchFarmerIdentification)
                                || !this.isNullorEmpty(searchFarmerName));
                if (withFilter)
                {
                    page = 1;
                }
                else
                {
                    searchName = currentName;
                    searchCode = currentCode;
                    searchMunicipality = currentMunicipality;
                    searchDepartment = currentDepartment;
                    searchVillage = currentVillage;
                    searchFarmerName = currentFarmerName;
                    searchFarmerIdentification = currentFarmerIdentification;
                }
                var dep = db.Departments.OrderBy(d => d.Name).ToList();
                SelectList ld = new SelectList(dep, "Id", "Name");
                ViewBag.Departments = ld;
                string guid = id.ToString();
                ViewBag.id = guid;
                ViewBag.CurrentName = searchName;
                ViewBag.CurrentCode = searchCode;
                ViewBag.CurrentFarmerName = searchFarmerName;
                ViewBag.CurrentFarmerIdentification = searchFarmerIdentification;
                ViewBag.CurrentVillage = searchVillage;
                ViewBag.CurrentMunicipality = searchMunicipality;
                ViewBag.CurrentDepartment = searchDepartment;
                var villageId = this.isNullorEmpty(searchVillage) ? Guid.Empty : Guid.Parse(searchVillage);
                var municipalityId = this.isNullorEmpty(searchMunicipality) ? Guid.Empty : Guid.Parse(searchMunicipality);
                var departmentId = this.isNullorEmpty(searchDepartment) ? Guid.Empty : Guid.Parse(searchDepartment);

                if (start.HasValue == true)
                    ViewBag.start = start;
                else
                    ViewBag.start = DateTime.Now.AddDays(-30);

                if (end.HasValue == true)
                    ViewBag.end = end;
                else
                    ViewBag.end = DateTime.Now;

                if (start != null && end != null)
                    ViewBag.TASQ = this.TasqResume(id, Convert.ToDateTime(start), Convert.ToDateTime(end), searchDepartment, searchMunicipality, searchVillage);
                else
                    ViewBag.TASQ = new List<TASQAssessments>();

                ViewBag.start = start;
                ViewBag.end = end;
                AssessmentTemplate assessmentTemplate = db.AssessmentTemplates.Find(id);
                ViewBag.name = assessmentTemplate.Name;
            }
            catch (Exception ex)
            {

            }
            
            return View();
        }
        public String FarmsFail()
        {

            return "";
        }
        public ActionResult ViewFarmsFail(int idCriteria, Guid idAssessment, DateTime? start, DateTime? end, string searchDepartment, string searchMunicipality, string searchVillage)
        {
            
            if (idCriteria == 0 || idAssessment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dep = this.stringnull(searchDepartment);
            var mun = this.stringnull(searchMunicipality);
            var vill = this.stringnull(searchVillage);

            List<FarmsFail> lf = db.ExecuteQuery<FarmsFail>("FarmsFail @CriteriaId,@AssessmentId,@stardate,@enddate,@Department,@Municipality,@Village", new SqlParameter("CriteriaId", idCriteria), new SqlParameter("AssessmentId", idAssessment), new SqlParameter("stardate", start), new SqlParameter("enddate", end), new SqlParameter("Department", dep), new SqlParameter("Municipality", mun), new SqlParameter("Village", vill)).ToList();

            ViewBag.farms = lf;
            if (lf == null)
            {
                return HttpNotFound();
            }

            //return PartialView(lf.ToPagedList(pageNumber, pageSize));
            return PartialView();
        }
        private List<TASQAssessments> TasqResume(Guid? id, DateTime startDate, DateTime endDate, string searchDepartment, string searchMunicipality, string searchVillage)
        {
            var dep = this.stringnull(searchDepartment);
            var mun = this.stringnull(searchMunicipality);
            var vill = this.stringnull(searchVillage);

            var outer = db.ExecuteQuery<TASQAssessments>("AssessmentCoocentralResume @AsId, @stardate, @enddate, @Department, @Municipality, @Village", new SqlParameter("AsId", id), new SqlParameter("stardate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("enddate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("Department", dep), new SqlParameter("Municipality", mun), new SqlParameter("Village", vill)).ToList();
            return outer;
        }
        [HttpPost]
        public ActionResult getMucipalitiesByDepartment(Guid dep)
        {
            List<Municipality> listMun = new List<Municipality>();
            listMun = db.Municipalities.Where(m => m.Name != null && m.Name != "" && m.Department.Id == dep).OrderBy(m => m.Name.Trim()).ToList();
            return Json(new SelectList(listMun, "Id", "Name"));
        }
        [HttpPost]
        public ActionResult getVillagesByMunicipality(Guid mun)
        {
            List<Village> listVill = new List<Village>();
            listVill = db.Villages.Where(v => v.Name != null && v.Name != "" && v.Municipality.Id == mun).OrderBy(v => v.Name.Trim()).ToList();
            return Json(new SelectList(listVill, "Id", "Name"));
        }
        // GET: SustainabilityArea/AssessmentTemplates/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssessmentTemplate assessmentTemplate = db.AssessmentTemplates.Find(id);
            if (assessmentTemplate == null)
            {
                return HttpNotFound();
            }
            var asMod = assessmentTemplate.Modules.ToList();
            var modId = asMod.Select(m => m.Id).ToList();
            ViewBag.mod = asMod.FirstOrDefault();

            var asMod2 = assessmentTemplate.Id;
            ViewBag.mod2 = asMod2;

            ICollection<SubModule> SubModules = new Collection<SubModule>();
            if (db.SubModule.ToList().Count != 0)
                SubModules = db.SubModule.Where(sub => modId.Contains(sub.ModuleId)).OrderBy(x => x.Name).ThenBy(x => x.SubModuleOrder).ToList();
            ViewBag.SubModules = SubModules;

            var subId = SubModules.Select(s => s.Id).ToList();
            ICollection<TASQCriteria> TasqCriterias = new Collection<TASQCriteria>();
            if (db.TASQCriteria.ToList().Count != 0)
                TasqCriterias = db.TASQCriteria.Where(c => subId.Contains(c.SubModuleId)).OrderBy(u=>u.SubModule.SubModuleOrder).ThenBy(a=>a.CriteriaOrder).ToList();
            ViewBag.TasqCriterias = TasqCriterias;

            return View(assessmentTemplate);
        }

        // GET: SustainabilityArea/AssessmentTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SustainabilityArea/AssessmentTemplates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Type,CreatedAt,UpdatedAt,DeletedAt")] AssessmentTemplate assessmentTemplate)
        {
            if (ModelState.IsValid)
            {
                assessmentTemplate.Id = Guid.NewGuid();
                db.AssessmentTemplates.Add(assessmentTemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assessmentTemplate);
        }

        // GET: SustainabilityArea/AssessmentTemplates/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssessmentTemplate assessmentTemplate = db.AssessmentTemplates.Find(id);
            if (assessmentTemplate == null)
            {
                return HttpNotFound();
            }
            return View(assessmentTemplate);
        }

        // POST: SustainabilityArea/AssessmentTemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Type,CreatedAt,UpdatedAt,DeletedAt")] AssessmentTemplate assessmentTemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessmentTemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assessmentTemplate);
        }

        // GET: SustainabilityArea/AssessmentTemplates/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssessmentTemplate assessmentTemplate = db.AssessmentTemplates.Find(id);
            if (assessmentTemplate == null)
            {
                return HttpNotFound();
            }
            return View(assessmentTemplate);
        }

        // POST: SustainabilityArea/AssessmentTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AssessmentTemplate assessmentTemplate = db.AssessmentTemplates.Find(id);
            assessmentTemplate.DeletedAt = DateTime.Now;
            db.Entry(assessmentTemplate).State = EntityState.Modified;
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
        private string stringnull(string variable)
        {
            if (string.IsNullOrEmpty(variable) || variable.Equals("null") || variable.Equals("0"))
                return "";
            else
                return variable;
        }
        private Boolean isNullorEmpty(string variable)
        {
            if (string.IsNullOrEmpty(variable) || variable.Equals("null") || variable.Equals("0"))
                return true;
            else
                return false;
        }
    }
    public class TASQAssessments
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Cantidadno { get; set; }
        public int Cantidadyes { get; set; }
    }
    public class FarmsFail
    {   
        public string farmName { get; set; }
        public string depName { get; set; }
        public string munName { get; set; }
        public string villName { get; set; }
    }
}
