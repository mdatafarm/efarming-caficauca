using EFarming.Common.Logging;
using EFarming.Common.Validator;
using EFarming.DAL;
using EFarming.DAL.Contract;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Repository.FarmModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using EFarming.Manager.Conversion;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.Repository.AdminModule;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Common.Encription;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.AdminModule.FarmStatusAggregate;
using EFarming.Core.AdminModule.FarmSubstatusAggregate;
using EFarming.Core.AdminModule.OwnershipTypeAggregate;
using EFarming.Core.AdminModule.CooperativeAggregate;
using EFarming.Core.AdminModule.SoilTypeAggregate;
using EFarming.Core.AdminModule.PlantationTypeAggregate;
using EFarming.Core.AdminModule.PlantationVarietyAggregate;
using EFarming.Core.AdminModule.PlantationStatusAggregate;
using EFarming.Core.AdminModule.FloweringPeriodQualificationAggregate;
using EFarming.Core.AdminModule.OtherActivitiesAggregate;
using EFarming.Core.FarmModule.FamilyUnitAggregate;
using EFarming.Core;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Repository.ImpactModule;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Common;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Repository.QualityModule;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.Repository.TraceabilityModule;
using EFarming.Core.CoreServices.Contracts;
using EFarming.Core.CoreServices.Implementation;
using EFarming.Integration.Models;
using System.Web.Http;
using EFarming.Core.AdminModule.CountryAggregate;
using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.Repository.ProjectModule;
using EFarming.Core.TraceabilityModule.LotAggregate;
using EFarming.Core.QualityModule.MicrolotAggregate;

