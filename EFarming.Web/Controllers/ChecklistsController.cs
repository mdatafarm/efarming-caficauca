//using EFarming.DTO.QualityModule;
//using EFarming.Manager.Contract;
//using EFarming.Web.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Web.Mvc;

//namespace EFarming.Web.Controllers
//{
//    /// <summary>
//    /// Controller for Checklist
//    /// This functionality is dont in production because the survey application replace this functionality
//    /// </summary>
//    [CustomAuthorize(Roles = "User,Technician")]
//    public class ChecklistsController : BaseController
//    {
//        private IChecklistManager _checklistManager;
//        public ChecklistsController(IChecklistManager checklistManager)
//        {
//            _checklistManager = checklistManager;

//            InitializeDropdownLists();
//        }

//        /// <summary>
//        /// Gets the specified identifier.
//        /// </summary>
//        /// <param name="id">The identifier.</param>
//        /// <returns></returns>
//        public ActionResult Get(Guid id)
//        {
//            var checklist = _checklistManager.Get(id);
//            return View(checklist);
//        }

//        /// <summary>
//        /// Creates this instance.
//        /// </summary>
//        /// <returns></returns>
//        public ActionResult Create()
//        {
//            return View(new ChecklistDTO { IsNew = true });
//        }

//        [HttpPost]
//        public ActionResult Create(ChecklistDTO checklist)
//        {
//            try
//            {
//                _checklistManager.Add(checklist);
//                return RedirectToAction("Edit", new { id = checklist.Id });
//            }
//            catch
//            {
//                return View(checklist);
//            }
//        }

//        public ActionResult Edit(Guid id)
//        {
//            return View(_checklistManager.Get(id));
//        }

//        [HttpPut]
//        public ActionResult Update(Guid id, ChecklistDTO checklist)
//        {
//            return View(_checklistManager.Edit(checklist));
//        }

//        [HttpDelete]
//        public ActionResult Delete(Guid id)
//        {
//            if (_checklistManager.Delete(id))
//            {
//                return RedirectToAction("Edit", new { id = id });
//            }
//            return RedirectToAction("Add");
//        }

//        [AllowAnonymous]
//        [HttpPost]
//        public JsonResult Upload(FormCollection collection)
//        {
//            var file = Request.Files[0];
//            try
//            {
//                string path = Server.MapPath(string.Format("~/Content/Uploads/signatures/{0}", collection["checklistId"]));
//                Directory.CreateDirectory(path);
                
//                var filePath = string.Format("{0}/{1}", path, file.FileName);

//                file.SaveAs(filePath);
//                var checklist = _checklistManager.Get(Guid.Parse(collection["checklistId"]));
//                if(bool.Parse(collection["isFarmer"]))
//                {
//                    checklist.FarmerSignatureUrl = filePath;
//                }
//                else
//                {
//                    checklist.TechnicianSignatureUrl = filePath;
//                }

//                _checklistManager.Edit(checklist);
//                return Json(new { status = "OK" }, JsonRequestBehavior.AllowGet);
//            }
//            catch (Exception e)
//            {
//                return Json(new { files = new object[] { new { name = file.FileName, size = file.ContentLength, error = e.Message } } }, JsonRequestBehavior.AllowGet);
//            }
//        }

//        #region Private methods
//        private void InitializeDropdownLists()
//        {
//            // General
//            ViewBag.SINO = new SelectList(
//                new List<SelectListItem> { new SelectListItem { Text = "SI", Value = "1"},
//                                           new SelectListItem { Text = "NO", Value = "0"}},"Value","Text");
            
//            // Fermentacion
//            ViewBag.TipoBeneficiadero = new SelectList(
//                new List<SelectListItem> { new SelectListItem {Text = "Tradicional", Value = "Tradicional"}, 
//                                           new SelectListItem {Text = "Belcosub", Value = "Belcosub"}, 
//                                           new SelectListItem {Text = "Mixto", Value = "Mixto" }},"Value","Text");
            
//            ViewBag.SistemaFermentacion = new SelectList(
//                new List<SelectListItem> { new SelectListItem {Text = "Sustrato Sumergido", Value="Sustrato Sumergido"},
//                                           new SelectListItem {Text = "Sustrato Seco", Value="Sustrato Seco"},
//                                           new SelectListItem {Text = "Abierto", Value="Abierto"},
//                                           new SelectListItem {Text = "Cerrado", Value="Cerrado"}},"Value","Text");
            
//            ViewBag.FuenteAgua = new SelectList(
//                new List<SelectListItem>(){ new SelectListItem {Text="Acueducto Municipal", Value="Acueducto Municipal"},
//                                        new SelectListItem {Text="Acueducto Comunitario", Value="Acueducto Comunitario"},
//                                        new SelectListItem {Text="Arroyo o Nacimiento", Value="Acueducto Comunitario"},
//                                        new SelectListItem {Text="Tanque de Almacenamiento",Value="Tanque de Almacenamiento"}}, "Value", "Text");

//            ViewBag.CriterioIdentificacionLavado = new SelectList(
//                new List<SelectListItem> { new SelectListItem {Text="Opcion 1", Value="Opcion 1"},
//                                           new SelectListItem {Text="Opcion 2", Value="Opcion 2"},
//                                           new SelectListItem {Text="Opcion 2", Value="Opcion 2"}}, "Value", "Text");
                    
//        }
//        #endregion
//    }
//}