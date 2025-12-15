using EFarming.Common;
using EFarming.Manager.Contract.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EFarming.Integration.Controllers
{
    [RoutePrefix("test")]
    public class TestsController : ApiController
    {
        private IDepartmentManager _departmentManager;
        private IFarmStatusManager _statusManager;
        private ICooperativeManager _cooperativeManager;
        private IOwnershipTypeManager _ownershipTypeManager;

        public TestsController(
            IDepartmentManager departmentManager,
            IFarmStatusManager statusManager,
            ICooperativeManager cooperativeManager,
            IOwnershipTypeManager ownershipTypeManager)
        {
            _departmentManager = departmentManager;
            _statusManager = statusManager;
            _cooperativeManager = cooperativeManager;
            _ownershipTypeManager = ownershipTypeManager;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Entró";
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("exception")]
        public string Exception()
        {
            throw new EFarmingException("Exception lanzada desde la prueba");
        }
    }
}
