using EFarming.DAL;
using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Web.Models;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Controller for see the Members of the family
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability")]
    public class FamilyUnitMembersController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        public const int PERPAGE = 6;

        /// <summary>
        /// The _manager
        /// </summary>
        private IFarmManager _manager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyUnitMembersController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public FamilyUnitMembersController(FarmManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        public ActionResult Index(Guid farmId, int? page = 1)
        {
            var farm = _manager.Details(farmId);
            ViewBag.PagedFamilyUnitMembers = farm.FamilyUnitMembers.ToPagedList(page.Value, 6);
            return PartialView("~/Views/FamilyUnitMembers/Index.cshtml", farm);
        }

        /// <summary>
        /// Creates the specified farm identifier.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        public ActionResult Create(Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var familyUnitMember = new FamilyUnitMemberDTO { FarmId = farmId };
            return PartialView("~/Views/FamilyUnitMembers/Create.cshtml", familyUnitMember);
        }

        /// <summary>
        /// Creates the specified family unit member.
        /// </summary>
        /// <param name="familyUnitMember">The family unit member.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Create(FamilyUnitMemberDTO familyUnitMember, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(familyUnitMember.FarmId);

                if (farm.FamilyUnitMembers.Count == 0)
                {
                    int max = db.FamilyUnitMembers.Where(p => p.IDProductor.HasValue).ToList().Max(x => x.IDProductor.Value);

                    familyUnitMember.IDProductor = max + 1;
                    familyUnitMember.IsOwner = true;
                }

                farm.FamilyUnitMembers.Add(familyUnitMember);
                _manager.Edit(farm.Id, farm, FarmManager.FAMILY_UNIT_MEMBERS);
                ViewBag.PagedFamilyUnitMembers = farm.FamilyUnitMembers.ToPagedList(page.Value, 6);
                return PartialView("~/Views/FamilyUnitMembers/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/FamilyUnitMembers/Create.cshtml", familyUnitMember);
            }
        }


        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        public ActionResult Edit(Guid id, Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var familyUnitMember = _manager.Details(farmId).FamilyUnitMembers.Find(sa => sa.Id.Equals(id));
            return PartialView("~/Views/FamilyUnitMembers/Edit.cshtml", familyUnitMember);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="familyUnitMember">The family unit member.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Edit(Guid id, FamilyUnitMemberDTO familyUnitMember, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(familyUnitMember.FarmId);
                var existingMember = farm.FamilyUnitMembers.FirstOrDefault(sa => sa.Id.Equals(id));

                if (existingMember != null)
                {
                  
                    existingMember.FirstName = familyUnitMember.FirstName;
                    existingMember.LastName = familyUnitMember.LastName;
                    existingMember.Relationship = familyUnitMember.Relationship;
                    existingMember.PhoneNumber = familyUnitMember.PhoneNumber;
                    existingMember.Education = familyUnitMember.Education;
                    existingMember.Identification = familyUnitMember.Identification;
                    existingMember.MaritalStatus = familyUnitMember.MaritalStatus;
                    
                }

                _manager.Edit(farm.Id, farm, FarmManager.FAMILY_UNIT_MEMBERS);
                ViewBag.PagedFamilyUnitMembers = farm.FamilyUnitMembers.ToPagedList(page.Value, 6);
                return PartialView("~/Views/FamilyUnitMembers/Index.cshtml", farm);
            }
            catch
            {
                return PartialView("~/Views/FamilyUnitMembers/Edit.cshtml", familyUnitMember);
            }
        }

        /// <summary>
        /// Owners the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        [HttpPost]
        public ActionResult Owner(Guid id, Guid farmId, int? page = 1)
        {
            var farm = _manager.Details(farmId);
            var idprod = db.FamilyUnitMembers.Where(f => f.IDProductor.HasValue && f.FarmId == farmId).FirstOrDefault();

            int idp = 0;

            if (idprod != null)
            {
                idp = idprod.IDProductor.Value;
            }
            else
            {
                int max = db.FamilyUnitMembers.Where(p=>p.IDProductor.HasValue).ToList().Max(x => x.IDProductor.Value);

                idp = max + 1;
            }

            foreach (var member in farm.FamilyUnitMembers)
            {
                if (member.Id.Equals(id))
                {
                    member.IDProductor = idp;
                }
                member.IsOwner = member.Id.Equals(id);
            }
            _manager.Edit(farm.Id, farm, FarmManager.FAMILY_UNIT_MEMBERS);
            ViewBag.PagedFamilyUnitMembers = farm.FamilyUnitMembers.ToPagedList(page.Value, 6);
            return PartialView("~/Views/FamilyUnitMembers/Index.cshtml", farm);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmId">The farm identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns>PartialView</returns>
        public ActionResult Delete(Guid id, Guid farmId, int? page = 1)
        {
            ViewBag.PageNumber = page.Value;
            var familyUnitMember = _manager.Details(farmId).FamilyUnitMembers.Find(sa => sa.Id.Equals(id));
            return PartialView("~/Views/FamilyUnitMembers/Delete.cshtml", familyUnitMember);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="familyUnitMember">The family unit member.</param>
        /// <param name="page">The page.</param>
        /// <returns>The View</returns>
        [HttpPost]
        public ActionResult Delete(Guid id, FamilyUnitMemberDTO familyUnitMember, int? page = 1)
        {
            try
            {
                var farm = _manager.Details(familyUnitMember.FarmId);
                var toRemove = farm.FamilyUnitMembers.First(sa => sa.Id.Equals(id));
                farm.FamilyUnitMembers.Remove(toRemove);
                _manager.Edit(farm.Id, farm, FarmManager.FAMILY_UNIT_MEMBERS);
                ViewBag.PagedFamilyUnitMembers = farm.FamilyUnitMembers.ToPagedList(page.Value, 6);
                return PartialView("~/Views/FamilyUnitMembers/Index.cshtml", farm);
            }
            catch
            {
                return View();
            }
        }
    }
}
