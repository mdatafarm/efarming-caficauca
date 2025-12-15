using EFarming.DTO.ProjectModule;
using EFarming.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProjectsController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IProjectManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public ProjectsController(IProjectManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ProjectDTO> Index()
        {
            var Projects = _manager.GetAll();
            //foreach (var project in Projects)
            //{
            //    project.Farms = null;
            //}
            return Projects;
        }
    }
}
