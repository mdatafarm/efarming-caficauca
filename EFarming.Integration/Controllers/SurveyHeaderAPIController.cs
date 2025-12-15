using EFarming.Core.TasqModule;
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
    public class SurveyHeaderAPIController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        [HttpPost]
        [Route("SurveyHeaderAPI")]
        public HttpResponseMessage Post(TasqSurveyHeaderAPI surveyHeader)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TASQAssessment TASQAssessment = db.TASQAssessment.Find(surveyHeader.ID);

                    TASQAssessment Assessment = new TASQAssessment();

                    if (TASQAssessment == null)
                    {
                        Assessment.Id = surveyHeader.ID;
                        Assessment.Date = DateTime.Now;
                        Assessment.Description = surveyHeader.Observations;
                        Assessment.FarmId = new Guid("{" + surveyHeader.Farm + "}");
                        Assessment.AssessmentTemplateId = surveyHeader.AssessmentTemplateId;
                        Assessment.UserId = new Guid("{" + surveyHeader.UserId + "}");
                        Assessment.SyncOperation = surveyHeader.SyncOperation;
                        db.TASQAssessment.Add(Assessment);
                        db.SaveChanges();
                    }

                    //Guid IDSurveyHeader = Assessment.Id;
                    Guid IDSurveyHeader = surveyHeader.ID;
                    //return Request.CreateResponse(HttpStatusCode.Created, IDSurveyHeader);
                    return Request.CreateResponse(HttpStatusCode.NoContent, "Invalid Model");
                }
                else { return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Model"); }
            }
            catch (Exception ex) { return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message); }
        }
    }
}