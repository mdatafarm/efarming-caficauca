using EFarming.Web.Models;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Base Controlller
    /// </summary>
    [CustomAuthorize(Roles = "Admin,Technician,Sustainability")]
    public class BaseController : Controller
    {
        /// <summary>
        /// Gets the user security information for the current HTTP request.
        /// </summary>
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}
