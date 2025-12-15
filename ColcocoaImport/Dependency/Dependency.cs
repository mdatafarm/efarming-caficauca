using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EFarming.Common.Caching;
using EFarming.Common.Encription;
using EFarming.Common.Logging;
using EFarming.Common.Validator;
using EFarming.Core;
using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.AdminModule.CooperativeAggregate;
using EFarming.Core.AdminModule.CountryAggregate;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.Core.AdminModule.FarmStatusAggregate;
using EFarming.Core.AdminModule.FarmSubstatusAggregate;
using EFarming.Core.AdminModule.FloweringPeriodQualificationAggregate;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.OtherActivitiesAggregate;
using EFarming.Core.AdminModule.OwnershipTypeAggregate;
using EFarming.Core.AdminModule.PlantationStatusAggregate;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using EFarming.Core.AdminModule.SoilTypeAggregate;
using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.CoreServices.Contracts;
using EFarming.Core.CoreServices.Implementation;
using EFarming.Core.FarmModule.FamilyUnitAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.Core.TraceabilityModule.LotAggregate;
using EFarming.DAL;
using EFarming.DAL.Contract;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Conversion;
using EFarming.Manager.Implementation;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Repository.AdminModule;
using EFarming.Repository.FarmModule;
using EFarming.Repository.ImpactModule;
using EFarming.Repository.ProjectModule;
using EFarming.Repository.QualityModule;
using EFarming.Repository.TraceabilityModule;

