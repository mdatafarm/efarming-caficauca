using EFarming.DAL;
using EFarming.DTO.TasqModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Integration.Controllers
{
    public class SurveyAPIController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        [HttpGet]
        [Route("listSurvey")]
        public HttpResponseMessage Get()
        {
            var listSurvey = db.AssessmentTemplates.Where(t => t.Type != "EFarming.Core.QualityModule.SensoryProfileAggregate.SensoryProfileAssessment" && t.Id != new Guid("0FA00E22-6C37-484C-8606-09CC1298C12A") && t.Id != new Guid("CF2785AD-8654-40E9-B22D-E6159E584B48") && t.Id != new Guid("0626E26C-3B70-42AA-975F-E3D2E1DEAA1B") && t.DeletedAt == null).Select(s => new TasqAssessmentAPI()
            {
                idSurvey = s.Id,
                Title = s.Name,
                Description = s.Type,
                CreatedOn = s.CreatedOn,
                ExpiredOn = s.ExpiredOn,
                CreatedBy = s.CreatedBy,
                Public = s.Public,
                TypeSurvey = "0"
            }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, listSurvey.OrderBy(x=> x.Title));
        }

        [HttpGet]
        [Route("listModules")]
        public object Get2()
        {
            var modules = db.SubModule.Select(s => new TasqSubModuleApi() {
                moduleID = s.ModuleId,
                moduleName = s.Module.Name,
                submoduleID = s.Id,
                submoduleName = s.Name,
                AssessmentId = s.Module.AssessmentTemplateId,
            });

            return Request.CreateResponse(HttpStatusCode.OK, modules.OrderBy(x => x.submoduleName));
        }


        [HttpGet]
        [Route("listModules/{id}")]
        public object Get3(Guid id)
        {
            var modules = db.SubModule.Select(s => new TasqSubModuleApi()
            {
                moduleID = s.ModuleId,
                moduleName = s.Module.Name,
                submoduleID = s.Id,
                submoduleName = s.Name,
                AssessmentId = s.Module.AssessmentTemplateId,
            });

            return Request.CreateResponse(HttpStatusCode.OK, modules.Where(x=>x.AssessmentId == id).OrderBy(x => x.submoduleName));
        }

        [HttpGet]
        [Route("Survey/{id}")]
        public object Get(Guid id)
        {
            var QuestionList = db.TASQCriteria.Where(s => s.SubModule.Module.AssessmentTemplateId == id).Select(q => new TasqQuestionAPI()
            {
                moduleID = q.SubModule.Module.Id,
                moduleName = q.SubModule.Module.Name,
                categoryID = q.SubModuleId,
                categoryName = q.SubModule.Name,
                orden = q.CriteriaOrder,
                questionID = q.Id,
                text = q.Description,
                questionType = q.CriteriaType.QuestionType,
                options = q.Options,
                flag = q.FlagIndicator.Name,
                txtInfo = q.Description,
                numText = q.Description,
                numQuestion = q.CriteriaOrder,
                required = "",
                answer = ""
            });

            var group = QuestionList.GroupBy(c => c.categoryName).Select(q => new TasqGroupedQuestionsAPI()
            {
                categoryName = q.Key,
                NumberQuestions = q.Count(),
                questions = q.ToList().OrderBy(o => o.orden)
            });

            return group;
        }

        [HttpGet]
        [Route("surveybyuser")]
        public object SurveyByUser(string UserId)
        {
            Guid UserIdGuid = new Guid("{" + UserId + "}");
            var result = db.TASQAssessment.Where(u => u.UserId == UserIdGuid)
                            .Select(a => new TasqSurveyHeaderAPI()
                            {
                                ID = a.Id,
                                Farm = a.Farm.Code.ToString(),
                                LabelSurvey = a.Description,
                                UserId = a.UserId.ToString(),
                                DateSurvey = a.Date.ToString(),
                                Observations = "",
                                AssessmentTemplateId = a.AssessmentTemplateId,
                                SyncOperation = a.SyncOperation
                            }).ToList();

            return result;
        }

        [HttpGet]
        [Route("surveybyfarm")]
        public object SurveyByFarm(string FarmId)
        {
            Guid FarmIdGuid = new Guid("{" + FarmId + "}");
            var result = db.TASQAssessment.Where(f => f.FarmId == FarmIdGuid)
                            .Select(a => new TasqSurveyHeaderAPI()
                            {
                                ID = a.Id,
                                Farm = a.Farm.Code.ToString(),
                                LabelSurvey = a.Description,
                                UserId = a.UserId.ToString(),
                                DateSurvey = a.Date.ToString(),
                                Observations = "",
                                AssessmentTemplateId = a.AssessmentTemplateId,
                                SyncOperation = a.SyncOperation
                            }).ToList();

            return result;
        }

    }
}
