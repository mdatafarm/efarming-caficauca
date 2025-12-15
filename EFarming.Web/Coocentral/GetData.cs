using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.DAL;
using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Oracle.Models;
using EFarming.Repository.AdminModule;
using EFarming.Repository.FarmModule;
using EFarming.Repository.ProjectModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace EFarming.Web.Coocentral
{
    public class GetData
    {
        private UnitOfWork db = new UnitOfWork();
        public async Task<List<FarmDTO>> FarmInformation()
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
            GetFarmData GetFarmInformation = new GetFarmData();

            List<FarmDTO> Farms = new List<FarmDTO>();

            //Calling the methods for get and save the information
            await GetFarmInformation.GetFarmInformation(_farmmanager, Farms);
            await GetFamilyInformation.GetFamilyInformation(_farmmanager);
            return Farms;
        }

    }
}