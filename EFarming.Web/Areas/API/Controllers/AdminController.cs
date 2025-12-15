using EFarming.Manager.Contract.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// Admin Controller
    /// </summary>
    public class AdminController : ApiController
    {
        /// <summary>
        /// The _user manager
        /// </summary>
        private IUserManager _userManager;
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public AdminController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object Index()
        {
            return new
            {
                PendingUsers = _userManager.CountPending()
            };
        }
    }
}
