using AutoMapper;
using EFarming.Common;
using EFarming.Common.Resources;
using EFarming.Core;
using EFarming.Core.AdminModule.SoilTypeAggregate;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.FarmModule.FamilyUnitAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.Core.Specification.Implementation;
using EFarming.DAL;
using EFarming.DTO.AdminModule;
using EFarming.DTO.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Repository.AdminModule;
using EFarming.Repository.FarmModule;
using EFarming.Repository.ProjectModule;
using PagedList;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// Farm Manager
    /// </summary>
    public class FarmManager : IFarmManager
    {
        private UnitOfWork db = new UnitOfWork();
        /// <summary>
        /// The farms
        /// </summary>
        public const string FARMS = "FARMS";
        /// <summary>
        /// The soi l_ analysis
        /// </summary>
        public const string SOIL_ANALYSIS = "SOIL_ANALYSIS";
        /// <summary>
        /// The plantations
        /// </summary>
        public const string PLANTATIONS = "PLANTATIONS";
        /// <summary>
        /// The flowerin g_ periods
        /// </summary>
        public const string FLOWERING_PERIODS = "FLOWERING_PERIODS";
        /// <summary>
        /// The fertilizers
        /// </summary>
        public const string FERTILIZERS = "FERTILIZERS";
        /// <summary>
        /// The othe r_ activities
        /// </summary>
        public const string OTHER_ACTIVITIES = "OTHER_ACTIVITIES";
        /// <summary>
        /// The famil y_ uni t_ members
        /// </summary>
        public const string FAMILY_UNIT_MEMBERS = "FAMILY_UNIT_MEMBERS";
        /// <summary>
        /// The images
        /// </summary>
        public const string IMAGES = "IMAGES";
        /// <summary>
        /// The projects
        /// </summary>
        public const string PROJECTS = "PROJECTS";

        /// <summary>
        /// The _farm repository
        /// </summary>
        private IFarmRepository _farmRepository;
        /// <summary>
        /// The _soil type repository
        /// </summary>
        private ISoilTypeRepository _soilTypeRepository;
        /// <summary>
        /// The _family unit repository
        /// </summary>
        private IFamilyUnitRepository _familyUnitRepository;
        /// <summary>
        /// The _user repository
        /// </summary>
        private IUserRepository _userRepository;
        /// <summary>
        /// The _project repository
        /// </summary>
        private IProjectRepository _projectRepository;
        /// <summary>
        /// The _storage
        /// </summary>
        private IStorage _storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmManager"/> class.
        /// </summary>
        /// <param name="farmRepository">The farm repository.</param>
        /// <param name="soilTypeRepository">The soil type repository.</param>
        /// <param name="familyUnitRepository">The family unit repository.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="storage">The storage.</param>
        public FarmManager(
            FarmRepository farmRepository,
            SoilTypeRepository soilTypeRepository,
            FamilyUnitRepository familyUnitRepository,
            UserRepository userRepository,
            ProjectRepository projectRepository,
            Storage storage)
        {
            _farmRepository = farmRepository;
            _soilTypeRepository = soilTypeRepository;
            _familyUnitRepository = familyUnitRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _storage = storage;
        }

        /// <summary>
        /// Finds this instance.
        /// </summary>
        /// <returns>
        /// List of FarmDTO
        /// </returns>
        public List<FarmDTO> Find()
        {
            return Mapper.Map<List<FarmDTO>>(_farmRepository.GetAll().ToList());
        }

        public int CountFarms(string type, Guid? id)
        {
            int FarmsNumber = 0;
            switch (type)
            {
                case "World":
                    FarmsNumber = db.Farms.Count();
                    break;
                case "Country":
                    FarmsNumber = db.Farms.Where(f => f.SupplyChain.Supplier.CountryId == id).Count();
                    break;
                case "Supplier":
                    FarmsNumber = db.Farms.Where(f => f.SupplyChain.Supplier.Id == id).Count();
                    break;
                case "SupplierChain":
                    FarmsNumber = db.Farms.Where(f => f.SupplyChain.Id == id).Count();
                    break;
                case "Cooperative":
                    FarmsNumber = db.Farms.Where(f => f.CooperativeId == id).Count();
                    break;
                default:
                    break;
            }
            return FarmsNumber;
        }

        public double TotalArea(string type, Guid? id)
        {
            double Area = 0;
            switch (type)
            {
                case "World":
                    Area = db.Farms.Sum(f => Convert.ToDouble(f.Productivity.TotalHectares));
                    break;
                case "Country":
                    Area = db.Farms.Where(f => f.SupplyChain.Supplier.CountryId == id).Sum(f => Convert.ToDouble(f.Productivity.TotalHectares));
                    break;
                case "Supplier":
                    Area = db.Farms.Where(f => f.SupplyChain.Supplier.Id == id).Sum(f => Convert.ToDouble(f.Productivity.TotalHectares));
                    break;
                case "SupplierChain":
                    Area = db.Farms.Where(f => f.SupplyChain.Id == id).Sum(f => Convert.ToDouble(f.Productivity.TotalHectares));
                    break;
                case "Cooperative":
                    Area = db.Farms.Where(f => f.CooperativeId == id).Sum(f => Convert.ToDouble(f.Productivity.TotalHectares));
                    break;
                default:
                    break;
            }
            return Area;
        }

        /// <summary>
        /// Alls the queryable.
        /// </summary>
        /// <returns>
        /// IQuerable of Farm
        /// </returns>
        public IQueryable<Farm> AllQueryable()
        {
            return _farmRepository.GetAll();
        }

        /// <summary>
        /// Gets all Farms.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filterSpecification">The filter specification.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>
        /// ICollection of FarmDTO
        /// </returns>
        public ICollection<FarmDTO> GetAll<KProperty>(Specification<Farm> filterSpecification, Expression<Func<Farm, KProperty>> orderByExpression)
        {
            var result = _farmRepository.AllMatching(filterSpecification, 
                "Village", 
                "Village.Municipality",
                "Village.Municipality.Department")
                .OrderBy(orderByExpression);
            return Mapper.Map<ICollection<FarmDTO>>(result.ToList());
        }

        /// <summary>
        /// Gets the by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public ICollection<FarmDTO> GetByCode(string code)
        {
            var result = _farmRepository.FarmByCode(code);
            return Mapper.Map<ICollection<FarmDTO>>(result);
        }

        /// <summary>
        /// Gets the farm by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public FarmDTO GetFarmByCode(string code)
        {
            var result = _farmRepository.GetFarmByCode(code);
            return Mapper.Map<FarmDTO>(result);
        }

        /// <summary>
        /// Gets all queryable.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filterSpecification">The filter specification.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>
        /// IQuerable of Farm
        /// </returns>
        public IQueryable<Farm> GetAllQueryable<KProperty>(Specification<Farm> filterSpecification, Expression<Func<Farm, KProperty>> orderByExpression)
        {
            var result = _farmRepository.AllMatching(filterSpecification,
                "Village",
                "Village.Municipality",
                "Village.Municipality.Department")
                .OrderBy(orderByExpression);
            return result;
        }

        /// <summary>
        /// Creates the specified farm dto.
        /// </summary>
        /// <param name="farmDTO">The farm dto.</param>
        /// <returns>
        /// FarmDTO
        /// </returns>
        public FarmDTO Create(FarmDTO farmDTO)
        {
            try
            {
                var farm = Mapper.Map<Farm>(farmDTO);
                _farmRepository.Add(farm);
                _farmRepository.UnitOfWork.Commit();
                return Mapper.Map<FarmDTO>(farm);
            }
            catch (Exception ex)
            {
                return farmDTO;
            }
        }

        /// <summary>
        /// Creates the without commit.
        /// </summary>
        /// <param name="farmDTO">The farm dto.</param>
        public void CreateWithoutCommit(FarmDTO farmDTO)
        {
            try
            {
                var farm = Mapper.Map<Farm>(farmDTO);
                _farmRepository.Add(farm);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException db)
            {
                
            }
        }

        /// <summary>
        /// Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>
        /// FarmDTO
        /// </returns>
        public FarmDTO Details(Guid id, params string[] includes)
        {
            return Mapper.Map<FarmDTO>(_farmRepository.Get(id, includes));
        }

        /// <summary>
        /// Bies the family member identification.
        /// </summary>
        /// <param name="identification">The identification.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>
        /// FarmDTO
        /// </returns>
        public FarmDTO ByFamilyMemberIdentification(string identification, params string[] includes)
        {
            var member = _familyUnitRepository.AllMatching(FamilyUnitSpecification.ByIdentification(identification)).FirstOrDefault();
            if (member != null)
                return Mapper.Map<FarmDTO>(_farmRepository.Get(member.FarmId, includes));
            return null;
        }

        /// <summary>
        /// Edits the specified identifier Farm.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmDTO">The farm dto.</param>
        /// <param name="mainController">The main controller.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool Edit(Guid id, FarmDTO farmDTO, string mainController, params string[] includes)
        {
            try
            {
                var farm = Mapper.Map<Farm>(farmDTO);
                var persisted = _farmRepository.Get(id, includes);
                UpdateFarmInfo(mainController, farm, persisted);
                _farmRepository.UnitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Edits the without commit.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="farmDTO">The farm dto.</param>
        /// <param name="mainController">The main controller.</param>
        public void EditWithoutCommit(Guid id, FarmDTO farmDTO, string mainController)
        {
            try
            {
                var farm = Mapper.Map<Farm>(farmDTO);
                var persisted = _farmRepository.Get(id);
                UpdateFarmInfo(mainController, farm, persisted);
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Removes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool remove(Guid id)
        {
            try
            {
                var farm = _farmRepository.Get(id);
                _farmRepository.Remove(farm);
                _farmRepository.UnitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            try
            {
                _farmRepository.UnitOfWork.CommitWithoutTracking();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException db)
            {
            }
        }

        /// <summary>
        /// Initializes the list.
        /// </summary>
        /// <returns>
        /// Dictionary
        /// </returns>
        public Dictionary<string, List<string>> InitializeList()
        {
            return FamilyUnitMember.InitializeList();
        }

        #region Private Methods
        /// <summary>
        /// Updates the farm information.
        /// </summary>
        /// <param name="mainController">The main controller.</param>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        /// <exception cref="System.Exception"></exception>
        private void UpdateFarmInfo(string mainController, Farm farm, Farm persisted)
        {
            switch (mainController)
            {
                case IMAGES:
                    UpdateImages(farm, persisted);
                    break;
                case FAMILY_UNIT_MEMBERS:
                    UpdateFamilyUnitMembers(farm, persisted);
                    break;
                case OTHER_ACTIVITIES:
                    UpdateOtherActivities(farm, persisted);
                    break;
                case FERTILIZERS:
                    UpdateFertilizers(farm, persisted);
                    break;
                case FLOWERING_PERIODS:
                    UpdateFloweringPeriods(farm, persisted);
                    break;
                case PLANTATIONS:
                    UpdateProductivity(farm, persisted);
                    UpdatePlantations(farm, persisted);
                    break;
                case SOIL_ANALYSIS:
                    UpdateSoilAnalysis(farm, persisted);
                    break;
                case PROJECTS:
                    UpdateProjects(farm, persisted);
                    break;
                case FARMS:
                    _farmRepository.Merge(persisted, farm);
                    //UpdateFarm(farm, persisted);
                    UpdateProductivityArea(farm, persisted);
                    UpdateSoilTypes(farm, persisted);
                    UpdateAssociatedPeople(farm, persisted);
                    break;
                default: throw new Exception(ExceptionMessage.Controller_Not_Supported);
            }
        }

        /// <summary>
        /// Updates the images.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateImages(Farm farm, Farm persisted)
        {
            var uow = (UnitOfWork)_farmRepository.UnitOfWork;

            var added = farm.Images.Except(persisted.Images, new EntityComparer<Image>()).ToList();
            var removed = persisted.Images.Except(farm.Images, new EntityComparer<Image>()).ToList();
            var edited = farm.Images.Except(added, new EntityComparer<Image>()).ToList();

            foreach (var item in added)
            {
                var Image = db.Images.Where(i => i.FarmId == item.FarmId && i.Name == item.Name && i.Size == item.Size && i.ThumbName == item.ThumbName).ToList();
                if(Image.Count() == 0)
                {
                    uow.Add(item);
                }
                // Already have the path, uncomment when Azure is used
                //item.Url = _storage.Upload(item.FarmId, item.Id, item.Name, item.Url);
                //item.Thumb = _storage.Upload(item.FarmId, item.Id, item.ThumbName, item.Thumb);
            }
            foreach (var item in removed)
            {
                uow.Remove(item);
                // Uncomment when azure is used
                //_storage.Remove(item.FarmId, item.Id, item.Name);
                //_storage.Remove(item.FarmId, item.Id, item.ThumbName);
            }
            foreach (var item in edited)
            {
                var actual = persisted.Images.First(i => i.Id.Equals(item.Id));
                uow.SetModified(actual);
                uow.ApplyCurrentValues(actual, item);
            }
        }

        /// <summary>
        /// Updates the family unit members.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateFamilyUnitMembers(Farm farm, Farm persisted)
        {
            var uow = (UnitOfWork)_farmRepository.UnitOfWork;
            var added = farm.FamilyUnitMembers.Except(persisted.FamilyUnitMembers, new EntityComparer<FamilyUnitMember>());
            var removed = persisted.FamilyUnitMembers.Except(farm.FamilyUnitMembers, new EntityComparer<FamilyUnitMember>());
            var edited = farm.FamilyUnitMembers.Except(added, new EntityComparer<FamilyUnitMember>());

            added.ToList().ForEach(fum => uow.FamilyUnitMembers.Add(fum));
            removed.ToList().ForEach(fum => uow.FamilyUnitMembers.Remove(fum));

            foreach (var item in edited.ToList())
            {
                var actual = persisted.FamilyUnitMembers.First(fum => fum.Id.Equals(item.Id));
                actual.UpdatedAt = DateTime.Now;
                item.UpdatedAt = DateTime.Now;
                uow.ApplyCurrentValues(actual, item);
            }
        }

        /// <summary>
        /// Updates the other activities.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateOtherActivities(Farm farm, Farm persisted)
        {
            var uow = (UnitOfWork)_farmRepository.UnitOfWork;

            var added = farm.OtherActivities.Except(persisted.OtherActivities, new EntityComparer<FarmOtherActivity>());
            var removed = persisted.OtherActivities.Except(farm.OtherActivities, new EntityComparer<FarmOtherActivity>());
            var edited = farm.OtherActivities.Except(added, new EntityComparer<FarmOtherActivity>());

            added.ToList().ForEach(foa => uow.Add(foa));
            removed.ToList().ForEach(foa => uow.Remove(foa));
            foreach (var item in edited.ToList())
            {
                var actual = persisted.OtherActivities.First(foa => foa.Id.Equals(item.Id));
                uow.ApplyCurrentValues(actual, item);
            }
        }

        /// <summary>
        /// Updates the fertilizers.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateFertilizers(Farm farm, Farm persisted)
        {
            var uow = (UnitOfWork)_farmRepository.UnitOfWork;

            var added = farm.Fertilizers.Except(persisted.Fertilizers, new EntityComparer<Fertilizer>());
            var removed = persisted.Fertilizers.Except(farm.Fertilizers, new EntityComparer<Fertilizer>());
            var edited = farm.Fertilizers.Except(added, new EntityComparer<Fertilizer>());

            added.ToList().ForEach(f => uow.Add(f));
            removed.ToList().ForEach(f => uow.Remove(f));
            foreach (var item in edited.ToList())
            {
                var actual = persisted.Fertilizers.First(sa => sa.Id.Equals(item.Id));
                uow.ApplyCurrentValues(actual, item);
            }
        }

        /// <summary>
        /// Updates the farm.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateFarm(Farm farm, Farm persisted)
        {
            var uow = (UnitOfWork)_farmRepository.UnitOfWork;
            if (persisted.Worker == null)
            {
                farm.Worker.Id = farm.Id;
                uow.Workers.Add(farm.Worker);
            }
            else
            {
                uow.ApplyCurrentValues(persisted.Worker, farm.Worker);
            }
        }

        private void UpdateProductivityArea(Farm farm, Farm persisted)
        {
            double conservation = 0;
            double infrastructure = 0;
            double protectedH = 0;
            double coffeeArea = 0;
            double OthersProductivities = 0;

            var farm1 = persisted;

            if(farm.Productivity.ConservationHectares != null)
                conservation = Convert.ToDouble(farm.Productivity.ConservationHectares.Replace(".", ","));
            if (farm.Productivity.InfrastructureHectares != null)
                infrastructure = Convert.ToDouble(farm.Productivity.InfrastructureHectares.Replace(".", ","));
            if (farm.Productivity.ForestProtectedHectares != null)
                protectedH = Convert.ToDouble(farm.Productivity.ForestProtectedHectares.Replace(".", ","));
            if (persisted.Productivity.coffeeArea != null)
                coffeeArea = Convert.ToDouble(persisted.Productivity.coffeeArea.Replace(".", ","));

            var Dif_Coffe = farm1.Productivity.Plantations.Where(x=> x.PlantationTypeId != new Guid("D221BEC9-5F73-43A0-9EBF-16417F5674F5"));

            if (Dif_Coffe != null)
            {
                foreach (var item in Dif_Coffe)
                {
                    OthersProductivities = OthersProductivities + Convert.ToDouble(item.Hectares);
                }
            }
            

            farm1.Productivity.ConservationHectares = conservation.ToString();
            farm1.Productivity.InfrastructureHectares = infrastructure.ToString();
            farm1.Productivity.ForestProtectedHectares = protectedH.ToString();
            farm1.Productivity.coffeeArea = coffeeArea.ToString();

            farm1.Productivity.TotalHectares = (conservation +
                infrastructure +
                protectedH +
                coffeeArea+ OthersProductivities).ToString();

            var uow = (UnitOfWork)_farmRepository.UnitOfWork;
            if (persisted.Productivity == null)
            {
                farm.Productivity.Id = farm1.Id;
                uow.Productivities.Add(farm1.Productivity);
            }
            else
            {
                uow.ApplyCurrentValues(persisted.Productivity, farm1.Productivity);
            }
        }

        /// <summary>
        /// Updates the productivity.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateProductivity(Farm farm, Farm persisted)
        {
            //var pants = farm.Productivity.Plantations.Where(t => t.PlantationTypeId == new Guid("{D221BEC9-5F73-43A0-9EBF-16417F5674F5}"));
            //var plants = farm.Productivity.Plantations.Where(t => t.PlantationTypeId == new Guid("{D221BEC9-5F73-43A0-9EBF-16417F5674F5}"));
            var pants = farm.Productivity.Plantations;
            var plants = farm.Productivity.Plantations.Where(t => t.PlantationTypeId == new Guid("{D221BEC9-5F73-43A0-9EBF-16417F5674F5}"));
            if (!farm.Productivity.Plantations.Any())
            {
                pants = persisted.Productivity.Plantations;
            }

            double totalHectareas = 0;
            double totalPlants = 0;
            foreach (var plantation in pants)
            {
                plantation.Hectares = plantation.Hectares.Replace(".", ",");
                totalHectareas += Convert.ToDouble(plantation.Hectares) * 1.0;
                totalPlants += plantation.NumberOfPlants;
            }

            double coffeHectareas = 0;
            foreach (var plantation in plants)
            {
                plantation.Hectares = plantation.Hectares.Replace(".", ",");
                coffeHectareas += Convert.ToDouble(plantation.Hectares) * 1.0;
            }

            double averageDensity = 0;
            double averageAge = 0;
            double estimatedProduction = 0;
            double productionPlants = 0;
            double growingPlants = 0;
            double colombiaHectares = 0;
            double caturraHectares = 0;
            double castilloHectares = 0;
            double otherHectares = 0;
            double productionHectares = 0;
            double growingHectares = 0;
            string now = DateTime.Now.ToString();

            foreach (var plantation in plants)
            {
                plantation.Hectares = plantation.Hectares.Replace(".", ",");
                plantation.EstimatedProduction = plantation.EstimatedProduction.Replace(".", ",");
                double percentage = (Convert.ToDouble(plantation.Hectares) * 1.0) / totalHectareas;
                TimeSpan dateAge = DateTime.Now.Subtract(plantation.Age);
                double Age = (dateAge.Days * 1.0) / 365;
                if (plantation.PlantationVariety == null)
                {
                    plantation.PlantationVariety = db.PlantationVarieties.Find(plantation.PlantationVarietyId);
                }
                string variety = plantation.PlantationVariety.Name;
                switch (variety)
                {
                    case "COLOMBIA":
                        colombiaHectares += Convert.ToDouble(plantation.Hectares) * 1.0;
                        break;
                    case "CATURRA":
                        caturraHectares += Convert.ToDouble(plantation.Hectares) * 1.0;
                        break;
                    case "CASTILLO":
                        castilloHectares += Convert.ToDouble(plantation.Hectares) * 1.0;
                        break;
                    default:
                        otherHectares += Convert.ToDouble(plantation.Hectares) * 1.0;
                        break;
                }
                
                if(Age >= 2)
                {
                    productionPlants += plantation.NumberOfPlants;
                    productionHectares += Convert.ToDouble(plantation.Hectares) * 1.0;
                }
                else
                {
                    growingPlants += plantation.NumberOfPlants;
                    growingHectares += Convert.ToDouble(plantation.Hectares) * 1.0;
                }

                estimatedProduction += (Convert.ToDouble(plantation.EstimatedProduction) * 1.0);// * plantation.NumberOfPlants;

                averageAge += Age * percentage;

                if (!plantation.Density.Contains("Infinity") && !plantation.Density.Contains("Density"))
                {
                    averageDensity += (Convert.ToDouble(plantation.Density.Replace(".", ",")) * 1.0) * percentage;
                }
                else {
                    plantation.Density = "0";
                    averageDensity += (Convert.ToDouble(plantation.Density.Replace(".", ",")) * 1.0) * percentage;
                }

                plantation.PlantationVariety = null;
            }

            farm.Productivity.UpdatedAt = DateTime.Now;
            farm.Productivity.coffeeArea = coffeHectareas.ToString();

            farm.Productivity.TotalHectares = (Convert.ToDouble(farm.Productivity.ConservationHectares.Replace(".", ",")) +
                Convert.ToDouble(farm.Productivity.InfrastructureHectares.Replace(".", ",")) +
                Convert.ToDouble(farm.Productivity.ForestProtectedHectares.Replace(".", ",")) +
                + Convert.ToDouble(totalHectareas)).ToString();

            if (totalHectareas == 0)
            {
                farm.Productivity.percentageColombia = 0;
                farm.Productivity.percentageCaturra = 0;
                farm.Productivity.percentageCastillo = 0;
                farm.Productivity.percentageotra = 0;
                farm.Productivity.growingAreaPercentage = 0;
                farm.Productivity.productionAreaPercentage = 0;
            }
            else
            {
                farm.Productivity.percentageColombia = Math.Round((colombiaHectares / totalHectareas) * 100.0,3);
                farm.Productivity.percentageCaturra = Math.Round((caturraHectares / totalHectareas) * 100.0, 3);
                farm.Productivity.percentageCastillo = Math.Round((castilloHectares / totalHectareas) * 100.0, 3);
                farm.Productivity.percentageotra = Math.Round((otherHectares / totalHectareas) * 100.0, 3);
                farm.Productivity.growingAreaPercentage = Math.Round((growingHectares / totalHectareas) * 100.0, 3);
                farm.Productivity.productionAreaPercentage = Math.Round((productionHectares / totalHectareas) * 100.0, 3);
            }

            farm.Productivity.growingPlants = growingPlants;
            farm.Productivity.productionPlants = productionPlants;

            if (totalPlants == 0)
            {
                farm.Productivity.growingPercentage = 0;
                farm.Productivity.productionPercentage = 0;

                productionPlants = 0;
                growingPlants = 0;
                estimatedProduction = 0;
            }
            else
            {
                farm.Productivity.growingPercentage = Math.Round((farm.Productivity.growingPlants / totalPlants) * 100.0, 3);
                farm.Productivity.productionPercentage = Math.Round((farm.Productivity.productionPlants / totalPlants) * 100.0, 3);
            }
         
            farm.Productivity.growingArea = growingHectares.ToString();
            farm.Productivity.productionArea = productionHectares.ToString();
            farm.Productivity.estimatedProduction = Math.Round(estimatedProduction, 3);
            if(averageAge.ToString() == "NaN")
            {
                farm.Productivity.averageAge = 0;
            }
            else
            {
                farm.Productivity.averageAge = Math.Round(averageAge, 3);
            }
            farm.Productivity.averageDensity = averageDensity.ToString();

            var uow = (UnitOfWork)_farmRepository.UnitOfWork;
            if (persisted.Productivity == null)
            {
                farm.Productivity.Id = farm.Id;
                uow.Productivities.Add(farm.Productivity);
            }
            else
            {
                uow.ApplyCurrentValues(persisted.Productivity, farm.Productivity);
            }

            //actualizar total hectareas
            decimal totalHectareas1 = 0;
            if (farm.Productivity.Plantations.Count > 0)
            {
                foreach (var p in farm.Productivity.Plantations)
                {
                    //if (p.PlantationTypeId == new Guid("D221BEC9-5F73-43A0-9EBF-16417F5674F5"))
                    //{
                    //    totalHectareas = totalHectareas + Convert.ToDecimal(p.Hectares);
                    //}

                    totalHectareas1 = totalHectareas1 + Convert.ToDecimal(p.Hectares);
                }
            }

            decimal infHct = farm.Productivity.InfrastructureHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.InfrastructureHectares);
            decimal fpHct = farm.Productivity.ForestProtectedHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ForestProtectedHectares);
            decimal conHct = farm.Productivity.ConservationHectares == "" ? 0 : Convert.ToDecimal(farm.Productivity.ConservationHectares);
            //decimal othHct = farm.Productivity.OthersHectareas == "" ? 0 : Convert.ToDecimal(farm.Productivity.OthersHectareas);

            totalHectareas1 = totalHectareas1 + infHct + fpHct + conHct;

            var th = Convert.ToDouble(totalHectareas1);

            if (farm.Productivity.TotalHectares != totalHectareas1.ToString())
            {
                try
                {
                    farm.Productivity.TotalHectares = totalHectareas1.ToString();

                    var productivity = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);
                    productivity.TotalHectares = totalHectareas1.ToString();
                    db.SaveChanges();
                }
                catch
                {
                    farm.Productivity.TotalHectares = totalHectareas1.ToString();
                }
            }

            var idFarm = farm.Id;

            //PORCENTAJE COLOMBIA
            var PerColombia = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7"));
            double sumPerColombia = 0;
            foreach (var item in PerColombia)
            {
                sumPerColombia = sumPerColombia + Convert.ToDouble(item.Hectares);
            }

            //PORCENTAJE CATURRA
            var PerCaturra = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"));
            double sumPerCaturra = 0;
            foreach (var item in PerCaturra)
            {
                sumPerCaturra = sumPerCaturra + Convert.ToDouble(item.Hectares);
            }

            //PORCENTAJE CASTILLO
            var PerCastillo = db.Plantations.Where(x => x.ProductivityId == idFarm && x.PlantationVarietyId == new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C"));
            double sumPerCastillo = 0;
            foreach (var item in PerCastillo)
            {
                sumPerCastillo = sumPerCastillo + Convert.ToDouble(item.Hectares);
            }

            //PORCENTAJE OTRO
            var PerOtro = db.Plantations.Where(x => x.ProductivityId == idFarm);
            double sumPerOtro = 0;
            foreach (var item in PerOtro)
            {
                if (item.PlantationVarietyId != new Guid("AD0BD175-CC13-43D8-B95A-907F92B00FA7") && item.PlantationVarietyId != new Guid("99B1D465-44EE-4633-BDA1-F6CA6AEF5A2C") && item.PlantationVarietyId != new Guid("3C9722D9-302D-44FC-8CA3-EDA865493B44"))
                {
                    sumPerOtro = sumPerOtro + Convert.ToDouble(item.Hectares);
                }
            }

            double TotalHectareas2 = Convert.ToDouble(totalHectareas1);

            //OPERACIONES PORCENTAJES    
            var opeColombia = Math.Round((sumPerColombia / (coffeHectareas + sumPerOtro)) * 100.0,3);
            var opeCaturra = Math.Round((sumPerCaturra / (coffeHectareas + sumPerOtro)) * 100.0, 3);
            var opeCastillo = Math.Round((sumPerCastillo / (coffeHectareas + sumPerOtro)) * 100.0, 3);
            var opeOtro = Math.Round((sumPerOtro / (TotalHectareas2)) * 100.0, 3);


            //ACTUALIZACION
            var sum = opeColombia + opeCaturra + opeCastillo + opeOtro;
            var dif = 0.0;
            if (sum > 100)
            {
                dif = Convert.ToDouble(sum) - Convert.ToDouble(100);
                if (Math.Round(Convert.ToDecimal(opeOtro), 2) != Math.Round(Convert.ToDecimal(dif), 2))
                {
                    opeOtro = Convert.ToDouble(opeOtro) - Convert.ToDouble(dif);
                }
                
            }
            else if (opeColombia == 0 && opeCaturra == 0 && opeCastillo == 0)
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

            //var productivitiesChange2 = db.Productivities.FirstOrDefault(f => f.Id == farm.Id);

            //Thread.Sleep(6000);
        }

        /// <summary>
        /// Updates the flowering periods.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateFloweringPeriods(Farm farm, Farm persisted)
        {
            var uow = (UnitOfWork)_farmRepository.UnitOfWork;

            List<FloweringPeriod> periods = new List<FloweringPeriod>();
            foreach (var plantation in farm.Productivity.Plantations)
            {
                periods.AddRange(plantation.FloweringPeriods);

            }
            List<FloweringPeriod> persistedPeriods = new List<FloweringPeriod>();
            foreach (var plantation in persisted.Productivity.Plantations)
            {
                persistedPeriods.AddRange(plantation.FloweringPeriods);
            }

            var added = periods.Except(persistedPeriods, new EntityComparer<FloweringPeriod>());
            var removed = persistedPeriods.Except(periods, new EntityComparer<FloweringPeriod>());

            added.ToList().ForEach(p => uow.FloweringPeriods.Add(p));
            removed.ToList().ForEach(p => uow.FloweringPeriods.Remove(p));
        }

        /// <summary>
        /// Updates the plantations.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdatePlantations(Farm farm, Farm persisted)
        {
            var uow = (UnitOfWork)_farmRepository.UnitOfWork;
            var added = farm.Productivity.Plantations.Except(persisted.Productivity.Plantations, new EntityComparer<Plantation>());
            var removed = persisted.Productivity.Plantations.Except(farm.Productivity.Plantations, new EntityComparer<Plantation>());
            var edited = farm.Productivity.Plantations.Except(added, new EntityComparer<Plantation>());

            added.ToList().ForEach(p => persisted.Productivity.Plantations.Add(p));
            removed.ToList().ForEach(p => uow.Plantations.Remove(p));
            foreach (var item in edited.ToList())
            {
                var actual = persisted.Productivity.Plantations.First(p => p.Id.Equals(item.Id));
                uow.ApplyCurrentValues(actual, item);
            }
        }

        /// <summary>
        /// Updates the soil analysis.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateSoilAnalysis(Farm farm, Farm persisted)
        {
            var uow = (UnitOfWork)_farmRepository.UnitOfWork;

            var added = farm.SoilAnalysis.Except(persisted.SoilAnalysis, new EntityComparer<SoilAnalysis>());
            var removed = persisted.SoilAnalysis.Except(farm.SoilAnalysis, new EntityComparer<SoilAnalysis>());
            var edited = farm.SoilAnalysis.Except(added, new EntityComparer<SoilAnalysis>());

            added.ToList().ForEach(sa => uow.Add<SoilAnalysis>(sa));
            removed.ToList().ForEach(sa => uow.Remove<SoilAnalysis>(sa));
            foreach (var item in edited.ToList())
            {
                var actual = persisted.SoilAnalysis.First(sa => sa.Id.Equals(item.Id));
                uow.ApplyCurrentValues<SoilAnalysis>(actual, item);
            }
        }

        /// <summary>
        /// Updates the soil types.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateSoilTypes(Farm farm, Farm persisted)
        {
            var added = farm.SoilTypes.Except(persisted.SoilTypes, new EntityComparer<SoilType>());
            var removed = persisted.SoilTypes.Except(farm.SoilTypes, new EntityComparer<SoilType>()).ToList();

            foreach (var item in added)
            {
                var toAdd = _soilTypeRepository.Get(item.Id);
                persisted.SoilTypes.Add(toAdd);
            }
            foreach (var item in removed)
            {
                var toRemove = _soilTypeRepository.Get(item.Id);
                persisted.SoilTypes.Remove(toRemove);
            }
        }

        /// <summary>
        /// Updates the projects.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateProjects(Farm farm, Farm persisted)
        {
            var added = farm.Projects.Except(persisted.Projects, new EntityComparer<Project>());
            var removed = persisted.Projects.Except(farm.Projects, new EntityComparer<Project>()).ToList();

            foreach (var item in added)
            {
                var toAdd = _projectRepository.Get(item.Id);
                persisted.Projects.Add(toAdd);
            }
            foreach (var item in removed)
            {
                var toRemove = _projectRepository.Get(item.Id);
                persisted.Projects.Remove(toRemove);
            }
        }

        /// <summary>
        /// Updates the associated people.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <param name="persisted">The persisted.</param>
        private void UpdateAssociatedPeople(Farm farm, Farm persisted)
        {
            var added = farm.AssociatedPeople.Except(persisted.AssociatedPeople, new EntityComparer<User>());
            var removed = persisted.AssociatedPeople.Except(farm.AssociatedPeople, new EntityComparer<User>()).ToList();

            foreach (var item in added)
            {
                var toAdd = _userRepository.Get(item.Id);
                persisted.AssociatedPeople.Add(toAdd);
            }
            foreach (var item in removed)
            {
                var toRemove = _userRepository.Get(item.Id);
                persisted.AssociatedPeople.Remove(toRemove);
            }
        }
        #endregion


        /// <summary>
        /// Calculates the density.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <returns>
        /// FarmDTO
        /// </returns>
        public FarmDTO CalculateDensity(FarmDTO farm)
        {
            farm.Hectares = farm.Productivity.Plantations.Sum(p => Convert.ToDouble(p.Hectares));
            farm.Plants = farm.Productivity.Plantations.Sum(p => p.NumberOfPlants);

            return farm;
        }


        /// <summary>
        /// Calculates the fertilizer.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <returns>
        /// FarmDTO
        /// </returns>
        public FarmDTO CalculateFertilizer(FarmDTO farm)
        {
            farm.FertilizerBags = farm.Fertilizers.Where(f => f.Date.Year == DateTime.Now.Year).Sum(f => f.Quantity);
            farm.ProductivePlants = farm.Productivity.Plantations.Sum(p => p.NumberOfPlants);
            farm.Hectares = farm.Productivity.Plantations.Sum(p => Convert.ToDouble(p.Hectares));

            return farm;
        }


        /// <summary>
        /// Calculates the productivity.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <returns>
        /// FarmDTO
        /// </returns>
        public FarmDTO CalculateProductivity(FarmDTO farm)
        {
            farm.EstimatedProduction = farm.Productivity.Plantations.Sum(p => Convert.ToDouble(p.EstimatedProduction) / Convert.ToDouble(p.Hectares));
            farm.Hectares = farm.Productivity.Plantations.Sum(p => Convert.ToDouble(p.Hectares));

            return farm;
        }

        /// <summary>
        /// Calculates the age.
        /// </summary>
        /// <param name="farm">The farm.</param>
        /// <returns>
        /// FarmDTO
        /// </returns>
        //public FarmDTO CalculateAge(FarmDTO farm)
        //{
        //    farm.AgeIndicator = Convert.ToInt32(Math.Round(farm.FamilyUnitMembers.Average(fum => fum.Age), MidpointRounding.AwayFromZero));
            
        //    return farm;
        //}
    }
}
