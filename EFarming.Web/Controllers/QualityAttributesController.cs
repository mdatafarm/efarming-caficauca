using EFarming.Common.Consts;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Web.Models;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for manage the Atributes for the Quality tests
    /// </summary>
    [CustomAuthorize(Roles = "User,Quality")]
    public class QualityAttributesController : BaseController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IQualityAttributeManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="QualityAttributesController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public QualityAttributesController(QualityAttributeManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>The View</returns>
        public ActionResult Index()
        {
            ViewBag.Types = JsonConvert.SerializeObject(QualityAttributeTypes.TYPES);
            return View();
        }

        /// <summary>
        /// Previews the specified template identifier.
        /// </summary>
        /// <param name="templateId">The template identifier.</param>
        /// <returns>The View whit attributes</returns>
        public ActionResult Preview(Guid templateId)
        {
            var attributes = _manager.Get(templateId);
            return View("Preview", "", attributes);
        }
    }
}