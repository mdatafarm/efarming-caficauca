using EFarming.DTO.ProjectModule;
using EFarming.Manager.Contract;
using EFarming.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class AdminProjectsController : BaseController
    {
        private IProjectManager _manager;

        public AdminProjectsController(IProjectManager manager){
	  _manager = manager;
        }

        // GET: /AdminProjects/
        public ActionResult Index()
        {
	  return View(_manager.GetAll());
        }

        // GET: /AdminProjects/Create
        public ActionResult Create()
        {
	  return View(new ProjectDTO());
        }

        // POST: /Indicators/Create
        [HttpPost]
        public ActionResult Create(ProjectDTO projectDTO)
        {
	  try
	  {
	      _manager.Create(projectDTO);
	      return RedirectToAction("Index");
	  }
	  catch
	  {
	      return View(projectDTO);
	  }
        }

        // GET: /Indicators/Edit/5
        public ActionResult Edit(Guid id)
        {
	  var projectDTO = _manager.Get(id);
	  return View(projectDTO);
        }

        // POST: /Indicators/Edit
        [HttpPost]
        public ActionResult Edit(Guid id, ProjectDTO projectDTO)
        {
	  try{
	      _manager.Edit(projectDTO);

	      return RedirectToAction("Index");
	  }
	  catch{
	      return View(projectDTO);
	  }
        }
    }
}