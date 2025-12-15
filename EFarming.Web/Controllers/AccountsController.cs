using EFarming.Common.Resources;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Accounts Controller
    /// </summary>
    [Authorize]
    public class AccountsController : Controller
    {
        /// <summary>
        /// The _user manager
        /// </summary>
        private IUserManager _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountsController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public AccountsController(UserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Indexes the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>loginDTO</returns>
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            if (!_userManager.IsConfigured())
                return RedirectToAction("Create", "Install");

            var loginDTO = new LoginDTO
            {
                ReturnUrl = returnUrl
            };
            return View(loginDTO);
        }

        public class UserToCookie
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public List<string> Roles { get; set; }
        }

        /// <summary>
        /// Indexes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>UserLogin DTO</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginDTO model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.VerifyLogin(model);
                if (user != null)
                {
                    UserToCookie userToCookie = new UserToCookie();
                    UserDTO userDTO = new UserDTO();
                    userDTO.Id = user.Id;
                    userDTO.FirstName = user.FirstName;
                    userDTO.LastName = user.LastName;
                    userDTO.Roles = user.Roles;

                    userToCookie.Id = user.Id;
                    userToCookie.FirstName = user.FirstName;
                    userToCookie.LastName = user.LastName;
                    userToCookie.Roles = new List<string>();
                    foreach(var role in user.Roles)
                    {
                        userToCookie.Roles.Add(role.RoleName);
                    }

                    string userData = JsonConvert.SerializeObject(userToCookie);
                    FormsAuthenticationTicket authTicket = 
		    new FormsAuthenticationTicket(1,Guid.NewGuid().ToString(),DateTime.Now,DateTime.Now.AddHours(10),false,userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);
                    var cookies = Response.Cookies.AllKeys;
                    
		var roles = user.Roles.Select(r => r.RoleName);
                    
		if (string.IsNullOrEmpty(returnUrl))
                    {
		    // Se quita esta redireccion por solicitud de pablo, al autenticarce por defecto debe ir a dashboard		    
                        var controller = "Farms";
                        var _action = "Index";
                        //if (roles.Contains(CustomPrincipal.ADMIN)) controller = "Departments";
                        //if (roles.Contains(CustomPrincipal.REPORTS)) controller = "Dashboard";
                        if (roles.Contains(CustomPrincipal.QUALITY)) {
                            controller = "QualityReports";
                            _action = "Clasification";
                        } else if (roles.Contains(CustomPrincipal.TASTER))
                        {
                            controller = "QualityReports";
                            _action = "ByTaster";
                        }else
                        {
                            controller = "Farms";
                            _action = "Index";
                        }
                        //var area = roles.Contains(CustomPrincipal.ADMIN) ? "admin" : string.Empty;
                        return RedirectToAction(_action, controller, new { area = "" });
                    }

                    return Redirect(returnUrl);
                }

                ModelState.AddModelError("", Message.LoginPasswordIncorrect);
            }

            return View(model);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>The View</returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(_userManager.GetRoles(), "Id", "RoleName");
            return View();
        }

        /// <summary>
        /// Creates the specified user dto.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <returns>Redirecto to Index</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(UserDTO userDTO)
        {
            userDTO.IsActive = false;
            _userManager.Create(userDTO);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Return User</returns>
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var user = _userManager.Get(id);
            return View(user);
        }

        /// <summary>
        /// Edits the specified user dto.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <returns>UserDTO</returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(UserDTO userDTO, string actualPassword)
        {
            ViewBag.ChangeSuccess = "";
            if (ModelState.IsValid)
            {
                userDTO.OldPassword = actualPassword;
                if (_userManager.Edit(userDTO))
                {
                    userDTO.OldPassword = 
                        userDTO.NewPassword = 
                        userDTO.ConfirmPassword = 
                        string.Empty;
                    ViewBag.ChangeSuccess = "Password changed";
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Invalid password");
                }
            }
            return View(userDTO);
        }

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns>Redirecto to login</returns>
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("login");
        }

        //[AllowAnonymous]
        //public ActionResult Profile()
        //{
	  
        //}

        [AllowAnonymous]
        public ActionResult TeamEfarming()
        {
	  return View();
        }
    
    }
}

