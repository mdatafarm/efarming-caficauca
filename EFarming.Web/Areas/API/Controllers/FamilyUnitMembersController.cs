using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// FamilyUnitMembers Controller
    /// </summary>
    public class FamilyUnitMembersController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        IFarmManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyUnitMembersController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FamilyUnitMembersController(FarmManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Initialize()
        {
            return JsonConvert.SerializeObject(_manager.InitializeList());
        }
    }
}
