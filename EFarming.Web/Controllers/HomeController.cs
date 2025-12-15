using EFarming.Web.Models;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for the index and Home controller
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Admin,Sustainability,Reports,Reader")]
    public class HomeController : BaseController
    {

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>The View</returns>
        public ActionResult Index()
        {
            string FullName = User.FirstName + " " + User.LastName;
            return View();
        }

        /// <summary>
        /// Soportes this instance.
        /// </summary>
        /// <returns>The View</returns>
        [AllowAnonymous]
        public ActionResult Soporte()
        {
            return View();
        }
    }
}