namespace EFarmingConsole.Dependency
{
    public class DependencyConventions : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(

                        Component.For<IQueryableUnitOfWork, UnitOfWork>().ImplementedBy<UnitOfWork>(),

                        Component.For<IFarmRepository, FarmRepository>().ImplementedBy<FarmRepository>(),
                        Component.For<IFamilyUnitRepository, FamilyUnitRepository>().ImplementedBy<FamilyUnitRepository>(),
                        Component.For<ICategoryRepository, CategoryRepository>().ImplementedBy<CategoryRepository>(),
                        Component.For<IIndicatorRepository, IndicatorRepository>().ImplementedBy<IndicatorRepository>(),
                        Component.For<ICriteriaRepository, CriteriaRepository>().ImplementedBy<CriteriaRepository>(),
                        Component.For<ICriteriaOptionRepository, CriteriaOptionRepository>().ImplementedBy<CriteriaOptionRepository>(),
                        Component.For<IAssessmentTemplateRepository, AssessmentTemplateRepository>().ImplementedBy<AssessmentTemplateRepository>(),
                        Component.For<IImpactAssessmentRepository, ImpactAssessmentRepository>().ImplementedBy<ImpactAssessmentRepository>(),
                        Component.For<IQualityAttributeRepository, QualityAttributeRepository>().ImplementedBy<QualityAttributeRepository>(),
                        Component.For<ISensoryProfileRepository, SensoryProfileRepository>().ImplementedBy<SensoryProfileRepository>(),
                        Component.For<IInvoiceRepository, InvoiceRepository>().ImplementedBy<InvoiceRepository>(),
                        Component.For<ILotRepository, LotRepository>().ImplementedBy<LotRepository>(),

                        Component.For<IFarmManager, FarmManager>().ImplementedBy<FarmManager>(),
                        Component.For<IIndicatorManager, IndicatorManager>().ImplementedBy<IndicatorManager>(),
                        Component.For<IImpactManager, ImpactManager>().ImplementedBy<ImpactManager>(),
                        Component.For<IQualityAttributeManager, QualityAttributeManager>().ImplementedBy<QualityAttributeManager>(),
                        Component.For<IQualityProfileManager, QualityProfileManager>().ImplementedBy<QualityProfileManager>(),
                        Component.For<ISensoryProfileManager, SensoryProfileManager>().ImplementedBy<SensoryProfileManager>(),
                        Component.For<IAssessmentTemplateManager, AssessmentTemplateManager>().ImplementedBy<AssessmentTemplateManager>(),
                        Component.For<IInvoiceManager, InvoiceManager>().ImplementedBy<InvoiceManager>(),
                        Component.For<ILotManager, LotManager>().ImplementedBy<LotManager>(),

                        Component.For<ICountryRepository, CountryRepository>().ImplementedBy<CountryRepository>(),
                        Component.For<ISupplierRepository, SupplierRepository>().ImplementedBy<SupplierRepository>(),
                        Component.For<ISupplyChainRepository, SupplyChainRepository>().ImplementedBy<SupplyChainRepository>(),
                        Component.For<IDepartmentRepository, DepartmentRepository>().ImplementedBy<DepartmentRepository>(),
                        Component.For<IMunicipalityRepository, MunicipalityRepository>().ImplementedBy<MunicipalityRepository>(),
                        Component.For<IVillageRepository,VillageRepository>().ImplementedBy<VillageRepository>(),
                        Component.For<IFarmStatusRepository, FarmStatusRepository>().ImplementedBy<FarmStatusRepository>(),
                        Component.For<IFarmSubstatusRepository, FarmSubstatusRepository>().ImplementedBy<FarmSubstatusRepository>(),
                        Component.For<IOwnershipTypeRepository, OwnershipTypeRepository>().ImplementedBy<OwnershipTypeRepository>(),
                        Component.For<ICooperativeRepository, CooperativeRepository>().ImplementedBy<CooperativeRepository>(),
                        Component.For<ISoilTypeRepository, SoilTypeRepository>().ImplementedBy<SoilTypeRepository>(),
                        Component.For<IPlantationTypeRepository, PlantationTypeRepository>().ImplementedBy<PlantationTypeRepository>(),
                        Component.For<IPlantationVarietyRepository, PlantationVarietyRepository>().ImplementedBy<PlantationVarietyRepository>(),
                        Component.For<IPlantationStatusRepository, PlantationStatusRepository>().ImplementedBy<PlantationStatusRepository>(),
                        Component.For<IFloweringPeriodQualificationRepository, FloweringPeriodQualificationRepository>().ImplementedBy<FloweringPeriodQualificationRepository>(),
                        Component.For<IOtherActivityRepository, OtherActivityRepository>().ImplementedBy<OtherActivityRepository>(),
                        Component.For<IProjectRepository, ProjectRepository>().ImplementedBy<ProjectRepository>(),

                        Component.For<ICountryManager, CountryManager>().ImplementedBy<CountryManager>(),
                        Component.For<ISupplierManager, SupplierManager>().ImplementedBy<SupplierManager>(),
                        Component.For<ISupplyChainManager, SupplyChainManager>().ImplementedBy<SupplyChainManager>(),
                        Component.For<IDepartmentManager, DepartmentManager>().ImplementedBy<DepartmentManager>(),
                        Component.For<IMunicipalityManager, MunicipalityManager>().ImplementedBy<MunicipalityManager>(),
                        Component.For<IVillageManager, VillageManager>().ImplementedBy<VillageManager>(),
                        Component.For<IFarmStatusManager, FarmStatusManager>().ImplementedBy<FarmStatusManager>(),
                        Component.For<IFarmSubstatusManager, FarmSubstatusManager>().ImplementedBy<FarmSubstatusManager>(),
                        Component.For<IOwnershipTypeManager, OwnershipTypeManager>().ImplementedBy<OwnershipTypeManager>(),
                        Component.For<ICooperativeManager, CooperativeManager>().ImplementedBy<CooperativeManager>(),
                        Component.For<ISoilTypeManager, SoilTypeManager>().ImplementedBy<SoilTypeManager>(),
                        Component.For<IPlantationTypeManager, PlantationTypeManager>().ImplementedBy<PlantationTypeManager>(),
                        Component.For<IPlantationVarietyManager, PlantationVarietyManager>().ImplementedBy<PlantationVarietyManager>(),
                        Component.For<IPlantationStatusManager, PlantationStatusManager>().ImplementedBy<PlantationStatusManager>(),
                        Component.For<IFloweringPeriodQualificationManager, FloweringPeriodQualificationManager>().ImplementedBy<FloweringPeriodQualificationManager>(),
                        Component.For<IOtherActivityManager, OtherActivityManager>().ImplementedBy<OtherActivityManager>(),
                        Component.For<IProjectManager, ProjectManager>().ImplementedBy<ProjectManager>(),
                      
                        Component.For<IUserManager, UserManager>().ImplementedBy<UserManager>(),
                        Component.For<IUserRepository, UserRepository>().ImplementedBy<UserRepository>(),
                        Component.For<IRoleRepository, RoleRepository>().ImplementedBy<RoleRepository>(),

                        Component.For<IDashboardServices, DashboardServices>().ImplementedBy<DashboardServices>(),
                        Component.For<IDashboardManager, DashboardManager>().ImplementedBy<DashboardManager>(),

                        Component.For<IStorage, Storage>().ImplementedBy<Storage>()

                        );

            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            EncriptorFactory.SetCurrent(new SHA256EncriptorFactory());
            CacheFactory.SetCurrent(new MemcachedFactory());
            Mapping.Configure();

        }

    }
}
