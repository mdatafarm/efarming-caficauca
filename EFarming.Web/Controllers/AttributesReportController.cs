using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.DAL;
using EFarming.DTO.QualityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EFarming.Web.Controllers
{
    [CustomAuthorize(Roles = "User,Quality,Manager,Taster,Admin,Reader")]
    public class AttributesReportController : BaseController
    {
        private UnitOfWork db = new UnitOfWork();
        IUserManager _userManager;
        ISensoryProfileManager _sensoryProfileManager;
        IQualityAttributeManager _qualityAttributeManager;
        ICooperativeManager _cooperativeManager;

        
    public AttributesReportController(IUserManager userManager,
           ISensoryProfileManager sensoryProfileManager,
           IQualityAttributeManager qualityAttributeManager,
           ICooperativeManager cooperativeManager)
        {
            _userManager = userManager;
            _sensoryProfileManager = sensoryProfileManager;
            _qualityAttributeManager = qualityAttributeManager;
            _cooperativeManager = cooperativeManager;
        }
        // GET: AttributesReport
        public ActionResult ViewAttributes(Guid? ass)
        {
            if (ass == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // ICollection<DTO.QualityModule.SensoryProfileAssessmentDTO> spa = _sensoryProfileManager.FilterById(ass);
            ICollection<DTO.QualityModule.SensoryProfileAssessmentDTO> results = _sensoryProfileManager.FilterById(ass.Value);
            //ICollection<SensoryProfileAssessmentDTO> results = _sensoryProfileManager.Filter(ass.Value,"");
           
            ViewBag.QualityAttributes = _qualityAttributeManager.Get(AssessmentTemplate.CuppingId);
            //SensoryProfileAssessment spa = db.SensoryProfileAssessments.Find(ass);
            if (results == null)
            {
                return HttpNotFound();
            }
     
           // ViewBag.QualityAttributes = _qualityAttributeManager.Get_qual(AssessmentTemplate.CuppingId);
            return PartialView(results);
        }
    }
}