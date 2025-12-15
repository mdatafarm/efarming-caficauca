using EFarming.Manager.Contract;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Integration.Controllers
{
    [RoutePrefix("monitor")]
    [AllowAnonymous]
    public class MonitorController : ApiController
    {
        private IMonitorManager _manager;

        public MonitorController(IMonitorManager manager)
        {
            _manager = manager;
        }

        [Route("check")]
        [HttpGet]
        public HttpResponseMessage Check()
        {
            if (_manager.IsDatabaseUp())
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.ServiceUnavailable);
        }
    }
}
