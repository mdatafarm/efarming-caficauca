using EFarming.DTO.ImpactModule;
using EFarming.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// Categories Controller
    /// </summary>
    public class CategoriesController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IIndicatorManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public CategoriesController(IIndicatorManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Gets the specified template identifier.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>All Categories</returns>
        [HttpGet]
        public ICollection<CategoryDTO> Get(Guid templateId)
        {
            return _manager.GetAllCategories(templateId);
        }
    }
}
