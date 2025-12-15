using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.DAL;
using EFarming.DTO.AdminModule;
using EFarming.DTO.FarmModule;
using EFarming.DTO.ProjectModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    /// <summary>
    /// Principal Controller for manage all the Farms
    /// You can filter the farms
    /// See moore information about the farms
    /// Edit Farm information
    /// Delete Farm information
    /// Export Farm information
    /// </summary>
    [CustomAuthorize(Roles = "Technician,Sustainability,Reader")]
    public class FarmsController : BaseController
    {

        private UnitOfWork db = new UnitOfWork();

        /// <summary>
        /// The _farm manager
        /// </summary>
        private IFarmManager _farmManager;
        /// <summary>
        /// The _soil type manager
        /// </summary>
        private ISoilTypeManager _soilTypeManager;

        private ILotManager _lotManager;

        private IInvoiceManager _invoicemanager;



        /// <summary>
        /// Initializes a new instance of the <see cref="FarmsController"/> class.
        /// </summary>
        /// <param name="farmManager">The farm manager.</param>
        /// <param name="soilTypeManager">The soil type manager.</param>
        public FarmsController(FarmManager farmManager, SoilTypeManager soilTypeManager, LotManager lotManager, InvoiceManager invoicemanager)
        {
            _farmManager = farmManager;
            _soilTypeManager = soilTypeManager;
            _lotManager = lotManager;
            _invoicemanager = invoicemanager;
        }


        /// <summary>
        /// Indexes the specified current name.
        /// </summary>
        /// <param name="currentName">Name of the current.</param>
        /// <param name="searchName">Name of the search.</param>
        /// <param name="currentDepartment">The current department.</param>
        /// <param name="searchDepartment">The search department.</param>
        /// <param name="currentMunicipality">The current municipality.</param>
        /// <param name="searchMunicipality">The search municipality.</param>
        /// <param name="searchVillage">The search village.</param>
        /// <param name="currentVillage">The current village.</param>
        /// <param name="currentCode">The current code.</param>
        /// <param name="searchCode">The search code.</param>
        /// <param name="searchFarmerIdentification">The search farmer identification.</param>
        /// <param name="currentFarmerIdentification">The current farmer identification.</param>
        /// <param name="searchFarmerName">Name of the search farmer.</param>
        /// <param name="currentFarmerName">Name of the current farmer.</param>
        /// <param name="page">The page.</param>
        /// <returns>The View with the farm</returns>
        public ViewResult Index(string sortOrder, string currentName, string searchName, string currentDepartment,
            string searchDepartment, string currentMunicipality, string searchMunicipality,
            string searchVillage, string currentVillage, string currentCode, string searchCode,
            string searchFarmerIdentification, string currentFarmerIdentification,
            string searchFarmerName, string currentFarmerName, int? page)
        {

            


            var withFilter = (!string.IsNullOrEmpty(searchName)
                                || !string.IsNullOrEmpty(searchDepartment)
                                || !string.IsNullOrEmpty(searchMunicipality)
                                || !string.IsNullOrEmpty(searchVillage)
                                || !string.IsNullOrEmpty(searchCode)
                                || !string.IsNullOrEmpty(searchFarmerIdentification)
                                || !string.IsNullOrEmpty(searchFarmerName));
            if (withFilter)
            {
                page = 1;
            }
            else
            {
                searchName = currentName;
                searchCode = currentCode;
                searchMunicipality = currentMunicipality;
                searchDepartment = currentDepartment;
                searchVillage = currentVillage;
                searchFarmerName = currentFarmerName;
                searchFarmerIdentification = currentFarmerIdentification;
            }
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CodeSortParm = sortOrder == "Code" ? "code_desc" : "Code";
            ViewBag.CurrentName = searchName;
            ViewBag.CurrentCode = searchCode;
            ViewBag.CurrentFarmerName = searchFarmerName;
            ViewBag.CurrentFarmerIdentification = searchFarmerIdentification;
            ViewBag.CurrentVillage = searchVillage;
            ViewBag.CurrentMunicipality = searchMunicipality;
            ViewBag.CurrentDepartment = searchDepartment;
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var villageId = string.IsNullOrEmpty(searchVillage) ? Guid.Empty : Guid.Parse(searchVillage);
            var municipalityId = string.IsNullOrEmpty(searchMunicipality) ? Guid.Empty : Guid.Parse(searchMunicipality);
            var departmentId = string.IsNullOrEmpty(searchDepartment) ? Guid.Empty : Guid.Parse(searchDepartment);

            //IPagedList<Farm> farms;

            var farms = _farmManager
                .GetAllQueryable(FarmSpecification.FilterWithFarmerInfo(searchCode, searchName, Guid.Empty, Guid.Empty, Guid.Empty, villageId, municipalityId, departmentId, searchFarmerName),
                                        f => f.Code);

            List<Farm> ListFarms = new List<Farm>();
            List<Farm> ListFarmsGroupby = new List<Farm>();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();

            foreach (var FarmProject in ListProjectsUser)
            {
                foreach (var AssociatedFarm in FarmProject.Farms)
                {
                    ListFarmsGroupby.Add(new Farm
                    {
                        Id = AssociatedFarm.Id,
                        Code = AssociatedFarm.Code,
                        Name = AssociatedFarm.Name
                    });
                }
            }

            var FarmsSelect = ListFarmsGroupby.GroupBy(g => g.Id).Select(s => new { Id = s.Key, Code = s.FirstOrDefault().Code, Name = s.FirstOrDefault().Name }).ToList();

            switch (sortOrder)
            {
                case "name_desc":
                    farms = farms.OrderByDescending(s => s.Name);
                    break;
                case "Code":
                    farms = farms.OrderByDescending(s => s.Code);
                    break;
                case "code_desc":
                    farms = farms.OrderBy(s => s.Code);
                    break;
                default:
                    farms = farms.OrderBy(s => s.Name);
                    break;
            }

            if (ListProjectsUser.Count() == 0)
            {
                return View(farms.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                 foreach (var farm in farms)
                 {
                    foreach (var FarmItem in FarmsSelect)
                    {
                        if (farm.Id == FarmItem.Id)
                        {
                            ListFarms.Add(farm);
                        }
                    }
                 }
            }




            

            return View(ListFarms.ToPagedList(pageNumber, pageSize));
        }

        public bool CheckDocument(string code)
        {
            if (code != null && code != "")
            {
                Farm farmCheck = db.Farms.Where(x => x.Code == code).FirstOrDefault();

                if (farmCheck != null)
                    return true;
                else
                    return false;
            }
            else {
                return false;
            }
            
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The view with the farm</returns>
        [CustomAuthorize(Roles = "Technician,Sustainability,Reader")]
        public ActionResult Details(Guid id)
        {
            var farm = _farmManager.Details(id,
                "Productivity",
                "Productivity.Plantations",
                "Productivity.Plantations.FloweringPeriods",
                "Worker",
                "OtherActivities",
                "SoilAnalysis",
                "SoilTypes",
                "Fertilizers",
                "FamilyUnitMembers");
            ViewBag.PagedSoilAnalysis = farm.SoilAnalysis.ToPagedList(0, 10);
            return View(farm);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Technician,Sustainability")]
        public ActionResult Create()
        {
            var User = HttpContext.User as CustomPrincipal;
            ViewBag.TechnicianId = User.UserId;
            ViewBag.autorizated = User.UserId;
            return View(new FarmDTO { IsNew = true });
        }


        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        [CustomAuthorize(Roles = "Technician,Sustainability,Reader")]
        public ActionResult FlorationFarms()
        {
            //ViewBag.o_farm = db.Farms.Select(c => new {
            //	 Latitude = c.GeoLocation.Latitude,
            //	 Longitude = c.GeoLocation.Longitude,
            //	 Name = c.Name,
            //	 Cooperative = c.Cooperative,
            //	 Vereda = c.Village.Name,
            //	 url_farm = "Edit/"+c.Id,
            //	 url_farm_dashboard = "Dashboard/" + c.Id,
            //	 Code = c.Code,

            //}).Take(2000).ToList();

            ViewBag.villages = db.Villages.OrderBy(c => c.Name).ToList();
            ViewBag.Municipality = db.Municipalities.OrderBy(c => c.Name).ToList();
            ViewBag.Status = db.FloweringPeriodQualifications.OrderBy(f => f.Name).ToList();
                        
            List<Farm> ListFarmsGroupby = new List<Farm>();
            List<FloweringPeriod> ListFloweringPeriod = new List<FloweringPeriod>();
            List<Farm> ListFarms = new List<Farm>();
            var farms = db.Farms.ToList();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();
            
            foreach (var FarmProject in ListProjectsUser)
            {
                foreach (var AssociatedFarm in FarmProject.Farms)
                {
                    ListFarmsGroupby.Add(new Farm
                    {
                        Id = AssociatedFarm.Id,
                        Code = AssociatedFarm.Code,
                        Name = AssociatedFarm.Name
                    });
                }
            }
            var FarmsSelect = ListFarmsGroupby.GroupBy(g => g.Id).Select(s => new { Id = s.Key, Code = s.FirstOrDefault().Code, Name = s.FirstOrDefault().Name }).ToList();

            foreach (var farm in farms)
            {
                foreach (var FarmItem in FarmsSelect)
                {
                    if (farm.Id == FarmItem.Id)
                    {
                        ListFarms.Add(farm);
                    }
                }
            }
            
            foreach (var farm in ListFarms)
            {
                var floration = db.FloweringPeriods.Where(fl => fl.Plantation.Productivity.Farm.Id == farm.Id).ToList();
                foreach (var item in floration)
                {
                    ListFloweringPeriod.Add(item);
                }
            }

            if (ListProjectsUser.Count() == 0)
            {
                var floration = db.FloweringPeriods.Take(200).ToList();
                ViewBag.o_farm = floration.Select(c => new
                {
                    Latitude = c.Plantation.Productivity.Farm.GeoLocation.Latitude,
                    Longitude = c.Plantation.Productivity.Farm.GeoLocation.Longitude,
                    Name = c.Plantation.Productivity.Farm.Name,
                    Cooperative = c.Plantation.Productivity.Farm.Cooperative,
                    Vereda = c.Plantation.Productivity.Farm.Village.Name,
                    url_farm = "Edit/" + c.Plantation.Productivity.Farm.Id,
                    url_farm_dashboard = "Dashboard/" + c.Id,
                    Code = c.Plantation.Productivity.Farm.Code,
                    StarDate = c.StartDate.ToString("yyyy-MM-dd")
                });
            }
            else
            {
                ViewBag.o_farm = ListFloweringPeriod.Select(c => new
                {
                    Latitude = c.Plantation.Productivity.Farm.GeoLocation.Latitude,
                    Longitude = c.Plantation.Productivity.Farm.GeoLocation.Longitude,
                    Name = c.Plantation.Productivity.Farm.Name,
                    Cooperative = c.Plantation.Productivity.Farm.Cooperative,
                    Vereda = c.Plantation.Productivity.Farm.Village.Name,
                    url_farm = "Edit/" + c.Plantation.Productivity.Farm.Id,
                    url_farm_dashboard = "Dashboard/" + c.Id,
                    Code = c.Plantation.Productivity.Farm.Code,
                    StarDate = c.StartDate.ToString("yyyy-MM-dd")
                });
            }
            return View();
        }


        [HttpPost]
        public ActionResult SearchFloration(Guid? idVillage, Guid? municipality, string Name, string code, DateTime? Dstart, DateTime? DEnd, Guid? Status)
        {
            List<FloweringPeriod> floration = new List<FloweringPeriod>();
            List<Farm> ListFarms = new List<Farm>();
            var farms = db.Farms.ToList();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();
            List<Farm> ListFarmsGroupby = new List<Farm>();

            foreach (var FarmProject in ListProjectsUser)
            {
                foreach (var AssociatedFarm in FarmProject.Farms)
                {
                    ListFarmsGroupby.Add(new Farm
                    {
                        Id = AssociatedFarm.Id,
                        Code = AssociatedFarm.Code,
                        Name = AssociatedFarm.Name
                    });
                }
            }
            var FarmsSelect = ListFarmsGroupby.GroupBy(g => g.Id).Select(s => new { Id = s.Key, Code = s.FirstOrDefault().Code, Name = s.FirstOrDefault().Name }).ToList();

            foreach (var farm in farms)
            {
                foreach (var FarmItem in FarmsSelect)
                {
                    if (farm.Id == FarmItem.Id)
                    {
                        ListFarms.Add(farm);
                    }
                }
            }
          
            if (ListProjectsUser.Count() == 0)
            {
                var florationList = db.FloweringPeriods.ToList();
                foreach (var item in florationList)
                {
                    floration.Add(item);
                }
            }
            else
            {
                foreach (var farm in ListFarms)
                {
                    var florationFilterProject = db.FloweringPeriods.Where(fl => fl.Plantation.Productivity.Farm.Id == farm.Id).ToList();
                    foreach (var item in florationFilterProject)
                    {
                        floration.Add(item);
                    }
                }
            }

            floration = idVillage != null ? floration.Where(c => c.Plantation.Productivity.Farm.Village.Id == idVillage).ToList() : floration;

            floration = municipality != null ? floration.Where(c => c.Plantation.Productivity.Farm.Village.Municipality.Id == municipality).ToList() : floration;

            floration = code != null && code != "" ? floration.Where(c => c.Plantation.Productivity.Farm.Code == code).ToList() : floration;

            floration = Name != null && Name != "" ? floration.Where(c => c.Plantation.Productivity.Farm.Name.Contains(Name)).ToList() : floration;

            floration = Status != null ? floration.Where(s => s.FloweringPeriodQualificationId == Status).ToList() : floration;

            if (Dstart != null && DEnd != null)
            {
                var h = string.Format("{0:yyyy-MM-dd}", DEnd) + " 23:59:59";
                DateTime time = DateTime.Parse(h);

                floration = floration.Where(c => c.StartDate >= Dstart && c.StartDate <= DEnd).ToList();
            }
            var a_location = floration.Select(c => new
            {
                Latitude = c.Plantation.Productivity.Farm.GeoLocation.Latitude,
                Longitude = c.Plantation.Productivity.Farm.GeoLocation.Longitude,
                Name = c.Plantation.Productivity.Farm.Name,
                Cooperative = c.Plantation.Productivity.Farm.Cooperative,
                Vereda = c.Plantation.Productivity.Farm.Village.Name,
                url_farm = "Edit/" + c.Plantation.Productivity.Farm.Id,
                url_farm_dashboard = "Dashboard/" + c.Id,
                Code = c.Plantation.Productivity.Farm.Code,
                StarDate = c.StartDate.ToString("yyyy-MM-dd")
            });

            return Json(a_location.ToList(), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Get_Villages(Guid? Municipality)
        {

            if (Municipality != null)
            {

                return Json(db.Villages.Where(c => c.MunicipalityId == Municipality).Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name
                }).OrderBy(c => c.Name).ToList());
            }
            else
                return Json(new { });
        }






        /// <summary>
        /// Creates the specified farm.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <returns>The View</returns>
        [CustomAuthorize(Roles = "Technician,Sustainability")]
        [HttpPost]
        public ActionResult Create(FarmDTO farm)
        {
            var exit = db.Farms.Where(x => x.Code == farm.Code).FirstOrDefault();

            if (exit == null)
            {
                farm.IsNew = true;
                try
                {
                    _farmManager.Create(farm);
                    var associatedFarm = db.ExecuteCommand("INSERT INTO farmAssociatedPeople VALUES ({0},{1})", farm.Id, farm.CurrentTechnician);
                    return RedirectToAction("Edit", new { id = farm.Id });
                }
                catch (Exception ex)
                {
                    return View(farm);
                }
            }
            else {
                return RedirectToAction("Edit", new { id = exit.Id });
            }
            
        }

       private decimal SafeDecimal(string input)
       {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            input = input.Replace(".", ",");
            decimal.TryParse(input, out decimal result);
            return result;
       }
        private double SafeDouble(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            input = input.Replace(".", ",");
            double.TryParse(input, out double result);
            return result;
        }



        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The View with farm</returns>
        [CustomAuthorize(Roles = "Technician,Sustainability,Reader")]
        public ActionResult Edit(Guid id)
        {
            var User = HttpContext.User as CustomPrincipal;
            Guid? itsAutorizated = User.UserId;

            ViewBag.autorizated = null;

            if (itsAutorizated == new Guid("72994FDC-B4A9-42D7-9237-24113DDA034D") || itsAutorizated == new Guid("D87A4D23-68D0-4407-AE69-C3CEB082C396"))
            {
                ViewBag.autorizated = itsAutorizated;
            }

                UnitOfWork db = new UnitOfWork();


            var productivitiesChange2 = db.Productivities.FirstOrDefault(f => f.Id == id);

            decimal totalHectareas = 0;
            decimal totalHectareas_SumPerc = 0;
            decimal totalHectareasCoffe = 0;


            if (productivitiesChange2.Plantations.Count > 0)
            {
                foreach (var p in productivitiesChange2.Plantations)
                {
                    if (p.PlantationTypeId == new Guid("D221BEC9-5F73-43A0-9EBF-16417F5674F5"))
                    {
                        totalHectareasCoffe = (totalHectareasCoffe + SafeDecimal(p.Hectares));
                    }

                    totalHectareas = totalHectareas + SafeDecimal(p.Hectares);
                }
            }

            double averageDensity = 0;
            double averageAge = 0;
            string now = DateTime.Now.ToString();

            var plants2 = productivitiesChange2.Plantations.Where(t => t.PlantationTypeId == new Guid("{D221BEC9-5F73-43A0-9EBF-16417F5674F5}"));

            foreach (var plantation in plants2)
            {
                plantation.Hectares = plantation.Hectares?.Replace(".", ",") ?? "0";
                plantation.EstimatedProduction = plantation.EstimatedProduction?.Replace(".", ",") ?? "0";

                double percentage = (SafeDouble(plantation.Hectares) * 1.0) / Convert.ToDouble(totalHectareasCoffe);

                TimeSpan dateAge = DateTime.Now.Subtract(plantation.Age);
                double Age = (dateAge.Days * 1.0) / 365;

                averageAge += Age * percentage;

                double densityValue = SafeDouble(plantation.Density);
                averageDensity += densityValue * percentage;
            }

            if (averageAge.ToString() == "NaN")
            {
                averageAge = 0;
            }
            productivitiesChange2.coffeeArea = totalHectareasCoffe.ToString().Replace(".", ",");
            productivitiesChange2.averageDensity = averageDensity.ToString();
            productivitiesChange2.averageAge = Math.Round(averageAge, 3);
            db.SaveChanges();

            var farm = _farmManager.Details(id,
                "Productivity",
                "Contacts",
                "Productivity.Plantations",
                "Productivity.Plantations.FloweringPeriods",
                "Worker",
                "OtherActivities",
                "SoilAnalysis",
                "SoilTypes",
                "Fertilizers",
                "FamilyUnitMembers",
                "ImpactAssessments",
                "ImpactAssessments.Answers",
                "Village",
                "Village.Municipality",
                "Village.Municipality.Department");



            decimal infHct = 0;
            if (!string.IsNullOrWhiteSpace(farm.Productivity.InfrastructureHectares) &&
                decimal.TryParse(farm.Productivity.InfrastructureHectares, out decimal result))
            {
                infHct = result;
            }
            decimal fpHct = farm.Productivity.ForestProtectedHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ForestProtectedHectares);
            decimal conHct = farm.Productivity.ConservationHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ConservationHectares);
            //decimal othHct = farm.Productivity.OthersHectareas == "" ? 0 : Convert.ToDecimal(farm.Productivity.OthersHectareas);

            totalHectareas_SumPerc = totalHectareas;
            totalHectareas = totalHectareas + infHct + fpHct + conHct;

            if (farm.Productivity.TotalHectares != totalHectareas.ToString())
            {
                try
                {
                    farm.Productivity.TotalHectares = totalHectareas.ToString();

                    var productivity = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
                    productivity.TotalHectares = totalHectareas.ToString();
                    db.SaveChanges();
                }
                catch
                {
                    farm.Productivity.TotalHectares = totalHectareas.ToString();
                }
            }

            var idFarm = farm.Id;

            //PORCENTAJE COLOMBIA
            var PerColombia = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7"));
            decimal sumPerColombia = 0;
            foreach (var item in PerColombia)
            {
                sumPerColombia = sumPerColombia + SafeDecimal(item.Hectares);
            }

            //PORCENTAJE CATURRA
            var PerCaturra = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"));
            decimal sumPerCaturra = 0;
            foreach (var item in PerCaturra)
            {
                sumPerCaturra = sumPerCaturra + SafeDecimal(item.Hectares);
            }

            //PORCENTAJE CASTILLO
            var PerCastillo = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C"));
            decimal sumPerCastillo = 0;
            foreach (var item in PerCastillo)
            {
                sumPerCastillo = sumPerCastillo + SafeDecimal(item.Hectares);
            }

            //PORCENTAJE OTRO
            var PerOtro = db.Plantations.Where(x => x.ProductivityId == idFarm);
            decimal sumPerOtro = 0;
            foreach (var item in PerOtro)
            {
                if (item.PlantationVarietyId != new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7") && item.PlantationVarietyId != new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C") && item.PlantationVarietyId != new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"))
                {
                    sumPerOtro = sumPerOtro + SafeDecimal(item.Hectares);
                }
            }

            //OPERACIONES PORCENTAJES
            var opeColombia = Convert.ToDecimal(0);
            if (sumPerColombia != 0 && totalHectareasCoffe != 0)
            {
                opeColombia = (sumPerColombia / (totalHectareas_SumPerc)) * 100;
                if (opeColombia > 100)
                {
                    opeColombia = 100;
                }
            }

            var opeCaturra = Convert.ToDecimal(0);
            if (sumPerCaturra != 0 && totalHectareasCoffe != 0)
            {
                opeCaturra = (sumPerCaturra / (totalHectareas_SumPerc)) * 100;
                if (opeCaturra > 100)
                {
                    opeCaturra = 100;
                }
            }

            var opeCastillo = Convert.ToDecimal(0);
            if (sumPerCastillo != 0 && totalHectareasCoffe != 0)
            {
                opeCastillo = (sumPerCastillo / (totalHectareas_SumPerc)) * 100;
                if (opeCastillo > 100)
                {
                    opeCastillo = 100;
                }
            }

            var opeOtro = Convert.ToDecimal(0);
            if (sumPerOtro != 0 && totalHectareasCoffe != 0)
            {
                opeOtro = (sumPerOtro / (totalHectareas_SumPerc)) * 100;
                if (opeOtro > 100)
                {
                    opeOtro = 100;
                }
            }
            else if (sumPerOtro != 0 && totalHectareasCoffe == 0)
            {
                opeOtro = (sumPerOtro / sumPerOtro) * 100;
                if (opeOtro > 100)
                {
                    opeOtro = 100;
                }
            }


            //ACTUALIZACION
            var sum = opeColombia + opeCaturra + opeCastillo + opeOtro;
            var dif = 0.0;
            if (sum > 100)
            {
                dif = Convert.ToDouble(sum) - Convert.ToDouble(100);
                if (Math.Round(Convert.ToDecimal(opeOtro), 2) != Math.Round(Convert.ToDecimal(dif),2))
                {
                    opeOtro = Convert.ToDecimal(opeOtro) - Convert.ToDecimal(dif);
                }
            }
            else if (opeColombia == 0 && opeCaturra == 0 && opeCastillo== 0 && opeOtro != 0)
            {
                opeOtro = 100;
            }
                

            var productivitiesChange = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
            productivitiesChange.percentageColombia = Convert.ToDouble(opeColombia);
            productivitiesChange.percentageCaturra = Convert.ToDouble(opeCaturra);
            productivitiesChange.percentageCastillo = Convert.ToDouble(opeCastillo);
            productivitiesChange.percentageotra = Convert.ToDouble(opeOtro);
            productivitiesChange.UpdatedAt = DateTime.Now;
            db.SaveChanges();

            ViewBag.opeColombia = opeColombia;
            ViewBag.opeCaturra = opeCaturra;
            ViewBag.opeCastillo = opeCastillo;
            ViewBag.opeOtro = opeOtro;
            ViewBag.total = null;
            if (Math.Round((opeColombia + opeCaturra + opeCastillo + opeOtro), 2) >= 100)
            {
                ViewBag.total = 100;
            }
            else {
                ViewBag.total = Math.Round(opeColombia + opeCaturra + opeCastillo + opeOtro);
            } 


            //var farm2 = _farmManager.Details(id,
            //    "Productivity",
            //    "Contacts",
            //    "Productivity.Plantations",
            //    "Productivity.Plantations.FloweringPeriods",
            //    "Worker",
            //    "OtherActivities",
            //    "SoilAnalysis",
            //    "SoilTypes",
            //    "Fertilizers",
            //    "FamilyUnitMembers",
            //    "ImpactAssessments",
            //    "ImpactAssessments.Answers",
            //    "Village",
            //    "Village.Municipality",
            //    "Village.Municipality.Department");

            farm.IsNew = false;
            ViewBag.start = DateTime.Now.AddDays(-90);
            ViewBag.end = DateTime.Now;
            ViewBag.PagedSoilAnalysis = farm.SoilAnalysis.ToPagedList(1, 6);
            
            ViewBag.SoilTypes = _soilTypeManager.GetAll();
            ViewBag.PagedFertilizers = farm.Fertilizers.ToPagedList(1, FertilizersController.PERPAGE);
            ViewBag.PagedActivities = farm.OtherActivities.ToPagedList(1, FarmOtherActivitiesController.PERPAGE);
            ViewBag.PagedFamilyUnitMembers = farm.FamilyUnitMembers.ToPagedList(1, FamilyUnitMembersController.PERPAGE);
            ViewBag.Lots = new SelectList(_lotManager.GetAll(), "Id", "Code", string.Empty);
            ViewBag.PagedInvoices = _invoicemanager.GetAllByFarm(id, null, null, null).ToPagedList(1, 10);


            double prodEsManual = 0;

            foreach (PlantationDTO p in farm.Productivity.Plantations)
            {
                decimal densityValue;
                var culture = System.Globalization.CultureInfo.InvariantCulture;

                if (!string.IsNullOrWhiteSpace(p.Density) &&
                    decimal.TryParse(p.Density.Replace(",", "."), System.Globalization.NumberStyles.Any, culture, out densityValue) &&
                    densityValue > 0)
                {
                    PlantationDTO pl = calcularProdEstimada(p);

                    var plantationToUpdate = farm.Productivity.Plantations.FirstOrDefault(x => x.Id == p.Id);
                    if (plantationToUpdate != null)
                    {
                        plantationToUpdate.Density = pl.Density;
                        plantationToUpdate.EstimatedProduction = pl.EstimatedProduction;
                    }
                }

                // Suma segura de EstimatedProductionManual o EstimatedProduction
                if (!string.IsNullOrWhiteSpace(p.EstimatedProductionManual))
                {
                    if (double.TryParse(p.EstimatedProductionManual, out double manualProd))
                    {
                        prodEsManual += manualProd;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(p.EstimatedProduction))
                {
                    if (double.TryParse(p.EstimatedProduction, out double estimatedProd))
                    {
                        prodEsManual += estimatedProd;
                    }
                }
            }

            _farmManager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);

            ViewBag.PagedPlantations = farm.Productivity.Plantations.ToPagedList(1, 10);

            ViewBag.ProdEstManual = prodEsManual;
            //MapAssociatedPeople(farm2);
            //Thread.Sleep(4000);
            return View(farm);
        }


        public void UpdateProductivity()
        {
            UnitOfWork db = new UnitOfWork();

            var fincas = db.Farms.ToList();


            foreach (var finca in fincas)
            {

                var productivitiesChange2 = db.Productivities.FirstOrDefault(f => f.Id == finca.Id);

                decimal totalHectareas = 0;
                decimal totalHectareas_SumPerc = 0;
                decimal totalHectareasCoffe = 0;


                if (productivitiesChange2 != null)
                {


                    if (productivitiesChange2.Plantations.Count > 0)
                    {
                        foreach (var p in productivitiesChange2.Plantations)
                        {
                            if (p.PlantationTypeId == new Guid("D221BEC9-5F73-43A0-9EBF-16417F5674F5"))
                            {
                                totalHectareasCoffe = totalHectareasCoffe + Convert.ToDecimal(p.Hectares);
                            }

                            totalHectareas = totalHectareas + Convert.ToDecimal(p.Hectares);
                        }
                    }

                    double averageDensity = 0;
                    double averageAge = 0;
                    string now = DateTime.Now.ToString();

                    var plants2 = productivitiesChange2.Plantations.Where(t => t.PlantationTypeId == new Guid("{D221BEC9-5F73-43A0-9EBF-16417F5674F5}"));

                    foreach (var plantation in plants2)
                    {
                        plantation.Hectares = plantation.Hectares.Replace(".", ",");
                        plantation.EstimatedProduction = plantation.EstimatedProduction.Replace(".", ",");
                        double percentage = (Convert.ToDouble(plantation.Hectares) * 1.0) / Convert.ToDouble(totalHectareasCoffe);
                        TimeSpan dateAge = DateTime.Now.Subtract(plantation.Age);
                        double Age = (dateAge.Days * 1.0) / 365;

                        averageAge += Age * percentage;

                        averageDensity += (Convert.ToDouble(plantation.Density.Replace(".", ",")) * 1.0) * percentage;
                    }

                    if (averageAge.ToString() == "NaN")
                    {
                        averageAge = 0;
                    }
                    productivitiesChange2.coffeeArea = totalHectareasCoffe.ToString().Replace(".", ",");
                    productivitiesChange2.averageDensity = averageDensity.ToString();
                    productivitiesChange2.averageAge = Math.Round(averageAge, 3);
                    db.SaveChanges();

                    var farm = _farmManager.Details(finca.Id,
                        "Productivity",
                        "Contacts",
                        "Productivity.Plantations",
                        "Productivity.Plantations.FloweringPeriods",
                        "Worker",
                        "OtherActivities",
                        "SoilAnalysis",
                        "SoilTypes",
                        "Fertilizers",
                        "FamilyUnitMembers",
                        "ImpactAssessments",
                        "ImpactAssessments.Answers",
                        "Village",
                        "Village.Municipality",
                        "Village.Municipality.Department");



                    decimal infHct = farm.Productivity.InfrastructureHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.InfrastructureHectares);
                    decimal fpHct = farm.Productivity.ForestProtectedHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ForestProtectedHectares);
                    decimal conHct = farm.Productivity.ConservationHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ConservationHectares);
                    //decimal othHct = farm.Productivity.OthersHectareas == "" ? 0 : Convert.ToDecimal(farm.Productivity.OthersHectareas);

                    totalHectareas_SumPerc = totalHectareas;
                    totalHectareas = totalHectareas + infHct + fpHct + conHct;

                    if (farm.Productivity.TotalHectares != totalHectareas.ToString())
                    {
                        try
                        {
                            farm.Productivity.TotalHectares = totalHectareas.ToString();

                            var productivity = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
                            productivity.TotalHectares = totalHectareas.ToString();
                            db.SaveChanges();
                        }
                        catch
                        {
                            farm.Productivity.TotalHectares = totalHectareas.ToString();
                        }
                    }

                    var idFarm = farm.Id;

                    //PORCENTAJE COLOMBIA
                    var PerColombia = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7"));
                    decimal sumPerColombia = 0;
                    foreach (var item in PerColombia)
                    {
                        sumPerColombia = sumPerColombia + Convert.ToDecimal(item.Hectares);
                    }

                    //PORCENTAJE CATURRA
                    var PerCaturra = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"));
                    decimal sumPerCaturra = 0;
                    foreach (var item in PerCaturra)
                    {
                        sumPerCaturra = sumPerCaturra + Convert.ToDecimal(item.Hectares);
                    }

                    //PORCENTAJE CASTILLO
                    var PerCastillo = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C"));
                    decimal sumPerCastillo = 0;
                    foreach (var item in PerCastillo)
                    {
                        sumPerCastillo = sumPerCastillo + Convert.ToDecimal(item.Hectares);
                    }

                    //PORCENTAJE OTRO
                    var PerOtro = db.Plantations.Where(x => x.ProductivityId == idFarm);
                    decimal sumPerOtro = 0;
                    foreach (var item in PerOtro)
                    {
                        if (item.PlantationVarietyId != new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7") && item.PlantationVarietyId != new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C") && item.PlantationVarietyId != new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"))
                        {
                            sumPerOtro = sumPerOtro + Convert.ToDecimal(item.Hectares);
                        }
                    }

                    //OPERACIONES PORCENTAJES
                    var opeColombia = Convert.ToDecimal(0);
                    if (sumPerColombia != 0 && totalHectareasCoffe != 0)
                    {
                        opeColombia = (sumPerColombia / (totalHectareas_SumPerc)) * 100;
                        if (opeColombia > 100)
                        {
                            opeColombia = 100;
                        }
                    }

                    var opeCaturra = Convert.ToDecimal(0);
                    if (sumPerCaturra != 0 && totalHectareasCoffe != 0)
                    {
                        opeCaturra = (sumPerCaturra / (totalHectareas_SumPerc)) * 100;
                        if (opeCaturra > 100)
                        {
                            opeCaturra = 100;
                        }
                    }

                    var opeCastillo = Convert.ToDecimal(0);
                    if (sumPerCastillo != 0 && totalHectareasCoffe != 0)
                    {
                        opeCastillo = (sumPerCastillo / (totalHectareas_SumPerc)) * 100;
                        if (opeCastillo > 100)
                        {
                            opeCastillo = 100;
                        }
                    }

                    var opeOtro = Convert.ToDecimal(0);
                    if (sumPerOtro != 0 && totalHectareasCoffe != 0)
                    {
                        opeOtro = (sumPerOtro / (totalHectareas_SumPerc)) * 100;
                        if (opeOtro > 100)
                        {
                            opeOtro = 100;
                        }
                    }
                    else if (sumPerOtro != 0 && totalHectareasCoffe == 0)
                    {
                        opeOtro = (sumPerOtro / sumPerOtro) * 100;
                        if (opeOtro > 100)
                        {
                            opeOtro = 100;
                        }
                    }


                    //ACTUALIZACION
                    var sum = opeColombia + opeCaturra + opeCastillo + opeOtro;
                    var dif = 0.0;
                    if (sum > 100)
                    {
                        dif = Convert.ToDouble(sum) - Convert.ToDouble(100);
                        if (Math.Round(Convert.ToDecimal(opeOtro), 2) != Math.Round(Convert.ToDecimal(dif), 2))
                        {
                            opeOtro = Convert.ToDecimal(opeOtro) - Convert.ToDecimal(dif);
                        }
                    }
                    else if (opeColombia == 0 && opeCaturra == 0 && opeCastillo == 0 && opeOtro != 0)
                    {
                        opeOtro = 100;
                    }


                    var productivitiesChange = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
                    productivitiesChange.percentageColombia = Convert.ToDouble(opeColombia);
                    productivitiesChange.percentageCaturra = Convert.ToDouble(opeCaturra);
                    productivitiesChange.percentageCastillo = Convert.ToDouble(opeCastillo);
                    productivitiesChange.percentageotra = Convert.ToDouble(opeOtro);
                    productivitiesChange.UpdatedAt = DateTime.Now;
                    db.SaveChanges();




                    var farm2 = _farmManager.Details(finca.Id,
                        "Productivity",
                        "Contacts",
                        "Productivity.Plantations",
                        "Productivity.Plantations.FloweringPeriods",
                        "Worker",
                        "OtherActivities",
                        "SoilAnalysis",
                        "SoilTypes",
                        "Fertilizers",
                        "FamilyUnitMembers",
                        "ImpactAssessments",
                        "ImpactAssessments.Answers",
                        "Village",
                        "Village.Municipality",
                        "Village.Municipality.Department");


                    foreach (PlantationDTO p in farm.Productivity.Plantations)
                    {
                        if (p.Density != null && !p.Density.Equals(string.Empty) && Convert.ToDecimal(p.Density) > 0)
                        {
                            PlantationDTO pl = farm.Productivity.Plantations.Where(x => x.Id == p.Id).FirstOrDefault();

                            farm.Productivity.Plantations.Where(x => x.Id == p.Id).FirstOrDefault().Density = calcularProdEstimada(p).Density;

                            _farmManager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);
                            //pl.Density = calcularProdEstimada(p).Density;
                        }
                    }


                }

            }
            
        }
        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmDTO">The farm dto.</param>
        /// <param name="soilTypes">The soil types.</param>
        /// <returns>The view with farmdto</returns>
        [CustomAuthorize(Roles = "Technician,Sustainability")]
        [HttpPost]
        public ActionResult Edit(Guid id, FarmDTO farmDTO, Guid[] soilTypes)
        {
            if (soilTypes == null)
            {
                soilTypes = new Guid[] { };
            }
            try
            {
                MapSoilTypesToFarm(farmDTO, soilTypes);
                MapAssociatedPeople(farmDTO);
                _farmManager.Edit(id, farmDTO, FarmManager.FARMS);
                return RedirectToAction("Index");
            }
            catch
            {
                farmDTO.IsNew = false;
                return View(farmDTO);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The View</returns>
        [CustomAuthorize(Roles = "Technician,Sustainability")]
        public ActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }





        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>The View</returns>
        [CustomAuthorize(Roles = "Technician,Sustainability")]
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _farmManager.remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        /// <summary>
        /// Dashboards the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The view of the farm</returns>
        [HttpGet]
        [CustomAuthorize(Roles = "Technician,Sustainability,Reader")]
        public ActionResult Dashboard(Guid id)
        {
            ViewBag.FarmId = id;
            var farm = _farmManager.Details(id,
                "SupplyChain",
                "SupplyChain.Supplier",
                "SupplyChain.Supplier.Country",
                "Village",
                "Village.Municipality",
                "Village.Municipality.Department",
                "Productivity",
                "Productivity.Plantations",
                "FamilyUnitMembers");
            farm = _farmManager.CalculateDensity(farm);
            farm = _farmManager.CalculateFertilizer(farm);
            farm = _farmManager.CalculateProductivity(farm);
            //farm = _farmManager.CalculateAge(farm);
            return View(farm);
        }

        /// <summary>
        /// Exports the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>as pdf</returns>
        [HttpGet]
        [CustomAuthorize(Roles = "Technician,Sustainability,Reader")]
        public ActionResult Export(Guid id)
        {
            ViewBag.FarmId = id;
            var farm = _farmManager.Details(id,
                "SupplyChain",
                "SupplyChain.Supplier",
                "SupplyChain.Supplier.Country",
                "Village",
                "Village.Municipality",
                "Village.Municipality.Department",
                "Productivity",
                "Productivity.Plantations",
                "FamilyUnitMembers");
            farm = _farmManager.CalculateDensity(farm);
            farm = _farmManager.CalculateFertilizer(farm);
            farm = _farmManager.CalculateProductivity(farm);
            //farm = _farmManager.CalculateAge(farm);
            return new Rotativa.ViewAsPdf(farm);
        }


        public ActionResult ExportPDF(string sortOrder, string currentName, string searchName, string currentDepartment,
            string searchDepartment, string currentMunicipality, string searchMunicipality,
            string searchVillage, string currentVillage, string currentCode, string searchCode,
            string searchFarmerIdentification, string currentFarmerIdentification,
            string searchFarmerName, string currentFarmerName, int? page)
        {
            var withFilter = (!string.IsNullOrEmpty(searchName)
                                || !string.IsNullOrEmpty(searchDepartment)
                                || !string.IsNullOrEmpty(searchMunicipality)
                                || !string.IsNullOrEmpty(searchVillage)
                                || !string.IsNullOrEmpty(searchCode)
                                || !string.IsNullOrEmpty(searchFarmerIdentification)
                                || !string.IsNullOrEmpty(searchFarmerName));
            if (withFilter)
            {
                page = 1;
            }
            else
            {
                searchName = currentName;
                searchCode = currentCode;
                searchMunicipality = currentMunicipality;
                searchDepartment = currentDepartment;
                searchVillage = currentVillage;
                searchFarmerName = currentFarmerName;
                searchFarmerIdentification = currentFarmerIdentification;
            }
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CodeSortParm = sortOrder == "Code" ? "code_desc" : "Code";
            ViewBag.CurrentName = searchName;
            ViewBag.CurrentCode = searchCode;
            ViewBag.CurrentFarmerName = searchFarmerName;
            ViewBag.CurrentFarmerIdentification = searchFarmerIdentification;
            ViewBag.CurrentVillage = searchVillage;
            ViewBag.CurrentMunicipality = searchMunicipality;
            ViewBag.CurrentDepartment = searchDepartment;
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            var villageId = string.IsNullOrEmpty(searchVillage) ? Guid.Empty : Guid.Parse(searchVillage);
            var municipalityId = string.IsNullOrEmpty(searchMunicipality) ? Guid.Empty : Guid.Parse(searchMunicipality);
            var departmentId = string.IsNullOrEmpty(searchDepartment) ? Guid.Empty : Guid.Parse(searchDepartment);

            //IPagedList<Farm> farms;

            var farms = _farmManager
                .GetAllQueryable(FarmSpecification.FilterWithFarmerInfo(searchCode, searchName, Guid.Empty, Guid.Empty, Guid.Empty, villageId, municipalityId, departmentId, searchFarmerName),
                                        f => f.Code);

            List<Farm> ListFarms = new List<Farm>();
            List<Farm> ListFarmsGroupby = new List<Farm>();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();

            foreach (var FarmProject in ListProjectsUser)
            {
                foreach (var AssociatedFarm in FarmProject.Farms)
                {
                    ListFarmsGroupby.Add(new Farm
                    {
                        Id = AssociatedFarm.Id,
                        Code = AssociatedFarm.Code,
                        Name = AssociatedFarm.Name
                    });
                }
            }

            var FarmsSelect = ListFarmsGroupby.GroupBy(g => g.Id).Select(s => new { Id = s.Key, Code = s.FirstOrDefault().Code, Name = s.FirstOrDefault().Name }).ToList();

            switch (sortOrder)
            {
                case "name_desc":
                    farms = farms.OrderByDescending(s => s.Name);
                    break;
                case "Code":
                    farms = farms.OrderByDescending(s => s.Code);
                    break;
                case "code_desc":
                    farms = farms.OrderBy(s => s.Code);
                    break;
                default:
                    farms = farms.OrderBy(s => s.Name);
                    break;
            }

            if (ListProjectsUser.Count() == 0)
            {
                if (withFilter)
                {
                    ViewBag.filter = 1;
                    return View("SummaryFarms", farms.ToPagedList(pageNumber, pageSize));
                }
                else {
                    return View("SummaryFarms");
                }
                
            }
            else
            {
                foreach (var farm in farms)
                {
                    foreach (var FarmItem in FarmsSelect)
                    {
                        if (farm.Id == FarmItem.Id)
                        {
                            ListFarms.Add(farm);
                        }
                    }
                }
            }

            if (withFilter)
            {
                ViewBag.filter = 1;
                return View("SummaryFarms", ListFarms.ToPagedList(pageNumber, pageSize));
            }
            else {
                return View("SummaryFarms");
            }
               
            //return View("SummaryFarms");
        }

        [HttpGet]
        [CustomAuthorize(Roles = "Admin,Technician,Sustainability,Reader")]
        public ActionResult ExportPDFSummaryFarms(Guid code)
        {
            ViewBag.FarmId = code;
            var farm = _farmManager.Details(code,
                "SupplyChain",
                "SupplyChain.Supplier",
                "SupplyChain.Supplier.Country",
                "Village",
                "Village.Municipality",
                "Village.Municipality.Department",
                "Productivity",
                "Productivity.Plantations",
                "FamilyUnitMembers");
            farm = _farmManager.CalculateDensity(farm);
            farm = _farmManager.CalculateFertilizer(farm);
            farm = _farmManager.CalculateProductivity(farm);
            //farm = _farmManager.CalculateAge(farm);
            return new Rotativa.ViewAsPdf(farm);
        }


        public ActionResult ActualizarProdEstimada(string filtrado)
        {
            List<Farm> fincas = new List<Farm>();

            if (filtrado != null)
            {
                List<string> filtro = new List<string>{ "10316491",
                    "10566666",
                    "1059356137",
                    "1061599393",
                    "10660297",
                    "10660877",
                    "1497516",
                    "25324905",
                    "25480733",
                    "25632442",
                    "25634780",
                    "34573190",
                    "34639804",
                    "34672069",
                    "4634582",
                    "4634902",
                    "4635165",
                    "4695453",
                    "4709144",
                    "4734706",
                    "4734768",
                    "4774808",
                    "76285034",
                    "321198",
                    "1364492",
                    "1426219",
                    "1426845",
                    "1431305",
                    "1434849",
                    "1462536",
                    "1462912",
                    "1463462",
                    "1470682",
                    "1473523",
                    "1476782",
                    "1478868",
                    "1483394",
                    "1487696",
                    "1489792",
                    "1497388",
                    "1503569",
                    "1503804",
                    "1504089",
                    "1504636",
                    "1522632",
                    "1522861",
                    "1522945",
                    "2472452",
                    "2617521",
                    "4384484",
                    "4464048",
                    "4525815",
                    "4555015",
                    "4614614",
                    "4614763",
                    "4618770",
                    "4618828",
                    "4620722",
                    "4626758",
                    "4627794",
                    "4634990",
                    "4640862",
                    "4640938",
                    "4640939",
                    "4641708",
                    "4641765",
                    "4643313",
                    "4643644",
                    "4643837",
                    "4643844",
                    "4644428",
                    "4644432",
                    "4645175",
                    "4645388",
                    "4645430",
                    "4645503",
                    "4645964",
                    "4646193",
                    "4663786",
                    "4664841",
                    "4665150",
                    "4665182",
                    "4665300",
                    "4666579",
                    "4666658",
                    "4668086",
                    "4668133",
                    "4672338",
                    "4672464",
                    "4672749",
                    "4672787",
                    "4672922",
                    "4674814",
                    "4675212",
                    "4675232",
                    "4675257",
                    "4675440",
                    "4677058",
                    "4677064",
                    "4677065",
                    "4677084",
                    "4677222",
                    "4677295",
                    "4677329",
                    "4678191",
                    "4687835",
                    "4687977",
                    "4687988",
                    "4691545",
                    "4691549",
                    "4691600",
                    "4691636",
                    "4695412",
                    "4695448",
                    "4695673",
                    "4696594",
                    "4696705",
                    "4698300",
                    "4700320",
                    "4708555",
                    "4708824",
                    "4708831",
                    "4708867",
                    "4708870",
                    "4708904",
                    "4708981",
                    "4709170",
                    "4717386",
                    "4717795",
                    "4720373",
                    "4738530",
                    "4739109",
                    "4741446",
                    "4741513",
                    "4741851",
                    "4741875",
                    "4741893",
                    "4741904",
                    "4742062",
                    "4742097",
                    "4742103",
                    "4742130",
                    "4742418",
                    "4751401",
                    "4751499",
                    "4751642",
                    "4751687",
                    "4751695",
                    "4751763",
                    "4752007",
                    "4767706",
                    "4773678",
                    "4773844",
                    "4774887",
                    "4778155",
                    "5227894",
                    "5280374",
                    "6073089",
                    "6212355",
                    "6272691",
                    "6336200",
                    "6372469",
                    "6392238",
                    "6421028",
                    "6458377",
                    "6458581",
                    "6459705",
                    "6460675",
                    "6464967",
                    "7520059",
                    "10290218",
                    "10291032",
                    "10301378",
                    "10315108",
                    "10315133",
                    "10315135",
                    "10315138",
                    "10315145",
                    "10315158",
                    "10340057",
                    "10340154",
                    "10345001",
                    "10360163",
                    "10370133",
                    "10521272",
                    "10523458",
                    "10529166",
                    "10530819",
                    "10531321",
                    "10542537",
                    "10546199",
                    "10565885",
                    "10565913",
                    "10565943",
                    "10565969",
                    "10660664",
                    "10750409",
                    "10750419",
                    "10751398",
                    "10751652",
                    "10751670",
                    "10751953",
                    "10753531",
                    "10755376",
                    "12365041",
                    "16585874",
                    "16697955",
                    "19334509",
                    "24482758",
                    "25269162",
                    "25278154",
                    "25278157",
                    "25279431",
                    "25298145",
                    "25324478",
                    "25344612",
                    "25337347",
                    "25337565",
                    "25337810",
                    "25339155",
                    "25340313",
                    "25344731",
                    "25349488",
                    "25396107",
                    "25396200",
                    "25399864",
                    "25401511",
                    "25405641",
                    "25414905",
                    "25416897",
                    "25416919",
                    "25416948",
                    "25416957",
                    "25417074",
                    "25417084",
                    "25417092",
                    "25417564",
                    "25420621",
                    "25420749",
                    "25420770",
                    "25420773",
                    "25420788",
                    "25423885",
                    "25428275",
                    "25466156",
                    "25467819",
                    "25467904",
                    "25469813",
                    "25482733",
                    "25482869",
                    "25610083",
                    "25611350",
                    "25611884",
                    "25612131",
                    "25632602",
                    "25632799",
                    "25632805",
                    "25632850",
                    "25633142",
                    "25705425",
                    "25705662",
                    "25707037",
                    "25713692",
                    "25713824",
                    "31929498",
                    "31970666",
                    "34365116",
                    "34559464",
                    "34565108",
                    "34570251",
                    "34615756",
                    "34638555",
                    "34640087",
                    "34659789",
                    "48573506",
                    "48573954",
                    "76000239",
                    "76000329",
                    "76001008",
                    "76001016",
                    "76001040",
                    "76010174",
                    "76024501",
                    "76024509",
                    "76026023",
                    "76029217",
                    "76029615",
                    "76029829",
                    "76030013",
                    "76080007",
                    "76110001",
                    "76110012",
                    "76110035",
                    "76110045",
                    "76110096",
                    "76110102",
                    "76190215",
                    "76120050",
                    "76170078",
                    "76170399",
                    "76170441",
                    "76170460",
                    "76175039",
                    "76175067",
                    "76175095",
                    "76175145",
                    "76190052",
                    "76190137",
                    "76190251",
                    "76190370",
                    "76200002",
                    "76200107",
                    "76200155",
                    "76200200",
                    "76211115",
                    "76214268",
                    "76215497",
                    "76215512",
                    "76215883",
                    "76219812",
                    "76219849",
                    "76227753",
                    "76227926",
                    "76227951",
                    "76227984",
                    "76227999",
                    "76228153",
                    "76228547",
                    "76228589",
                    "76228632",
                    "76228649",
                    "76229150",
                    "76229949",
                    "76235724",
                    "76237065",
                    "76237212",
                    "76237230",
                    "76237833",
                    "76237862",
                    "76238114",
                    "76238117",
                    "76238118",
                    "76238151",
                    "76238706",
                    "76238736",
                    "76238755",
                    "76240610",
                    "76241408",
                    "76241425",
                    "76241446",
                    "76245974",
                    "76245975",
                    "76247430",
                    "76258212",
                    "76258225",
                    "76258227",
                    "76258237",
                    "76258260",
                    "76258274",
                    "76258818",
                    "76258943",
                    "76259167",
                    "76262411",
                    "76263340",
                    "76265415",
                    "76265423",
                    "76265427",
                    "76265432",
                    "76265448",
                    "76265469",
                    "76265494",
                    "76265735",
                    "76266338",
                    "76266614",
                    "76266618",
                    "76266619",
                    "76267211",
                    "76267219",
                    "76267221",
                    "76267235",
                    "76267238",
                    "76267283",
                    "76267289",
                    "76268469",
                    "76268490",
                    "76269029",
                    "76269043",
                    "76269059",
                    "76272903",
                    "76272948",
                    "76272968",
                    "76276215",
                    "76276593",
                    "76282507",
                    "76282572",
                    "76282805",
                    "76282818",
                    "76282852",
                    "76282868",
                    "76285263",
                    "76285283",
                    "76285400",
                    "76288124",
                    "76288165",
                    "76288218",
                    "76290555",
                    "76291068",
                    "76291471",
                    "76291485",
                    "76291507",
                    "76291581",
                    "76291632",
                    "76291647",
                    "76291982",
                    "76292005",
                    "76292057",
                    "76292257",
                    "76292373",
                    "76293136",
                    "76293137",
                    "76293256",
                    "76293442",
                    "76293554",
                    "76293567",
                    "76293834",
                    "76294072",
                    "76294327",
                    "76294697",
                    "76294980",
                    "76296755",
                    "76296929",
                    "76297311",
                    "76299015",
                    "76299221",
                    "76299249",
                    "76299357",
                    "76299898",
                    "76300124",
                    "76300207",
                    "76300409",
                    "76302012",
                    "76302074",
                    "76305387",
                    "76305477",
                    "76307438",
                    "76309147",
                    "76309840",
                    "76310981",
                    "76311043",
                    "76311238",
                    "76311482",
                    "76311572",
                    "76312694",
                    "76312824",
                    "76312890",
                    "76313068",
                    "76313339",
                    "76314092",
                    "76314810",
                    "76315045",
                    "76315230",
                    "76316343",
                    "76317164",
                    "76317764",
                    "76317953",
                    "76318074",
                    "76318213",
                    "25338352",
                    "76319156",
                    "76322929",
                    "76323440",
                    "76326236",
                    "76326449",
                    "76327192",
                    "76328985",
                    "76330652",
                    "76331057",
                    "76332586",
                    "76333519",
                    "76334382",
                    "76335231",
                    "76335259",
                    "76335724",
                    "76336027",
                    "76336089",
                    "76338068",
                    "76343063",
                    "76350655",
                    "76350670",
                    "76350714",
                    "76350793",
                    "76350812",
                    "76351261",
                    "76351400",
                    "76351858",
                    "76351872",
                    "76352098",
                    "76352134",
                    "76352226",
                    "76352753",
                    "76356710",
                    "76356720",
                    "76356922",
                    "76357212",
                    "76357368",
                    "76357472",
                    "76357713",
                    "76358024",
                    "76453000",
                    "79299224",
                    "79822688",
                    "80414987",
                    "83224155",
                    "83233266",
                    "83233423",
                    "87740021",
                    "89003821",
                    "89003822",
                    "94055007",
                    "94252245",
                    "94255132",
                    "94386110",
                    "94422104",
                    "97435026",
                    "98146277",
                    "98322250",
                    "1007201737",
                    "1058963418",
                    "1060102342",
                    "1060105036",
                    "1060357775",
                    "1060796300",
                    "1060868171",
                    "1060868678",
                    "1060868821",
                    "1060869104",
                    "1060872213",
                    "1061218275",
                    "1061220471",
                    "1061689723",
                    "1061702774",
                    "1061708498",
                    "1061711076",
                    "1061764216",
                    "1061984714",
                    "1062280443",
                    "1062280654",
                    "1085903220",
                    "1113644848",
                    "4717721",
                    "76351461",
                    "10340118",
                    "4672901",
                    "25313074",
                    "76292574",
                    "48607951",
                    "31468701",
                    "25295988",
                    "34555449",
                    "34658996",
                    "34523074",
                    "1059357825",
                    "76266610",
                    "25312981",
                    "31842692",
                    "4673205",
                    "10566513",
                    "87245630",
                    "34541788",
                    "48614471",
                    "76338048",
                    "4776459",
                    "76000180",
                    "4668750",
                    "25485654",
                    "4668421",
                    "25270715",
                    "25339001",
                    "1063809870",
                    "25492669",
                    "1060876719",
                    "25467801",
                    "10750844",
                    "4752119",
                    "52848336",
                    "4620111",
                    "25310434",
                    "26473359",
                    "76291175",
                    "4695825",
                    "1061598833",
                    "1061737571",
                    "25482073",
                    "76294178",
                    "48653769",
                    "76292078",
                    "34564340",
                    "10567085",
                    "10567897",
                    "4696920",
                    "10307635",
                    "1061535955",
                    "25480311",
                    "25543661",
                    "43576662",
                    "12277147",
                    "10751273",
                    "94279357",
                    "4634228",
                    "4720774",
                    "10753669",
                    "76333976",
                    "34569414",
                    "1456656",
                    "4663553",
                    "4664845",
                    "4666089",
                    "4666172",
                    "4666542",
                    "4666586",
                    "4672971",
                    "4742019",
                    "6709578",
                    "9735487",
                    "10292299",
                    "10300596",
                    "10753244",
                    "10754342",
                    "10755687",
                    "10757120",
                    "16838349",
                    "25294995",
                    "25296873",
                    "25328428",
                    "25396256",
                    "25396319",
                    "25396423",
                    "25398169",
                    "25399929",
                    "25401439",
                    "25415094",
                    "25415496",
                    "25610428",
                    "25611580",
                    "48573540",
                    "48573872",
                    "48671848",
                    "59121369",
                    "70000565",
                    "76170084",
                    "76265403",
                    "76288155",
                    "76302727",
                    "1060533080",
                    "1060869969",
                    "1061529143",
                    "1061535607",
                    "4664300",
                    "34640373",
                    "4752416",
                    "34671606",
                    "76285281",
                    "76236295",
                    "34659820",
                    "12998993",
                    "4722099",
                    "10316897",
                    "76211102",
                    "34495559",
                    "76235963",
                    "4697245",
                    "25492455",
                    "25275682",
                    "10540675",
                    "10751715",
                    "4946533",
                    "4667222",
                    "25556932",
                    "25492515",
                    "34561523",
                    "4651370",
                    "10527272",
                    "10566135",
                    "34327971",
                    "18611700",
                    "4666189",
                    "25394891",
                    "25422759",
                    "25394002",
                    "76292637",
                    "36283839",
                    "10751194",
                    "76030127",
                    "25422645",
                    "25424007",
                    "16585850",
                    "76222286",
                    "10315078",
                    "76218640",
                    "66920442",
                    "4696930",
                    "76247401",
                    "76240552",
                    "10753536",
                    "4644792",
                    "25485742",
                    "94309916",
                    "4751359",
                    "48574330",
                    "53073599",
                    "10497550",
                    "25422798",
                    "34340141",
                    "38681246",
                    "25311424",
                    "25559612",
                    "76259319",
                    "4634589",
                    "4672712",
                    "4673229",
                    "4729172",
                    "4770667",
                    "4778333",
                    "10315889",
                    "10565064",
                    "10752956",
                    "10754890",
                    "25272324",
                    "25272440",
                    "25344560",
                    "25455236",
                    "25478865",
                    "27379822",
                    "31961506",
                    "34365245",
                    "34638883",
                    "34658739",
                    "48571583",
                    "48651915",
                    "76024507",
                    "76029314",
                    "76237858",
                    "76291909",
                    "76317339",
                    "76330338",
                    "76334464",
                    "76334978",
                    "76335501",
                    "1060800128",
                    "1061017304",
                    "1061599751",
                    "1061709813",
                    "1061749036",
                    "1063806727",
                    "4750987",
                    "48572475",
                    "76259115",
                    "25611425",
                    "76027987",
                    "4696789",
                    "10753148",
                    "4675704",
                    "4720700",
                    "4774820",
                    "66908596",
                    "10420108",
                    "25275907",
                    "25396249",
                    "25424779",
                    "25605887",
                    "34676674",
                    "76259320",
                    "76302922",
                    "1061223011",
                    "1061598978",
                    "1061696055",
                    "1094912286",
                    "4695698",
                    "4696810",
                    "25556918",
                    "25633475",
                    "25690294",
                    "93123895",
                    "94283305",
                    "395486",
                    "4666788",
                    "4688163",
                    "4739487",
                    "4742155",
                    "4751718",
                    "4767206",
                    "6455826",
                    "7523188",
                    "10317029",
                    "10455129",
                    "10538735",
                    "10566223",
                    "10567086",
                    "10593921",
                    "10594186",
                    "12277630",
                    "4778267",
                    "25292540",
                    "25299044",
                    "25417135",
                    "25480359",
                    "25634192",
                    "31966512",
                    "34495811",
                    "34538363",
                    "34551959",
                    "34652502",
                    "34658847",
                    "34770022",
                    "39789448",
                    "48575907",
                    "76268482",
                    "76273006",
                    "76285067",
                    "94375595",
                    "98396824",
                    "1058964480",
                    "1058964603",
                    "1058965355",
                    "1058970972",
                    "1060871381",
                    "1060990077",
                    "1061532253",
                    "1061599808",
                    "1061702588",
                    "1061720349",
                    "1115941417",
                    "34545957",
                    "10567329",
                    "25283675",
                    "25480748",
                    "25700214",
                    "34331651",
                    "48571475",
                    "48655534",
                    "48659807",
                    "25344852",
                    "34365259",
                    "1061599275",
                    "1523493",
                    "4645834",
                    "76306814",
                    "25483603",
                    "4751368",
                    "4770740",
                    "10566561",
                    "10566673",
                    "10567278",
                    "10754417",
                    "18110073",
                    "25290776",
                    "25422568",
                    "25477711",
                    "25479347",
                    "25479423",
                    "25484637",
                    "25634384",
                    "25702017",
                    "26527185",
                    "34325904",
                    "34700185",
                    "76293975",
                    "76294272",
                    "76334165",
                    "1002846743",
                    "1060986204",
                    "1061599870",
                    "1061737763",
                    "1061986624",
                    "48644777",
                    "10585394",
                    "25285659",
                    "25289537",
                    "34323847",
                    "34325311",
                    "48633901",
                    "1081411425",
                    "76269051",
                    "76318324",
                    "4641283",
                    "4750800",
                    "10585221",
                    "10660317",
                    "16823825",
                    "25484627",
                    "25490507",
                    "31254397",
                    "48676532",
                    "1003163524",
                    "4668622",
                    "25633199",
                    "94282940",
                    "25485988",
                    "25492063",
                    "29820985",
                    "76304377",
                    "76330097",
                    "4622355" };
                fincas = db.Farms.Where(f => filtro.Contains(f.Code)).ToList();
            }
            else
            {
                fincas = db.Farms.ToList();
            }


            foreach (var f in fincas)
            {
                var farm = _farmManager.Details(f.Id,
                "Productivity",
                "Contacts",
                "Productivity.Plantations",
                "Productivity.Plantations.FloweringPeriods",
                "Worker",
                "OtherActivities",
                "SoilAnalysis",
                "SoilTypes",
                "Fertilizers",
                "FamilyUnitMembers",
                "ImpactAssessments",
                "ImpactAssessments.Answers",
                "Village",
                "Village.Municipality",
                "Village.Municipality.Department");

                foreach (PlantationDTO p in farm.Productivity.Plantations)
                {
                    if (p.Density != null && !p.Density.Equals(string.Empty) && Convert.ToDecimal(p.Density) > 0)
                    {
                        PlantationDTO pl = calcularProdEstimada(p);

                        farm.Productivity.Plantations.Where(x => x.Id == p.Id).FirstOrDefault().Density = pl.Density;
                        farm.Productivity.Plantations.Where(x => x.Id == p.Id).FirstOrDefault().EstimatedProduction = pl.EstimatedProduction;

                        
                        //pl.Density = calcularProdEstimada(p).Density;
                    }
                }

                _farmManager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);
            }

            

            return RedirectToAction("Index");
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ActualizarProductividadMasivo(int? year, int? month, int? day)
        {
            DateTime fechaX;

            if (!year.HasValue && !month.HasValue && !day.HasValue)
            {
                fechaX = new DateTime(2021, 8, 4);
            }
            else
            {
                fechaX = new DateTime(year.Value, month.Value, day.Value);
            }
            
            var fincas = db.Farms.Where(f => f.Productivity.UpdatedAt.Value > fechaX).ToList();

            foreach (var f in fincas)
            {
                UnitOfWork db = new UnitOfWork();


                var productivitiesChange2 = db.Productivities.FirstOrDefault(fa => fa.Id == f.Id);

                decimal totalHectareas = 0;
                decimal totalHectareas_SumPerc = 0;
                decimal totalHectareasCoffe = 0;


                if (productivitiesChange2.Plantations.Count > 0)
                {
                    foreach (var p in productivitiesChange2.Plantations)
                    {
                        if (p.PlantationTypeId == new Guid("D221BEC9-5F73-43A0-9EBF-16417F5674F5"))
                        {
                            totalHectareasCoffe = totalHectareasCoffe + Convert.ToDecimal(p.Hectares);
                        }

                        totalHectareas = totalHectareas + Convert.ToDecimal(p.Hectares);
                    }
                }

                double averageDensity = 0;
                double averageAge = 0;
                string now = DateTime.Now.ToString();

                var plants2 = productivitiesChange2.Plantations.Where(t => t.PlantationTypeId == new Guid("{D221BEC9-5F73-43A0-9EBF-16417F5674F5}"));

                foreach (var plantation in plants2)
                {
                    plantation.Hectares = plantation.Hectares.Replace(".", ",");
                    plantation.EstimatedProduction = plantation.EstimatedProduction.Replace(".", ",");
                    double percentage = (Convert.ToDouble(plantation.Hectares) * 1.0) / Convert.ToDouble(totalHectareasCoffe);
                    TimeSpan dateAge = DateTime.Now.Subtract(plantation.Age);
                    double Age = (dateAge.Days * 1.0) / 365;

                    averageAge += Age * percentage;

                    averageDensity += (Convert.ToDouble(plantation.Density.Replace(".", ",")) * 1.0) * percentage;
                }

                if (averageAge.ToString() == "NaN")
                {
                    averageAge = 0;
                }
                productivitiesChange2.coffeeArea = totalHectareasCoffe.ToString().Replace(".", ",");
                productivitiesChange2.averageDensity = averageDensity.ToString();
                productivitiesChange2.averageAge = Math.Round(averageAge, 3);
                db.SaveChanges();

                var farm = _farmManager.Details(f.Id,
                    "Productivity",
                    "Contacts",
                    "Productivity.Plantations",
                    "Productivity.Plantations.FloweringPeriods",
                    "Worker",
                    "OtherActivities",
                    "SoilAnalysis",
                    "SoilTypes",
                    "Fertilizers",
                    "FamilyUnitMembers",
                    "ImpactAssessments",
                    "ImpactAssessments.Answers",
                    "Village",
                    "Village.Municipality",
                    "Village.Municipality.Department");



                decimal infHct = farm.Productivity.InfrastructureHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.InfrastructureHectares);
                decimal fpHct = farm.Productivity.ForestProtectedHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ForestProtectedHectares);
                decimal conHct = farm.Productivity.ConservationHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ConservationHectares);
                //decimal othHct = farm.Productivity.OthersHectareas == "" ? 0 : Convert.ToDecimal(farm.Productivity.OthersHectareas);

                totalHectareas_SumPerc = totalHectareas;
                totalHectareas = totalHectareas + infHct + fpHct + conHct;

                if (farm.Productivity.TotalHectares != totalHectareas.ToString())
                {
                    try
                    {
                        farm.Productivity.TotalHectares = totalHectareas.ToString();

                        var productivity = db.Productivities.FirstOrDefault(fa => fa.Id == farm.Id);
                        productivity.TotalHectares = totalHectareas.ToString();
                        db.SaveChanges();
                    }
                    catch
                    {
                        farm.Productivity.TotalHectares = totalHectareas.ToString();
                    }
                }

                var idFarm = farm.Id;

                //PORCENTAJE COLOMBIA
                var PerColombia = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7"));
                decimal sumPerColombia = 0;
                foreach (var item in PerColombia)
                {
                    sumPerColombia = sumPerColombia + Convert.ToDecimal(item.Hectares);
                }

                //PORCENTAJE CATURRA
                var PerCaturra = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"));
                decimal sumPerCaturra = 0;
                foreach (var item in PerCaturra)
                {
                    sumPerCaturra = sumPerCaturra + Convert.ToDecimal(item.Hectares);
                }

                //PORCENTAJE CASTILLO
                var PerCastillo = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C"));
                decimal sumPerCastillo = 0;
                foreach (var item in PerCastillo)
                {
                    sumPerCastillo = sumPerCastillo + Convert.ToDecimal(item.Hectares);
                }

                //PORCENTAJE OTRO
                var PerOtro = db.Plantations.Where(x => x.ProductivityId == idFarm);
                decimal sumPerOtro = 0;
                foreach (var item in PerOtro)
                {
                    if (item.PlantationVarietyId != new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7") && item.PlantationVarietyId != new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C") && item.PlantationVarietyId != new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"))
                    {
                        sumPerOtro = sumPerOtro + Convert.ToDecimal(item.Hectares);
                    }
                }

                //OPERACIONES PORCENTAJES
                var opeColombia = Convert.ToDecimal(0);
                if (sumPerColombia != 0 && totalHectareasCoffe != 0)
                {
                    opeColombia = (sumPerColombia / (totalHectareas_SumPerc)) * 100;
                    if (opeColombia > 100)
                    {
                        opeColombia = 100;
                    }
                }

                var opeCaturra = Convert.ToDecimal(0);
                if (sumPerCaturra != 0 && totalHectareasCoffe != 0)
                {
                    opeCaturra = (sumPerCaturra / (totalHectareas_SumPerc)) * 100;
                    if (opeCaturra > 100)
                    {
                        opeCaturra = 100;
                    }
                }

                var opeCastillo = Convert.ToDecimal(0);
                if (sumPerCastillo != 0 && totalHectareasCoffe != 0)
                {
                    opeCastillo = (sumPerCastillo / (totalHectareas_SumPerc)) * 100;
                    if (opeCastillo > 100)
                    {
                        opeCastillo = 100;
                    }
                }

                var opeOtro = Convert.ToDecimal(0);
                if (sumPerOtro != 0 && totalHectareasCoffe != 0)
                {
                    opeOtro = (sumPerOtro / (totalHectareas_SumPerc)) * 100;
                    if (opeOtro > 100)
                    {
                        opeOtro = 100;
                    }
                }
                else if (sumPerOtro != 0 && totalHectareasCoffe == 0)
                {
                    opeOtro = (sumPerOtro / sumPerOtro) * 100;
                    if (opeOtro > 100)
                    {
                        opeOtro = 100;
                    }
                }


                //ACTUALIZACION
                var sum = opeColombia + opeCaturra + opeCastillo + opeOtro;
                var dif = 0.0;
                if (sum > 100)
                {
                    dif = Convert.ToDouble(sum) - Convert.ToDouble(100);
                    if (Math.Round(Convert.ToDecimal(opeOtro), 2) != Math.Round(Convert.ToDecimal(dif), 2))
                    {
                        opeOtro = Convert.ToDecimal(opeOtro) - Convert.ToDecimal(dif);
                    }
                }
                else if (opeColombia == 0 && opeCaturra == 0 && opeCastillo == 0 && opeOtro != 0)
                {
                    opeOtro = 100;
                }


                var productivitiesChange = db.Productivities.FirstOrDefault(fa => fa.Id == farm.Id);
                productivitiesChange.percentageColombia = Convert.ToDouble(opeColombia);
                productivitiesChange.percentageCaturra = Convert.ToDouble(opeCaturra);
                productivitiesChange.percentageCastillo = Convert.ToDouble(opeCastillo);
                productivitiesChange.percentageotra = Convert.ToDouble(opeOtro);
                productivitiesChange.UpdatedAt = DateTime.Now;
                db.SaveChanges();

                

                farm.IsNew = false;
                

                double prodEsManual = 0;
                foreach (PlantationDTO p in farm.Productivity.Plantations)
                {
                    if (p.Density != null && !p.Density.Equals(string.Empty) && Convert.ToDecimal(p.Density) > 0)
                    {
                        PlantationDTO pl = calcularProdEstimada(p);

                        farm.Productivity.Plantations.Where(x => x.Id == p.Id).FirstOrDefault().Density = pl.Density;
                        farm.Productivity.Plantations.Where(x => x.Id == p.Id).FirstOrDefault().EstimatedProduction = pl.EstimatedProduction;

                        _farmManager.Edit(farm.Id, farm, FarmManager.PLANTATIONS);
                        //pl.Density = calcularProdEstimada(p).Density;
                    }

                    if (p.EstimatedProductionManual.Length > 0)
                    {
                        prodEsManual = prodEsManual + Convert.ToDouble(p.EstimatedProductionManual);
                    }
                    else
                    {
                        prodEsManual = prodEsManual + Convert.ToDouble(p.EstimatedProduction);
                    }
                }
            }


            return RedirectToAction("Index");
        }

        #region Private Methods
        /// <summary>
        /// Maps the associated people.
        /// </summary>
        /// <param name="farm">The farm.</param>
        //private void MapAssociatedPeople(FarmDTO farm)
        //{
        //    farm.AssociatedPeople = new List<UserDTO> { new UserDTO { Id = farm.CurrentTechnician, FirstName = User.FirstName, LastName = User.LastName } };
        //    _farmManager.Edit(farm.Id, farm, FarmManager.FARMS);
        //}

        private void MapAssociatedPeople(FarmDTO farm)
        {
            farm.AssociatedPeople = new List<UserDTO> { new UserDTO { Id = farm.CurrentTechnician } };
        }

        /// <summary>
        /// Maps the soil types to farm.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="soilTypes">The soil types.</param>
        private void MapSoilTypesToFarm(FarmDTO farm, Guid[] soilTypes)
        {
            farm.SoilTypes = soilTypes.Select(st => new SoilTypeDTO { Id = st }).ToList();
        }

        /// <summary>
        /// Maps the soil analysis to farm.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="soilAnalysis">The soil analysis.</param>
        private void MapSoilAnalysisToFarm(FarmDTO farm, Guid[] soilAnalysis)
        {
            farm.SoilAnalysis = soilAnalysis.Select(sa => new SoilAnalysisDTO { Id = sa }).ToList();
        }
        #endregion

        private PlantationDTO calcularProdEstimada(PlantationDTO plantation)
        {
            try
            {


                List<TablaProduccion> tablaProd = db.ExecuteQuery<TablaProduccion>("TablaProdEstimada_Get").ToList();

                DateTime a = plantation.Age;
                //DateTime b = new DateTime(DateTime.Today.Year-1,12,31);
                DateTime b = DateTime.Today;

                TimeSpan span = b - a;

                double ageTemp = span.TotalDays / 365;

                int years = Convert.ToInt32(Math.Round(span.TotalDays / 365, 0));

                //int years = Math.Round(span / (1000 * 3600 * 24) / 365);
                if (ageTemp < 2)
                {
                    plantation.EstimatedProduction = "0";
                }
                else
                {
                    int edad = 0;

                    if (ageTemp >= 10)
                    {
                        edad = 10;
                    }
                    else
                    {
                        edad = years;
                    }

                    int conceptoDensidades = Convert.ToDecimal(plantation.Density) <= 6500 ? 1 : 2;

                    string estadoLote = ""; //plantation.PlantationStatusId.ToString().ToUpper().Equals("08DB007B-56EE-487E-BB48-129DC9F18A48") ? "B" : "RM";

                    if(plantation.PlantationStatusId.ToString().ToUpper().Equals("08DB007B-56EE-487E-BB48-129DC9F18A48"))
                    {
                        estadoLote = "B";
                    }
                    else if(plantation.PlantationStatusId.ToString().ToUpper().Equals("697c6775-8cc2-4fc8-bb03-74ed6181076c"))
                    {
                        estadoLote = "R";
                    }
                    else if(plantation.PlantationStatusId.ToString().ToUpper().Equals("315fb3f4-3690-4ff5-8382-c0a055466a38"))
                    {
                        estadoLote = "M";
                    }

                    var laFinca = db.Farms.Where(f => f.Id == plantation.ProductivityId).FirstOrDefault();
                    var vereda = db.Villages.Where(v => v.Id == laFinca.VillageId).FirstOrDefault();

                    var municipio = db.Municipalities.Where(f => f.Id == vereda.MunicipalityId).FirstOrDefault();

                    TablaProduccion factor = tablaProd.Where(r => r.Mun_Id == municipio.Id && r.Concepto_densidades == conceptoDensidades && r.Edad == edad && r.Estado.Equals(estadoLote)).FirstOrDefault();

                    if (factor == null)
                    {
                        plantation.EstimatedProduction = "0";
                        return plantation;
                    }

                    decimal cps = SafeDecimal(factor.CPS_KG_Arbol_1_eje_Cauca.ToString());
                    decimal numberOfPlants = SafeDecimal(plantation.NumberOfPlants.ToString());

                    decimal estimated = 0;

                    if (plantation.NumEjeArbLot == 1)
                    {
                        estimated = Math.Round(cps * numberOfPlants, 4);
                    }
                    else
                    {
                        if (conceptoDensidades == 1)
                        {
                            estimated = Math.Round(cps * numberOfPlants * 1.1m, 4);
                        }
                        else
                        {
                            estimated = Math.Round(cps * numberOfPlants, 4);
                        }

                        plantation.EstimatedProduction = estimated.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al calcular la producción estimada: " + ex.Message);
            }
            return plantation;
        }

        private class TablaProduccion
        {
            public Guid Mun_Id { get; set; }
            public decimal Den_Cua_Tri { get; set; }
            public decimal Concepto_densidades { get; set; }
            public string Estado { get; set; }
            public decimal Edad { get; set; }
            public decimal CPS_Cuadro_YARA { get; set; }
            public decimal CPS_Cuadro_Cauca { get; set; }
            public decimal CPS_Kg_ha_Cauca { get; set; }
            public decimal Porc_Producción { get; set; }
            public decimal CPS_KG_Arbol_1_eje_Cauca { get; set; }
        }
    }
}