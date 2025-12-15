using EFarming.Common;
using EFarming.Common.Resources;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.SustainabilityModule.ContactAggregate;
using EFarming.DAL;
using EFarming.DTO.AdminModule;
using EFarming.DTO.APIModule;
using EFarming.DTO.FarmModule;
using EFarming.DTO.ImpactModule;
using EFarming.DTO.ProjectModule;
using EFarming.DTO.QualityModule;
using EFarming.DTO.SustainabilityModule;
using EFarming.DTO.TraceabilityModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Integration.Controllers
{

    [RoutePrefix("farms")]
    public class FarmsController : ApiController
    {
        private IInvoiceManager _manager;
        private IFarmManager _farmManager;
        private IImpactManager _impactManager;
        private ISensoryProfileManager _sensoryProfileManager;
        private DateTime? start;
        private DateTime? end;
        private Guid? lotId;

        public FarmsController(
            IFarmManager farmManager,
            IImpactManager impactManager,
            ISensoryProfileManager sensoryProfileManager,
            IInvoiceManager manager)
        {
            _farmManager = farmManager;
            _impactManager = impactManager;
            _sensoryProfileManager = sensoryProfileManager;
            _manager = manager;
        }

        [HttpGet]
        [Route("list")]
        public HttpResponseMessage Search()
        {
            UnitOfWork db = new UnitOfWork();
            var result1 = db.Farms.Select(f => new FarmInformation {
                Id = f.Id,
                Name = f.Name,
                Code = f.Code,
                DepartmentId = f.Village.Municipality.Department.Id,
                MunicipalityId = f.Village.Municipality.Id,
                VillageId = f.VillageId,
                Farmer = f.FamilyUnitMembers.Where(p => p.Identification == f.Code).Select(n => n.FirstName + " " + n.LastName).FirstOrDefault()
            }).ToList();

            var res = Request.CreateResponse(HttpStatusCode.OK, result1);

            return res;
        }


        [HttpPost]
        [Route("search")]
        public HttpResponseMessage Search([FromBody]FarmSearchAPIDTO search)
        {
            UnitOfWork db = new UnitOfWork();
            var result1 = db.Farms.Where(x=> x.AssociatedPeople.Select(u => u.Id).FirstOrDefault() == search.UserId).ToList();

            if (search.Code != "")
            {
                result1 = result1.Where(f => f.Code.Contains(search.Code)).ToList();
            }
            else if (search.Name != "")
            {
                result1 = result1.Where(f => f.Name.ToLower().Contains(search.Name.ToLower())).ToList();
            }
            else
            {
                if (search.DepartmentId != new Guid("{00000000-0000-0000-0000-000000000000}"))
                    result1 = result1.Where(f => f.Village.Municipality.DepartmentId == search.DepartmentId).ToList();
                if (search.MunicipalityId != new Guid("{00000000-0000-0000-0000-000000000000}"))
                    result1 = result1.Where(f => f.Village.MunicipalityId == search.MunicipalityId).ToList();
                if (search.VillageId != new Guid("{00000000-0000-0000-0000-000000000000}"))
                    result1 = result1.Where(f => f.VillageId == search.VillageId).ToList();
            }
            //var result = _farmManager.GetAll(
            //                    FarmSpecification.Filter(
            //                                        search.Code,
            //                                        search.Name,
            //                                        null,
            //                                        null,
            //                                        null,
            //                                        search.VillageId,
            //                                        search.MunicipalityId,
            //                                        search.DepartmentId), f => f.Name)
            //                                        .Where(f => f.AssociatedPeople != null)
            //                                        .Select(f => new
            //                                        {
            //                                            code = f.Code,
            //                                            name = f.Name,
            //                                            id = f.Id,
            //                                            villageId = f.VillageId,
            //                                            municipalityId = f.Village.MunicipalityId,
            //                                            departmentId = f.Village.Municipality.DepartmentId,
            //                                            userId = f.AssociatedPeople.Select(u => u.Id),
            //                                        });

            var resultToSend = result1.Where(f => f.AssociatedPeople != null)
                    .Select(f => new
                    {
                        code = f.Code,
                        name = f.Name,
                        id = f.Id,
                        villageId = f.VillageId,
                        municipalityId = f.Village.MunicipalityId,
                        departmentId = f.Village.Municipality.DepartmentId,
                        userId = f.AssociatedPeople.Select(u => u.Id)
                    }).ToList();
            resultToSend = resultToSend.Where(f => f.userId.Contains(search.UserId)).ToList();
            //result = result.Where(f => f.userId.Contains(search.UserId));
            var res = Request.CreateResponse(HttpStatusCode.OK, resultToSend);

            return res;
        }

        [HttpGet]
        [Route("bycode/{code}")]
        public HttpResponseMessage ByCode(string code)
        {
            var farm = _farmManager.ByFamilyMemberIdentification(code, "FamilyUnitMembers");

            if (farm == null)
            {
                farm = _farmManager.GetAll(FarmSpecification.ByExactCode(code),
                    f => f.Code).FirstOrDefault();
                if (farm == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Finca no encontrada");
                }
            }

            for(int i=0;i< farm.AssociatedPeople.Count;i++)
            {
                farm.AssociatedPeople[i].Password = "";
                farm.AssociatedPeople[i].Salt = "";
            }
            
            

            return Request.CreateResponse(HttpStatusCode.OK, farm);
        }

        [HttpGet]
        [Route("sync/{id}")]
        public HttpResponseMessage Sync(Guid id)
        {
            UnitOfWork db = new UnitOfWork();

            //id = new Guid("bfa9694f-88d6-4bb3-ade6-54c8b70aa0fb");

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
                plantation.EstimatedProductionManual = plantation.EstimatedProductionManual == null ? plantation.EstimatedProductionManual : plantation.EstimatedProductionManual.Replace(".", ",");
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
            productivitiesChange2.averageDensity = averageDensity.ToString();
            productivitiesChange2.averageAge = Math.Round(averageAge, 3);
            db.SaveChanges();

            decimal infHct = productivitiesChange2.InfrastructureHectares == "" ? 0 : Convert.ToDecimal(productivitiesChange2.InfrastructureHectares);
            decimal fpHct = productivitiesChange2.ForestProtectedHectares == "" ? 0 : Convert.ToDecimal(productivitiesChange2.ForestProtectedHectares);
            decimal conHct = productivitiesChange2.ConservationHectares == "" ? 0 : Convert.ToDecimal(productivitiesChange2.ConservationHectares);

            totalHectareas_SumPerc = totalHectareas;
            totalHectareas = totalHectareas + infHct + fpHct + conHct;

            if (productivitiesChange2.TotalHectares != totalHectareas.ToString())
            {
                
                    var productivity = db.Productivities.FirstOrDefault(f => f.Id == id);
                    productivity.TotalHectares = totalHectareas.ToString();
                    db.SaveChanges();
            }

            //PORCENTAJE COLOMBIA
            var PerColombia = db.Plantations.Where(x => x.ProductivityId == id && x.PlantationVarietyId == new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7"));
            decimal sumPerColombia = 0;
            foreach (var item in PerColombia)
            {
                sumPerColombia = sumPerColombia + Convert.ToDecimal(item.Hectares);
            }

            //PORCENTAJE CATURRA
            var PerCaturra = db.Plantations.Where(x => x.ProductivityId == id && x.PlantationVarietyId == new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"));
            decimal sumPerCaturra = 0;
            foreach (var item in PerCaturra)
            {
                sumPerCaturra = sumPerCaturra + Convert.ToDecimal(item.Hectares);
            }

            //PORCENTAJE CASTILLO
            var PerCastillo = db.Plantations.Where(x => x.ProductivityId == id && x.PlantationVarietyId == new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C"));
            decimal sumPerCastillo = 0;
            foreach (var item in PerCastillo)
            {
                sumPerCastillo = sumPerCastillo + Convert.ToDecimal(item.Hectares);
            }

            //PORCENTAJE OTRO
            var PerOtro = db.Plantations.Where(x => x.ProductivityId == id);
            decimal sumPerOtro = 0;
            foreach (var item in PerOtro)
            {
                if (item.PlantationVarietyId == new Guid("3A643A6E-A64C-4305-B2D9-01D147A5F926") || item.PlantationVarietyId == new Guid("14ed528a-7ec4-49cd-9efb-bc2a42091b54"))
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

            var productivitiesChange = db.Productivities.FirstOrDefault(f => f.Id == id);
            productivitiesChange.percentageColombia = Convert.ToDouble(opeColombia);
            productivitiesChange.percentageCaturra = Convert.ToDouble(opeCaturra);
            productivitiesChange.percentageCastillo = Convert.ToDouble(opeCastillo);
            productivitiesChange.percentageotra = Convert.ToDouble(opeOtro);
            productivitiesChange.UpdatedAt = DateTime.Now;
            db.SaveChanges();

            var result = _farmManager.Details(id,
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

            result.Images = new List<ImageDTO>();

            foreach (var item in result.AssociatedPeople)
            {
                item.Password = "";
                item.Salt = "";
            }

            List <ProjectDTO> Projects = new List<ProjectDTO>();
            var projects = db.Farms.Find(id).Projects;
            foreach (var project in projects)
            {
                ProjectDTO projectdto = new ProjectDTO();
                projectdto.Id = project.Id;
                projectdto.Name = project.Name;
                projectdto.Description = project.Description;
                Projects.Add(projectdto);
            }
            result.Projects = Projects;

            var InvoicesList = result.Invoices;
            result.SupplyChain.Name = result.SupplyChain.Supplier.Name + " " + result.SupplyChain.Name;

            var InvoicesWet = InvoicesList.Where(t => t.CoffeeTypeId == 6).Select(o => new InvoiceDTO
            {
                Id = o.Id,
                Cash = o.Cash,
                CoffeeTypeId = o.CoffeeTypeId,
                CreatedAt = o.CreatedAt,
                Date = o.Date,
                DateInvoice = o.DateInvoice,
                FarmId = o.FarmId,
                Hold = o.Hold,
                Identification = o.Identification,
                InvoiceNumber = o.InvoiceNumber,
                IsNew = o.IsNew,
                Ubication = o.Ubication,
                UpdatedAt = o.UpdatedAt,
                BaseKg = o.BaseKg,
                Value = o.Value,
                Weight = o.Weight / 2
            });
            var InvoicesDry = InvoicesList.Where(t => t.CoffeeTypeId != 6).Select(o => new InvoiceDTO
            {
                Id = o.Id,
                Cash = o.Cash,
                CoffeeTypeId = o.CoffeeTypeId,
                CreatedAt = o.CreatedAt,
                Date = o.Date,
                DateInvoice = o.DateInvoice,
                FarmId = o.FarmId,
                Hold = o.Hold,
                Identification = o.Identification,
                InvoiceNumber = o.InvoiceNumber,
                IsNew = o.IsNew,
                Ubication = o.Ubication,
                UpdatedAt = o.UpdatedAt,
                BaseKg = o.BaseKg,
                Value = o.Value,
                Weight = o.Weight
            });

            IEnumerable<InvoiceDTO> DetailedInvoices = InvoicesWet.Union(InvoicesDry).OrderByDescending(o => o.Date);
            result.Invoices = new List<InvoiceDTO>();
            result.Invoices = DetailedInvoices.ToList();

            result.GroupedInvoices = DetailedInvoices.GroupBy(y => y.Date.Year)
                .Select(g => new FarmDTO.groupedInvoice { Year = g.Key, Totalkg = g.Sum(i => i.Weight), TotalValue = g.Sum(i => i.Value), AverageValue = g.Sum(i => i.Value)/ g.Sum(i => i.Weight) })
                .OrderByDescending(y => y.Year).ToList();

            result.Fertilizers = result.Fertilizers.OrderByDescending(d => d.Date).ToList();

            result.GroupedFertilizers = result.Fertilizers.GroupBy(f => new { f.Date.Year })
            .Select(g => new FarmDTO.GroupedFertilizer { Year = g.Key.Year, Quantity = g.Sum(f => f.Quantity), TotalValue = g.Sum(f => f.Value), AveragePrice = g.Average(f => f.UnitPrice) })
            .OrderByDescending(y => y.Year).ToList();

            result.Images = new List<ImageDTO>();
            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("sync/{id}/info")]
        public HttpResponseMessage SaveInfo(Guid id, [FromBody]FarmDTOAPI farm)
        {
            UnitOfWork db = new UnitOfWork();

            farm.AssociatedPeople = new List<UserDTO>();
            var Original_farm = _farmManager.Details(farm.Id);

            if (!string.IsNullOrEmpty(farm.Longitude) && !string.IsNullOrEmpty(farm.Latitude))
            {
                var position = string.Format("POINT({0} {1})", farm.Longitude, farm.Latitude);
                farm.GeoLocation = DbGeography.FromText(position);
            }

            farm.FarmStatusId = Original_farm.FarmStatusId;
            farm.AssociatedPeople.Add(Original_farm.AssociatedPeople.FirstOrDefault());
            var members = farm.FamilyUnitMembers;
            farm.FamilyUnitMembers = new List<FamilyUnitMemberDTO>();
            //var fertilizers = farm.Fertilizers;
            //farm.Fertilizers = new List<FertilizerDTO>();
            //var activities = farm.OtherActivities;
            //farm.OtherActivities = new List<FarmOtherActivityDTO>();
            var plantations = farm.Productivity.Plantations;
            farm.Productivity.Plantations = new List<PlantationDTO>();
            var projects = farm.Projects;
            farm.Projects = new List<ProjectDTO>();
            //var soilAnalysis = farm.SoilAnalysis;
            //farm.SoilAnalysis = new List<SoilAnalysisDTO>();
            farm.FamilyUnitMembers = members;
            farm.Productivity.Plantations = plantations;
            farm.Projects = projects;

            FarmDTO FarmDTOConvert = new FarmDTO();
            FarmAPIToDTO(farm, FarmDTOConvert);

            _farmManager.Edit(farm.Id, FarmDTOConvert, FarmManager.FARMS);

            _farmManager.Edit(farm.Id, FarmDTOConvert, FarmManager.FAMILY_UNIT_MEMBERS,
                "FamilyUnitMembers");
            farm.FamilyUnitMembers = new List<FamilyUnitMemberDTO>();

            //farm.Fertilizers = fertilizers;
            //_farmManager.Edit(farm.Id, farm, FarmManager.FERTILIZERS,
            //    "Fertilizers");
            //farm.Fertilizers = new List<FertilizerDTO>();

            //farm.OtherActivities = activities;
            //_farmManager.Edit(farm.Id, farm, FarmManager.OTHER_ACTIVITIES,
            //    "OtherActivities");
            //farm.OtherActivities = new List<FarmOtherActivityDTO>();


            _farmManager.Edit(farm.Id, FarmDTOConvert, FarmManager.PLANTATIONS,
                "Productivity.Plantations");

            _farmManager.Edit(farm.Id, FarmDTOConvert, FarmManager.FLOWERING_PERIODS);

            farm.Productivity.Plantations = new List<PlantationDTO>();

            _farmManager.Edit(farm.Id, FarmDTOConvert, FarmManager.PROJECTS,
                "Projects");
            farm.Projects = new List<ProjectDTO>();

            //farm.SoilAnalysis = soilAnalysis;
            //_farmManager.Edit(farm.Id, farm, FarmManager.SOIL_ANALYSIS,
            //    "SoilAnalysis");
            //farm.SoilAnalysis = new List<SoilAnalysisDTO>();

            foreach (var contact in farm.Contacts)
            {
                if (contact.ActionType == "New" || contact.ActionType == "")
                {
                    creatingContacts(contact, FarmDTOConvert);
                }
                else if (contact.ActionType == "Edited")
                {
                    editingContact(contact, FarmDTOConvert);
                }
                else if (contact.ActionType == "Deleted")
                {
                    RemovingContact(contact, FarmDTOConvert);
                }

            }

            var idFarm = farm.Id;
            var productivity = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
            var totalHectareas1 = Convert.ToDecimal(productivity.coffeeArea);
            var totalHectareas2 = Convert.ToDecimal(productivity.TotalHectares);


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
                if (item.PlantationVarietyId == new Guid("3A643A6E-A64C-4305-B2D9-01D147A5F926") || item.PlantationVarietyId == new Guid("14ed528a-7ec4-49cd-9efb-bc2a42091b54"))
                { 
                    sumPerOtro = sumPerOtro + Convert.ToDecimal(item.Hectares);
                }
            }

            //OPERACIONES PORCENTAJES
            var opeColombia = Convert.ToDecimal(0);
            if (sumPerColombia != 0 && totalHectareas1 != 0)
            {
                opeColombia = (sumPerColombia / (totalHectareas1 + sumPerOtro) * 100);
            }

            var opeCaturra = Convert.ToDecimal(0);
            if (sumPerCaturra != 0 && totalHectareas1 != 0)
            {
                opeCaturra = (sumPerCaturra / (totalHectareas1 + sumPerOtro) * 100);
            }

            var opeCastillo = Convert.ToDecimal(0);
            if (sumPerCastillo != 0 && totalHectareas1 != 0)
            {
                opeCastillo = (sumPerCastillo / (totalHectareas1+ sumPerOtro) * 100);
            }

            var opeOtro = Convert.ToDecimal(0);
            if (sumPerOtro != 0 && totalHectareas2 != 0)
            {
                opeOtro = (sumPerOtro / totalHectareas2) * 100;
            }

            //ACTUALIZACION
            var productivitiesChange = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
            productivitiesChange.percentageColombia = Convert.ToDouble(opeColombia);
            productivitiesChange.percentageCaturra = Convert.ToDouble(opeCaturra);
            productivitiesChange.percentageCastillo = Convert.ToDouble(opeCastillo);
            productivitiesChange.percentageotra = Convert.ToDouble(opeOtro);
            productivitiesChange.UpdatedAt = DateTime.Now;
            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private static void FarmAPIToDTO(FarmDTOAPI farm, FarmDTO FarmDTOConvert)
        {
            FarmDTOConvert.AgeIndicator = farm.AgeIndicator;
            FarmDTOConvert.AssociatedPeople = farm.AssociatedPeople;
            FarmDTOConvert.Code = farm.Code;
            FarmDTOConvert.Contacts = farm.Contacts;
            FarmDTOConvert.Cooperative = farm.Cooperative;
            FarmDTOConvert.CooperativeId = farm.CooperativeId;
            FarmDTOConvert.CreatedAt = farm.CreatedAt;
            FarmDTOConvert.CurrentTechnician = farm.CurrentTechnician;
            //FarmDTOConvert.Elevation = farm.Elevation;
            FarmDTOConvert.EstimatedProduction = farm.EstimatedProduction;
            //FarmDTOConvert.EstimatedProduction = farm.EstimatedProduction;
            FarmDTOConvert.FamilyUnitMembers = farm.FamilyUnitMembers;
            FarmDTOConvert.FarmStatusId = farm.FarmStatusId;
            FarmDTOConvert.FarmSubstatus = farm.FarmSubstatus;
            FarmDTOConvert.FarmSubstatusId = farm.FarmSubstatusId;
            FarmDTOConvert.FertilizerBags = farm.FertilizerBags;
            FarmDTOConvert.Fertilizers = farm.Fertilizers;
            FarmDTOConvert.GeoLocation = farm.GeoLocation;
            FarmDTOConvert.Hectares = farm.Hectares;
            FarmDTOConvert.Id = farm.Id;
            FarmDTOConvert.Images = farm.Images;
            FarmDTOConvert.Invoices = farm.Invoices;
            FarmDTOConvert.IsNew = farm.IsNew;
            FarmDTOConvert.Latitude = farm.Latitude;
            FarmDTOConvert.Longitude = farm.Longitude;
            FarmDTOConvert.Name = farm.Name;
            FarmDTOConvert.OtherActivities = farm.OtherActivities;
            FarmDTOConvert.OwnershipType = farm.OwnershipType;
            FarmDTOConvert.OwnershipTypeId = farm.OwnershipTypeId;
            FarmDTOConvert.Plants = farm.Plants;
            FarmDTOConvert.ProductivePlants = farm.ProductivePlants;
            FarmDTOConvert.Productivity = farm.Productivity;
            FarmDTOConvert.Projects = farm.Projects;
            FarmDTOConvert.SoilAnalysis = farm.SoilAnalysis;
            FarmDTOConvert.SoilTypes = farm.SoilTypes;
            FarmDTOConvert.SupplyChain = farm.SupplyChain;
            FarmDTOConvert.SupplyChainId = farm.SupplyChainId;
            FarmDTOConvert.UpdatedAt = farm.UpdatedAt;
            FarmDTOConvert.Village = farm.Village;
            FarmDTOConvert.VillageId = farm.VillageId;
            FarmDTOConvert.Worker = farm.Worker;
        }

        private static void creatingContacts(ContactDTO contact, FarmDTO farm)
        {
            UnitOfWork db = new UnitOfWork();

            Contact persisted = db.Contacts.Find(contact.Id);

            if (persisted == null)
            {
                Contact createContact = new Contact();
                createContact.Topics = new List<Topic>();

                createContact.Id = Guid.NewGuid();
                createContact.Id = contact.Id;
                createContact.Name = contact.Name;
                createContact.Comment = contact.Comment;
                createContact.TypeId = contact.TypeId;
                createContact.LocationId = contact.LocationId;
                createContact.UserId = contact.UserId;
                createContact.Date = DateTime.Now;

                var topicslist = contact.Topics.ToList();
                var receivedTopicList = new List<Topic>();

                List<Farm> FarmsToAdd = new List<Farm>();

                if (topicslist != null)
                {
                    foreach (var topic in topicslist)
                    {
                        receivedTopicList.Add(db.Topic.Find(topic.Id));
                    }
                    createContact.Topics = receivedTopicList;
                }
                FarmsToAdd.Add(db.Farms.Find((farm.Id)));
                createContact.Farms = FarmsToAdd;

                db.Contacts.Add(createContact);
                db.SaveChanges();
            }
        }

        private static void editingContact(ContactDTO contact, FarmDTO farm)
        {
            UnitOfWork db = new UnitOfWork();

            Contact persistedForValidate = db.Contacts.Find(contact.Id);

            if (persistedForValidate != null)
            {
                Contact persisted = new Contact();
                persisted.Topics = new List<Topic>();
                persisted = db.Contacts.Find(contact.Id);
                persisted.Name = contact.Name;
                persisted.Date = contact.Date;
                persisted.Comment = contact.Comment;
                persisted.TypeId = contact.TypeId;
                persisted.LocationId = contact.LocationId;
                persisted.UserId = contact.UserId;

                var topicslist = contact.Topics.ToList();
                var receivedTopicList = new List<Topic>();

                foreach (var topic in topicslist)
                {
                    receivedTopicList.Add(db.Topic.Find(topic.Id));

                }

                foreach (var topic in persisted.Topics)
                {
                    persisted.Topics.Add(db.Topic.Find(topic.Id));
                }

                List<Topic> deletedTopics = new List<Topic>();
                List<Topic> addedTopics = new List<Topic>();

                if (persisted.Topics != null && persisted.Topics != null)
                {
                    deletedTopics = persisted.Topics.Except(receivedTopicList).ToList();
                    addedTopics = receivedTopicList.Except(persisted.Topics).ToList();
                    deletedTopics.ForEach(t => persisted.Topics.Remove(t));
                    foreach (Topic t in addedTopics)
                    {
                        db.Topic.Attach(t);
                        persisted.Topics.Add(t);
                    }
                }
                db.SaveChanges();
            }
            else {
                creatingContacts(contact, farm);
            }

            
        }


        private static void RemovingContact(ContactDTO contact, FarmDTO farm)
        {
            UnitOfWork db = new UnitOfWork();
            Contact deleteContact = new Contact();
            deleteContact = db.Contacts.Find(contact.Id);
            if(deleteContact != null)
            {
                deleteContact.Id = contact.Id;
                db.Contacts.Remove(deleteContact);
                db.SaveChanges();
            }
        }

        [HttpPost]
        [Route("sync/{id}/sustainability")]
        public HttpResponseMessage SaveSustainability(Guid id, [FromBody]IEnumerable<ImpactAssessmentDTO> assessments)
        {
            foreach (var assessment in assessments)
            {
                var ans = assessment.Answers;
                assessment.Answers = null;
                if (_impactManager.Get(assessment.Id) == null)
                    _impactManager.Add(assessment);
                assessment.Answers = ans;
                _impactManager.Edit(assessment.Id, assessment);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpPost]
        [Route("sync/{id}/Contacts")]
        public HttpResponseMessage SaveInfoContacts(Guid id, [FromBody]FarmDTOAPI farm)
        {
            foreach (var contact in farm.Contacts)
            {
                if (contact.ActionType == "New" || contact.ActionType == "")
                {
                    SyncCreatingContacts(contact, id);
                }
                else if (contact.ActionType == "Edited")
                {
                    SyncEditingContact(contact, id);
                }
                else if (contact.ActionType == "Deleted")
                {
                    SyncRemovingContact(contact);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("sync/{id}/FamilyUnitMembers")]
        public HttpResponseMessage SaveInfoFamilyUnitMembers(Guid id, [FromBody]FarmDTOAPI farm)
        {
            var members = farm.FamilyUnitMembers;
            farm.FamilyUnitMembers = new List<FamilyUnitMemberDTO>();
            farm.FamilyUnitMembers = members;
            FarmDTO FarmDTOConvert = new FarmDTO();
            FarmAPIToDTO(farm, FarmDTOConvert);
            _farmManager.Edit(farm.Id, FarmDTOConvert, FarmManager.FAMILY_UNIT_MEMBERS,
            "FamilyUnitMembers");
            farm.FamilyUnitMembers = new List<FamilyUnitMemberDTO>();
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        private static void SyncCreatingContacts(ContactDTO contact, Guid id)
        {
            UnitOfWork db = new UnitOfWork();

            Contact persisted = db.Contacts.Find(contact.Id);

            if (persisted == null)
            {
                Contact createContact = new Contact();
                createContact.Topics = new List<Topic>();

                createContact.Id = Guid.NewGuid();
                createContact.Id = contact.Id;
                createContact.Name = contact.Name;
                createContact.Comment = contact.Comment;
                createContact.TypeId = contact.TypeId;
                createContact.LocationId = contact.LocationId;
                createContact.UserId = contact.UserId;
                createContact.Date = DateTime.Now;

                var topicslist = contact.Topics.ToList();
                var receivedTopicList = new List<Topic>();

                List<Farm> FarmsToAdd = new List<Farm>();

                if (topicslist != null)
                {
                    foreach (var topic in topicslist)
                    {
                        receivedTopicList.Add(db.Topic.Find(topic.Id));
                    }
                    createContact.Topics = receivedTopicList;
                }
                FarmsToAdd.Add(db.Farms.Find((id)));
                createContact.Farms = FarmsToAdd;

                db.Contacts.Add(createContact);
                db.SaveChanges();
            }
        }

        private static void SyncEditingContact(ContactDTO contact, Guid FarmId)
        {
            UnitOfWork db = new UnitOfWork();
            Contact persistedForValidate = db.Contacts.Find(contact.Id);

            if(persistedForValidate != null)
            {
                Contact persisted = new Contact();
                persisted.Topics = new List<Topic>();
                persisted = db.Contacts.Find(contact.Id);
                persisted.Name = contact.Name;
                persisted.Date = contact.Date;
                persisted.Comment = contact.Comment;
                persisted.TypeId = contact.TypeId;
                persisted.LocationId = contact.LocationId;
                persisted.UserId = contact.UserId;

                var topicslist = contact.Topics.ToList();
                var receivedTopicList = new List<Topic>();

                foreach (var topic in topicslist)
                {
                    receivedTopicList.Add(db.Topic.Find(topic.Id));
                }

                foreach (var topic in persisted.Topics)
                {
                    persisted.Topics.Add(db.Topic.Find(topic.Id));
                }

                List<Topic> deletedTopics = new List<Topic>();
                List<Topic> addedTopics = new List<Topic>();

                if (persisted.Topics != null && persisted.Topics != null)
                {
                    deletedTopics = persisted.Topics.Except(receivedTopicList).ToList();
                    addedTopics = receivedTopicList.Except(persisted.Topics).ToList();
                    deletedTopics.ForEach(t => persisted.Topics.Remove(t));
                    foreach (Topic t in addedTopics)
                    {
                        db.Topic.Attach(t);
                        persisted.Topics.Add(t);
                    }
                }
                db.SaveChanges();
            }
            else
            {
                SyncCreatingContacts(contact, FarmId);
            }

        }

        public class FarmInformation
        {
            public Guid Id { get; set; }
            public string Code { get; set; }
            public Guid DepartmentId { get; set; }
            public Guid MunicipalityId { get; set; }
            public Guid VillageId { get; set; }
            public string Name { get; set; }
            public string Farmer { get; set; }
        }

        private static void SyncRemovingContact(ContactDTO contact)
        {
            UnitOfWork db = new UnitOfWork();
            Contact deleteContact = new Contact();
            deleteContact = db.Contacts.Find(contact.Id);
            if(deleteContact != null)
            {
                deleteContact.Id = contact.Id;
                db.Contacts.Remove(deleteContact);
                db.SaveChanges();
            }
        }

    }
}