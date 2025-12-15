using EFarming.Core.TasqModule;
using EFarming.DAL;
using EFarming.DTO.TasqModule;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Integration.Controllers
{
    public class SurveyResponseAPIController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        public class ResponseApp
        {
            public int SurveyId { get; set; }
            public string HeaderId { get; set; }
            public List<QuestionsJSON> QuestionsJSON { get; set; }
        }

        //public class QuestionsJSON
        //{
        //    public string categoryName { get; set; }
        //    public string NumberQuestions { get; set; }
        //    public int id { get; set; }
        //    public List<Q> questions { get; set; }
        //}

        public class QuestionsJSON
        {
            public string Id { get; set; }
            public int CriteriaId { get; set; }
            public string Value { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public DateTime DeletedAt { get; set; }
            public string TasqAssessmentId { get; set; }
        }

        public class Q
        {
            public string txtInfo { get; set; }
            public string answer { get; set; }
            public bool required { get; set; }
            public int categoryID { get; set; }
            public string questionType { get; set; }
            public string text { get; set; }
            public int orden { get; set; }
            public int numQuestion { get; set; }
            public int questionID { get; set; }
            public string numText { get; set; }
            public string options { get; set; }
            public string flag { get; set; }
            public string categoryName { get; set; }
        }

        [HttpPost]
        [Route("SurveyResponseAPI")]
        public HttpResponseMessage Post(ResponseApp _responseApp)
        {
            String Header = _responseApp.HeaderId.ToString();
            Header = Header.Replace("\"", "");
            try
            {
                //foreach (var CategoryQuestion in _responseApp.QuestionsJSON)
                //{
                //    var Answer = new TASQAssessmentAnswer();
                //    Answer.Id = new Guid("{" + CategoryQuestion.Id + "}");
                //    Answer.TASQAssessmentId = new Guid("{" + Header + "}");
                //    Answer.CriteriaId = CategoryQuestion.CriteriaId;
                //    Answer.Value = CategoryQuestion.Value;
                //    Answer.CreatedAt = DateTime.Now;
                //    db.TASQAssessmentAnswer.Add(Answer);
                //}
                //db.SaveChanges();
                //return Request.CreateResponse(HttpStatusCode.Created);
                Guid TASQAssessmentId = new Guid("{" + Header + "}");
                var TASQAssessmentAnswer = db.TASQAssessmentAnswer.Where(t => t.TASQAssessmentId == TASQAssessmentId).ToList();

                if (TASQAssessmentAnswer.Count() == 0)
                {
                    foreach (var CategoryQuestion in _responseApp.QuestionsJSON)
                    {
                        var Answer = new TASQAssessmentAnswer();
                        Answer.Id = new Guid("{" + CategoryQuestion.Id + "}");
                        Answer.TASQAssessmentId = new Guid("{" + Header + "}");
                        Answer.CriteriaId = CategoryQuestion.CriteriaId;
                        Answer.Value = CategoryQuestion.Value;
                        Answer.CreatedAt = DateTime.Now;
                        db.TASQAssessmentAnswer.Add(Answer);
                        db.SaveChanges();
                    }
                    //db.SaveChanges();
                }
                else
                {
                    foreach (var CategoryQuestion in _responseApp.QuestionsJSON)
                    {
                        Guid TASQAssessmentAnswerId = new Guid("{" + CategoryQuestion.Id + "}");
                        TASQAssessmentAnswer AnswerUpdate = db.TASQAssessmentAnswer.Find(TASQAssessmentAnswerId);

                        AnswerUpdate.TASQAssessmentId = new Guid("{" + Header + "}");
                        AnswerUpdate.CriteriaId = CategoryQuestion.CriteriaId;
                        AnswerUpdate.Value = CategoryQuestion.Value;
                        AnswerUpdate.UpdatedAt = DateTime.Now;

                        db.Entry(AnswerUpdate).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                return Request.CreateResponse(HttpStatusCode.Created);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "MESSAGE: " + ex.Message + " ---- " +
                                                                                       "InnerException" + ex.InnerException + " ---- " +
                                                                                       "HResult" + ex.HResult + " ---- " +
                                                                                       "Source" + ex.Source + " ---- " +
                                                                                       "StackTrace" + ex.StackTrace + " ---- " +
                                                                                       "TargetSite" + ex.TargetSite);
            }
        }
    }
}