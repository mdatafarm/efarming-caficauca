using AutoMapper;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.DAL;
using EFarming.DTO.FarmModule;
using EFarming.DTO.ProjectModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// This controller is for the use of the Projects Managers of the company
    /// We have a lot of fuctions to manage the projetcs of the company    
    /// </summary>
    [CustomAuthorize(Roles = "Project,Technician,Sustainability,Reader")]
    public class ProjectsController : Controller
    {
        /// <summary>
        /// FarmManager
        /// </summary>
        private IFarmManager _manager;

        /// <summary>
        /// The _project manager
        /// </summary>
        private IProjectManager _projectManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        /// <param name="projectManager">The project manager.</param>
        public ProjectsController(IFarmManager manager, IProjectManager projectManager)
        {
            _manager = manager;
            _projectManager = projectManager;
        }

        UnitOfWork db = new UnitOfWork();

        /// <summary>
        /// Indexes the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>PartialView with farm</returns>
        public ActionResult Index(Guid farmId)
        {
            var farm = _manager.Details(farmId, "Projects");
            return PartialView("~/Views/Projects/Index.cshtml", farm);
        }

        /// <summary>
        /// List of created Projects
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectsList(string sortOrder, string currentProject, string searchProject, string currentFarm, string searchFarm, string searchIDP, string searchNameP, int? page)
        {
            var withFilter = (!string.IsNullOrEmpty(searchProject) || !string.IsNullOrEmpty(searchFarm) || !string.IsNullOrEmpty(searchIDP) || !string.IsNullOrEmpty(searchNameP));


            List<Farm> ListFarms = new List<Farm>();
            List<Project> ListProject = new List<Project>();
            var User = HttpContext.User as CustomPrincipal;
            var prj = db.Projects.OrderBy(p => p.Name).ToList();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            List<Project> projectsList = new List<Project>();
            if (withFilter)
            {
                page = 1;

                if (!string.IsNullOrEmpty(searchProject))
                {
                    projectsList = db.Projects.Where(p => p.Id.ToString().Equals(searchProject)).ToList();
                }
                if (!string.IsNullOrEmpty(searchFarm))
                {
                    //Farm f = new Farm();
                    //f = db.Farms.Where(frm => frm.Id.ToString().Equals(searchFarm)).FirstOrDefault();
                    foreach (var p in prj)
                    {
                        foreach (var f in p.Farms.OrderBy(f => f.Name))
                        {
                            if (f.Code.Equals(searchFarm))
                            {
                                projectsList.Add(p);
                            }
                        }
                    }

                    ViewBag.type = 1;
                    ViewBag.dato = searchFarm;
                }
                if (!string.IsNullOrEmpty(searchIDP))
                {
                    //Farm f = new Farm();
                    var f = db.FamilyUnitMembers.Where(frm => frm.Identification.ToString().Equals(searchIDP) && frm.IsOwner == true).ToList();


                    if (f != null)
                    {

                        foreach (var item in f)
                        {
                            if (item.Farm != null)
                            {
                                ViewBag.dato = item.Farm.Code;
                                var projects = item.Farm.Projects.ToList();

                                foreach (var p in projects)
                                {
                                    projectsList.Add(p);
                                }
                            }
                        }

                    }

                    ViewBag.type = 1;

                    //foreach (var p in prj)
                    //{
                    //    foreach (var f in p.Farms)
                    //    {
                    //        if (f.FamilyUnitMembers.Where(x => x.IsOwner == true).Select(x=> x.Identification).FirstOrDefault() == searchIDP)
                    //        {
                    //            projectsList.Add(p);
                    //        }
                    //    }
                    //}
                }
            }
            else
            {
                searchProject = currentProject;
                searchFarm = currentFarm;
                projectsList = prj;
            }
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "description" ? "description_desc" : "description";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentProject = searchProject;
            ViewBag.CurrentFarm = searchFarm;

            SelectList lprj = new SelectList(prj.Distinct(), "Id", "Name");
            ViewBag.NameProjects = lprj;
            List<Farm> frmaux = new List<Farm>();
            foreach (var p in prj)
            {
                foreach (var f in p.Farms)
                {

                    frmaux.Add(f);
                }
            }
            // List<FarmDTO> lf = new List<FarmDTO>();
            // lf = Mapper.Map<List<FarmDTO>>(frmaux);

            SelectList lfrm = new SelectList(frmaux.Distinct().OrderBy(f => f.Name), "Id", "Name");
            ViewBag.Farms = lfrm;


            if (ListProjectsUser.Count() == 0)
            {
                return View(projectsList.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                foreach (var project in projectsList)
                {
                    foreach (var projectUser in ListProjectsUser)
                    {
                        if (project.Id == projectUser.Id)
                        {
                            ListProject.Add(project);
                        }
                    }
                }
            }
            return View(ListProject.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ViewFarmsByProject(Guid id, string Dato, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.Find(id);
            ViewBag.ProjectId = project.Id;
            List<Farm> lf = project.Farms.OrderBy(f => f.Name).Distinct().ToList();
            ViewBag.ProjectName = project.Name;

            if (Dato != null)
            {

                lf = lf.Where(x => x.Code.Equals(Dato)).ToList();
                ViewBag.cantidad = lf.Count();

            }
            else
            {

                ViewBag.currentId = id;
                int pageSize = 20;
                int pageNumber = (page ?? 1);

                ViewBag.cantidad = lf.Count();

                if (project == null)
                {
                    return HttpNotFound();
                }
            }

            //return PartialView(lf.ToPagedList(pageNumber, pageSize));
            return PartialView(lf);
        }

        public ActionResult DetailsProject(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        public ActionResult CreateProject()
        {
            var project = new Project();
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject([Bind(Include = "Name,Description")] Project project, string selectedFarmsString)
        {
            if (ModelState.IsValid)
            {
                List<Farm> FarmsToAdd = new List<Farm>();
                project.Id = Guid.NewGuid();

                var farms = selectedFarmsString.Split('|');
                foreach (var farm in farms)
                {
                    if (farm != "")
                    {
                        FarmsToAdd.Add(db.Farms.Find(new Guid(farm)));
                    }
                }
                project.Farms = FarmsToAdd;
                db.Projects.Add(project);
                db.SaveChanges();
            }
            return RedirectToAction("ProjectsList");
        }

        public ActionResult EditProject(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProject([Bind(Include = "Id,Name,Description")] Project Project, string selectedFarmsString)
        {
            if (ModelState.IsValid && selectedFarmsString != "")
            {
                Project persisted = db.Projects.Find(Project.Id);
                persisted.Name = Project.Name;
                persisted.Description = Project.Description;

                Project.Farms = new List<Farm>();

                var farms = selectedFarmsString.Split('|');
                foreach (var farmId in farms)
                {
                    if (farmId != "")
                    {
                        var farm = db.Farms.Find(new Guid(farmId));
                        Project.Farms.Add(farm);
                    }
                }

                List<Farm> deletedFarms = new List<Farm>();
                List<Farm> addedFarms = new List<Farm>();

                deletedFarms = persisted.Farms.Except(Project.Farms).ToList();
                addedFarms = Project.Farms.Except(persisted.Farms).ToList();

                deletedFarms.ForEach(f => persisted.Farms.Remove(f));
                foreach (Farm f in addedFarms)
                {
                    db.Farms.Attach(f);
                    persisted.Farms.Add(f);
                }
                db.SaveChanges();
            }
            return RedirectToAction("ProjectsList");
        }

        public ActionResult DeleteProject(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("DeleteProject")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Project project = db.Projects.Find(id);

            foreach (Farm farm in project.Farms)
            {
                farm.Projects.Remove(project);
            }
            db.Projects.Remove(project);
            db.SaveChanges();

            return RedirectToAction("ProjectsList");
        }

        /// <summary>
        /// Creates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>PartialView with farm</returns>
        [HttpPost]
        public ActionResult Create(Guid id, Guid farmId)
        {
            var farm = _manager.Details(farmId);
            if (!farm.Projects.Any(p => p.Id.Equals(id)))
            {
                var project = _projectManager.Get(id);
                farm.Projects.Add(project);
                _manager.Edit(farmId, farm, FarmManager.PROJECTS);
            }
            return PartialView("~/Views/Projects/Index.cshtml", farm);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns>PartialView with farm</returns>
        [HttpDelete]
        public ActionResult Delete(Guid id, Guid farmId)
        {
            var farm = _manager.Details(farmId);
            ProjectDTO project = farm.Projects.FirstOrDefault(p => p.Id.Equals(id));
            if (project != null)
            {
                farm.Projects.Remove(project);
                _manager.Edit(farmId, farm, FarmManager.PROJECTS);
            }
            return PartialView("~/Views/Projects/Index.cshtml", farm);
        }


        public void ToExcel()
        {
            var grid = new GridView();
            List<ProjectInformation> ListFarmProjectInformation = new List<ProjectInformation>();
            var ListFarmProject = db.Projects.OrderBy(p => p.Name).ToList();

            List<Farm> ListFarms = new List<Farm>();
            List<Project> ListProject = new List<Project>();
            var User = HttpContext.User as CustomPrincipal;
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();

            if (ListProjectsUser.Count() == 0)
            {
                foreach (var FarmProject in ListFarmProject)
                {
                    foreach (var Farms in FarmProject.Farms)
                    {
                        ProjectInformation FarmProjectInformation = new ProjectInformation();
                        FarmProjectInformation.NameProductor = Farms.FamilyUnitMembers.Where(p => p.Identification == Farms.Code).Select(s => s.FirstName + "" + s.LastName).FirstOrDefault();
                        FarmProjectInformation.Document = Farms.FamilyUnitMembers.Where(p => p.Identification == Farms.Code).Select(s => s.Identification).FirstOrDefault();
                        FarmProjectInformation.ProjectName = FarmProject.Name;
                        FarmProjectInformation.Description = FarmProject.Description;
                        FarmProjectInformation.FarmCode = Farms.Code;
                        FarmProjectInformation.FarmName = Farms.Name;
                        ListFarmProjectInformation.Add(FarmProjectInformation);
                    }
                }
                grid.DataSource = ListFarmProjectInformation;
            }
            else
            {
                foreach (var FarmProject in ListFarmProject)
                {
                    foreach (var Farms in FarmProject.Farms)
                    {
                        foreach (var projectUser in ListProjectsUser)
                        {
                            if (projectUser.Id == FarmProject.Id)
                            {
                                ProjectInformation FarmProjectInformation = new ProjectInformation();
                                FarmProjectInformation.NameProductor = Farms.FamilyUnitMembers.Where(p => p.Identification == Farms.Code).Select(s => s.FirstName + "" + s.LastName).FirstOrDefault();
                                FarmProjectInformation.Document = Farms.FamilyUnitMembers.Where(p => p.Identification == Farms.Code).Select(s => s.Identification).FirstOrDefault();
                                FarmProjectInformation.ProjectName = FarmProject.Name;
                                FarmProjectInformation.Description = FarmProject.Description;
                                FarmProjectInformation.FarmCode = Farms.Code;
                                FarmProjectInformation.FarmName = Farms.Name;
                                ListFarmProjectInformation.Add(FarmProjectInformation);
                            }
                        }
                    }
                }
                grid.DataSource = ListFarmProjectInformation;
            }

            //foreach (var FarmProject in ListFarmProject)
            //{
            //    foreach (var Farms in FarmProject.Farms)
            //    {
            //        ProjectInformation FarmProjectInformation = new ProjectInformation();
            //        FarmProjectInformation.NameProductor = Farms.FamilyUnitMembers.Where(p => p.Identification == Farms.Code).Select(s => s.FirstName + "" + s.LastName).FirstOrDefault();
            //        FarmProjectInformation.Document = Farms.FamilyUnitMembers.Where(p => p.Identification == Farms.Code).Select(s => s.Identification).FirstOrDefault();
            //        FarmProjectInformation.ProjectName = FarmProject.Name;
            //        FarmProjectInformation.Description = FarmProject.Description;
            //        FarmProjectInformation.FarmCode = Farms.Code;
            //        FarmProjectInformation.FarmName = Farms.Name;
            //        ListFarmProjectInformation.Add(FarmProjectInformation);
            //    }
            //}
            //grid.DataSource = ListFarmProjectInformation;

            grid.DataBind();
            Response.AddHeader("content-disposition", "attachment; filename=Project information.xls");
            Response.ClearContent();
            Response.ContentType = "application/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(sw);

            grid.HeaderRow.Cells[0].Text = "Nombre del productor";
            grid.HeaderRow.Cells[1].Text = "Cedula";
            grid.HeaderRow.Cells[2].Text = "Nombre del proyecto";
            grid.HeaderRow.Cells[3].Text = "Descripcion";
            grid.HeaderRow.Cells[4].Text = "Codigo de la finca";
            grid.HeaderRow.Cells[5].Text = "Nombre de la finca";

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                GridViewRow row = grid.Rows[i];
                row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
            }
            grid.RenderControl(htmlTextWriter);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        public class ProjectInformation
        {
            public string NameProductor { get; set; }
            public string Document { get; set; }
            public string ProjectName { get; set; }
            public string Description { get; set; }
            public string FarmCode { get; set; }
            public string FarmName { get; set; }
        }

    }
}