using EFarming.Oracle.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Oracle.Controllers
{
    [RoutePrefix("api/familymembers")]
    public class FamilyMemberController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            string SqlQuery = System.IO.File.ReadAllText(@"C:/Utils/Coocentral/Content/queries/familyMember.txt");

            MapperUtils Mapper = new MapperUtils();

            var result = Mapper.query(SqlQuery, "Family", null);
            if (result == null)
            {
                result = "doesn't exist data";
            }
            return Ok(result);
        }
    }
}
