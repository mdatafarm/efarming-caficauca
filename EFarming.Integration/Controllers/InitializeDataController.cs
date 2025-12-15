using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.FarmModule.FamilyUnitAggregate;
using EFarming.Core.SustainabilityModule.ContactAggregate;
using EFarming.DAL;
using EFarming.DTO.AdminModule;
using EFarming.DTO.APIModule;
using EFarming.DTO.ContactModule;
using EFarming.DTO.ProjectModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;

namespace EFarming.Integration.Controllers
{
    [RoutePrefix("initializedata")]
    [Authorize]
    public class InitializeDataController : ApiController
    {
        private ICountryManager _countryManager;
        private ISupplierManager _supplierManager;
        private ISupplyChainManager _supplyChainManager;
        private IDepartmentManager _departmentManager;
        private IFarmStatusManager _statusManager;
        private ICooperativeManager _cooperativeManager;
        private IOwnershipTypeManager _ownershipTypeManager;
        private IPlantationTypeManager _plantationTypeManager;
        private IPlantationVarietyManager _plantationVarietyManager;
        private IPlantationStatusManager _plantationStatusManager;
        private IFloweringPeriodQualificationManager _floweringPeriodQualificationManager;
        private ISoilTypeManager _soilTypeManager;
        private IOtherActivityManager _otherActivityManager;
        private IProjectManager _projectManager;
        private ISensoryProfileManager _sensoryProfileManager;


        public InitializeDataController(
            ICountryManager countryManager,
            ISupplierManager supplierManager,
            ISupplyChainManager supplyChainManager,
            IDepartmentManager departmentManager,
            IFarmStatusManager statusManager,
            ICooperativeManager cooperativeManager,
            IOwnershipTypeManager ownershipTypeManager,
            IPlantationTypeManager plantationTypeManager,
            IPlantationVarietyManager plantationVarietyManager,
            IPlantationStatusManager plantationStatusManager,
            IFloweringPeriodQualificationManager floweringPeriodQualificationManager,
            ISoilTypeManager soilTypeManager,
            IOtherActivityManager otherActivityManager,
            IProjectManager projectManager,
            ISensoryProfileManager sensoryProfileManager)

        {
            _countryManager = countryManager;
            _supplierManager = supplierManager;
            _supplyChainManager = supplyChainManager;
            _departmentManager = departmentManager;
            _statusManager = statusManager;
            _cooperativeManager = cooperativeManager;
            _ownershipTypeManager = ownershipTypeManager;
            _plantationTypeManager = plantationTypeManager;
            _plantationStatusManager = plantationStatusManager;
            _floweringPeriodQualificationManager = floweringPeriodQualificationManager;
            _soilTypeManager = soilTypeManager;
            _otherActivityManager = otherActivityManager;
            _plantationVarietyManager = plantationVarietyManager;
            _projectManager = projectManager;
            _sensoryProfileManager = sensoryProfileManager;

        }


