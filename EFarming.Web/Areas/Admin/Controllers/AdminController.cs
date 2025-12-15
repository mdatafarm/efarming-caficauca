using EFarming.Common;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// Controller Admin
/// </summary>
namespace EFarming.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Admin Controller
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="E"></typeparam>
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController<T, E> : Controller
        where T : EntityDTO
        where E : Entity
    {
        /// <summary>
        /// The pe r_ page
        /// </summary>
        public const int PER_PAGE = 15;

        /// <summary>
        /// The _manager
        /// </summary>
        IAdminManager<T, E> _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController{T, E}"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public AdminController(IAdminManager<T, E> manager)
        {
	  _manager = manager;
        }

        /// <summary>
        /// Gets the user security information for the current HTTP request.
        /// </summary>
        protected virtual new CustomPrincipal User
        {
	  get { return HttpContext.User as CustomPrincipal; }
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>The View</returns>
        public virtual ActionResult Create()
        {
	  return View();
        }

        /// <summary>
        /// Creates the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>Redirect to Index</returns>
        [HttpPost]
        public virtual ActionResult Create(T entityDTO)
        {
	  try
	  {
	      _manager.Create(entityDTO);
	      return RedirectToAction("Index");
	  }
	  catch { return View(); }
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The View</returns>
        public virtual ActionResult Edit(Guid id)
        {
	  return View(_manager.Get(id));
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>Redirect to Index</returns>
        [HttpPost]
        public virtual ActionResult Edit(Guid id, T entityDTO)
        {
	  try
	  {
	      _manager.Edit(entityDTO);
	      return RedirectToAction("Index");
	  }
	  catch { return View(); }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The View</returns>
        public virtual ActionResult Delete(Guid id)
        {
	  ViewBag.Id = id;
	  return View();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>The Index View</returns>
        [HttpPost]
        public virtual ActionResult Delete(Guid id, T entityDTO)
        {
	  try
	  {
	      _manager.Remove(entityDTO);
	      return RedirectToAction("Index");
	  }
	  catch
	  {
	      return View();
	  }
        }
    }
}