using EFarming.DAL;
using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Repository.AdminModule;
using EFarming.Repository.FarmModule;
using EFarming.Repository.ProjectModule;
using EFarming.Repository.TraceabilityModule;
using EFarming.Web.Coocentral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    public class UpdateDataController : Controller
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: UpdateData
        public async Task<ActionResult> Index(string answer)
        {
            ViewBag.answer = answer;
            return View();
        }

        public async Task<ActionResult> UpdateFarmInformation()
        {
            //FarmManager necesary variables
            FarmRepository farmRepository = new FarmRepository(db);
            SoilTypeRepository soilTypeRepository = new SoilTypeRepository(db);
            FamilyUnitRepository familyUnitRepository = new FamilyUnitRepository(db);
            UserRepository userRepository = new UserRepository(db);
            ProjectRepository projectRepository = new ProjectRepository(db);
            Storage storage = new Storage();

            FarmManager farmMananger = new FarmManager(farmRepository, soilTypeRepository, familyUnitRepository, userRepository, projectRepository, storage);
            IFarmManager _farmmanager = farmMananger;

            GetFarmData GetFarmInformation = new GetFarmData();

            List<FarmDTO> Farms = new List<FarmDTO>();

            //Calling the methods for get and save the information
            var answer = await GetFarmInformation.GetFarmInformation(_farmmanager, Farms);
            return RedirectToAction("Index", new { answer = answer});
        }

        public async Task<ActionResult> UpdateFamilyInformation()
        {
            //FarmManager necesary variables
            FarmRepository farmRepository = new FarmRepository(db);
            SoilTypeRepository soilTypeRepository = new SoilTypeRepository(db);
            FamilyUnitRepository familyUnitRepository = new FamilyUnitRepository(db);
            UserRepository userRepository = new UserRepository(db);
            ProjectRepository projectRepository = new ProjectRepository(db);
            Storage storage = new Storage();

            FarmManager farmMananger = new FarmManager(farmRepository, soilTypeRepository, familyUnitRepository, userRepository, projectRepository, storage);
            IFarmManager _farmmanager = farmMananger;

            GetFamilyData GetFamilyInformation = new GetFamilyData();

            List<FarmDTO> Farms = new List<FarmDTO>();

            //Calling the methods for get and save the information
            var answer = await GetFamilyInformation.GetFamilyInformation(_farmmanager);

            return RedirectToAction("Index", new { answer = answer });
        }

        public async Task<ActionResult> UpdateInvoiceInformation()
        {
            //FarmManager necesary variables
            FarmRepository farmRepository = new FarmRepository(db);
            SoilTypeRepository soilTypeRepository = new SoilTypeRepository(db);
            FamilyUnitRepository familyUnitRepository = new FamilyUnitRepository(db);
            UserRepository userRepository = new UserRepository(db);
            ProjectRepository projectRepository = new ProjectRepository(db);
            InvoiceRepository invoiceRepository = new InvoiceRepository(db);
            Storage storage = new Storage();

            FarmManager farmMananger = new FarmManager(farmRepository, soilTypeRepository, familyUnitRepository, userRepository, projectRepository, storage);
            IFarmManager _farmmanager = farmMananger;

            InvoiceManager invoiceManager = new InvoiceManager(invoiceRepository);

            GetInvoicesData GetInvoicesInformation = new GetInvoicesData(invoiceManager);

            List<FarmDTO> Farms = new List<FarmDTO>();

            var LastInvoice = db.Invoices.OrderByDescending(d => d.Date).First();

            //Calling the methods for get and save the information
            var answer = await GetInvoicesInformation.GetInvoicesInformation(_farmmanager, LastInvoice.Date.AddDays(1));

            return RedirectToAction("Index", new { answer = answer });
        }

        public async Task<ActionResult> UpdateFertilizerInformation()
        {
            //FarmManager necesary variables
            FarmRepository farmRepository = new FarmRepository(db);
            SoilTypeRepository soilTypeRepository = new SoilTypeRepository(db);
            FamilyUnitRepository familyUnitRepository = new FamilyUnitRepository(db);
            UserRepository userRepository = new UserRepository(db);
            ProjectRepository projectRepository = new ProjectRepository(db);
            Storage storage = new Storage();

            FarmManager farmMananger = new FarmManager(farmRepository, soilTypeRepository, familyUnitRepository, userRepository, projectRepository, storage);
            IFarmManager _farmmanager = farmMananger;

            GetFertilizersData GetFerilizersData = new GetFertilizersData();

            List<FarmDTO> Farms = new List<FarmDTO>();

            var LastFertilizer = db.Fertilizers.OrderByDescending(d => d.Date).First();

            //Calling the methods for get and save the information
            var answer = await GetFerilizersData.GetFertilizersInformation(_farmmanager, LastFertilizer.Date.AddDays(1));

            return RedirectToAction("Index", new { answer = answer });
        }
    }
}