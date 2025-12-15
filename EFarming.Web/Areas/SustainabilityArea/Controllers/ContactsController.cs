using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EFarming.Core.SustainabilityModule.ContactAggregate;
using EFarming.DAL;
using System.Web.UI;
using EFarming.Web.Controllers;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Web.Models;
using System.Threading.Tasks;
using EFarming.DTO.ContactModule;
using PagedList; 
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.DTO.AdminModule;
using AutoMapper;

namespace EFarming.Web.Areas.SustainabilityArea.Controllers
{
    [CustomAuthorize(Roles = "Sustainability,Technician,Reader")]
    public class ContactsController : BaseController
    {
        private UnitOfWork db = new UnitOfWork();
        //IContactManager _sensoryProfileManager;
        //Index Ini
        public ActionResult Index(string sortOrder, DateTime? currentStartDate, DateTime? searchStartDate, DateTime? currentEndDate, DateTime? searchEndDate, Guid? currentDepartment, Guid? searchDepartment, Guid? currentMunicipality, Guid? searchMunicipality, Guid? currentTechnician, Guid? searchTechnician, string currentFarmCode, string searchFarmCode, Guid? currentVillage, Guid? searchVillage, int? page)
        {
            var User = HttpContext.User as CustomPrincipal;
            List<Contact> ListContact = new List<Contact>();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();
            ViewBag.ListProjectsUser = ListProjectsUser;
            ViewBag.User = User;
            
            var dep = db.Departments.OrderBy(d => d.Name).ToList();
            //var mun = db.Municipalities;
            //var vill = db.Villages;

            SelectList ld = new SelectList(dep, "Id", "Name");
            //SelectList lm = new SelectList(mun.OrderBy(d => d.Name.ToUpper()).ToList(), "Id", "Name");
            //SelectList lv = new SelectList(vill.OrderBy(d => d.Name.ToUpper()).ToList(), "Id", "Name");
            ViewBag.Departments = ld;
            //ViewBag.Municipalities = lm;
            //ViewBag.Villages = lv;

            List<GroupByFarm> ListFarmsGroupby = new List<GroupByFarm>();
            foreach (var FarmProject in ListProjectsUser)
            {
                foreach (var AssociatedFarm in FarmProject.Farms)
                {
                    ListFarmsGroupby.Add(new GroupByFarm
                    {
                        Id = AssociatedFarm.Id,
                        Code = AssociatedFarm.Code,
                        Name = AssociatedFarm.Name
                    });
                }
            }
            ViewBag.ListFarmsGroupby = ListFarmsGroupby.GroupBy(g => g.Id).Select(s => new GroupByFarm { Id = s.Key, Code = s.FirstOrDefault().Code, Name = s.FirstOrDefault().Name }).ToList();
    
            var withFilter = (searchStartDate.HasValue || searchEndDate.HasValue || searchDepartment!=null || searchMunicipality!=null || searchVillage!=null || searchTechnician != null);
            if (withFilter)
            {
                page = 1;
            }
            else
            {
                searchDepartment = currentDepartment;
                searchMunicipality = currentMunicipality;
                searchVillage = currentVillage;
                searchStartDate = currentStartDate;
                searchEndDate = currentEndDate;
                searchFarmCode = currentFarmCode;
                searchTechnician = currentTechnician;
            }
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.FarmManagerSortParm = sortOrder == "FarmManager" ? "farmmanager_desc" : "FarmManager";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CurrentVillage = searchVillage;
            ViewBag.CurrentMunicipality = searchMunicipality;
            ViewBag.CurrentDepartment = searchDepartment;
            ViewBag.CurrentStartDate = searchStartDate;
            ViewBag.CurrentEndDate = searchEndDate;
            ViewBag.CurrentFarmCode = searchFarmCode;
            ViewBag.CurrentTechnician = searchTechnician;
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            if (!searchStartDate.HasValue)
            {
                searchStartDate = DateTime.Now.AddDays(-120);
            }
            
            if (!searchEndDate.HasValue)
            {
                searchEndDate = DateTime.Now;
            }
            ViewBag.currentStartDate = string.Format("{0:yyyy-MM-dd}", searchStartDate.Value);
            ViewBag.currentEndDate = string.Format("{0:yyyy-MM-dd}", searchEndDate.Value);

            var Contactaux = db.Contacts.Where(c => c.Date >= searchStartDate && c.Date <= searchEndDate).ToList();
            Contactaux = Contactaux.OrderByDescending(c=>c.Date).ToList();

           

            List<User> lu = new List<User>();

            var contacts = db.Contacts.ToList();

            foreach (var i in contacts)
            {
                lu.Add(i.User);
            }
            lu = lu.Distinct().OrderBy(u => u.FirstName).ToList();
            foreach (User ud in lu)
            {
                ud.FirstName = ud.FirstName.ToUpper();
                ud.LastName = ud.LastName.ToUpper();
            }
            List<UserDTO> luDTO = new List<UserDTO>();
            luDTO = Mapper.Map<List<UserDTO>>(lu);
            SelectList lusers = new SelectList(luDTO, "Id", "FullName");
            ViewBag.Technicians = lusers;
            List<Contact> Contacts = new List<Contact>();
            try
            {
                if (searchDepartment!=null || searchMunicipality!=null || searchVillage != null || searchTechnician != null || (searchFarmCode !="" && searchFarmCode != null))
                {
                    if (searchFarmCode != null && searchFarmCode != "")
                    {
                        foreach (var c in Contactaux)
                        {
                            Boolean flagFarmCode = false;
                            foreach (var f in c.Farms)
                            {
                                if (f.Code.Equals(searchFarmCode))
                                {
                                    flagFarmCode = true;
                                }
                            }
                            if (flagFarmCode)
                            {
                                Contacts.Add(c);
                            }
                        }
                    }
                    if (searchTechnician != null )
                    {
                        foreach (var c in Contactaux)
                        {
                            if (c.UserId.Equals(searchTechnician))
                            {
                                Contacts.Add(c);
                            }       
                        }
                    }
                    if (searchDepartment != null || searchMunicipality != null || searchVillage != null)
                    {
                        if (searchVillage != null)
                        {

                            foreach (var c in Contactaux)
                            {
                                Boolean flagFarmVill = false;
                                foreach (var f in c.Farms)
                                {
                                    if (f.VillageId.Equals(searchVillage) && f.Village.MunicipalityId.Equals(searchMunicipality))
                                    {
                                        flagFarmVill = true;
                                    }
                                }
                                if (flagFarmVill)
                                {
                                    Contacts.Add(c);
                                }
                            }
                        }
                        else
                        {
                            if (searchMunicipality != null)
                            {
                                foreach (var c in Contactaux)
                                {
                                    Boolean flagFarmMun = false;
                                    foreach (var f in c.Farms)
                                    {
                                        if (f.Village.MunicipalityId.Equals(searchMunicipality))
                                        {
                                            flagFarmMun = true;
                                        }
                                    }
                                    if (flagFarmMun)
                                    {
                                        Contacts.Add(c);
                                    }
                                }
                            }
                            else
                            {       
                                foreach (var c in Contactaux)
                                {
                                    Boolean flagFarmDep = false;
                                        foreach (var f in c.Farms)
                                        {
                                            if (f.Village.Municipality.DepartmentId.Equals(searchDepartment))
                                            {
                                                flagFarmDep = true;
                                            }
                                        }
                                        if (flagFarmDep)
                                        {
                                            Contacts.Add(c);
                                        }
                                }
                            }
                        }
                    }
                    
                }
                   
                else
                {
                    Contacts = Contactaux;
                }
                
            }
            catch (Exception ex)
            {

            }
            switch (sortOrder)
            {
                case "date_desc":
                    //results = results.   OrderByDescending(spa => spa.Farm.Name.ToString);
                    Contacts = Contacts.OrderBy(c =>c.Date).ToList();
                    break;
                case "FarmManager":
                    Contacts = Contacts.OrderBy(c => c.User != null ? c.User.FirstName : string.Empty).ToList();
                    break;
                case "farmmanager_desc":
                    Contacts = Contacts.OrderByDescending(c => c.User != null ? c.User.FirstName : string.Empty).ToList();
                    break;   
            }
            /*CODE PRUEBA*/
            if (User.IsAdmin() || User.IsSustainability() || User.IsReader())
            {
                if (ListProjectsUser.Count() == 0)
                {
                    return View(Contacts.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    foreach (var Contact in Contacts)
                    {
                        foreach (var ListContactFarm in Contact.Farms)
                        {
                            foreach (var projectUser in ViewBag.ListProjectsUser)
                            {
                                foreach (var project in ListContactFarm.Projects)
                                {
                                    if (projectUser.Id == project.Id)
                                    {
                                        ListContact.Add(Contact);
                                    }
                                }
                            }
                        }
                    }
                    var GroupByListContact = ListContact.GroupBy(g => g.Id).Select(s => new ContactList
                    {
                        Id = s.Key,
                        Date = s.FirstOrDefault().Date,
                        Name = s.FirstOrDefault().Name,
                        UserName = s.FirstOrDefault().User.FirstName + " " + s.FirstOrDefault().User.LastName,
                        LocationName = s.FirstOrDefault().Location.Name,
                        TypeName = s.FirstOrDefault().Type.Name,
                        Topics = s.FirstOrDefault().Topics,
                        Farms = s.FirstOrDefault().Farms,
                    }).OrderByDescending(o => o.Date).ToList();
                    ViewBag.GroupByListContact = GroupByListContact;
                    return View(Contacts.ToPagedList(pageNumber, pageSize));
                }
            }
            else if (!User.IsAdmin() || !User.IsSustainability() || !User.IsReader())
            {
                var ListContactsUser = Contacts.Where(u => u.UserId == User.UserId).ToList();
                if (ListProjectsUser.Count() > 0)
                {
                    foreach (var Contact in ListContactsUser)
                    {
                        foreach (var ListContactFarm in Contact.Farms)
                        {
                            foreach (var projectUser in ViewBag.ListProjectsUser)
                            {
                                foreach (var project in ListContactFarm.Projects)
                                {
                                    if (projectUser.Id == project.Id)
                                    {
                                        ListContact.Add(Contact);
                                    }
                                }
                            }
                        }
                    }
                    var GroupByListContact = ListContact.GroupBy(g => g.Id).Select(s => new ContactList
                    {
                        Id = s.Key,
                        Date = s.FirstOrDefault().Date,
                        Name = s.FirstOrDefault().Name,
                        UserName = s.FirstOrDefault().User.FirstName + " " + s.FirstOrDefault().User.LastName,
                        LocationName = s.FirstOrDefault().Location.Name,
                        TypeName = s.FirstOrDefault().Type.Name,
                        Topics = s.FirstOrDefault().Topics,
                        Farms = s.FirstOrDefault().Farms,
                    }).OrderByDescending(o => o.Date).ToList();
                    ViewBag.GroupByListContact = GroupByListContact;
                    return View(ListContactsUser.ToPagedList(pageNumber, pageSize));
                }
                return View(ListContactsUser.ToPagedList(pageNumber, pageSize));
            }
            return View(Contacts.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult getMucipalitiesByDepartment(Guid dep)
        {
            List<Municipality> listMun = new List<Municipality>();
            listMun = db.Municipalities.Where(m => m.Name != null && m.Name != "" && m.Department.Id == dep).OrderBy(m => m.Name.Trim()).ToList();
            return Json(new SelectList(listMun, "Id", "Name"));
        }
        [HttpPost]
        public ActionResult getVillagesByMunicipality(Guid mun)
        {
            List<Village> listVill = new List<Village>();
            listVill = db.Villages.Where(v => v.Name != null && v.Name != "" && v.Municipality.Id == mun).OrderBy(v => v.Name.Trim()).ToList();
            return Json(new SelectList(listVill, "Id", "Name"));
        }
        //Index end
        // GET: Sustainability/Contacts
        public ActionResult Index1(DateTime? start, DateTime? end, int? page)
        {
            var User = HttpContext.User as CustomPrincipal;
            List<Contact> ListContact = new List<Contact>();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();
            ViewBag.ListProjectsUser = ListProjectsUser;
            ViewBag.User = User;
            List<GroupByFarm> ListFarmsGroupby = new List<GroupByFarm>();
            foreach (var FarmProject in ListProjectsUser)
            {
                foreach (var AssociatedFarm in FarmProject.Farms)
                {
                    ListFarmsGroupby.Add(new GroupByFarm
                    {
                        Id = AssociatedFarm.Id,
                        Code = AssociatedFarm.Code,
                        Name = AssociatedFarm.Name
                    });
                }
            }
            ViewBag.ListFarmsGroupby = ListFarmsGroupby.GroupBy(g => g.Id).Select(s => new GroupByFarm { Id = s.Key, Code = s.FirstOrDefault().Code, Name = s.FirstOrDefault().Name }).ToList();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            if (start == null)
            {
                start = DateTime.Now.AddDays(-30);
                ViewBag.SelectedStart = start;
            }
            else
            {
                ViewBag.SelectedStart = start;
            }

            if (end == null)
            {
                end = DateTime.Now;
                ViewBag.SelectedEnd = end;
            }
            else
            {
                ViewBag.SelectedEnd = end;
            }

            var Contacts = db.Contacts.Where(c => c.Date >= start && c.Date <= end).OrderByDescending(c => c.Date).ToList();
            if (User.IsAdmin() || User.IsSustainability() || User.IsReader())
            {
                if (ListProjectsUser.Count() == 0)
                {
                    return View(Contacts.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    foreach (var Contact in Contacts)
                    {
                        foreach (var ListContactFarm in Contact.Farms)
                        {
                            foreach (var projectUser in ViewBag.ListProjectsUser)
                            {
                                foreach (var project in ListContactFarm.Projects)
                                {
                                    if (projectUser.Id == project.Id)
                                    {
                                        ListContact.Add(Contact);
                                    }
                                }
                            }
                        }
                    }
                    var GroupByListContact = ListContact.GroupBy(g => g.Id).Select(s => new ContactList
                    {
                        Id = s.Key,
                        Date = s.FirstOrDefault().Date,
                        Name = s.FirstOrDefault().Name,
                        UserName = s.FirstOrDefault().User.FirstName + " " + s.FirstOrDefault().User.LastName,
                        LocationName = s.FirstOrDefault().Location.Name,
                        TypeName = s.FirstOrDefault().Type.Name,
                        Topics = s.FirstOrDefault().Topics,
                        Farms = s.FirstOrDefault().Farms,
                    }).OrderByDescending(o => o.Date).ToList();
                    ViewBag.GroupByListContact = GroupByListContact;
                    return View(Contacts.ToPagedList(pageNumber, pageSize));
                }
            }
            else if (!User.IsAdmin() || !User.IsSustainability() || !User.IsReader())
            {
                var ListContactsUser = Contacts.Where(u => u.UserId == User.UserId).ToList();
                if (ListProjectsUser.Count() > 0)
                {
                    foreach (var Contact in ListContactsUser)
                    {
                        foreach (var ListContactFarm in Contact.Farms)
                        {
                            foreach (var projectUser in ViewBag.ListProjectsUser)
                            {
                                foreach (var project in ListContactFarm.Projects)
                                {
                                    if (projectUser.Id == project.Id)
                                    {
                                        ListContact.Add(Contact);
                                    }
                                }
                            }
                        }
                    }
                    var GroupByListContact = ListContact.GroupBy(g => g.Id).Select(s => new ContactList
                    {
                        Id = s.Key,
                        Date = s.FirstOrDefault().Date,
                        Name = s.FirstOrDefault().Name,
                        UserName = s.FirstOrDefault().User.FirstName + " " + s.FirstOrDefault().User.LastName,
                        LocationName = s.FirstOrDefault().Location.Name,
                        TypeName = s.FirstOrDefault().Type.Name,
                        Topics = s.FirstOrDefault().Topics,
                        Farms = s.FirstOrDefault().Farms,
                    }).OrderByDescending(o => o.Date).ToList();
                    ViewBag.GroupByListContact = GroupByListContact;
                    return View(ListContactsUser.ToPagedList(pageNumber, pageSize));
                }
                return View(ListContactsUser.ToPagedList(pageNumber, pageSize));
            }
            return View(Contacts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Sustainability/Contacts
        public ActionResult ByFarm(Guid id)
        {
            var User = HttpContext.User as CustomPrincipal;
            ViewBag.User = User;
            var farm = db.Farms.Where(f => f.Id == id).FirstOrDefault();
            return View(farm.Contacts.ToList());
        }

        // GET: Sustainability/Contacts/Details/5
        public ActionResult Details(Guid? id)
        {
            var User = HttpContext.User as CustomPrincipal;
            ViewBag.User = User;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Sustainability/Contacts/Create
        public ActionResult Create()
        {
            //SelecList for contact topics
            var Topics = db.Topic.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.Topics = new SelectList(Topics, "Value", "Text");

            //SelecList for contact types
            var Types = db.Type.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.Types = new SelectList(Types, "Value", "Text");

            //SelecList for contact Locations
            var Locations = db.Location.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.Locations = new SelectList(Locations, "Value", "Text");
            ViewBag.listNames = listNames();
            ViewBag.User = User.UserId;
            return View();
        }

        // POST: Sustainability/Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Date,Comment,TypeId,LocationId,CreatedAt,UpdatedAt,DeletedAt")] Contact contact, int?[] topics, int? Type, string selectedFarmsString)
        {
            
            if (ModelState.IsValid)
            {
                List<Topic> TopicsToAdd = new List<Topic>();
                List<Farm> FarmsToAdd = new List<Farm>();

                contact.Id = Guid.NewGuid();
                contact.TypeId = contact.TypeId;
                contact.UserId = User.UserId;

                if(topics != null)
                {
                    foreach (var topic in topics)
                    {
                        TopicsToAdd.Add(db.Topic.Find(topic));
                    }
                    contact.Topics = TopicsToAdd;
                }

                var farms = selectedFarmsString.Split('|');
                foreach (var farm in farms)
                {
                    if(farm != "")
                    {
                        FarmsToAdd.Add(db.Farms.Find(new Guid(farm)));
                    }
                }
                contact.Farms = FarmsToAdd;

                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //SelecList for contact topics
            var Topics = db.Topic.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.Topics = new SelectList(Topics, "Value", "Text");

            //SelecList for contact types
            var Types = db.Type.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.Types = new SelectList(Types, "Value", "Text");

            //SelecList for contact Locations
            var Locations = db.Location.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.Locations = new SelectList(Locations, "Value", "Text");
            ViewBag.listNames = listNames();

            return View(contact);
        }

        // GET: Sustainability/Contacts/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            //SelecList for contact topics
            var Topics = db.Topic.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.SelectedTopics = contact.Topics.Select(t => t.Id).ToList();
            ViewBag.Topics = new SelectList(Topics, "Value", "Text", contact.Topics.Select(t => t.Id));
            ViewBag.listNames = listNames();

            //SelecList for contact types
            var Types = db.Type.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            
            foreach(var item in Types)
            {
                item.Selected = true;
            }
            
            ViewBag.Types = new SelectList(Types, "Value", "Text");

            //SelecList for contact Locations
            var Locations = db.Location.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.Locations = new SelectList(Locations, "Value", "Text", contact.Location.Id);

            return View(contact);
        }

        // POST: Sustainability/Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Date,Comment,TypeId,LocationId,CreatedAt,UpdatedAt,DeletedAt")] Contact contact, int?[] topics, string selectedFarmsString)
        {
            if (ModelState.IsValid)
            {
                Contact persisted = db.Contacts.Find(contact.Id);
                persisted.Comment = contact.Comment;
                persisted.Name = contact.Name;
                persisted.Date = contact.Date;
                persisted.TypeId = contact.TypeId;
                persisted.LocationId = contact.LocationId;

                List<Topic> TopicsToAdd = new List<Topic>();
                List<Farm> FarmsToAdd = new List<Farm>();
                contact.UserId = User.UserId;

                if (topics != null)
                {
                    foreach (var topic in topics)
                    {
                        TopicsToAdd.Add(db.Topic.Find(topic));
                    }
                }
                contact.Topics = TopicsToAdd;

                var farms = selectedFarmsString.Split('|');
                foreach (var farm in farms)
                {
                    if (farm != "")
                    {
                        FarmsToAdd.Add(db.Farms.Find(new Guid(farm)));
                    }
                }
                contact.Farms = FarmsToAdd;

                List<Farm> deletedFarms = new List<Farm>();
                List<Farm> addedFarms = new List<Farm>();
                List<Topic> deletedTopics = new List<Topic>();
                List<Topic> addedTopics = new List<Topic>();
                //List of deleted farms and deleted topics
                if (persisted.Farms != null && contact.Farms != null)
                {
                    deletedFarms = persisted.Farms.Except(contact.Farms).ToList();
                    addedFarms = contact.Farms.Except(persisted.Farms).ToList();
                    deletedFarms.ForEach(f => persisted.Farms.Remove(f));
                    foreach (Farm f in addedFarms)
                    {
                        db.Farms.Attach(f);
                        persisted.Farms.Add(f);
                    }
                } 
                if (persisted.Topics != null && contact.Topics != null)
                {
                    deletedTopics = persisted.Topics.Except(contact.Topics).ToList();
                    addedTopics = contact.Topics.Except(persisted.Topics).ToList();
                    deletedTopics.ForEach(t => persisted.Topics.Remove(t));
                    foreach (Topic t in addedTopics)
                    {
                        db.Topic.Attach(t);
                        persisted.Topics.Add(t);
                    }
                }  
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //SelecList for contact topics
            var Topics = db.Topic.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.SelectedTopics = contact.Topics.Select(t => t.Id).ToList();
            ViewBag.Topics = new SelectList(Topics, "Value", "Text", contact.Topics.Select(t => t.Id));
            ViewBag.listNames = listNames();

            //SelecList for contact types
            var Types = db.Type.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });

            foreach (var item in Types)
            {
                item.Selected = true;
            }

            ViewBag.Types = new SelectList(Types, "Value", "Text");

            //SelecList for contact Locations
            var Locations = db.Location.OrderBy(t => t.Name).ToList().Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name,
            });
            ViewBag.Locations = new SelectList(Locations, "Value", "Text", contact.Location.Id);
            return View(contact);
        }

        // GET: Sustainability/Contacts/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Sustainability/Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Sustainability/Dashboard
        /// <summary>
        /// Dashboards the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public ActionResult Dashboard(DateTime? start, DateTime? end)
        {
            var User = HttpContext.User as CustomPrincipal;
            List<Contact> ListContact = new List<Contact>();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();
            ViewBag.ListProjectsUser = ListProjectsUser;
            List<GroupByFarm> ListFarmsGroupby = new List<GroupByFarm>();
            foreach (var FarmProject in ListProjectsUser)
            {
                foreach (var AssociatedFarm in FarmProject.Farms)
                {
                    ListFarmsGroupby.Add(new GroupByFarm
                    {
                        Id = AssociatedFarm.Id,
                        Code = AssociatedFarm.Code,
                        Name = AssociatedFarm.Name
                    });
                }
            }
            ViewBag.ListFarmsGroupby = ListFarmsGroupby.GroupBy(g => g.Id).Select(s => new GroupByFarm { Id = s.Key, Code = s.FirstOrDefault().Code, Name = s.FirstOrDefault().Name }).ToList();
            if (start == null)
            {
                start = DateTime.Now.AddDays(-30);
                ViewBag.SelectedStart = start;
            }
            else
            {
                ViewBag.SelectedStart = start;
            }

            if (end == null)
            {
                end = DateTime.Now;
                ViewBag.SelectedEnd = end;
            }
            else
            {
                ViewBag.SelectedEnd = end;
            }

            ViewBag.cooperatives = db.Cooperatives.ToList();

            var Contacts = db.Contacts.Where(c => c.Date >= start && c.Date <= end).OrderByDescending(c => c.Date).ToList();
            if (User.IsAdmin() || User.IsSustainability())
            {
                if (ListProjectsUser.Count() == 0)
                {
                    return View(Contacts);
                }
                else
                {
                    foreach (var Contact in Contacts)
                    {
                        foreach (var ListContactFarm in Contact.Farms)
                        {
                            foreach (var projectUser in ViewBag.ListProjectsUser)
                            {
                                foreach (var project in ListContactFarm.Projects)
                                {
                                    if (projectUser.Id == project.Id)
                                    {
                                        ListContact.Add(Contact);
                                    }
                                }
                            }
                        }
                    }
                    var GroupByListContact = ListContact.GroupBy(g => g.Id).Select(s => new ContactList
                    {
                        Id = s.Key,
                        Date = s.FirstOrDefault().Date,
                        Name = s.FirstOrDefault().Name,
                        UserName = s.FirstOrDefault().User.FirstName + " " + s.FirstOrDefault().User.LastName,
                        LocationName = s.FirstOrDefault().Location.Name,
                        TypeName = s.FirstOrDefault().Type.Name,
                        Topics = s.FirstOrDefault().Topics,
                        Farms = s.FirstOrDefault().Farms,
                    }).OrderByDescending(o => o.Date).ToList();
                    ViewBag.GroupByListContact = GroupByListContact;
                    return View(Contacts);
                }
            }
            else if (!User.IsAdmin() || !User.IsSustainability())
            {
                if (ListProjectsUser.Count() > 0)
                {
                    var ListContactsUser = Contacts.Where(u => u.UserId == User.UserId).ToList();
                    foreach (var Contact in ListContactsUser)
                    {
                        foreach (var ListContactFarm in Contact.Farms)
                        {
                            foreach (var projectUser in ViewBag.ListProjectsUser)
                            {
                                foreach (var project in ListContactFarm.Projects)
                                {
                                    if (projectUser.Id == project.Id)
                                    {
                                        ListContact.Add(Contact);
                                    }
                                }
                            }
                        }
                    }
                    var GroupByListContact = ListContact.GroupBy(g => g.Id).Select(s => new ContactList
                    {
                        Id = s.Key,
                        Date = s.FirstOrDefault().Date,
                        Name = s.FirstOrDefault().Name,
                        UserName = s.FirstOrDefault().User.FirstName + " " + s.FirstOrDefault().User.LastName,
                        LocationName = s.FirstOrDefault().Location.Name,
                        TypeName = s.FirstOrDefault().Type.Name,
                        Topics = s.FirstOrDefault().Topics,
                        Farms = s.FirstOrDefault().Farms,
                    }).OrderByDescending(o => o.Date).ToList();
                    ViewBag.GroupByListContact = GroupByListContact;
                    return View(Contacts);
                }
            }
            return View(Contacts.Where(u => u.UserId == User.UserId).ToList());
        }

        // GET: Sustainability/Contacts/DashboardByCoop
        /// <summary>
        /// Dashboards the by coop.
        /// </summary>
        /// <param name="CoppId">The copp identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        public ActionResult DashboardByCoop(Guid? CoopId, DateTime? start, DateTime? end)
        {
            var User = HttpContext.User as CustomPrincipal;
            List<Contact> ListContact = new List<Contact>();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();
            ViewBag.ListProjectsUser = ListProjectsUser;
            List<GroupByFarm> ListFarmsGroupby = new List<GroupByFarm>();
            foreach (var FarmProject in ListProjectsUser)
            {
                foreach (var AssociatedFarm in FarmProject.Farms)
                {
                    ListFarmsGroupby.Add(new GroupByFarm
                    {
                        Id = AssociatedFarm.Id,
                        Code = AssociatedFarm.Code,
                        Name = AssociatedFarm.Name
                    });
                }
            }
            ViewBag.ListFarmsGroupby = ListFarmsGroupby.GroupBy(g => g.Id).Select(s => new GroupByFarm { Id = s.Key, Code = s.FirstOrDefault().Code, Name = s.FirstOrDefault().Name }).ToList();
            if (start == null)
            {
                start = DateTime.Now.AddDays(-30);
                ViewBag.SelectedStart = start;
            }
            else
            {
                ViewBag.SelectedStart = start;
            }

            if (end == null)
            {
                end = DateTime.Now;
                ViewBag.SelectedEnd = end;
            }
            else
            {
                ViewBag.SelectedEnd = end;
            }

            ViewBag.cooperatives = db.Cooperatives.ToList();
            ViewBag.cooperativeId = CoopId;
            ViewBag.cooperative = db.Cooperatives.Where(c => c.Id == CoopId).Select(c => c.Name).First().ToString();

            var Contacts = db.Contacts.Where(c => c.Date >= start && c.Date <= end).OrderByDescending(c => c.Date).ToList();
            if (User.IsAdmin() || User.IsSustainability())
            {
                if (ListProjectsUser.Count() == 0)
                {
                    return View(Contacts);
                }
                else
                {
                    foreach (var Contact in Contacts)
                    {
                        foreach (var ListContactFarm in Contact.Farms)
                        {
                            foreach (var projectUser in ViewBag.ListProjectsUser)
                            {
                                foreach (var project in ListContactFarm.Projects)
                                {
                                    if (projectUser.Id == project.Id)
                                    {
                                        ListContact.Add(Contact);
                                    }
                                }
                            }
                        }
                    }
                    var GroupByListContact = ListContact.GroupBy(g => g.Id).Select(s => new ContactList
                    {
                        Id = s.Key,
                        Date = s.FirstOrDefault().Date,
                        Name = s.FirstOrDefault().Name,
                        UserName = s.FirstOrDefault().User.FirstName + " " + s.FirstOrDefault().User.LastName,
                        LocationName = s.FirstOrDefault().Location.Name,
                        TypeName = s.FirstOrDefault().Type.Name,
                        Topics = s.FirstOrDefault().Topics,
                        Farms = s.FirstOrDefault().Farms,
                    }).OrderByDescending(o => o.Date).ToList();
                    ViewBag.GroupByListContact = GroupByListContact;
                    return View(Contacts);
                }
            }
            else if (!User.IsAdmin() || !User.IsSustainability())
            {
                if (ListProjectsUser.Count() > 0)
                {
                    var ListContactsUser = Contacts.Where(u => u.UserId == User.UserId).ToList();
                    foreach (var Contact in ListContactsUser)
                    {
                        foreach (var ListContactFarm in Contact.Farms)
                        {
                            foreach (var projectUser in ViewBag.ListProjectsUser)
                            {
                                foreach (var project in ListContactFarm.Projects)
                                {
                                    if (projectUser.Id == project.Id)
                                    {
                                        ListContact.Add(Contact);
                                    }
                                }
                            }
                        }
                    }
                    var GroupByListContact = ListContact.GroupBy(g => g.Id).Select(s => new ContactList
                    {
                        Id = s.Key,
                        Date = s.FirstOrDefault().Date,
                        Name = s.FirstOrDefault().Name,
                        UserName = s.FirstOrDefault().User.FirstName + " " + s.FirstOrDefault().User.LastName,
                        LocationName = s.FirstOrDefault().Location.Name,
                        TypeName = s.FirstOrDefault().Type.Name,
                        Topics = s.FirstOrDefault().Topics,
                        Farms = s.FirstOrDefault().Farms,
                    }).OrderByDescending(o => o.Date).ToList();
                    ViewBag.GroupByListContact = GroupByListContact;
                    return View(Contacts);
                }
            }
            return View(Contacts.Where(u => u.UserId == User.UserId).ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public IEnumerable<SelectListItem> listNames() {

            List<SelectListItem> Names = new List<SelectListItem>();
            
            var name = new ContactNameList();
            var contactName = name.listNames();
            
            foreach (var item in contactName)
            {
                Names.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value,
                });
            }

            return Names;
        }

        public class ContactList
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
            public string TypeName { get; set; }
            public string LocationName { get; set; }
            public string UserName { get; set; }
            public ICollection<Topic> Topics { get; set; }
            public ICollection<Farm> Farms { get; set; }
        }

        public class GroupByFarm
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
        }
    }
}
