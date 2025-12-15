using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Integration.Controllers
{
    [RoutePrefix("checklists")]
    //[Authorize]
    public class ChecklistsController : ApiController
    {
        private IChecklistManager _manager;
        public ChecklistsController(IChecklistManager manager)
        {

        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage Index()
        {
            var res = Request.CreateResponse(HttpStatusCode.OK, new ChecklistDTO());
            return res;
        }

        [HttpPost]
        [Route("sync")]
        public HttpResponseMessage Create(IEnumerable<ChecklistDTO> checklists)
        {
            foreach (var checklist in checklists)
            {
                _manager.Add(checklist);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("sync/{id}")]
        public HttpResponseMessage Update(Guid id, ChecklistDTO checklist)
        {
            if (_manager.Edit(checklist))
            {
                return Request.CreateResponse(HttpStatusCode.OK, checklist);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, checklist);
        }

        [HttpDelete]
        [Route("sync/{id}")]
        public HttpResponseMessage Delete(Guid id)
        {
            if (_manager.Delete(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
