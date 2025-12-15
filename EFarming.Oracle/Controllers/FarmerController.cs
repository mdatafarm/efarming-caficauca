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
    [RoutePrefix("api/farmers")]
    public class FarmerController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            string SqlQuery = "select     terv_codigo" +
                                ",terc_primer_apellido || ' ' || terc_segundo_apellido LastName " +
                                ",terc_primer_nombre || ' ' || terc_segundo_nombre FarmerName " +
                                ",terf_nacimiento " +
                                ",terc_educacion " +
                                ",terc_estado_civil " +
                                "from bter_tercero " +
                                "where terc_publico = 'S' ";

            MapperUtils Mapper = new MapperUtils();

            var result = Mapper.query(SqlQuery, "Farmer", null);
            return Ok(result);
        }
    }
}
