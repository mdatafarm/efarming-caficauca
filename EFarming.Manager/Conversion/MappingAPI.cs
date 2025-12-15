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
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.APIModule;
using EFarming.DTO.FarmModule;
using EFarming.DTO.ImpactModule;
using EFarming.DTO.ProjectModule;
using EFarming.DTO.QualityModule;
using EFarming.DTO.TraceabilityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFarming.Manager.Conversion
{
    /// <summary>
    /// Mapping API
    /// </summary>
    public static class MappingAPI
    {
        /// <summary>
        /// Configures this instance.
        /// </summary>
        public static void Configure()
        {
            Mapping.Configure();

            // API Conversions
            Mapper.CreateMap<Department, DepartmentAPIDTO>();
            Mapper.CreateMap<Municipality, MunicipalityAPIDTO>();
            Mapper.CreateMap<Village, VillageAPIDTO>();
            Mapper.CreateMap<PlantationType, PlantationTypeAPIDTO>();
            Mapper.CreateMap<PlantationVariety, PlantationVarietyAPIDTO>();

            Mapper.CreateMap<DepartmentAPIDTO, Department>();
            Mapper.CreateMap<MunicipalityAPIDTO, Municipality>();
            Mapper.CreateMap<VillageAPIDTO, Village>();
            Mapper.CreateMap<PlantationTypeAPIDTO, PlantationType>();
            Mapper.CreateMap<PlantationVarietyAPIDTO, PlantationVariety>();

            // APP Conversions
            
            Mapper.CreateMap<Farm, FarmDTO>();
            Mapper.CreateMap<Worker, WorkerDTO>().ForMember(dst => dst.Farm, opt => opt.Ignore()); ;
            Mapper.CreateMap<Productivity, ProductivityDTO>().ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<SoilAnalysis, SoilAnalysisDTO>().ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<Plantation, PlantationDTO>().ForMember(dst => dst.Productivity, opt => opt.Ignore());
            Mapper.CreateMap<FloweringPeriod, FloweringPeriodDTO>().ForMember(dst => dst.Plantation, opt => opt.Ignore());
            Mapper.CreateMap<Fertilizer, FertilizerDTO>().ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<FarmOtherActivity, FarmOtherActivityDTO>().ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<FamilyUnitMember, FamilyUnitMemberDTO>().ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<Image, ImageDTO>().ForMember(dst => dst.Farm, opt => opt.Ignore());

            Mapper.CreateMap<ImpactAssessment, ImpactAssessmentDTO>()
                .ForMember(dst => dst.AssessmentTemplate, opt => opt.Ignore())
                .ForMember(dst => dst.Answers, opt => opt.MapFrom(a => new List<CriteriaOptionDTO>()))
                .ForMember(dst => dst.Answers, opt => opt.MapFrom(src => src.Answers.Select(a => a.Id)));

            Mapper.CreateMap<AssessmentTemplate, AssessmentTemplateDTO>();

            Mapper.CreateMap<Category, CategoryDTO>()
                .ForMember(dst => dst.AssessmentTemplate, opt => opt.Ignore());
            Mapper.CreateMap<Indicator, IndicatorDTO>()
                .ForMember(dst => dst.AssessmentTemplateId, opt => opt.MapFrom(src => src.Category.AssessmentTemplateId))
                .ForMember(dst => dst.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dst => dst.Category, opt => opt.Ignore());
            Mapper.CreateMap<Criteria, CriteriaDTO>()
                .ForMember(dst => dst.Indicator, opt => opt.Ignore())
                .ForMember(dst => dst.CriteriaOptions, opt => opt.MapFrom(src => src.CriteriaOptions.OrderBy(co => co.Description)));
            Mapper.CreateMap<CriteriaOption, CriteriaOptionDTO>().ForMember(dst => dst.Criteria, opt => opt.Ignore());
            Mapper.CreateMap<ImpactAssessment, ImpactAssessmentDTO>();

            Mapper.CreateMap<QualityAttribute, QualityAttributeDTO>()
                .ForMember(dst => dst.AssessmentTemplate, opt => opt.Ignore());
            Mapper.CreateMap<OptionAttribute, OptionAttributeDTO>().ForMember(dst => dst.QualityAttribute, opt => opt.Ignore());
            Mapper.CreateMap<RangeAttribute, RangeAttributeDTO>().ForMember(dst => dst.QualityAttribute, opt => opt.Ignore());
            Mapper.CreateMap<SensoryProfileAssessment, SensoryProfileAssessmentDTO>().ForMember(dst => dst.Farm, opt => opt.Ignore());
            Mapper.CreateMap<SensoryProfileAnswer, SensoryProfileAnswerDTO>().ForMember(dst => dst.SensoryProfileAssessment, opt => opt.Ignore());
        }
    }
}