namespace EFarming.Integration.Dependency
{
    public class DependencyConventions : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyNamed("Elmah.Mvc")
                                .BasedOn<IController>()
                                .LifestylePerWebRequest());

            container.Register(Classes.FromThisAssembly()
                                .BasedOn<ApiController>()
                                .LifestylePerWebRequest());

            container.Register(
                        Component.For<IQueryableUnitOfWork, UnitOfWork>().ImplementedBy<UnitOfWork>().LifestylePerWebRequest(),

                        Component.For<IMonitorManager, MonitorManager>().ImplementedBy<MonitorManager>().LifestylePerWebRequest(),

                        Component.For<IFarmRepository, FarmRepository>().ImplementedBy<FarmRepository>().LifestylePerWebRequest(),
                        Component.For<IFamilyUnitRepository, FamilyUnitRepository>().ImplementedBy<FamilyUnitRepository>().LifestylePerWebRequest(),
                        Component.For<ICategoryRepository, CategoryRepository>().ImplementedBy<CategoryRepository>().LifestylePerWebRequest(),
                        Component.For<IIndicatorRepository, IndicatorRepository>().ImplementedBy<IndicatorRepository>().LifestylePerWebRequest(),
                        Component.For<ICriteriaRepository, CriteriaRepository>().ImplementedBy<CriteriaRepository>().LifestylePerWebRequest(),
                        Component.For<ICriteriaOptionRepository, CriteriaOptionRepository>().ImplementedBy<CriteriaOptionRepository>().LifestylePerWebRequest(),
                        Component.For<IAssessmentTemplateRepository, AssessmentTemplateRepository>().ImplementedBy<AssessmentTemplateRepository>().LifestylePerWebRequest(),
                        Component.For<IImpactAssessmentRepository, ImpactAssessmentRepository>().ImplementedBy<ImpactAssessmentRepository>().LifestylePerWebRequest(),
                        Component.For<IQualityAttributeRepository, QualityAttributeRepository>().ImplementedBy<QualityAttributeRepository>().LifestylePerWebRequest(),
                        Component.For<ISensoryProfileRepository, SensoryProfileRepository>().ImplementedBy<SensoryProfileRepository>().LifestylePerWebRequest(),
                        Component.For<IInvoiceRepository, InvoiceRepository>().ImplementedBy<InvoiceRepository>().LifestylePerWebRequest(),
                        Component.For<ILotRepository, LotRepository>().ImplementedBy<LotRepository>().LifestylePerWebRequest(),
                        Component.For<IMicrolotRepository, MicrolotRepository>().ImplementedBy<MicrolotRepository>().LifestylePerWebRequest(),

                        Component.For<IFarmManager, FarmManager>().ImplementedBy<FarmManager>().LifestylePerWebRequest(),
                        Component.For<IIndicatorManager, IndicatorManager>().ImplementedBy<IndicatorManager>().LifestylePerWebRequest(),
                        Component.For<IImpactManager, ImpactManager>().ImplementedBy<ImpactManager>().LifestylePerWebRequest(),
                        Component.For<IQualityAttributeManager, QualityAttributeManager>().ImplementedBy<QualityAttributeManager>().LifestylePerWebRequest(),
                        Component.For<ISensoryProfileManager, SensoryProfileManager>().ImplementedBy<SensoryProfileManager>().LifestylePerWebRequest(),
                        Component.For<IAssessmentTemplateManager, AssessmentTemplateManager>().ImplementedBy<AssessmentTemplateManager>().LifestylePerWebRequest(),
                        Component.For<IInvoiceManager, InvoiceManager>().ImplementedBy<InvoiceManager>().LifestylePerWebRequest(),
                        Component.For<ILotManager, LotManager>().ImplementedBy<LotManager>().LifestylePerWebRequest(),
                        Component.For<IMicrolotManager, MicrolotManager>().ImplementedBy<MicrolotManager>().LifestylePerWebRequest(),

                        Component.For<ICountryRepository, CountryRepository>().ImplementedBy<CountryRepository>().LifestylePerWebRequest(),
                        Component.For<ISupplierRepository, SupplierRepository>().ImplementedBy<SupplierRepository>().LifestylePerWebRequest(),
                        Component.For<ISupplyChainRepository, SupplyChainRepository>().ImplementedBy<SupplyChainRepository>().LifestylePerWebRequest(),
                        Component.For<IDepartmentRepository, DepartmentRepository>().ImplementedBy<DepartmentRepository>().LifestylePerWebRequest(),
                        Component.For<IMunicipalityRepository, MunicipalityRepository>().ImplementedBy<MunicipalityRepository>().LifestylePerWebRequest(),
                        Component.For<IVillageRepository, VillageRepository>().ImplementedBy<VillageRepository>().LifestylePerWebRequest(),
                        Component.For<IFarmStatusRepository, FarmStatusRepository>().ImplementedBy<FarmStatusRepository>().LifestylePerWebRequest(),
                        Component.For<IFarmSubstatusRepository, FarmSubstatusRepository>().ImplementedBy<FarmSubstatusRepository>().LifestylePerWebRequest(),
                        Component.For<IOwnershipTypeRepository, OwnershipTypeRepository>().ImplementedBy<OwnershipTypeRepository>().LifestylePerWebRequest(),
                        Component.For<ICooperativeRepository, CooperativeRepository>().ImplementedBy<CooperativeRepository>().LifestylePerWebRequest(),
                        Component.For<ISoilTypeRepository, SoilTypeRepository>().ImplementedBy<SoilTypeRepository>().LifestylePerWebRequest(),
                        Component.For<IPlantationTypeRepository, PlantationTypeRepository>().ImplementedBy<PlantationTypeRepository>().LifestylePerWebRequest(),
                        Component.For<IPlantationVarietyRepository, PlantationVarietyRepository>().ImplementedBy<PlantationVarietyRepository>().LifestylePerWebRequest(),
                        Component.For<IPlantationStatusRepository, PlantationStatusRepository>().ImplementedBy<PlantationStatusRepository>().LifestylePerWebRequest(),
                        Component.For<IFloweringPeriodQualificationRepository, FloweringPeriodQualificationRepository>().ImplementedBy<FloweringPeriodQualificationRepository>().LifestylePerWebRequest(),
                        Component.For<IOtherActivityRepository, OtherActivityRepository>().ImplementedBy<OtherActivityRepository>().LifestylePerWebRequest(),
                        Component.For<IProjectRepository, ProjectRepository>().ImplementedBy<ProjectRepository>().LifestylePerWebRequest(),

                        Component.For<ICountryManager, CountryManager>().ImplementedBy<CountryManager>().LifestylePerWebRequest(),
                        Component.For<ISupplierManager, SupplierManager>().ImplementedBy<SupplierManager>().LifestylePerWebRequest(),
                        Component.For<ISupplyChainManager, SupplyChainManager>().ImplementedBy<SupplyChainManager>().LifestylePerWebRequest(),
                        Component.For<IDepartmentManager, DepartmentManager>().ImplementedBy<DepartmentManager>().LifestylePerWebRequest(),
                        Component.For<IMunicipalityManager, MunicipalityManager>().ImplementedBy<MunicipalityManager>().LifestylePerWebRequest(),
                        Component.For<IVillageManager, VillageManager>().ImplementedBy<VillageManager>().LifestylePerWebRequest(),
                        Component.For<IFarmStatusManager, FarmStatusManager>().ImplementedBy<FarmStatusManager>().LifestylePerWebRequest(),
                        Component.For<IFarmSubstatusManager, FarmSubstatusManager>().ImplementedBy<FarmSubstatusManager>().LifestylePerWebRequest(),
                        Component.For<IOwnershipTypeManager, OwnershipTypeManager>().ImplementedBy<OwnershipTypeManager>().LifestylePerWebRequest(),
                        Component.For<ICooperativeManager, CooperativeManager>().ImplementedBy<CooperativeManager>().LifestylePerWebRequest(),
                        Component.For<ISoilTypeManager, SoilTypeManager>().ImplementedBy<SoilTypeManager>().LifestylePerWebRequest(),
                        Component.For<IPlantationTypeManager, PlantationTypeManager>().ImplementedBy<PlantationTypeManager>().LifestylePerWebRequest(),
                        Component.For<IPlantationVarietyManager, PlantationVarietyManager>().ImplementedBy<PlantationVarietyManager>().LifestylePerWebRequest(),
                        Component.For<IPlantationStatusManager, PlantationStatusManager>().ImplementedBy<PlantationStatusManager>().LifestylePerWebRequest(),
                        Component.For<IFloweringPeriodQualificationManager, FloweringPeriodQualificationManager>().ImplementedBy<FloweringPeriodQualificationManager>().LifestylePerWebRequest(),
                        Component.For<IOtherActivityManager, OtherActivityManager>().ImplementedBy<OtherActivityManager>().LifestylePerWebRequest(),
                        Component.For<IProjectManager, ProjectManager>().ImplementedBy<ProjectManager>().LifestylePerWebRequest(),

                        Component.For<IUserManager, UserManager>().ImplementedBy<UserManager>().LifestylePerWebRequest(),
                        Component.For<IUserRepository, UserRepository>().ImplementedBy<UserRepository>().LifestylePerWebRequest(),
                        Component.For<IRoleRepository, RoleRepository>().ImplementedBy<RoleRepository>().LifestylePerWebRequest(),

                        Component.For<IDashboardServices, DashboardServices>().ImplementedBy<DashboardServices>().LifestylePerWebRequest(),
                        Component.For<IDashboardManager, DashboardManager>().ImplementedBy<DashboardManager>().LifestylePerWebRequest(),

                        Component.For<IStorage, Storage>().ImplementedBy<Storage>().LifestylePerWebRequest(),

                        Component.For<SimpleAuthorizationServerProvider>().ImplementedBy<SimpleAuthorizationServerProvider>().LifestylePerWebRequest(),

                        Types.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest()

                        )
                       .AddFacility<LoggingFacility>(f => f.UseLog4Net());

            LoggerFactory.SetCurrent(new TraceSourceLogFactory());
            EntityValidatorFactory.SetCurrent(new DataAnnotationsEntityValidatorFactory());
            EncriptorFactory.SetCurrent(new SHA256EncriptorFactory());
            MappingAPI.Configure();

        }
    }
}