using EFarming.Common.Resources;
using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.QualityModule.MicrolotAggregate;
using EFarming.DAL;
using EFarming.DTO.AdminModule;
using EFarming.DTO.FarmModule;
using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace EFarming.Integration.Controllers
{
    [RoutePrefix("quality")]
    public class QualityController : ApiController
    {
        private IAssessmentTemplateManager _manager;
        private IMicrolotManager _microlotManager;
        private ISensoryProfileManager _sensoryProfileManager;
        private IFarmManager _farmManager;
        public QualityController(IAssessmentTemplateManager manager,
            IMicrolotManager microlotManager,
            ISensoryProfileManager sensoryProfileManager,
            IFarmManager farmManager)
        {
            _manager = manager;
            _microlotManager = microlotManager;
            _sensoryProfileManager = sensoryProfileManager;
            _farmManager = farmManager;
        }

        UnitOfWork db = new UnitOfWork();

        [HttpGet]
        [Route("")]
        public ICollection<AssessmentTemplateDTO> Index()
        {
            var ans = _manager
                .GetAll(AssessmentTemplateSpecification.Quality()
                        , at => at.Name,
                        "QualityAttributes",
                        "QualityAttributes.RangeAttribute",
                        "QualityAttributes.OptionAttributes");
            foreach (var a in ans)
            {
                var ord = a.QualityAttributes.OrderBy(qa => qa.Position);
                a.QualityAttributes = ord.ToList();
            }

            return ans;
        }

        [HttpPost]
        [Route("microlots")]
        public HttpResponseMessage CreateMicrolot([FromBody]MicrolotDTO microlot)
        {
            var m = _microlotManager.ByCode(microlot.Code);
            if (microlot == null)
            {
                _microlotManager.Add(microlot);
                m = _microlotManager.ByCode(microlot.Code);
            }
            return Request.CreateResponse(HttpStatusCode.OK, m);
        }


        [HttpGet]
        [Route("microlots/{code}")]
        public HttpResponseMessage GetMicrolot(string code)
        {
            var microlot = _microlotManager.ByCode(code);
            if (microlot == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Microlote no encontrado");
            return Request.CreateResponse(HttpStatusCode.OK, microlot);
        }

        [HttpPost]
        [Route("sync")]
        public HttpResponseMessage SaveQuality([FromBody]IEnumerable<SensoryProfileAssessmentDTO> assessments)
        {
            foreach (var assessment in assessments)
            {
                var ans = assessment.SensoryProfileAnswers;
                assessment.SensoryProfileAnswers = new List<SensoryProfileAnswerDTO>();

                var existing_assessment = _sensoryProfileManager.Get(assessment.Id);
                if (existing_assessment == null)
                    _sensoryProfileManager.Add(assessment);

                assessment.SensoryProfileAnswers.Clear();

                foreach (var a in ans)
                {
                    var answers = a.Answer.Split(',');
                    foreach (var answer in answers)
                    {
                        if (string.IsNullOrEmpty(answer)) continue;
                        assessment.SensoryProfileAnswers.Add(new SensoryProfileAnswerDTO
                        {
                            Answer = answer,
                            QualityAttributeId = a.QualityAttributeId,
                            SensoryProfileAssessmentId = assessment.Id
                        });
                    }
                }

                _sensoryProfileManager.Edit(assessment);

                if (assessment.AssessmentTemplateId == new Guid("7B01B167-B114-4D6A-8174-8E45571A9216"))
                {
                    if (assessment.FarmId != null)
                    {
                        var farm = _farmManager.Details(assessment.FarmId ?? default(Guid),
                        "AssociatedPeople");
                        List<string> ListBodyMessage = new List<string>();
                        string bodyMessage = "";
                        DateTime today = DateTime.Now.Date;
                        bodyMessage = bodyMessage
                        + "Cordial Saludo,"
                        + "<br /><br />"
                        + "Envío link: http://caficauca.efarming.co/Reports/PDF/" + assessment.Id + ", donde encontraran las respectivas recomendaciones sobre los resultados de la prueba de taza que se le realizo a la muestra relacionada con la finca "
                        //+ "Envío link: http://192.168.1.89:620/Reports/PDF/" + assessment.Id + ", donde encontraran las respectivas recomendaciones sobre los resultados de la prueba de taza que se le realizo a la finca "
                        + farm.Name + " - " + farm.Code + ".";
                        //+ "<br /><br />"
                        //+ "Un saludo. ";
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
                        mail.Subject = "RESULTADOS PRUEBA DE TAZA, " + today.ToString("dd MMMM yyyy").ToUpper();
                        mail.IsBodyHtml = true;
                        mail.Body = bodyMessage;
                        // System.Net.Mail.Attachment attachment;
                        //attachment = new System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath("~/QualityRecommendations/" + assessment.FarmId + "/" + assessment.Date.Date.ToString("dd-MM-yyyy") + "-" + assessment.Id + ".pdf"));
                        //mail.Attachments.Add(attachment);
                        var TechnicianEmail = farm.AssociatedPeople.Select(s => s.Email).FirstOrDefault();
                        if (TechnicianEmail != null)
                        {
                           mail.To.Add(TechnicianEmail);
                        }
                        mail.To.Add("daniel@datafarm.com.co");
                        client.Send(mail);
                    }
                }

            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpGet]
        [Route("answer/{code}")]
        public HttpResponseMessage GetAnswer(string code)
        {
            var farm = db.Farms.Where(d => d.Code == code).First();

            var farmDTO = _farmManager.Details(farm.Id,
                "FamilyUnitMembers",
                "Village",
                "Village.Municipality",
                "Village.Municipality.Department");

            FamilyUnitMemberDTO Owner = new FamilyUnitMemberDTO();
            if (farmDTO.FamilyUnitMembers.Where(m => m.IsOwner == true).Count() > 0)
                Owner = farmDTO.FamilyUnitMembers.Where(m => m.IsOwner == true).First();

            var clasificationAnswers = db.SensoryProfileAnswers
                .Where(qa => qa.QualityAttributeId == new Guid("{D5FE6059-256D-46DD-A03A-533876191B96}"))
                .Where(f => f.SensoryProfileAssessment.FarmId == farm.Id)
                .GroupBy(ans => ans.Answer)
                .Select(ga => new AnswerObjectClasification
                {
                    name = ga.Key,
                    y = ga.Count(),
                    color = (
                                ga.Key == "CAFE EXCEPCIONAL" ? "#92CDDC" :
                                ga.Key == "CAFE PERFIL NN" ? "#C4D79B" :
                                ga.Key == "BORDERLINE" ? "#98A0C7" :
                                ga.Key == "CAFE SIN PERFIL" ? "#FABF8F" :
                                ga.Key == "CAFE CON DEFECTO" ? "#DA9694" : "#81B5C2"
                            )

                }).ToList();

            var clasificationAnswersDetail = db.SensoryProfileAssessments
                .Where(f => f.FarmId == farm.Id)
                .Where(at => at.AssessmentTemplateId == new Guid("{7B01B167-B114-4D6A-8174-8E45571A9216}"))
                .Select(ga => new AnswerObjectDetail
                {
                    Attribute = ga.SensoryProfileAnswers.Where(a => a.QualityAttributeId == new Guid("{D5FE6059-256D-46DD-A03A-533876191B96}")).Select(a => a.Answer).FirstOrDefault(),
                    Taster = ga.User.FirstName + " " + ga.User.LastName,
                    kg = ga.SensoryProfileAnswers.Where(a => a.QualityAttributeId == new Guid("{E75FF320-9F01-4197-B139-D5F147C401CA}")).Select(a => a.Answer).FirstOrDefault(),
                    Description = ga.SensoryProfileAnswers.Where(a => a.QualityAttributeId == new Guid("{90A8ECC4-14A5-4CA8-8B02-354A7F85341A}")).Select(a => a.Answer).FirstOrDefault(),
                    Observations = ga.SensoryProfileAnswers.Where(a => a.QualityAttributeId == new Guid("{E718DE5F-F366-4D3E-864A-BFCC57B9A555}")).Select(a => a.Answer).FirstOrDefault(),
                    date = ga.Date,
                })
                .OrderByDescending(d => d.date)
                .ToList();

            string html = "<table><tr><th>Clasificación</th><th>Catador</th><th>kg</th><th>Defecto</th><th>Fecha</th></tr>";
            foreach (var item in clasificationAnswersDetail)
            {
                html = html + "<tr>"
                    + "<td>" + item.Attribute + "</td>"
                    + "<td>" + item.Taster + "</td>"
                    + "<td>" + item.kg + "</td>"
                    + "<td>" + item.Description + "</td>"
                    + "<td>" + string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(item.date)) + "</td>";
            }

            html = html + "</table>";

            var defectsAnswers = db.SensoryProfileAnswers
                .Where(qa => qa.QualityAttributeId == new Guid("{90A8ECC4-14A5-4CA8-8B02-354A7F85341A}"))
                .Where(f => f.SensoryProfileAssessment.FarmId == farm.Id)
                .GroupBy(ans => ans.Answer)
                .Select(ga => new AnswerObject
                {
                    name = ga.Key,
                    y = ga.Count(),
                }).ToList();

            var positiveAnswers = db.SensoryProfileAnswers
                .Where(qa => qa.QualityAttributeId == new Guid("{945AD1F2-F3A7-4E89-8AB1-9472A85CDFF6}"))
                .Where(f => f.SensoryProfileAssessment.FarmId == farm.Id)
                .GroupBy(ans => ans.Answer)
                .Select(ga => new AnswerObject
                {
                    name = ga.Key,
                    y = ga.Count(),
                }).ToList();

            AnswerAndFarmObject Result = new AnswerAndFarmObject
            {
                Clasification = clasificationAnswers,
                Defects = defectsAnswers,
                Positives = positiveAnswers,
                DetailClasification = html,
                Farm = farmDTO,
                Owner = Owner,
            };
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }

        public class AnswerObjectClasification
        {
            public string name { get; set; }
            public double y { get; set; }
            public DateTime date { get; set; }
            public string color { get; set; }
        }

        public class AnswerObject
        {
            public string name { get; set; }
            public double y { get; set; }
            public DateTime date { get; set; }
        }

        public class AnswerObjectDetail
        {
            public string Attribute { get; set; }
            public string Taster { get; set; }
            public string kg { get; set; }
            public string Description { get; set; }
            public string Observations { get; set; }
            public DateTime date { get; set; }
        }

        public class AnswerAndFarmObject
        {
            public List<AnswerObjectClasification> Clasification { get; set; }
            public List<AnswerObject> Defects { get; set; }
            public List<AnswerObject> Positives { get; set; }
            public string DetailClasification { get; set; }
            public FarmDTO Farm { get; set; }
            public FamilyUnitMemberDTO Owner { get; set; }
        }
    }
}