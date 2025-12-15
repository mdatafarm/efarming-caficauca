using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// Countries Controller
    /// </summary>
    public class CountriesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private ICountryManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public CountriesController(CountryManager manager){
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<CountryDTO> Index(){
            return _manager.GetAll().ToList();
        }
    }
}
