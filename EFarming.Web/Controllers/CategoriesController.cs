//using EFarming.Core.AdminModule.AssessmentAggregate;
//using EFarming.DTO.ImpactModule;
//using EFarming.Manager.Contract;
//using EFarming.Manager.Contract.AdminModule;
//using EFarming.Web.Models;
//using System;
//using System.Web.Mvc;

//namespace EFarming.Web.Controllers
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    [CustomAuthorize(Roles = "User,Sustainability")]
//    public class CategoriesController : BaseController
//    {
//        /// <summary>
//        /// The _manager
//        /// </summary>
//        private IIndicatorManager _manager;
//        /// <summary>
//        /// The _assessment template manager
//        /// </summary>
//        private IAssessmentTemplateManager _assessmentTemplateManager;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
//        /// </summary>
//        /// <param name="manager">The manager.</param>
//        /// <param name="assessmentTemplateManager">The assessment template manager.</param>
//        public CategoriesController(IIndicatorManager manager, IAssessmentTemplateManager assessmentTemplateManager)
//        {
//	  _manager = manager;
//	  _assessmentTemplateManager = assessmentTemplateManager;
//        }

//        /// <summary>
//        /// Indexes the specified template identifier.
//        /// </summary>
//        /// <param name="templateId">The template identifier.</param>
//        /// <returns>all the categories</returns>
//        public ActionResult Index(Guid? templateId)
//        {
//	  if (!templateId.HasValue)
//	  {
//	      templateId = Guid.Empty;
//	  }
//	  ViewBag.AssessmentTemplateId =
//	      new SelectList(_assessmentTemplateManager.GetAll(AssessmentTemplateSpecification.Sustainability(), at => at.Name)
//		, "Id", "Name", templateId);
//	  return View(_manager.GetAllCategories(templateId.Value));
//        }

//        /// <summary>
//        /// Creates this instance.
//        /// </summary>
//        /// <returns>New Category DTO</returns>
//        public ActionResult Create()
//        {
//	  ViewBag.AssessmentTemplateId =
//	      new SelectList(_assessmentTemplateManager.GetAll(AssessmentTemplateSpecification.Sustainability(), at => at.Name)
//		, "Id", "Name");
//	  return View(new CategoryDTO());
//        }

//        /// <summary>
//        /// Creates the specified category dto.
//        /// </summary>
//        /// <param name="categoryDTO">The category dto.</param>
//        /// <returns>CategoryDTO</returns>
//        [HttpPost]
//        public ActionResult Create(CategoryDTO categoryDTO)
//        {
//	  ViewBag.AssessmentTemplateId =
//	      new SelectList(_assessmentTemplateManager.GetAll(AssessmentTemplateSpecification.Sustainability(), at => at.Name)
//		, "Id", "Name", categoryDTO.AssessmentTemplateId);
//	  try
//	  {
//	      _manager.CreateCategory(categoryDTO);
//	      return RedirectToAction("Index");
//	  }
//	  catch
//	  {
//	      return View(categoryDTO);
//	  }
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns>CategoryDTO</returns>
//        public ActionResult Edit(Guid id)
//        {
//	  var categoryDTO = _manager.GetCategory(id);
//	  var templates = _assessmentTemplateManager.GetAll(AssessmentTemplateSpecification.Sustainability(), at => at.Name);
//	  ViewBag.AssessmentTemplateId = new SelectList(templates, "Id", "Name", categoryDTO.AssessmentTemplate);
//	  return View(categoryDTO);
//        }

//        /// <summary>
//        /// Edits the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <param name="categoryDTO">The category dto.</param>
//        /// <returns>Redirecto to Index</returns>
//        [HttpPost]
//        public ActionResult Edit(Guid id, CategoryDTO categoryDTO)
//        {
//	  ViewBag.AssessmentTemplateId = new SelectList(_assessmentTemplateManager.GetAll(AssessmentTemplateSpecification.Sustainability(), at => at.Name), "Id", "Name", categoryDTO.AssessmentTemplateId);
//	  try
//	  {
//	      _manager.EditCategory(id, categoryDTO);
//	      return RedirectToAction("Index");
//	  }
//	  catch { return View(categoryDTO); }
//        }
//    }
//}