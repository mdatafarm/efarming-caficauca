using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.ComercialModule;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.DAL;
using EFarming.DTO.AdminModule;
using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using Rotativa;
using Rotativa.Options;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using AutoMapper;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for all the reports of Quality
    /// </summary>
    [CustomAuthorize(Roles = "User,Quality,Manager,Taster,Admin,Reader")]
    public class QualityReportsController : BaseController
    {
        /// <summary>
        /// The database
        /// </summary>
        private UnitOfWork db = new UnitOfWork();
        /// <summary>
        /// The _user manager
        /// </summary>
        IUserManager _userManager;
        /// <summary>
        /// The _sensory profile manager
        /// </summary>
        ISensoryProfileManager _sensoryProfileManager;
        /// <summary>
        /// The _quality attribute manager
        /// </summary>
        IQualityAttributeManager _qualityAttributeManager;
        /// <summary>
        /// The _cooperative manager
        /// </summary>
        ICooperativeManager _cooperativeManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="QualityReportsController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="sensoryProfileManager">The sensory profile manager.</param>
        /// <param name="qualityAttributeManager">The quality attribute manager.</param>
        /// <param name="cooperativeManager">The cooperative manager.</param>
        public QualityReportsController(IUserManager userManager,
            ISensoryProfileManager sensoryProfileManager,
            IQualityAttributeManager qualityAttributeManager,
            ICooperativeManager cooperativeManager)
        {
            _userManager = userManager;
            _sensoryProfileManager = sensoryProfileManager;
            _qualityAttributeManager = qualityAttributeManager;
            _cooperativeManager = cooperativeManager;
        }

        #region Taza
        /// <summary>
        /// Report Quality atributes the taster.
        /// </summary>
        /// <param name="tasterId">The taster identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="page">The page.</param>
        /// <returns>The view with results</returns>
        /// 
        public ActionResult ByTaster1(Guid? tasterId, DateTime? start, DateTime? end, int? page)
        {
            if (start.HasValue)
                ViewBag.SelectedStart = string.Format("{0:yyyy-MM-dd}", start.Value);
            else
                ViewBag.SelectedStart = string.Empty;

            if (end.HasValue)
                ViewBag.SelectedEnd = string.Format("{0:yyyy-MM-dd}", end.Value);
            else
                ViewBag.SelectedEnd = string.Empty;

            if (tasterId.HasValue)
                ViewBag.SeletedTasterId = tasterId.Value;
            else
                ViewBag.SeletedTasterId = null;

            ViewBag.Tasters = new SelectList(_userManager.GetTasters(), "Id", "FullName", ViewBag.SeletedTasterId);
            //Dates for the first data to be showed
            if (!start.HasValue)
            {
                start = DateTime.Now.AddDays(-20);
                ViewBag.SelectedStart = string.Format("{0:yyyy-MM-dd}", start.Value);
            }

            if (!end.HasValue)
            {
                end = DateTime.Now.AddDays(1);
                ViewBag.SelectedEnd = string.Format("{0:yyyy-MM-dd}", end.Value);
            }

            ICollection<DTO.QualityModule.SensoryProfileAssessmentDTO> results = _sensoryProfileManager.Filter(start, end, ViewBag.SeletedTasterId, AssessmentTemplate.CuppingId);

            ViewBag.Attributes = _qualityAttributeManager.Get(AssessmentTemplate.CuppingId);

            // No retornar paginacion por que se va a hacer con datatable.net
            //int pageSize = 20;
            //int pageNumber = (page ?? 1);
            //return View(results.ToPagedList(pageNumber, pageSize));        

            return View(results);
        }


        /*PDF*/
        [AllowAnonymous]
        public ActionResult PDF(Guid id)
        {
            //DownloadPDF();
            //EmailResultByTechnician();
            List<SensoryProfileAnswerResult> ListSensoryProfileAnswerResult = new List<SensoryProfileAnswerResult>();
            List<SensoryProfileResult> ListResultSensoryProfile = new List<SensoryProfileResult>();
            SensoryProfileResult ResultSensoryProfile = new SensoryProfileResult();

            DTO.QualityModule.SensoryProfileAssessmentDTO results = _sensoryProfileManager.Get(id);
            ViewBag.PT = results;
            ViewBag.Cupping = _qualityAttributeManager.Get(AssessmentTemplate.CuppingId);

            ResultSensoryProfile.Id = results.Id;
            ResultSensoryProfile.FarmId = results.FarmId;
            ResultSensoryProfile.CodeAAA = results.Farm.Code;
            ResultSensoryProfile.FarmerName = results.Farm.FamilyUnitMembers.Where(f => f.Identification == results.Farm.Code).Select(s => s.FirstName + " " + s.LastName).FirstOrDefault();
            ResultSensoryProfile.Agronomist = results.User.FirstName + " " + results.User.LastName;
            ResultSensoryProfile.EntryDate = results.CreatedAt;
            ResultSensoryProfile.AnalysisDate = results.Date;
            ResultSensoryProfile.FarmName = results.Farm.Name;
            ResultSensoryProfile.VillageName = results.Farm.Village.Name;
            ResultSensoryProfile.MunicipalityName = results.Farm.Village.Municipality.Name;
            ResultSensoryProfile.AnalyzedAmount = results.SensoryProfileAnswers.Where(f => f.QualityAttributeId == new Guid("e75ff320-9f01-4197-b139-d5f147c401ca")).Select(s => s.Answer).FirstOrDefault();
            ResultSensoryProfile.ResultPositiveAttribute = results.SensoryProfileAnswers.Where(s => s.QualityAttributeId == new Guid("945ad1f2-f3a7-4e89-8ab1-9472a85cdff6")).ToList();
            ResultSensoryProfile.ResultDefect = results.SensoryProfileAnswers.Where(f => f.QualityAttributeId == new Guid("90a8ecc4-14a5-4ca8-8b02-354a7f85341a")).ToList();
            ResultSensoryProfile.Observations = results.SensoryProfileAnswers.Where(o => o.QualityAttributeId == new Guid("71F45B44-692E-4487-8C93-3ACB7C9680F0")).Select(a => a.Answer).FirstOrDefault() + " " + results.Description;
            foreach (var item in results.SensoryProfileAnswers)
            {
                SensoryProfileAnswerResult ResultSensoryProfileAnswerResult = new SensoryProfileAnswerResult();
                ResultSensoryProfileAnswerResult.TypeQualityName = item.QualityAttribute.Description;
                ResultSensoryProfileAnswerResult.Answer = item.Answer;
                ResultSensoryProfileAnswerResult.QualityAttributeId = item.QualityAttributeId;
                ListSensoryProfileAnswerResult.Add(ResultSensoryProfileAnswerResult);
            }

            ResultSensoryProfile.PuntajeTotal = 0;
            foreach (var item in ListSensoryProfileAnswerResult)
            {
                switch (Convert.ToString(item.QualityAttributeId).ToUpper())
                {
                    case "DFD86928-815E-4331-9087-593FCA1944AB":
                        ResultSensoryProfile.Fragancia = ResultSensoryProfile.Aroma = item.Answer == "" ? "0" : item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                    case "50247799-F309-4FCD-AF4F-B726B554FB5C":
                        ResultSensoryProfile.Sabor = item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                    case "7A064241-A4B9-4F76-B9E5-5B9C7D4A214D":
                        ResultSensoryProfile.SaborResidual = item.Answer == "" ? "0" : item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                    case "EA98125A-42E4-4994-8EF2-FB5200529F48":
                        ResultSensoryProfile.Acidez = item.Answer == "" ? "0" : item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                    case "AB23012B-4891-49F4-AA58-0F24EC891390":
                        ResultSensoryProfile.Cuerpo = item.Answer == "" ? "0" : item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                    case "6338C6DA-7955-46ED-9094-DE68E5928544":
                        ResultSensoryProfile.Balance = item.Answer == "" ? "0" : item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                    case "39111AEC-6F10-41E8-8BBC-5DA0AB44151E":
                        ResultSensoryProfile.Dulzor = item.Answer == "" ? "0" : item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                    case "239A61F7-40FA-43FB-84CD-5E2767A49C75":
                        ResultSensoryProfile.PuntajeCatador = item.Answer == "" ? "0" : item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                    case "38A1DD49-1A28-4F53-8FB8-A6282525CC5D":
                        ResultSensoryProfile.TazaLimpia = item.Answer == "" ? "0" : item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                    case "3F712F43-B110-4A21-9394-9518D11C1731":
                        ResultSensoryProfile.Uniformidad = item.Answer == "" ? "0" : item.Answer;
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".", ","));
                        break;
                };
            }

            if (ResultSensoryProfile.ResultClassification == "TAZA LIMPIA")
            {
                ResultSensoryProfile.ClassificationColor = "#92d050";
            }
            else if (ResultSensoryProfile.ResultClassification == "CAFE CON DEFECTO")
            {
                ResultSensoryProfile.ClassificationColor = "#c00000";
            }
            else if (ResultSensoryProfile.ResultClassification == "CAFE EXCEPCIONAL")
            {
                ResultSensoryProfile.ClassificationColor = "#00b050";
            }
            else
            {
                ResultSensoryProfile.ResultClassification = "SIN MUESTRA";
                ResultSensoryProfile.ClassificationColor = "#ffffff";
            }

            //var ListDefect = results.SensoryProfileAnswers.Where(s => s.QualityAttributeId == new Guid("90A8ECC4-14A5-4CA8-8B02-354A7F85341A")).ToList();
            var ListDefect = results.SensoryProfileAnswers.Where(s => s.QualityAttributeId == new Guid("90A8ECC4-14A5-4CA8-8B02-354A7F85341A")).OrderBy(o => o.Answer).FirstOrDefault();
            if (ListDefect != null)
            {
                List<QualityRecommendations> ListRecommendations = new List<QualityRecommendations>();
                foreach (var resultRecommendation in ListDefect.QualityAttribute.OptionAttributes.Where(n => n.Description == ListDefect.Answer))
                {
                    var OptionAttributes = db.OptionAttributes.Find(resultRecommendation.Id).QualityRecommendations.OrderBy(o => o.OrderOpcion).ToList();
                    foreach (var itemRecommendation in OptionAttributes)
                    {
                        ListRecommendations.Add(new QualityRecommendations
                        {
                            Id = itemRecommendation.Id,
                            OrderOpcion = itemRecommendation.OrderOpcion,
                            Recommendations = itemRecommendation.Recommendations
                        });
                    }
                }
                ResultSensoryProfile.ResultRecommendation = ListRecommendations;
            }
            else
            {
                foreach (var item in results.SensoryProfileAnswers.Where(s => s.QualityAttributeId == new Guid("D5FE6059-256D-46DD-A03A-533876191B96")).ToList())
                {
                    foreach (var itemRecommendation in item.QualityAttribute.OptionAttributes.Where(n => n.Description == item.Answer))
                    {
                        ResultSensoryProfile.ResultRecommendationClassification = db.OptionAttributes.Find(itemRecommendation.Id).QualityRecommendations.Select(s => s.Recommendations).FirstOrDefault();
                    }
                }
            }
            ListResultSensoryProfile.Add(ResultSensoryProfile);
            //var technicianList = db.Users.ToList();
            //foreach (var Technician in technicianList)
            //{
            //    foreach (var rol in Technician.Roles)
            //    {
            //        if (rol.Id == new Guid("D3D50A4D-B63A-406F-BA90-0A56F06AFAD6") && Technician.Farms.Count() > 0)
            //        {
            //            foreach (var Farm in Technician.Farms)
            //            {
            //                var ListSensoryProfile = db.SensoryProfileAssessments.Where(s => s.FarmId == Farm.Id).ToList();
            //                if (ListSensoryProfile.Count() > 0)
            //                {
            //                    foreach (var SensoryProfile in ListSensoryProfile)
            //                    {
            //                        string path = Server.MapPath(string.Format("~/Content/QualityRecommendations/{0}/", Technician.FirstName + " " + Technician.LastName));
            //                        Directory.CreateDirectory(path);
            //                        string pathFarm = path + Farm.Code + "-" + Farm.Name.Replace("?", " ");
            //                        Directory.CreateDirectory(pathFarm);
            //                        var root = pathFarm;
            //                        //var pdfname = SensoryProfile.Date.Date.ToString("dd-MM-yyyy") + "-" + SensoryProfile.Id + ".pdf";
            //                        var pdfname = SensoryProfile.Id + ".pdf";
            //                        var pathPDF = Path.Combine(root, pdfname);
            //                        pathPDF = Path.GetFullPath(pathPDF);
            //                        using (WebClient client = new WebClient())
            //                        {
            //                            client.DownloadFile("http://192.168.1.89:92/Reports/PDF/" + SensoryProfile.Id, pathPDF);
            //                            //client.DownloadFile("http://cafexport.efarming.co/Reports/PDF/" + SensoryProfile.Id, pathPDF);
            //                            //client.DownloadFile("http://www.irs.gov/pub/irs-pdf/fw4.pdf", pathPDF);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            //return new Rotativa.UrlAsPdf("http://cafexport.efarming.co/Reports/PDF/"+id) { FileName = "UrlTest.pdf" };
            //return new Rotativa.UrlAsPdf("http://192.168.1.89:92/Reports/PDF/"+id) { FileName = "UrlIPTest.pdf" };
            //return new Rotativa.UrlAsPdf("http://192.168.1.89:92/Farms/Export/b5b54cc6-9941-4bc7-82b6-afbad764795a") { FileName = "Farm.pdf" };
            //return new Rotativa.UrlAsPdf("http://localhost:8888/Reports/PDF/e786b4b3-6078-4391-4150-d7408f9f6a23) { FileName = "UrlIPTest.pdf" };

            //return View(ListResultSensoryProfile.FirstOrDefault());
            return new Rotativa.ViewAsPdf("PDF", ListResultSensoryProfile.FirstOrDefault())
            {
                PageSize = Rotativa.Options.Size.A4,
            };
        }
        /*End PDF*/
        public ActionResult ByTaster(string sortOrder,  string currentDepartment, string searchDepartment, string currentMunicipality, string searchMunicipality, string currentVillage, string searchVillage, string currentFarm, string searchFarm, string currentTaster, string searchTaster, int? page)
        {
            var withFilter = (!string.IsNullOrEmpty(searchDepartment) || !string.IsNullOrEmpty(searchMunicipality) || !string.IsNullOrEmpty(searchVillage) || !string.IsNullOrEmpty(searchTaster) || !string.IsNullOrEmpty(searchFarm));
            if (withFilter)
            {
                page = 1;
            }
            else
            {
                searchDepartment = currentDepartment;
                searchMunicipality = currentMunicipality;
                searchVillage = currentVillage;
                searchTaster = currentTaster;
                searchFarm = currentFarm;
            }
            ViewBag.NameFarmSortParm = String.IsNullOrEmpty(sortOrder) ? "namefarm_desc" : "";
            ViewBag.FarmManagerSortParm = sortOrder == "FarmManager" ? "farmmanager_desc" : "FarmManager";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentVillage = searchVillage;
            ViewBag.CurrentMunicipality = searchMunicipality;
            ViewBag.CurrentDepartment = searchDepartment;
            ViewBag.CurrentFarm = searchFarm;
            ViewBag.CurrentTaster = searchTaster;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            /*
            if (start.HasValue)
                ViewBag.SelectedStart = string.Format("{0:yyyy-MM-dd}", start.Value);
            else
                ViewBag.SelectedStart = string.Empty;

            if (end.HasValue)
                ViewBag.SelectedEnd = string.Format("{0:yyyy-MM-dd}", end.Value);
            else
                ViewBag.SelectedEnd = string.Empty;

            if (tasterId.HasValue)
                ViewBag.SeletedTasterId = tasterId.Value;
            else
                ViewBag.SeletedTasterId = null;
                */

            //ViewBag.Tasters = new SelectList(_userManager.GetTasters(), "Id", "FullName", ViewBag.SeletedTasterId);
            //Dates for the first data to be showed
            /* if (!start.HasValue)
             {
                 start = DateTime.Now.AddDays(-20);
                 ViewBag.SelectedStart = string.Format("{0:yyyy-MM-dd}", start.Value);
             }
             */

            var villageId = string.IsNullOrEmpty(searchVillage) || searchVillage.Equals("0") ? Guid.Empty : Guid.Parse(searchVillage);
            var municipalityId = string.IsNullOrEmpty(searchMunicipality) || searchMunicipality.Equals("0") ? Guid.Empty : Guid.Parse(searchMunicipality);
            var departmentId = string.IsNullOrEmpty(searchDepartment) ? Guid.Empty : Guid.Parse(searchDepartment);
           
            var tasterId = string.IsNullOrEmpty(searchTaster) ? Guid.Empty : Guid.Parse(searchTaster);


            var lspa = _sensoryProfileManager.GetAllQueryable(SensoryProfileAssessmentSpecification.FilterByAnything(AssessmentTemplate.CuppingId, villageId, municipalityId, departmentId, searchFarm, tasterId),
                                        f => f.Farm.Name).Distinct();

            List<SensoryProfileAssessment> results = lspa.ToList();


            List<SensoryProfileAssessment> resultsaux = db.SensoryProfileAssessments.ToList();
            List<User> lu = new List<User>();
            using (var spa = resultsaux.GetEnumerator())
            {
                while (spa.MoveNext())
                {
                    //string ts = spa.Current.TotalScore(ViewBag.Attributes);
                   // spa.Current.Total_score = ts;
                    lu.Add(spa.Current.User);
                    
                }
            }


            lu = lu.Distinct().OrderBy(u => u.FirstName).ToList();
            foreach (User ud in lu)
            {
                ud.FirstName = ud.FirstName.ToUpper();
                ud.LastName = ud.LastName.ToUpper();
            }
            List<UserDTO> luDTO = new List<UserDTO>();
            luDTO = Mapper.Map<List<UserDTO>>(lu);
            
            
            //lu =lu.ConvertAll(u => u.FullName.ToUpper).ToList();
            SelectList lusers = new SelectList(luDTO, "Id", "FullName");
            ViewBag.Tasters = lusers;
            
            switch (sortOrder)
            {
                case "namefarm_desc":
                    //results = results.   OrderByDescending(spa => spa.Farm.Name.ToString);
                    results = results.OrderByDescending(spa => spa.Farm != null && spa.Farm.Name != null ? spa.Farm.Name.ToUpper() : string.Empty).ToList();
                    break;
                case "FarmManager":
                    results = results.OrderBy(spa => spa.Farm != null && spa.Farm.FamilyUnitMembers.Count > 0 ? spa.Farm.FamilyUnitMembers.First().FirstName.ToUpper() : string.Empty).ToList();
                    break;
                case "farmmanager_desc":
                    results = results.OrderByDescending(spa => spa.Farm != null && spa.Farm.FamilyUnitMembers.Count > 0 ? spa.Farm.FamilyUnitMembers.First().FirstName.ToUpper() : string.Empty).ToList();
                    break;
                default:
                   /* results = results.OrderBy(spa => spa.Date).ToList();
                    results = results.OrderBy(spa => spa.Farm != null && spa.Farm.Name != null ? spa.Farm.Name.ToUpper() : string.Empty).ToList();*/
                    results = results.OrderBy(spa => spa.Farm != null && spa.Farm.Name != null ? spa.Farm.Name.ToUpper() : string.Empty).ToList();
                    break;
            }
            var dep = db.Departments.OrderBy(d=>d.Name).ToList();
            //var mun = db.Municipalities.ToList();
            //var vill = db.Villages.ToList();
            SelectList ld = new SelectList(dep, "Id", "Name");
            //SelectList lm = new SelectList(mun, "Id", "Name");
            //SelectList lv = new SelectList(vill, "Id", "Name");
            ViewBag.Departments = ld;
            //ViewBag.Municipalities = lm;
            //ViewBag.Villages = lv;
            // No retornar paginacion por que se va a hacer con datatable.net
            //int pageSize = 20;
            //int pageNumber = (page ?? 1);
            //return View(results.ToPagedList(pageNumber, pageSize));        
            return View(results.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult getMucipalitiesByDepartment(Guid dep)
        {
            List<Municipality> listMun = new  List<Municipality>();
            listMun = db.Municipalities.Where(m => m.Name!=null && m.Name != ""&& m.Department.Id==dep).OrderBy(m=>m.Name.Trim()).ToList();
            return Json(new SelectList(listMun,"Id","Name"));
        }
        [HttpPost]
        public ActionResult getVillagesByMunicipality(Guid mun)
        {
            List<Village> listVill = new List<Village>();
            listVill = db.Villages.Where(v => v.Name != null && v.Name != "" && v.Municipality.Id == mun).OrderBy(v => v.Name.Trim()).ToList();
            return Json(new SelectList(listVill, "Id", "Name"));
        }
        public ActionResult Mailsend()
        {

            return View();
        }

        /// <summary>
        /// Bies the taster excel.
        /// </summary>
        /// <param name="tasterId">The taster identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>The View with assements</returns>
        public ActionResult ByTasterExcel(Guid? tasterId, DateTime? start, DateTime? end)
        {
            if (start.HasValue)
                ViewBag.SelectedStart = string.Format("{0:yyyy-MM-dd}", start.Value);
            else
                ViewBag.SelectedStart = string.Empty;

            if (end.HasValue)
                ViewBag.SelectedEnd = string.Format("{0:yyyy-MM-dd}", end.Value);
            else
                ViewBag.SelectedEnd = string.Empty;

            if (tasterId.HasValue)
                ViewBag.SeletedTasterId = tasterId.Value;
            else
                ViewBag.SeletedTasterId = string.Empty;

            var assessments = _sensoryProfileManager.Filter(start, end, tasterId, AssessmentTemplate.CuppingId);
            ViewBag.Attributes = _qualityAttributeManager.Get(AssessmentTemplate.CuppingId);

            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            Response.AddHeader("content-disposition", "attachment; filename=export.xls");
            return View(assessments);
        }
        #endregion

        #region Punto Cero
        /// <summary>
        /// Puntoes the cero.
        /// </summary>
        /// <param name="tasterId">The taster identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="page">The page.</param>
        /// <returns>The View with results</returns>
        public ActionResult PuntoCero(Guid? tasterId, DateTime? start, DateTime? end, int? page)
        {
            if (start.HasValue)
                ViewBag.SelectedStart = string.Format("{0:yyyy-MM-dd}", start.Value);
            else
                ViewBag.SelectedStart = string.Empty;

            if (end.HasValue)
                ViewBag.SelectedEnd = string.Format("{0:yyyy-MM-dd}", end.Value);
            else
                ViewBag.SelectedEnd = string.Empty;

            if (tasterId.HasValue)
                ViewBag.SeletedTasterId = tasterId.Value;
            else
                ViewBag.SeletedTasterId = null;

            ViewBag.Tasters = new SelectList(_userManager.GetTasters(), "Id", "FullName", ViewBag.SeletedTasterId);

            ICollection<DTO.QualityModule.SensoryProfileAssessmentDTO> results = _sensoryProfileManager.Filter(start, end, ViewBag.SeletedTasterId, AssessmentTemplate.PuntoZeroId);

            ViewBag.Attributes = _qualityAttributeManager.Get(AssessmentTemplate.PuntoZeroId);

            //return View(results);
            //int pageSize = 20;
            //int pageNumber = (page ?? 1);
            //results.ToPagedList(pageNumber, pageSize)

            return View(results);
        }
        public static DataTable QualityAssessmentsReport()
        {
            DataTable dt = new DataTable();
            dt = FillDataTableByProcedure("QualityAssessments");
            return dt;
        }
        private static DataTable FillDataTableByProcedure(string _NameProcedure)
        {
            DataTable _dataTable = new DataTable();
            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter _sqlAdapter = new SqlDataAdapter(_NameProcedure, _sqlConnection))
                    {
                        //_sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter("@userId", userId));
                        //_sqlAdapter.SelectCommand.ExecuteNonQuery();
                        _sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _sqlAdapter.Fill(_dataTable);
                        return _dataTable;
                    }
                }

            }
            catch (Exception) { throw; }
        }
        public void ToExcel()
        {
            DataTable _dataTable = new DataTable();
            _dataTable = QualityAssessmentsReport();
            Response.AddHeader("content-disposition", "attachment; filename=QualityAssessments.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            if (_dataTable == null)
                return;

            Response.ClearContent();
            //Response.ContentType = "application/vnd.ms-excel";
            Response.ContentType = "application/excel";

            string tab = "";
            foreach (DataColumn dc in _dataTable.Columns)
            {
               
                Response.Write(tab + dc.ColumnName );
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in _dataTable.Rows)
            {
                tab = "";
                for (i = 0; i < _dataTable.Columns.Count; i++)
                { 
                    Response.Write(tab + HttpUtility.HtmlDecode(dr[i].ToString()));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
            /// <summary>
            /// Puntoes the cero excel.
            /// </summary>
            /// <param name="tasterId">The taster identifier.</param>
            /// <param name="start">The start.</param>
            /// <param name="end">The end.</param>
            /// <returns>The view with results</returns>
            public ActionResult PuntoCeroExcel(Guid? tasterId, DateTime? start, DateTime? end)
        {
            if (start.HasValue)
                ViewBag.SelectedStart = string.Format("{0:yyyy-MM-dd}", start.Value);
            else
                ViewBag.SelectedStart = string.Empty;

            if (end.HasValue)
                ViewBag.SelectedEnd = string.Format("{0:yyyy-MM-dd}", end.Value);
            else
                ViewBag.SelectedEnd = string.Empty;

            if (tasterId.HasValue)
                ViewBag.SeletedTasterId = tasterId.Value;
            else
                ViewBag.SeletedTasterId = string.Empty;

            var assessments = _sensoryProfileManager.Filter(start, end, tasterId, AssessmentTemplate.PuntoZeroId);
            // Si es el reporte de Punto cero por que pasa el id de prueba de taza            
            //ViewBag.Attributes = _qualityAttributeManager.Get(AssessmentTemplate.CuppingId);
            ViewBag.Attributes = _qualityAttributeManager.Get(AssessmentTemplate.PuntoZeroId);

            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            Response.AddHeader("content-disposition", "attachment; filename=export.xls");

            return View(assessments);
        }
        #endregion

        #region Taza_Reports
        /// <summary>
        /// Clasifications the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>The View</returns>
        public ActionResult Clasification(DateTime? start, DateTime? end)

        {
            if (start.HasValue == true)
                ViewBag.start = start;
            else
                ViewBag.start = DateTime.Now.AddDays(-30);

            if (end.HasValue == true)
                ViewBag.end = end;
            else
                ViewBag.end = DateTime.Now.AddDays(1);

            if (User.IsTaster()&& !User.IsQuality() && !User.IsAdmin())
            {
                return RedirectToAction("Index", "Reports");
            }
            else
            {
                //var Cooperatives = db.Cooperatives.Where(c => c. == new Guid("{b11b7fcc-9759-4850-920a-f79531774218}"));
                //ViewBag.cooperatives = Cooperatives;
                //List<TASQResults>
                return View();
            }
        }

        /// <summary>
        /// Clasifications the by cooperative.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The View</returns>
        public ActionResult ClasificationByCooperative(Guid cooperativeId, DateTime? start, DateTime? end)

        {
            if (start.HasValue == true)
                ViewBag.start = start;
            else
                ViewBag.start = DateTime.Now.AddDays(-30);

            if (end.HasValue == true)
                ViewBag.end = end;
            else
                ViewBag.end = DateTime.Now;

            //var cooperative = _cooperativeManager.Get(cooperativeId);
            //ViewBag.cooperative = cooperative.Name;
            //ViewBag.cooperativeId = cooperative.Id;
            //var Cooperatives = db.CooperativeChain.Where(c => c.ChainId == new Guid("{b11b7fcc-9759-4850-920a-f79531774218}"));
            //ViewBag.cooperatives = Cooperatives;
            return View();
        }
        #endregion


        public class SensoryProfileAnswerResult
        {
            public string TypeQualityName { get; set; }
            public string Answer { get; set; }
            public Guid QualityAttributeId { get; set; }
        }
        public class SensoryProfileResult
        {
            public Guid Id { get; set; }
            public Guid? FarmId { get; set; }
            public string FarmerName { get; set; }
            public string CodeAAA { get; set; }
            public DateTime? EntryDate { get; set; }
            public string Agronomist { get; set; }
            public DateTime AnalysisDate { get; set; }
            public string AnalyzedAmount { get; set; }
            public string FarmName { get; set; }
            public string VillageName { get; set; }
            public string MunicipalityName { get; set; }
            public string Fragancia { get; set; }
            public string Aroma { get; set; }
            public string Sabor { get; set; }
            public string SaborResidual { get; set; }
            public string Acidez { get; set; }
            public string Cuerpo { get; set; }
            public string Balance { get; set; }
            public string Dulzor { get; set; }
            public string PuntajeCatador { get; set; }
            public string TazaLimpia { get; set; }
            public string Uniformidad { get; set; }
            public decimal PuntajeTotal { get; set; }
            public string ResultClassification { get; set; }
            public string ResultRecommendationClassification { get; set; }
            public string ClassificationColor { get; set; }
            public string Observations { get; set; }
            public List<SensoryProfileAnswerDTO> ResultPositiveAttribute { get; set; }
            public List<SensoryProfileAnswerDTO> ResultDefect { get; set; }
            public List<QualityRecommendations> ResultRecommendation { get; set; }
        }
    }
}