        [HttpGet]
        [Route("info")]
        public HttpResponseMessage Info()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (var claim in claims)
            {
                result.Add(claim.Type, claim.Value);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public class DepartmensToMovil
        {
            public List<Department> departments { get; set; }
            public List<Municipality> municipalities { get; set; }
            public List<Village> villages { get; set; }

        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage Index()
        {
            UnitOfWork db = new UnitOfWork();

            var identity = (ClaimsIdentity)User.Identity;

            var username = identity.Claims.FirstOrDefault().Value;

            var LoggedUser = db.Users.Where(u => u.Username == username).FirstOrDefault();
            var roles = LoggedUser.Roles.Select(r => r.RoleName);

            // List with data to syncronize with mobile app
            Dictionary<string, object> result = new Dictionary<string, object>();

            List<DepartmentAPIDTO> List = new List<DepartmentAPIDTO>();

            DepartmentAPIDTO dep = new DepartmentAPIDTO();
            List<MunicipalityAPIDTO> ListMuni = new List<MunicipalityAPIDTO>();
            ICollection<MunicipalityAPIDTO> Municipalities;

            var departments = _departmentManager.LoadFullData();
            var newPlantations = _plantationTypeManager.LoadFullData();
            var newVariety = _plantationVarietyManager.LoadFullData();
            var status = _statusManager.GetAll(new string[] { "FarmSubstatuses" });
            var cooperatives = _cooperativeManager.GetAll();
            var ownershipTypes = _ownershipTypeManager.GetAll();
            var contact_name = new ContactNameList();
            var names = contact_name.listNames();
            var contact_types = db.Type.Select(t => new
            {
                id = t.Id,
                name = t.Name
            });
            var contact_locations = db.Location.Where(d => d.DeletedAt == null).Select(l => new
            {
                id = l.Id,
                name = l.Name
            });
            var contact_topics = db.Topic.Where(d => d.DeletedAt == null).Select(to => new
            {
                id = to.Id,
                name = to.Name
            });


            // Add farm data to list
            result.Add("userRoles", roles);
            result.Add("countries", _countryManager.GetAll(new string[] { "Suppliers", "Suppliers.SupplyChains" }));
            result.Add("suppliers", _supplierManager.GetAll());
            //result.Add("supplyChain", _supplyChainManager.GetAll());
            result.Add("departments", departments);
            result.Add("status", status.OrderBy(x => x.Name));
            result.Add("cooperatives", cooperatives);
            result.Add("ownership_types", ownershipTypes.OrderBy(x => x.Name));
            result.Add("Contact_type", contact_types.OrderBy(x => x.name));
            result.Add("Contact_location", contact_locations.OrderBy(x => x.name));
            result.Add("Contact_topic", contact_topics.OrderBy(x => x.name));
            result.Add("Contact_names", names.OrderBy(x => x.Value));

            result.Add("plantation_types", newPlantations);
            result.Add("plantation_status", _plantationStatusManager.GetAll().OrderBy(x => x.Name));
            result.Add("plantation_varieties", newVariety);
            result.Add("flowering_periods", _floweringPeriodQualificationManager.GetAll().OrderBy(x => x.Name));

            //result.Add("soil_types", _soilTypeManager.GetAll());
            //result.Add("other_activities", _otherActivityManager.GetAll());
            List<ProjectDTO> Projects = new List<ProjectDTO>();
            var projects = db.Projects.ToList();
            foreach (var project in projects)
            {
                ProjectDTO projectdto = new ProjectDTO();
                projectdto.Id = project.Id;
                projectdto.Name = project.Name;
                projectdto.Description = project.Description;
                Projects.Add(projectdto);
            }
            result.Add("projects", Projects.OrderBy(x => x.Description));

            //var SupplyChains = db.SupplyChains.ToList();
            //foreach (var supply in SupplyChains)
            //{
            //    SupplyChain Supplydto = new SupplyChain();
            //    Supplydto.Id = supply.Id;
            //    Supplydto.Name = supply.Name;
            //    Supplydto.SupplierId = supply.SupplierId;
            //    Supplydto.Supplier = supply.Supplier;
            //    SupplyChainsList.Add(Supplydto);
            //}

            List<supplierDB> SupplyChainsList = new List<supplierDB>();
            var SupplyChains = db.ExecuteQuery<supplierDB>("list_supplyChains").ToList();
            foreach (var supply in SupplyChains)
            {
                supplierDB Supplydto = new supplierDB();
                Supplydto.Id_supplyChains = supply.Id_supplyChains;
                Supplydto.Name_suppliers = supply.Name_suppliers;
                Supplydto.Nombre_Junto = supply.Name_suppliers + " " + supply.Name_supplyChains;
                Supplydto.Name_supplyChains = supply.Name_suppliers + " " + supply.Name_supplyChains;
                SupplyChainsList.Add(Supplydto);
            }

            result.Add("supplyChain", SupplyChainsList);

            result.Add("assessment_targets", _sensoryProfileManager.GetTypes());

            var list = FamilyUnitMember.InitializeList();
            result.Add("education", list[FamilyUnitMember.EDUCATION_LIST]);
            result.Add("relationship", list[FamilyUnitMember.RELATIONSHIP_LIST]);
            result.Add("marital_status", list[FamilyUnitMember.MARITAL_STATUS_LIST]);

            //tipo de labor
            List<IDClaseIdi> List_IDClaseIdiLabor = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiLabor1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiLabor2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiLabor3 = new IDClaseIdi();


            obj_IDClaseIdiLabor1.Name = "Siembra";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor1);

            obj_IDClaseIdiLabor2.Name = "Zoca";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor2);

            obj_IDClaseIdiLabor3.Name = "No aplica";
            List_IDClaseIdiLabor.Add(obj_IDClaseIdiLabor3);

            result.Add("classLabor", List_IDClaseIdiLabor);

            //tipo de lote
            List<IDClaseIdi> List_IDClaseIdiTLot = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiTLot1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiTLot2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiTLot3 = new IDClaseIdi();

            obj_IDClaseIdiTLot1.Name = "Tecnificado";
            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot1);

            obj_IDClaseIdiTLot2.Name = "Tradicional";
            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot2);

            obj_IDClaseIdiTLot3.Name = "No aplica";
            List_IDClaseIdiTLot.Add(obj_IDClaseIdiTLot3);

            result.Add("classTLot", List_IDClaseIdiTLot);

            //forma de siembra
            List<IDClaseIdi> List_IDClaseIdiFSiem = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiFSiem1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiFSiem2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiFSiem3 = new IDClaseIdi();

            obj_IDClaseIdiFSiem1.Name = "Triángulo";
            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem1);

            obj_IDClaseIdiFSiem2.Name = "Cuadrado";
            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem2);

            obj_IDClaseIdiFSiem3.Name = "No aplica";
            List_IDClaseIdiFSiem.Add(obj_IDClaseIdiFSiem3);

            result.Add("classFSiem", List_IDClaseIdiFSiem);

            //numero de ejes
            List<IDClaseIdi> List_IDClaseIdiNumEjeArbLot = new List<IDClaseIdi>();
            IDClaseIdi obj_IDClaseIdiNumEjeArbLot1 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiNumEjeArbLot2 = new IDClaseIdi();
            IDClaseIdi obj_IDClaseIdiNumEjeArbLot3 = new IDClaseIdi();

            obj_IDClaseIdiNumEjeArbLot1.Name = "1";
            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot1);

            obj_IDClaseIdiNumEjeArbLot2.Name = "2";
            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot2);

            obj_IDClaseIdiNumEjeArbLot3.Name = "No aplica";
            List_IDClaseIdiNumEjeArbLot.Add(obj_IDClaseIdiNumEjeArbLot3);

            result.Add("classNumEjeArbLot", List_IDClaseIdiNumEjeArbLot);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public class IDClaseIdi
        {
            public string Id { get; set; }
            public string Name { get; set; }

        }

        public class supplierDB
        {
            public Guid Id_supplyChains { get; set; }
            public string Nombre_Junto { get; set; }
            public string Name_suppliers { get; set; }
            public string Name_supplyChains { get; set; }

        }


    }
}
