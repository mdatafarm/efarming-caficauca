using AutoMapper;
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
using EFarming.Core.FarmModule.FamilyUnitAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.FerilizersCalculatorModule;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.Core.QualityModule.ChecklistAggregate;
using EFarming.Core.QualityModule.MicrolotAggregate;
using EFarming.Core.QualityModule.QualityProfileAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.SustainabilityModule.ContactAggregate;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.Core.TraceabilityModule.LotAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.FarmModule;
using EFarming.DTO.FertilizersCalculatorModule;
using EFarming.DTO.ImpactModule;
using EFarming.DTO.ProjectModule;
using EFarming.DTO.QualityModule;
using EFarming.DTO.SustainabilityModule;
using EFarming.DTO.TraceabilityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFarming.Manager.Conversion
{
    /// <summary>
    /// Mapping
    /// </summary>
    public static class Mapping
    {
        /// <summary>
        /// Configures the mapper function for all the entites.
        /// </summary>
        public static void Configure()
        {
            Mapper.CreateMap<Country, CountryDTO>().ForMember(dst => dst.Suppliers, opt => opt.Ignore());
            Mapper.CreateMap<Supplier, SupplierDTO>().ForMember(dst => dst.SupplyChains, opt => opt.Ignore());
            Mapper.CreateMap<SupplyChain, SupplyChainDTO>().ForMember(dst => dst.Farms, opt => opt.Ignore());
            Mapper.CreateMap<SupplyChainStatus, SupplyChainStatusDTO>().ForMember(dst => dst.SupplyChains, opt => opt.Ignore());
            Mapper.CreateMap<Department, DepartmentDTO>().ForMember(dst => dst.Municipalities, opt => opt.Ignore());
            Mapper.CreateMap<Municipality, MunicipalityDTO>().ForMember(dst => dst.Villages, opt => opt.Ignore());
            Mapper.CreateMap<Village, VillageDTO>();
            Mapper.CreateMap<FarmStatus, FarmStatusDTO>().ForMember(dst => dst.FarmSubstatuses, opt => opt.Ignore());
            Mapper.CreateMap<FarmSubstatus, FarmSubstatusDTO>();
            Mapper.CreateMap<OwnershipType, OwnershipTypeDTO>();
            Mapper.CreateMap<Cooperative, CooperativeDTO>();
            Mapper.CreateMap<SoilType, SoilTypeDTO>();
            Mapper.CreateMap<PlantationType, PlantationTypeDTO>().ForMember(dst => dst.PlantationVarieties, opt => opt.Ignore());
            Mapper.CreateMap<PlantationVariety, PlantationVarietyDTO>();
            Mapper.CreateMap<PlantationStatus, PlantationStatusDTO>();
            Mapper.CreateMap<FloweringPeriodQualification, FloweringPeriodQualificationDTO>();
            Mapper.CreateMap<OtherActivity, OtherActivityDTO>();

            Mapper.CreateMap<Lot, LotDTO>();
            Mapper.CreateMap<Invoice, InvoiceDTO>();
            Mapper.CreateMap<Project, ProjectDTO>();

            Mapper.CreateMap<User, UserDTO>()
                .ForMember(dst => dst.Farms, opt => opt.Ignore())
                .ForMember(dst => dst.SensoryProfileAssessments, opt => opt.Ignore());
            Mapper.CreateMap<Role, RoleDTO>().ForMember(dst => dst.Users, opt => opt.Ignore());

            Mapper.CreateMap<Farm, FarmDTO>();
            Mapper.CreateMap<Worker, WorkerDTO>();
            Mapper.CreateMap<Productivity, ProductivityDTO>();//.ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<SoilAnalysis, SoilAnalysisDTO>();//.ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<Plantation, PlantationDTO>();
            Mapper.CreateMap<FloweringPeriod, FloweringPeriodDTO>();
            Mapper.CreateMap<Fertilizer, FertilizerDTO>();
            Mapper.CreateMap<FarmOtherActivity, FarmOtherActivityDTO>();
            Mapper.CreateMap<FamilyUnitMember, FamilyUnitMemberDTO>();
            Mapper.CreateMap<Image, ImageDTO>().ForMember(dst => dst.Farm, opt => opt.Ignore());

            Mapper.CreateMap<AssessmentTemplate, AssessmentTemplateDTO>();

            Mapper.CreateMap<Requirement, RequirementDTO>();
            Mapper.CreateMap<Category, CategoryDTO>();
            Mapper.CreateMap<Indicator, IndicatorDTO>()
                .ForMember(dst => dst.AssessmentTemplateId, opt => opt.MapFrom(src => src.Category.AssessmentTemplateId))
                .ForMember(dst => dst.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dst => dst.Category, opt => opt.Ignore());
            Mapper.CreateMap<Criteria, CriteriaDTO>()
                .ForMember(dst => dst.Indicator, opt => opt.Ignore())
                .ForMember(dst => dst.Requirement, opt => opt.Ignore());
            Mapper.CreateMap<CriteriaOption, CriteriaOptionDTO>().ForMember(dst => dst.Criteria, opt => opt.Ignore());
            Mapper.CreateMap<ImpactAssessment, ImpactAssessmentDTO>();

            Mapper.CreateMap<QualityAttribute, QualityAttributeDTO>()
                .ForMember(dst => dst.AssessmentTemplate, opt => opt.Ignore());
            Mapper.CreateMap<OptionAttribute, OptionAttributeDTO>().ForMember(dst => dst.QualityAttribute, opt => opt.Ignore());
            Mapper.CreateMap<RangeAttribute, RangeAttributeDTO>().ForMember(dst => dst.QualityAttribute, opt => opt.Ignore());
            Mapper.CreateMap<OpenTextAttribute, OpenTextAttributeDTO>().ForMember(dst => dst.QualityAttribute, opt => opt.Ignore());
            Mapper.CreateMap<SensoryProfileAssessment, SensoryProfileAssessmentDTO>()
                .ForMember(dst => dst.AssessmentTemplate, opt => opt.Ignore());//.ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<SensoryProfileAnswer, SensoryProfileAnswerDTO>().ForMember(dst => dst.SensoryProfileAssessment, opt => opt.Ignore());
            Mapper.CreateMap<Microlot, MicrolotDTO>();
            Mapper.CreateMap<QualityProfile, QualityProfileDTO>().ForMember(dst => dst.SupplyChains, opt => opt.Ignore());

            Mapper.CreateMap<Checklist, ChecklistDTO>();
            Mapper.CreateMap<Almacenamiento, AlmacenamientoDTO>();
            Mapper.CreateMap<Despulpado, DespulpadoDTO>();
            Mapper.CreateMap<Fermentacion, FermentacionDTO>();
            Mapper.CreateMap<Infraestructura, InfraestructuraDTO>();
            Mapper.CreateMap<Mantenimiento, MantenimientoDTO>();
            Mapper.CreateMap<Recoleccion, RecoleccionDTO>();
            Mapper.CreateMap<Secado, SecadoDTO>();

            Mapper.CreateMap<Contact, ContactDTO>();
            Mapper.CreateMap<ContactType, ContactTypeDTO>();
            Mapper.CreateMap<Location, LocationDTO>();
            Mapper.CreateMap<Topic, TopicDTO>();

            Mapper.CreateMap<FertilizerInformation, FertilizerInformationDTO>();
            Mapper.CreateMap<AverageExtraction, AverageExtractionDTO>();

            Mapper.CreateMap<CountryDTO, Country>().ForMember(dst => dst.Suppliers, opt => opt.Ignore());
            Mapper.CreateMap<SupplierDTO, Supplier>().ForMember(dst => dst.SupplyChains, opt => opt.Ignore());
            Mapper.CreateMap<SupplyChainDTO, SupplyChain>();
            Mapper.CreateMap<SupplyChainStatusDTO, SupplyChainStatus>().ForMember(dst => dst.SupplyChains, opt => opt.Ignore());
            Mapper.CreateMap<DepartmentDTO, Department>().ForMember(dst => dst.Municipalities, opt => opt.Ignore());
            Mapper.CreateMap<MunicipalityDTO, Municipality>().ForMember(dst => dst.Villages, opt => opt.Ignore());
            Mapper.CreateMap<VillageDTO, Village>();
            Mapper.CreateMap<FarmStatusDTO, FarmStatus>().ForMember(dst => dst.FarmSubstatuses, opt => opt.Ignore());
            Mapper.CreateMap<FarmSubstatusDTO, FarmSubstatus>();
            Mapper.CreateMap<OwnershipTypeDTO, OwnershipType>();
            Mapper.CreateMap<CooperativeDTO, Cooperative>();
            Mapper.CreateMap<SoilTypeDTO, SoilType>().ForMember(dst => dst.Farms, opt => opt.Ignore());
            Mapper.CreateMap<PlantationTypeDTO, PlantationType>().ForMember(dst => dst.PlantationVarieties, opt => opt.Ignore());
            Mapper.CreateMap<PlantationVarietyDTO, PlantationVariety>();
            Mapper.CreateMap<PlantationStatusDTO, PlantationStatus>();
            Mapper.CreateMap<FloweringPeriodQualificationDTO, FloweringPeriodQualification>();
            Mapper.CreateMap<OtherActivityDTO, OtherActivity>();
            Mapper.CreateMap<ProjectDTO, Project>();

            Mapper.CreateMap<UserDTO, User>().ForMember(dst => dst.Farms, opt => opt.Ignore());
            Mapper.CreateMap<RoleDTO, Role>().ForMember(dst => dst.Users, opt => opt.Ignore());

            Mapper.CreateMap<FarmDTO, Farm>();
            Mapper.CreateMap<WorkerDTO, Worker>();
            Mapper.CreateMap<ProductivityDTO, Productivity>();//.ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<SoilAnalysisDTO, SoilAnalysis>();//.ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<PlantationDTO, Plantation>();
            Mapper.CreateMap<FloweringPeriodDTO, FloweringPeriod>();
            Mapper.CreateMap<FertilizerDTO, Fertilizer>();
            Mapper.CreateMap<FarmOtherActivityDTO, FarmOtherActivity>();
            Mapper.CreateMap<FamilyUnitMemberDTO, FamilyUnitMember>();
            Mapper.CreateMap<ImageDTO, Image>();

            Mapper.CreateMap<LotDTO, Lot>();
            Mapper.CreateMap<InvoiceDTO, Invoice>();

            Mapper.CreateMap<AssessmentTemplateDTO, AssessmentTemplate>();

            Mapper.CreateMap<RequirementDTO, Requirement>();
            Mapper.CreateMap<CategoryDTO, Category>();
            Mapper.CreateMap<IndicatorDTO, Indicator>().ForMember(dst => dst.Category, opt => opt.Ignore());
            Mapper.CreateMap<CriteriaDTO, Criteria>();
            Mapper.CreateMap<CriteriaOptionDTO, CriteriaOption>();
            Mapper.CreateMap<ImpactAssessmentDTO, ImpactAssessment>();

            Mapper.CreateMap<QualityAttributeDTO, QualityAttribute>();
            Mapper.CreateMap<OptionAttributeDTO, OptionAttribute>().ForMember(dst => dst.QualityAttribute, opt => opt.Ignore());
            Mapper.CreateMap<RangeAttributeDTO, RangeAttribute>().ForMember(dst => dst.QualityAttribute, opt => opt.Ignore());
            Mapper.CreateMap<OpenTextAttributeDTO, OpenTextAttribute>().ForMember(dst => dst.QualityAttribute, opt => opt.Ignore());
            Mapper.CreateMap<SensoryProfileAnswerDTO, SensoryProfileAnswer>();
            Mapper.CreateMap<SensoryProfileAssessmentDTO, SensoryProfileAssessment>();
            Mapper.CreateMap<MicrolotDTO, Microlot>();
            Mapper.CreateMap<QualityProfileDTO, QualityProfile>().ForMember(dst => dst.SupplyChains, opt => opt.Ignore());
            Mapper.CreateMap<SensoryProfileAssessmentDTO, SensoryProfileAssessmentDTOAPI>()
                .ForMember(dst => dst.Farm, opt => opt.Ignore())
                .ForMember(dst => dst.User, opt => opt.Ignore());

            Mapper.CreateMap<ChecklistDTO, Checklist>();
            Mapper.CreateMap<AlmacenamientoDTO, Almacenamiento>();
            Mapper.CreateMap<DespulpadoDTO, Despulpado>();
            Mapper.CreateMap<FermentacionDTO,Fermentacion>();
            Mapper.CreateMap<InfraestructuraDTO, Infraestructura>();
            Mapper.CreateMap<MantenimientoDTO, Mantenimiento>();
            Mapper.CreateMap<RecoleccionDTO, Recoleccion>();
            Mapper.CreateMap<SecadoDTO, Secado>();

            Mapper.CreateMap<ContactDTO, Contact>();
            Mapper.CreateMap<ContactTypeDTO, ContactType>();
            Mapper.CreateMap<LocationDTO, Location>();
            Mapper.CreateMap<TopicDTO, Topic>();

            Mapper.CreateMap<FertilizerInformationDTO, FertilizerInformation>();
            Mapper.CreateMap<AverageExtractionDTO, AverageExtraction>();
        }
    }
}

