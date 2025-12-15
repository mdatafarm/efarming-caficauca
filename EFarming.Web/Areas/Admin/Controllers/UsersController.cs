using EFarming.Common.Encription;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.ProjectModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    public class UsersController : AdminController<UserDTO, User>
    {
        /// <summary>
        /// The User manager
        /// </summary>
        private IUserManager _manager;
        /// <summary>
        /// The _roles repository
        /// </summary>
        private IRoleRepository _rolesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public UsersController(UserManager manager) : base(manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            IPagedList<UserDTO> AllUsersUsers = _manager.GetAllUsers().ToPagedList(1, 100);

            //IPagedList<UserDTO> Users = _manager.GetTechnicians().ToPagedList(1,100);// GetAll(UserSpecification.AllUsers(), d => d.CreatedAt).ToPagedList(1, 100);
            //IPagedList<UserDTO> Users = _manager.GetAll(UserSpecification.AllUsers(),d => d.CreatedAt).ToPagedList(1,100);

            return View(AllUsersUsers);
        }

        /// <summary>
        /// Profiles the specified identifier user.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <returns></returns>
        public ActionResult Profile(Guid idUser)
        {

            UserDTO usr = new UserDTO();

            if (idUser != null)
                usr = _manager.Get(idUser);
            else
                return View("Index");

            ViewBag.Roles = _manager.GetRoles();

            return View(usr);
        }

        public ActionResult AssociateProject(Guid idUser)
        {
            UserDTO usr = new UserDTO();

            if (idUser != null)
                usr = _manager.Get(idUser);
            else
                return View("Index");

            ViewBag.Projects = _manager.GetProjects();

            return View(usr);
        }


        /// <summary>
        /// Pendings this instance.
        /// </summary>
        /// <returns>The View</returns>
        public ActionResult Pending()
        {
            return View();
        }


        /// <summary>
        /// Adds the role.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <param name="Roleid">The roleid.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRole(Guid idUser, Guid Roleid)
        {
            UserDTO usr = new UserDTO();

            if (idUser != null)
                usr = _manager.Get(idUser);
            else
                return View("Index");

            ICollection<RoleDTO> roles = _manager.GetRoles();

            RoleDTO roleToAdd = roles.SingleOrDefault(r => r.Id == Roleid);

            usr.Roles.Add(roleToAdd);

            _manager.AddRole(usr, roleToAdd);

            return RedirectToActionPermanent("Profile", "Users", new { idUser = idUser });
        }

        /// <summary>
        /// Deletes the role.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <param name="Roleid">The roleid.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRole(Guid idUser, Guid Roleid)
        {
            UserDTO usr = new UserDTO();

            if (idUser != null)
                usr = _manager.Get(idUser);
            else
                return View("Index");

            ICollection<RoleDTO> roles = _manager.GetRoles();

            RoleDTO roleToAdd = roles.SingleOrDefault(r => r.Id == Roleid);

            usr.Roles.Remove(roleToAdd);

            _manager.RemoveRole(usr, roleToAdd);

            return RedirectToActionPermanent("Profile", "Users", new { idUser = idUser });
        }

        /// <summary>
        /// Shows the password.
        /// </summary>
        /// <param name="idUser">The identifier user.</param>
        /// <returns>Redirect</returns>
        [HttpPost]
        public ActionResult ShowPassword(Guid idUser)
        {
            string password = "";
            UserDTO usr = new UserDTO();

            if (idUser != null)
                usr = _manager.Get(idUser);
            else
                return View("Index");

            if (usr != null)
                password = EncriptorFactory.CreateEncriptor().HashPassword(usr.Password, usr.Salt);

            ViewBag.GTVCFSXERTUJHGNB = password;

            return RedirectToActionPermanent("Profile", "Users", new { idUser = idUser });
        }

        [HttpPost]
        public ActionResult AddProject(Guid idUser, Guid ProjectId)
        {
            UserDTO usr = new UserDTO();

            if (idUser != null)
                usr = _manager.Get(idUser);
            else
                return View("Index");

            ICollection<ProjectDTO> projects = _manager.GetProjects();

            ProjectDTO projectToAdd = projects.SingleOrDefault(r => r.Id == ProjectId);

            usr.Projects.Add(projectToAdd);

            _manager.AddProject(usr, projectToAdd);

            return RedirectToActionPermanent("AssociateProject", "Users", new { idUser = idUser });
        }
        
        [HttpPost]
        public ActionResult DeleteProject(Guid idUser, Guid ProjectId)
        {
            UserDTO usr = new UserDTO();

            if (idUser != null)
                usr = _manager.Get(idUser);
            else
                return View("Index");

            ICollection<ProjectDTO> projects = _manager.GetProjects();

            ProjectDTO projectToAdd = projects.SingleOrDefault(r => r.Id == ProjectId);

            usr.Projects.Remove(projectToAdd);

            _manager.RemoveProject(usr, projectToAdd);

            return RedirectToActionPermanent("AssociateProject", "Users", new { idUser = idUser });
        }


        /// <summary>
        /// Roleses this instance.
        /// </summary>
        /// <returns>The View</returns>
        public ActionResult Roles()
        {
            ICollection<RoleDTO> roles = _manager.GetRoles();

            return View(roles);
        }
    }
}