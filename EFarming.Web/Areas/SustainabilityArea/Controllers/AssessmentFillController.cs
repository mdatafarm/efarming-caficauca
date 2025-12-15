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
using EFarming.Web.Models;
using System.Text;


namespace EFarming.Web.Areas.SustainabilityArea.Controllers
{
    /// <summary>
    /// Controller
    /// </summary>
    public class AssessmentFillController : EFarming.Web.Controllers.BaseController
    {
        private UnitOfWork db = new UnitOfWork();

        
        /// <summary>
        /// GET: SustainabilityArea/AssesmentFill
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(Guid? TemplateID)
        {

            var Assessments = db.AssessmentTemplates.Where(a => a.Public != false && a.DeletedAt == null).ToList();
            ViewBag.TypeAssessment = Assessments;

            Guid uId = User.UserId;

            List<AnswersAssements> query = new List<AnswersAssements>();
            if (TemplateID.HasValue)
            {
                query = db.ExecuteQuery<AnswersAssements>("AnswersAssements @startdate={0}, @enddate={1}, @Id={2}, @user={3}", null, null, TemplateID, uId).OrderByDescending(x => x.A_NULLS).OrderByDescending(a => a.Fecha).ToList();
                ViewBag.Answers = query;
            }

            ViewBag.TemplateID = TemplateID;



            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public ActionResult NewAssessment(Guid? TemplateID)
        {

            var farms = db.Farms.Where(a => a.FarmStatus.Name.Equals("ACTIVO")).ToList();
            ViewBag.Farms = farms;


            string assesmentType = db.AssessmentTemplates.Where(t => t.Id == TemplateID).Select(a => a.Name).FirstOrDefault();

            ViewBag.TemplateID = TemplateID;
            ViewBag.Template = assesmentType;


            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TemplateID"></param>
        /// <param name="FarmID"></param>
        /// <param name="Desc"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]        
        public ActionResult SaveAssessment(Guid TemplateID, Guid FarmID, string Desc, string Date)
        {

            TASQAssessment ass = new TASQAssessment();

            var UserId = System.Web.HttpContext.Current.User as CustomPrincipal;

            ass.Id = Guid.NewGuid();
            ass.AssessmentTemplateId = TemplateID;
            ass.FarmId = FarmID;
            ass.Date = Convert.ToDateTime(Date);
            ass.Description = Desc;
            ass.CreatedAt = DateTime.Now;
            ass.UserId = UserId.UserId;
            ass.SyncOperation = new Guid("00000000-0000-0000-0000-000000000000");

            db.TASQAssessment.Add(ass);
            db.SaveChanges();

            List<int> modules = db.Module.Where(m => m.AssessmentTemplateId == TemplateID).Select(s=>s.Id).ToList();
            List<int> submodules = db.SubModule.Where(s => modules.Contains(s.ModuleId)).Select(x => x.Id).ToList();
            List<int> criterias = db.TASQCriteria.Where(c => submodules.Contains(c.SubModuleId)).Select(s => s.Id).ToList();

            foreach(int criteriaId in criterias)
            {
                TASQAssessmentAnswer answer = new TASQAssessmentAnswer();
                answer.Id = Guid.NewGuid();
                answer.CriteriaId = criteriaId;
                answer.Value = null;
                answer.CreatedAt = DateTime.Now;
                answer.TASQAssessmentId = ass.Id;
                answer.UpdatedAt = null;
                answer.DeletedAt = null;

                db.TASQAssessmentAnswer.Add(answer);
                db.SaveChanges();
            }


            return RedirectToAction("Index", "AssessmentFill", new { area= "SustainabilityArea", @TemplateID = TemplateID });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AssessmentID"></param>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public ActionResult AssessmentFill(Guid AssessmentID, Guid TemplateID)
        {

            var Template = db.AssessmentTemplates.Where(t => t.Id == TemplateID).FirstOrDefault();
            var Assesment = db.TASQAssessment.Where(a => a.Id == AssessmentID).FirstOrDefault();
            var Modules = db.Module.Where(m => m.AssessmentTemplateId == TemplateID).ToList();
            var SubModules = db.SubModule.Where(s => s.Module.AssessmentTemplateId == TemplateID).ToList();
            var Criterias = db.TASQCriteria.Where(c => c.SubModule.Module.AssessmentTemplateId == TemplateID).ToList();
            var Answers = db.TASQAssessmentAnswer.Where(r => r.TASQAssessmentId == AssessmentID).ToList();

            

            ViewBag.Template = Template;
            ViewBag.Assesment = Assesment;
            ViewBag.Modules = Modules;
            ViewBag.SubModules = SubModules;
            ViewBag.Criterias = Criterias;
            ViewBag.Answers = Answers;

            return View("AssesmentFilling");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AssessmentID"></param>
        /// <param name="Answers"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public ActionResult SaveAnswers(Guid AssessmentID, string Answers)
        {

            int result = 0;


            int pregunta = 0;
            string[] respuestasAll = Answers.Split(new string[] { "~" }, StringSplitOptions.None);

            TASQAssessmentAnswer respuesta = new TASQAssessmentAnswer();

            for (int i = 0; i < respuestasAll.Length - 1; i++)
            {
                if (respuestasAll[i].Split('|')[1].ToString().Length > 0)
                {
                    pregunta = Convert.ToInt32(respuestasAll[i].Split('|')[0]);
                    respuesta = db.TASQAssessmentAnswer.Where(a => a.TASQAssessmentId == AssessmentID && a.CriteriaId == pregunta).FirstOrDefault();

                    if (respuesta != null)
                    {
                        respuesta.Value = respuestasAll[i].Split('|')[1].ToString();
                        try
                        {
                            db.SaveChanges();
                            result = 1;
                        }
                        catch { }
                    }
                    else
                    {
                        respuesta = new TASQAssessmentAnswer();

                        respuesta.Id = Guid.NewGuid();
                        respuesta.CriteriaId = Convert.ToInt32(respuestasAll[i].Split('|')[0]);
                        respuesta.Value = respuestasAll[i].Split('|')[1];
                        respuesta.CreatedAt = DateTime.Now;
                        respuesta.TASQAssessmentId = AssessmentID;

                        db.TASQAssessmentAnswer.Add(respuesta);
                        try
                        {
                            db.SaveChanges();
                            result = 1;
                        }
                        catch { }

                    }
                }

            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        public class AnswersAssements
        {
            public Guid? Id_Assement { get; set; }
            public Guid? Id_Finca { get; set; }
            public DateTime? Fecha { get; set; }
            public string Prueba { get; set; }
            public string Tecnico { get; set; }
            public string Nomb_Finca { get; set; }
            public string Code_Finca { get; set; }
            public string CC_Producer { get; set; }
            public string Name_Producer { get; set; }
            public string Vereda { get; set; }
            public string Municipio { get; set; }
            public string Departamento { get; set; }
            public string Cooperativa { get; set; }
            public int? Total_Q { get; set; }
            public int? A_NULLS { get; set; }
            public int? A_NOT_NULLS { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class groupedAssessments
        {
            public List<TASQAssessmentAnswer> Answers { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class ListOptionAnswer
        {
            public string OptionAnswer { get; set; }
            public bool SelectOption { get; set; }
        }
    }
}