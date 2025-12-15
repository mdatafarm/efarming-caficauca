//using EFarming.DTO.AdminModule;
//using EFarming.Manager.Contract.AdminModule;
//using EFarming.Manager.Implementation.AdminModule;
//using System.Linq;
//using System.Web.Mvc;

//namespace EFarming.Web.Controllers
//{
//    /// <summary>
//    /// Controller for manage the installer options
//    /// </summary>
//    public class InstallController : Controller
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        private IUserManager _manager;
//        /// <summary>
//        /// Initializes a new instance of the <see cref="InstallController"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        public InstallController(UserManager manager)
//        {
//            _manager = manager;
//        }

//        /// <summary>
//        /// Creates this instance.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Create()
//        {
//            if (_manager.GetAll().Count() == 0)
//            {
//                ViewBag.Roles = new SelectList(_manager.GetRoles(), "Id", "RoleName");
//                return View();
//            }
//            return RedirectToAction("Index", "Accounts");
//        }

//        /// <summary>
//        /// Creates the specified user.
//        /// </summary>
//        /// <param name="user">The user.</param>
//        /// <returns>Redirecto to Index Accounts</returns>
//        [HttpPost]
//        public ActionResult Create(UserDTO user)
//        {
//            _manager.InitialConfiguration(user);
//            return RedirectToAction("Index", "Accounts");
//        }

//    }
//}