using EFarming.Oracle.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Oracle.Controllers
{
    [RoutePrefix("api/fertilizers")]
    public class FertilizersController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get(string DateClause)
        {

            string SqlQuery = System.IO.File.ReadAllText(@"C:/Utils/Coocentral/Content/queries/fertilizers.txt") + " " + DateClause;

            MapperUtils Mapper = new MapperUtils();

            var result = Mapper.query(SqlQuery, "Fertilizers", null);
            if(result == null)
            {
                result = "doesn't exist data";
            }
            return Ok(result);
        }
    }
}
