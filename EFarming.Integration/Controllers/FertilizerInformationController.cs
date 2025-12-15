using EFarming.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Integration.Controllers
{
    public class FertilizerInformationController : ApiController
    {
        UnitOfWork db = new UnitOfWork();

        [HttpGet]
        [Route("fertilizers")]
        public HttpResponseMessage GetFertilizers()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            var listFertilizers = db.FertilizerInformation.ToList();
            var listAverageExtraction = db.AverageExtraction.ToList();

            result.Add("Fertilizers", listFertilizers);
            result.Add("AverageExtraction", listAverageExtraction);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("averageextraction")]
        public HttpResponseMessage GetAverage()
        {
            var listAverageExtraction = db.AverageExtraction.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, listAverageExtraction);
        }
    }
}
