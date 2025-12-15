using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.DAL;
using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Web.Models;
using iTextSharp.text;
using MvcRazorToPdf;
using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for the Reports
    /// </summary>   
    [AllowAnonymous]
    [CustomAuthorize(Roles = "User,Technician,Taster,Quality,Sustainability,Reader")]
    public class ReportsController : BaseController
    {
        /// <summary>
        /// The database
        /// </summary>
        /// <summary>
        /// Farm Manager
        /// </summary>
        private IFarmManager _farmManager;
        /// <summary>
        /// The _sensory profile manager
        /// </summary>
        ISensoryProfileManager _sensoryProfileManager;
        /// <summary>
        /// The _quality attribute manager
        /// </summary>
        IQualityAttributeManager _qualityAttributeManager;

        /// <summary>
        /// Instance the managers
        /// </summary>
        /// <param name="farmManager"></param>
        /// <param name="sensoryProfileManager"></param>
        /// <param name="qualityAtributeManager"></param>
        public ReportsController(IFarmManager farmManager, ISensoryProfileManager sensoryProfileManager, IQualityAttributeManager qualityAtributeManager)
        {
            _farmManager = farmManager;
            _sensoryProfileManager = sensoryProfileManager;
            _qualityAttributeManager = qualityAtributeManager;
        }

        private UnitOfWork db = new UnitOfWork();

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>The View</returns>
        [Authorize(Roles = "User,Quality,Manager,Taster,Reader")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Surveyses the by user.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns>The View</returns>
        public ActionResult SurveysByUser(Guid guid)
        {
            ViewBag.UserId = guid;
            return View();
        }

        [Authorize(Roles = "User,Technician,Taster,Quality,Sustainability,Reader")]
        public ActionResult PTByFarm()
        {
            //ViewBag.farmGuid = _farmGuid;
            return View();
        }

        /// <summary>
        /// Get the information of the farm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "User,Technician,Taster,Quality,Sustainability,Reader")]
        public ActionResult PTByFarmID(Guid id)
        {
            var farm = _farmManager.Details(id,
                "SupplyChain",
                "SupplyChain.Supplier",
                "SupplyChain.Supplier.Country",
                "Village",
                "Village.Municipality",
                "Village.Municipality.Department",
                "Productivity",
                "Productivity.Plantations",
                "FamilyUnitMembers");
            farm = _farmManager.CalculateDensity(farm);
            farm = _farmManager.CalculateFertilizer(farm);
            farm = _farmManager.CalculateProductivity(farm);
            //farm = _farmManager.CalculateAge(farm);

            ICollection<DTO.QualityModule.SensoryProfileAssessmentDTO> results = _sensoryProfileManager.FilterByFarmAndTemplate(farm.Id, AssessmentTemplate.CuppingId);
            ViewBag.PT = results;
            ViewBag.Cupping = _qualityAttributeManager.Get(AssessmentTemplate.CuppingId);

            ICollection<DTO.QualityModule.SensoryProfileAssessmentDTO> results2 = _sensoryProfileManager.FilterByFarmAndTemplate(farm.Id, AssessmentTemplate.PuntoZeroId);
            ViewBag.PC = results2;
            ViewBag.PuntoZero = _qualityAttributeManager.Get(AssessmentTemplate.PuntoZeroId);

            return View(farm);
        }

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
                        ResultSensoryProfile.PuntajeTotal = ResultSensoryProfile.PuntajeTotal + Convert.ToDecimal(item.Answer.Replace(".",","));
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


        public void EmailResultByTechnician()
        {
            List<string> ListBodyMessage = new List<string>();
            string bodyMessage = "";
            DateTime today = DateTime.Now.Date;
            var technicianList = db.Users.OrderBy(o => o.FirstName).ToList();

            foreach (var Technician in technicianList)
            {
                foreach (var rol in Technician.Roles)
                {
                    if (rol.Id == new Guid("D3D50A4D-B63A-406F-BA90-0A56F06AFAD6") && Technician.Farms.Count() > 0)
                    {
                        bodyMessage = bodyMessage
                       + "Buenos Días,"
                       + "<br /><br />"
                       + "Envío listado de fincas con el enlace correspondiente en donde pueden visualizar los resultados de las pruebas de taza almacenadas en la plataforma Cafexport E-farming."
                       + "<br /><br />"
                       + "Un saludo. ";
                        bodyMessage = bodyMessage + "<br /><br />";
                        bodyMessage = bodyMessage
                       + "<table style='text-align:center; width:100%; border:1px solid black;border-collapse:collapse;'>"
                       + "<thead border:1px solid black>"
                       + "<tr border:1px solid black>"
                       + "<th colspan='5' style ='color:black;text-align:center'>LISTADO DE FINCAS CON RESULTADOS ANALISIS SENSORIAL</th>"
                       + "</tr>"
                       + "<tr border:1px solid black'>"
                       + "<th style='color: black;text-align:center; border:1px solid black'>NOMBRE DEL AGRÓNOMO</th>"
                       + "<th style='color: black;text-align:center; border:1px solid black'>CÓDIGO AAA</th>"
                       + "<th style='color: black;text-align:center; border:1px solid black'>NOMBRE DE LA FINCA</th>"
                       + "<th style='color: black;text-align:center; border:1px solid black'>FECHA DE ANÁLISIS</th>"
                       + "<th style='color: black;text-align:center; border:1px solid black'>LINK PARA VER RESULTADOS</th>"
                       + "</tr>";
                        bodyMessage = bodyMessage + "</thead>";
                        foreach (var Farm in Technician.Farms.OrderBy(o => o.Code))
                        {
                            var ListSensoryProfile = db.SensoryProfileAssessments.Where(s => s.FarmId == Farm.Id).OrderByDescending(o => o.Date).ToList();
                            if (ListSensoryProfile.Count() > 0)
                            {
                                bodyMessage = bodyMessage
                                + "<tr style='color: black;text-align:center; border:1px solid black'>"
                                + "<td rowspan='" + ListSensoryProfile.Count() + "'>" + Technician.FirstName + " " + Technician.LastName + "</td>"
                                + "<td rowspan='" + ListSensoryProfile.Count() + "'>" + Farm.Code + "</td>"
                                + "<td rowspan='" + ListSensoryProfile.Count() + "'>" + Farm.Name + "</td>";
                                int count = 1;
                                foreach (var SensoryProfile in ListSensoryProfile)
                                {
                                    var UrlResult = "http://caficauca.efarming.co/Reports/PDF/" + SensoryProfile.Id;
                                    //var UrlResult = "http://192.168.1.89:620/Reports/PDF/" + SensoryProfile.Id;
                                    if (count == 1)
                                    {
                                        bodyMessage = bodyMessage
                                        + "<th style ='border:1px solid black;border-right-color:#FFFFFF'>" + string.Format("{0:dd/MM/yyyy}", SensoryProfile.Date) + "</th>"
                                        + "<th style ='border:1px solid black;border-left-color:#FFFFFF'>" + "<a href = '" + UrlResult + "'>" + "Ver resultado" + "</ a >" + " </th>";
                                        count++;
                                    }
                                    else
                                    {
                                        bodyMessage = bodyMessage
                                        + "<tr>"
                                        + "<th style ='border:1px solid black;border-right-color:#FFFFFF'>" + string.Format("{0:dd/MM/yyyy}", SensoryProfile.Date) + "</th>"
                                        + "<th style ='border:1px solid black;border-left-color:#FFFFFF'>" + "<a href = '" + UrlResult + "'>" + "Ver resultado" + "</ a >" + " </th>";
                                    }
                                    bodyMessage = bodyMessage
                                    + "</tr>";
                                }
                            }
                        }
                        bodyMessage = bodyMessage + "</table>";
                        bodyMessage += string.Join("<br /><br />", ListBodyMessage);

                        string emailAdrress = ConfigurationManager.AppSettings["MailAccount"].ToString();
                        string emailPassword = ConfigurationManager.AppSettings["MailPassword"].ToString();

                        MailMessage mail = new MailMessage(emailAdrress, "alejandra@datafarm.com.co");
                        SmtpClient client = new SmtpClient();
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(emailAdrress, emailPassword);
                        client.Port = 587;
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Host = "smtp.gmail.com";
                        mail.Subject = "LISTADO DE FINCAS CON RESULTADOS SOBRE ANALISIS SENSORIAL.";
                        //mail.Subject = "LISTADO DE FINCAS CON RESULTADOS ANALISIS SENSORIAL, " + today.ToString("dd MMMM yyyy").ToUpper();
                        mail.IsBodyHtml = true;
                        mail.Body = bodyMessage;
                        mail.To.Add("alejandra@datafarm.com.co");
                        var EMAIL = Technician.Email;
                        //mail.To.Add(Technician.Email);
                        client.Send(mail);
                    }
                }
            }
        }

        //[AllowAnonymous]
        //public void DownloadPDF()
        //{
        //    // return new Rotativa.UrlAsPdf("http://192.168.1.89:92/Reports/PDF/e786b4b3-6078-4391-4150-d7408f9f6a23") { FileName = "UrlTest.pdf" };
        //    var technicianList = db.Users.ToList();
        //    foreach (var Technician in technicianList)
        //    {
        //        foreach (var rol in Technician.Roles)
        //        {
        //            if (rol.Id == new Guid("D3D50A4D-B63A-406F-BA90-0A56F06AFAD6") && Technician.Farms.Count() > 0)
        //            {
        //                foreach (var Farm in Technician.Farms)
        //                {
        //                    var ListSensoryProfile = db.SensoryProfileAssessments.Where(s => s.FarmId == Farm.Id).ToList();
        //                    if (ListSensoryProfile.Count() > 0)
        //                    {
        //                        foreach (var SensoryProfile in ListSensoryProfile)
        //                        {
        //                            string path = Server.MapPath(string.Format("~/Content/QualityRecommendations/{0}/", Technician.FirstName + " " + Technician.LastName));
        //                            Directory.CreateDirectory(path);
        //                            string pathFarm = path + Farm.Code + "-" + Farm.Name.Replace("?", " ");
        //                            Directory.CreateDirectory(pathFarm);
        //                            var root = pathFarm;
        //                            //var pdfname = SensoryProfile.Date.Date.ToString("dd-MM-yyyy") + "-" + SensoryProfile.Id + ".pdf";
        //                            var pdfname = SensoryProfile.Id + ".pdf";
        //                            var pathPDF = Path.Combine(root, pdfname);
        //                            pathPDF = Path.GetFullPath(pathPDF);
        //                            using (WebClient client = new WebClient())
        //                            {
        //                                client.DownloadFile("http://192.168.1.89:92/Reports/PDF/" + SensoryProfile.Id, pathPDF);
        //                                //client.DownloadFile("http://www.irs.gov/pub/irs-pdf/fw4.pdf", pathPDF);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        DateTime laFecha() { return DateTime.Now; }

        static async void Example()
        {
            // This method runs asynchronously.
            int t = await Task.Run(() => Allocate());
            Console.WriteLine("Compute: " + t);
        }

        static int Allocate()
        {
            // Compute total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    string value = i.ToString();
                    if (value == null)
                    {
                        return 0;
                    }
                    size += value.Length;
                }
            }
            return size;
        }

        async Task<int> AccessTheWebAsync()
        {
            // You need to add a reference to System.Net.Http to declare client.
            HttpClient client = new HttpClient();

            // GetStringAsync returns a Task<string>. That means that when you await the
            // task you'll get a string (urlContents).
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");

            // You can do work here that doesn't rely on the string from GetStringAsync.
            //DoIndependentWork();

            // The await operator suspends AccessTheWebAsync.
            //  - AccessTheWebAsync can't continue until getStringTask is complete.
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.
            //  - Control resumes here when getStringTask is complete. 
            //  - The await operator then retrieves the string result from getStringTask.
            string urlContents = await getStringTask;

            // The return statement specifies an integer result.
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.
            return urlContents.Length;
        }

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