using EFarming.Oracle.Mapper;
using EFarming.Oracle.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Oracle.Controllers
{
    [RoutePrefix("api/invoices")]
    public class InvoicesController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get(string DateClause)
        {
            string SqlQuery = System.IO.File.ReadAllText(@"C:/Utils/Coocentral/Content/queries/invoices.txt") + " " + DateClause;

            MapperUtils Mapper = new MapperUtils();

            var result = Mapper.query(SqlQuery, "Invoices", null);
            if (result == null)
            {
                result = "doesn't exist data";
            }
            return Ok(result);
        }
    }
}
