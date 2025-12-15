using EFarming.Common;
using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.AdminModule.CooperativeAggregate;
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
using EFarming.Core.AdminModule.CountryAggregate;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.ComercialModule;
using EFarming.Core.FarmModule.FamilyUnitAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.Core.QualityModule.MicrolotAggregate;
using EFarming.Core.QualityModule.QualityProfileAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.Core.TraceabilityModule.LotAggregate;
using EFarming.DAL.Contract;
using EFarming.DAL.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using EFarming.Core.SustainabilityModule.ContactAggregate;
using EFarming.Core.TasqModule;
using EFarming.Core.FerilizersCalculatorModule;
using System.Data;

namespace EFarming.DAL
{
    /// <summary>
    /// Unit of Work Class
    /// </summary>
    public class UnitOfWork : DbContext, IQueryableUnitOfWork
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork() : base("name=DefaultConnection")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;

            var objectContext = (this as IObjectContextAdapter).ObjectContext;

            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = 900;
        }

        #endregion Constructor

        #region IDbSet Members

        public class Parametros
        {
            public String Nombre { get; set; }
            public Object Valor { get; set; }
            public SqlDbType TipoDato { get; set; }
            public Int32 Tamanio { get; set; }
            public ParameterDirection Direccion { get; set; }


            // Constructor

            // C.Entrada
            public Parametros(String objNombre, Object objValor)
            {
                Nombre = objNombre;
                Valor = objValor;
                Direccion = ParameterDirection.Input;
            }

            //C.Salida
            public Parametros(String objNombre, SqlDbType objTipoDato, Int32 objTamanio)
            {
                Nombre = objNombre;
                TipoDato = objTipoDato;
                Tamanio = objTamanio;
                Direccion = ParameterDirection.Output;
            }
        }

        /// <summary>
        /// The _quality profiles
        /// </summary>
        IDbSet<QualityProfile> _qualityProfiles;

        /// <summary>
        /// Gets the quality profiles.
        /// </summary>
        /// <value>
        /// The quality profiles.
        /// </value>
        public IDbSet<QualityProfile> QualityProfiles
        {
            get
            {
                if (_qualityProfiles == null)
                    _qualityProfiles = base.Set<QualityProfile>();
                return _qualityProfiles;
            }
        }

        /// <summary>
        /// The _microlots
        /// </summary>
        IDbSet<Microlot> _microlots;
        /// <summary>
        /// Gets the microlots.
        /// </summary>
        /// <value>
        /// The microlots.
        /// </value>
        public IDbSet<Microlot> Microlots
        {
            get
            {
                if (_microlots == null)
                    _microlots = base.Set<Microlot>();
                return _microlots;
            }
        }

        /// <summary>
        /// The _lots
        /// </summary>
        IDbSet<Lot> _lots;
        /// <summary>
        /// Gets the lots.
        /// </summary>
        /// <value>
        /// The lots.
        /// </value>
        public IDbSet<Lot> Lots
        {
            get
            {
                if (_lots == null)
                    _lots = base.Set<Lot>();
                return _lots;
            }
        }

        /// <summary>
        /// The _projects
        /// </summary>
        IDbSet<Project> _projects;
        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public IDbSet<Project> Projects
        {
            get
            {
                if (_projects == null)
                    _projects = base.Set<Project>();
                return _projects;
            }
        }

        IDbSet<SupplyChain> _supplyChain;
        public IDbSet<SupplyChain> SupplyChains
        {
            get
            {
                if (_supplyChain == null)
                    _supplyChain = base.Set<SupplyChain>();
                return _supplyChain;
            }
        }

        /// <summary>
        /// The _assessment templates
        /// </summary>
        IDbSet<AssessmentTemplate> _assessmentTemplates;
        /// <summary>
        /// Gets the assessment templates.
        /// </summary>
        /// <value>
        /// The assessment templates.
        /// </value>
        public IDbSet<AssessmentTemplate> AssessmentTemplates
        {
            get
            {
                if (_assessmentTemplates == null)
                    _assessmentTemplates = base.Set<AssessmentTemplate>();
                return _assessmentTemplates;
            }
        }

        /// <summary>
        /// The _invoices
        /// </summary>
        IDbSet<Invoice> _invoices;
        /// <summary>
        /// Gets the invoices.
        /// </summary>
        /// <value>
        /// The invoices.
        /// </value>
        public IDbSet<Invoice> Invoices
        {
            get
            {
                if (_invoices == null)
                    _invoices = base.Set<Invoice>();
                return _invoices;
            }
        }

        /// <summary>
        /// The _sensory profile assessments
        /// </summary>
        IDbSet<SensoryProfileAssessment> _sensoryProfileAssessments;
        /// <summary>
        /// Gets the sensory profile assessments.
        /// </summary>
        /// <value>
        /// The sensory profile assessments.
        /// </value>
        public IDbSet<SensoryProfileAssessment> SensoryProfileAssessments
        {
            get
            {
                if (_sensoryProfileAssessments == null)
                    _sensoryProfileAssessments = base.Set<SensoryProfileAssessment>();
                return _sensoryProfileAssessments;
            }
        }

        /// <summary>
        /// The _sensory profile answers
        /// </summary>
        IDbSet<SensoryProfileAnswer> _sensoryProfileAnswers;
        /// <summary>
        /// Gets the sensory profile answers.
        /// </summary>
        /// <value>
        /// The sensory profile answers.
        /// </value>
        public IDbSet<SensoryProfileAnswer> SensoryProfileAnswers
        {
            get
            {
                if (_sensoryProfileAnswers == null)
                    _sensoryProfileAnswers = base.Set<SensoryProfileAnswer>();
                return _sensoryProfileAnswers;
            }
        }

        /// <summary>
        /// The _attributes
        /// </summary>
        IDbSet<QualityAttribute> _attributes;
        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public IDbSet<QualityAttribute> Attributes
        {
            get
            {
                if (_attributes == null)
                    _attributes = base.Set<QualityAttribute>();
                return _attributes;
            }
        }

        /// <summary>
        /// The _range attributes
        /// </summary>
        IDbSet<RangeAttribute> _rangeAttributes;
        /// <summary>
        /// Gets the range attributes.
        /// </summary>
        /// <value>
        /// The range attributes.
        /// </value>
        public IDbSet<RangeAttribute> RangeAttributes
        {
            get
            {
                if (_rangeAttributes == null)
                    _rangeAttributes = base.Set<RangeAttribute>();
                return _rangeAttributes;
            }
        }

        /// <summary>
        /// The _option attributes
        /// </summary>
        IDbSet<OptionAttribute> _optionAttributes;
        /// <summary>
        /// Gets the option attributes.
        /// </summary>
        /// <value>
        /// The option attributes.
        /// </value>
        public IDbSet<OptionAttribute> OptionAttributes
        {
            get
            {
                if (_optionAttributes == null)
                    _optionAttributes = base.Set<OptionAttribute>();
                return _optionAttributes;
            }
        }

        /// <summary>
        /// The _impact assessments
        /// </summary>
        IDbSet<ImpactAssessment> _impactAssessments;
        /// <summary>
        /// Gets the impact assessments.
        /// </summary>
        /// <value>
        /// The impact assessments.
        /// </value>
        public IDbSet<ImpactAssessment> ImpactAssessments
        {
            get
            {
                if (_impactAssessments == null)
                    _impactAssessments = base.Set<ImpactAssessment>();
                return _impactAssessments;
            }
        }

        /// <summary>
        /// The _indicators
        /// </summary>
        IDbSet<Indicator> _indicators;
        /// <summary>
        /// Gets the indicators.
        /// </summary>
        /// <value>
        /// The indicators.
        /// </value>
        public IDbSet<Indicator> Indicators
        {
            get
            {
                if (_indicators == null)
                    _indicators = base.Set<Indicator>();
                return _indicators;
            }
        }

        /// <summary>
        /// The _categories
        /// </summary>
        IDbSet<Category> _categories;
        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public IDbSet<Category> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = base.Set<Category>();
                return _categories;
            }
        }

        /// <summary>
        /// The _criteria
        /// </summary>
        IDbSet<Criteria> _criteria;
        /// <summary>
        /// Gets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        public IDbSet<Criteria> Criteria
        {
            get
            {
                if (_criteria == null)
                    _criteria = base.Set<Criteria>();
                return _criteria;
            }
        }

        /// <summary>
        /// The _criteria options
        /// </summary>
        IDbSet<CriteriaOption> _criteriaOptions;
        /// <summary>
        /// Gets the criteria options.
        /// </summary>
        /// <value>
        /// The criteria options.
        /// </value>
        public IDbSet<CriteriaOption> CriteriaOptions
        {
            get
            {
                if (_criteriaOptions == null)
                    _criteriaOptions = base.Set<CriteriaOption>();
                return _criteriaOptions;
            }
        }

        /// <summary>
        /// The _farms
        /// </summary>
        IDbSet<Farm> _farms;
        /// <summary>
        /// Gets the farms.
        /// </summary>
        /// <value>
        /// The farms.
        /// </value>
        public IDbSet<Farm> Farms
        {
            get
            {
                if (_farms == null)
                    _farms = base.Set<Farm>();

                return _farms;
            }
        }

        /// <summary>
        /// The _images
        /// </summary>
        IDbSet<Image> _images;
        /// <summary>
        /// Gets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public IDbSet<Image> Images
        {
            get
            {
                if (_images == null)
                    _images = base.Set<Image>();
                return _images;
            }
        }

        /// <summary>
        /// The _productivities
        /// </summary>
        IDbSet<Productivity> _productivities;
        /// <summary>
        /// Gets the productivities.
        /// </summary>
        /// <value>
        /// The productivities.
        /// </value>
        public IDbSet<Productivity> Productivities
        {
            get
            {
                if (_productivities == null)
                    _productivities = base.Set<Productivity>();
                return _productivities;
            }
        }

        /// <summary>
        /// The _workers
        /// </summary>
        IDbSet<Worker> _workers;
        /// <summary>
        /// Gets the workers.
        /// </summary>
        /// <value>
        /// The workers.
        /// </value>
        public IDbSet<Worker> Workers
        {
            get
            {
                if (_workers == null)
                    _workers = base.Set<Worker>();
                return _workers;
            }
        }

        /// <summary>
        /// The _fertilizers
        /// </summary>
        IDbSet<Fertilizer> _fertilizers;
        /// <summary>
        /// Gets the fertilizers.
        /// </summary>
        /// <value>
        /// The fertilizers.
        /// </value>
        public IDbSet<Fertilizer> Fertilizers
        {
            get
            {
                if (_fertilizers == null)
                    _fertilizers = base.Set<Fertilizer>();
                return _fertilizers;
            }
        }

        /// <summary>
        /// The _flowering periods
        /// </summary>
        IDbSet<FloweringPeriod> _floweringPeriods;
        /// <summary>
        /// Gets the flowering periods.
        /// </summary>
        /// <value>
        /// The flowering periods.
        /// </value>
        public IDbSet<FloweringPeriod> FloweringPeriods
        {
            get
            {
                if (_floweringPeriods == null)
                    _floweringPeriods = base.Set<FloweringPeriod>();
                return _floweringPeriods;
            }
        }

        /// <summary>
        /// The _plantations
        /// </summary>
        IDbSet<Plantation> _plantations;
        /// <summary>
        /// Gets the plantations.
        /// </summary>
        /// <value>
        /// The plantations.
        /// </value>
        public IDbSet<Plantation> Plantations
        {
            get
            {
                if (_plantations == null)
                    _plantations = base.Set<Plantation>();
                return _plantations;
            }
        }

        /// <summary>
        /// The _soil analysis
        /// </summary>
        IDbSet<SoilAnalysis> _soilAnalysis;
        /// <summary>
        /// Gets the soil analysis.
        /// </summary>
        /// <value>
        /// The soil analysis.
        /// </value>
        public IDbSet<SoilAnalysis> SoilAnalysis
        {
            get
            {
                if (_soilAnalysis == null)
                {
                    _soilAnalysis = base.Set<SoilAnalysis>();
                }
                return _soilAnalysis;
            }
        }

        /// <summary>
        /// The _farm other activities
        /// </summary>
        IDbSet<FarmOtherActivity> _farmOtherActivities;
        /// <summary>
        /// Gets the farm other activities.
        /// </summary>
        /// <value>
        /// The farm other activities.
        /// </value>
        public IDbSet<FarmOtherActivity> FarmOtherActivities
        {
            get
            {
                if (_farmOtherActivities == null)
                    _farmOtherActivities = base.Set<FarmOtherActivity>();
                return _farmOtherActivities;
            }
        }

        /// <summary>
        /// The _family unit members
        /// </summary>
        IDbSet<FamilyUnitMember> _familyUnitMembers;
        /// <summary>
        /// Gets the family unit members.
        /// </summary>
        /// <value>
        /// The family unit members.
        /// </value>
        public IDbSet<FamilyUnitMember> FamilyUnitMembers
        {
            get
            {
                if (_familyUnitMembers == null)
                    _familyUnitMembers = base.Set<FamilyUnitMember>();
                return _familyUnitMembers;
            }
        }

        /// <summary>
        /// The _departments
        /// </summary>
        IDbSet<Department> _departments;
        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <value>
        /// The departments.
        /// </value>
        public IDbSet<Department> Departments
        {
            get
            {
                if (_departments == null)
                {
                    _departments = base.Set<Department>();
                }
                return _departments;
            }
        }

        /// <summary>
        /// The _municipalities
        /// </summary>
        IDbSet<Municipality> _municipalities;
        /// <summary>
        /// Gets the municipalities.
        /// </summary>
        /// <value>
        /// The municipalities.
        /// </value>
        public IDbSet<Municipality> Municipalities
        {
            get
            {
                if (_municipalities == null)
                {
                    _municipalities = base.Set<Municipality>();
                }
                return _municipalities;
            }
        }

        /// <summary>
        /// The _villages
        /// </summary>
        IDbSet<Village> _villages;
        /// <summary>
        /// Gets the villages.
        /// </summary>
        /// <value>
        /// The villages.
        /// </value>
        public IDbSet<Village> Villages
        {
            get
            {
                if (_villages == null)
                {
                    _villages = base.Set<Village>();
                }
                return _villages;
            }
        }

        /// <summary>
        /// The _farm status
        /// </summary>
        IDbSet<FarmStatus> _farmStatus;
        /// <summary>
        /// Gets the farm status.
        /// </summary>
        /// <value>
        /// The farm status.
        /// </value>
        public IDbSet<FarmStatus> FarmStatus
        {
            get
            {
                if (_farmStatus == null)
                {
                    _farmStatus = base.Set<FarmStatus>();
                }
                return _farmStatus;
            }
        }

        /// <summary>
        /// The _farm substatus
        /// </summary>
        IDbSet<FarmSubstatus> _farmSubstatus;
        /// <summary>
        /// Gets the farm substatus.
        /// </summary>
        /// <value>
        /// The farm substatus.
        /// </value>
        public IDbSet<FarmSubstatus> FarmSubstatus
        {
            get
            {
                if (_farmSubstatus == null)
                {
                    _farmSubstatus = base.Set<FarmSubstatus>();
                }
                return _farmSubstatus;
            }
        }

        /// <summary>
        /// The _ownership types
        /// </summary>
        IDbSet<OwnershipType> _ownershipTypes;
        /// <summary>
        /// Gets the ownership types.
        /// </summary>
        /// <value>
        /// The ownership types.
        /// </value>
        public IDbSet<OwnershipType> OwnershipTypes
        {
            get
            {
                if (_ownershipTypes == null)
                {
                    _ownershipTypes = base.Set<OwnershipType>();
                }
                return _ownershipTypes;
            }
        }

        /// <summary>
        /// The _cooperatives
        /// </summary>
        IDbSet<Cooperative> _cooperatives;
        /// <summary>
        /// Gets the cooperatives.
        /// </summary>
        /// <value>
        /// The cooperatives.
        /// </value>
        public IDbSet<Cooperative> Cooperatives
        {
            get
            {
                if (_cooperatives == null)
                {
                    _cooperatives = base.Set<Cooperative>();
                }
                return _cooperatives;
            }
        }

        /// <summary>
        /// The _soil types
        /// </summary>
        IDbSet<SoilType> _soilTypes;
        /// <summary>
        /// Gets the soil types.
        /// </summary>
        /// <value>
        /// The soil types.
        /// </value>
        public IDbSet<SoilType> SoilTypes
        {
            get
            {
                if (_soilTypes == null)
                {
                    _soilTypes = base.Set<SoilType>();
                }
                return _soilTypes;
            }
        }

        /// <summary>
        /// The _plantation types
        /// </summary>
        IDbSet<PlantationType> _plantationTypes;
        /// <summary>
        /// Gets the type of the plantation.
        /// </summary>
        /// <value>
        /// The type of the plantation.
        /// </value>
        public IDbSet<PlantationType> PlantationType
        {
            get
            {
                if (_plantationTypes == null)
                {
                    _plantationTypes = base.Set<PlantationType>();
                }
                return _plantationTypes;
            }
        }

        /// <summary>
        /// The _plantation varieties
        /// </summary>
        IDbSet<PlantationVariety> _plantationVarieties;
        /// <summary>
        /// Gets the plantation varieties.
        /// </summary>
        /// <value>
        /// The plantation varieties.
        /// </value>
        public IDbSet<PlantationVariety> PlantationVarieties
        {
            get
            {
                if (_plantationVarieties == null)
                {
                    _plantationVarieties = base.Set<PlantationVariety>();
                }
                return _plantationVarieties;
            }
        }

        /// <summary>
        /// The _plantation statuses
        /// </summary>
        IDbSet<PlantationStatus> _plantationStatuses;

        /// <summary>
        /// Gets the plantation statuses.
        /// </summary>
        /// <value>
        /// The plantation statuses.
        /// </value>
        public IDbSet<PlantationStatus> PlantationStatuses
        {
            get
            {
                if (_plantationStatuses == null)
                {
                    _plantationStatuses = base.Set<PlantationStatus>();
                }
                return _plantationStatuses;
            }
        }

        /// <summary>
        /// The _flowering period qualifications
        /// </summary>
        IDbSet<FloweringPeriodQualification> _floweringPeriodQualifications;
        /// <summary>
        /// Gets the flowering period qualifications.
        /// </summary>
        /// <value>
        /// The flowering period qualifications.
        /// </value>
        public IDbSet<FloweringPeriodQualification> FloweringPeriodQualifications
        {
            get
            {
                if (_floweringPeriodQualifications == null)
                {
                    _floweringPeriodQualifications = base.Set<FloweringPeriodQualification>();
                }
                return _floweringPeriodQualifications;
            }
        }

        /// <summary>
        /// The _other activities
        /// </summary>
        IDbSet<OtherActivity> _otherActivities;
        /// <summary>
        /// Gets the other activities.
        /// </summary>
        /// <value>
        /// The other activities.
        /// </value>
        public IDbSet<OtherActivity> OtherActivities
        {
            get
            {
                if (_otherActivities == null)
                {
                    _otherActivities = base.Set<OtherActivity>();
                }
                return _otherActivities;
            }
        }

        /// <summary>
        /// The _users
        /// </summary>
        IDbSet<User> _users;
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public IDbSet<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = base.Set<User>();
                }
                return _users;
            }
        }

        /// <summary>
        /// The _roles
        /// </summary>
        IDbSet<Role> _roles;
        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IDbSet<Role> Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = base.Set<Role>();
                }
                return _roles;
            }
        }

        /// <summary>
        /// The _client
        /// </summary>
        IDbSet<Client> _client;
        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <value>
        /// The clients.
        /// </value>
        public IDbSet<Client> Clients
        {
            get
            {
                if (_client == null)
                    _client = base.Set<Client>();

                return _client;
            }
        }

        /// <summary>
        /// The _agent
        /// </summary>
        IDbSet<Agent> _agent;
        /// <summary>
        /// Gets the agents.
        /// </summary>
        /// <value>
        /// The agents.
        /// </value>
        public IDbSet<Agent> Agents
        {
            get
            {
                if (_agent == null)
                    _agent = base.Set<Agent>();

                return _agent;
            }
        }

        /// <summary>
        /// The _agreement
        /// </summary>
        IDbSet<Agreement> _agreement;
        /// <summary>
        /// Gets the agreements.
        /// </summary>
        /// <value>
        /// The agreements.
        /// </value>
        public IDbSet<Agreement> Agreements
        {
            get
            {
                if (_agreement == null)
                    _agreement = base.Set<Agreement>();

                return _agreement;
            }
        }

        /// <summary>
        /// The _seller
        /// </summary>
        IDbSet<Seller> _seller;
        /// <summary>
        /// Gets the sellers.
        /// </summary>
        /// <value>
        /// The sellers.
        /// </value>
        public IDbSet<Seller> Sellers
        {
            get
            {
                if (_seller == null)
                    _seller = base.Set<Seller>();

                return _seller;
            }
        }

        IDbSet<MoreInformation> _moreinformation;

        /// <summary>
        /// Gets the moreinformation.
        /// </summary>
        /// <value>
        /// The moreinformation.
        /// </value>
        public IDbSet<MoreInformation> Moreinformation
        {
            get
            {
                if (_moreinformation == null)
                    _moreinformation = base.Set<MoreInformation>();

                return _moreinformation;
            }
        }

        IDbSet<Fixation> _fixation;

        /// <summary>
        /// Gets the moreinformation.
        /// </summary>
        /// <value>
        /// The moreinformation.
        /// </value>
        public IDbSet<Fixation> Fixation
        {
            get
            {
                if (_fixation == null)
                    _fixation = base.Set<Fixation>();

                return _fixation;
            }
        }

        IDbSet<FixationType> _fixationtype;

        /// <summary>
        /// Gets the moreinformation.
        /// </summary>
        /// <value>
        /// The moreinformation.
        /// </value>
        public IDbSet<FixationType> FixationType
        {
            get
            {
                if (_fixationtype == null)
                    _fixationtype = base.Set<FixationType>();

                return _fixationtype;
            }
        }

        IDbSet<NYPosition> _nyposition;

        /// <summary>
        /// Gets the ny position.
        /// </summary>
        /// <value>
        /// The ny position.
        /// </value>
        public IDbSet<NYPosition> NYPosition
        {
            get
            {
                if (_nyposition == null)
                    _nyposition = base.Set<NYPosition>();

                return _nyposition;
            }
        }

        IDbSet<ContractInvoice> _contractinvoice;
        /// <summary>
        /// Gets the contract invoice.
        /// </summary>
        /// <value>
        /// The contract invoice.
        /// </value>
        public IDbSet<ContractInvoice> ContractInvoice
        {
            get
            {
                if (_contractinvoice == null)
                    _contractinvoice = base.Set<ContractInvoice>();

                return _contractinvoice;
            }
        }

        IDbSet<ContractLot> _contractlot;
        /// <summary>
        /// Gets the contract invoice.
        /// </summary>
        /// <value>
        /// The contract invoice.
        /// </value>
        public IDbSet<ContractLot> ContractLot
        {
            get
            {
                if (_contractlot == null)
                    _contractlot = base.Set<ContractLot>();

                return _contractlot;
            }
        }

        IDbSet<DocumentReference> _documentreference;
        /// <summary>
        /// Gets the contract invoice.
        /// </summary>
        /// <value>
        /// The contract invoice.
        /// </value>
        public IDbSet<DocumentReference> DocumentReference
        {
            get
            {
                if (_documentreference == null)
                    _documentreference = base.Set<DocumentReference>();

                return _documentreference;
            }
        }

        IDbSet<ReferenceRelationShip> _referencerelationship;
        /// <summary>
        /// Gets the contract invoice.
        /// </summary>
        /// <value>
        /// The contract invoice.
        /// </value>
        public IDbSet<ReferenceRelationShip> ReferenceRelationShip
        {
            get
            {
                if (_referencerelationship == null)
                    _referencerelationship = base.Set<ReferenceRelationShip>();

                return _referencerelationship;
            }
        }

        IDbSet<MDType> _mdtype;
        /// <summary>
        /// Gets the contract invoice.
        /// </summary>
        /// <value>
        /// The contract invoice.
        /// </value>
        public IDbSet<MDType> MDType
        {
            get
            {
                if (_mdtype == null)
                    _mdtype = base.Set<MDType>();

                return _mdtype;
            }
        }

        IDbSet<MDOrigin> _mdorigin;
        /// <summary>
        /// Gets the contract invoice.
        /// </summary>
        /// <value>
        /// The contract invoice.
        /// </value>
        public IDbSet<MDOrigin> MDOrigin
        {
            get
            {
                if (_mdorigin == null)
                    _mdorigin = base.Set<MDOrigin>();

                return _mdorigin;
            }
        }

        IDbSet<Country> _country;
        /// <summary>
        /// Gets the Country.
        /// </summary>
        /// <value>
        /// The Country.
        /// </value>
        public IDbSet<Country> Countries
        {
            get
            {
                if (_country == null)
                    _country = base.Set<Country>();

                return _country;
            }
        }

        IDbSet<Shipment> _shipment;
        /// <summary>
        /// Gets the contract invoice.
        /// </summary>
        /// <value>
        /// The contract invoice.
        /// </value>
        public IDbSet<Shipment> Shipments
        {
            get
            {
                if (_shipment == null)
                    _shipment = base.Set<Shipment>();

                return _shipment;
            }
        }

        IDbSet<Contact> _contact;
        /// <summary>
        /// Gets the Contact information.
        /// </summary>
        /// <value>
        /// The contact.
        /// </value>
        public IDbSet<Contact> Contacts
        {
            get
            {
                if (_contact == null)
                    _contact = base.Set<Contact>();

                return _contact;
            }
        }

        IDbSet<Topic> _topic;
        /// <summary>
        /// Gets the Topic.
        /// </summary>
        /// <value>
        /// The Topic.
        /// </value>
        public IDbSet<Topic> Topic
        {
            get
            {
                if (_topic == null)
                    _topic = base.Set<Topic>();

                return _topic;
            }
        }

        IDbSet<ContactType> _type;
        /// <summary>
        /// Gets the Type of Contact.
        /// </summary>
        /// <value>
        /// The Topic.
        /// </value>
        public IDbSet<ContactType> Type
        {
            get
            {
                if (_type == null)
                    _type = base.Set<ContactType>();

                return _type;
            }
        }

        IDbSet<Location> _location;
        /// <summary>
        /// Gets the Type of Contact.
        /// </summary>
        /// <value>
        /// The Topic.
        /// </value>
        public IDbSet<Location> Location
        {
            get
            {
                if (_location == null)
                    _location = base.Set<Location>();

                return _location;
            }
        }

        IDbSet<TASQCriteria> _tasqcriteria;
        /// <summary>
        /// Gets the criterias for the TASQ Survey
        /// </summary>
        /// <value>
        /// The Criteria
        /// </value>
        public IDbSet<TASQCriteria> TASQCriteria
        {
            get
            {
                if (_tasqcriteria == null)
                    _tasqcriteria = base.Set<TASQCriteria>();

                return _tasqcriteria;
            }
        }

        IDbSet<TASQAssessmentAnswer> _tasqassessmentanswer;
        /// <summary>
        /// Gets the answers for the TASQ Assessments
        /// </summary>
        /// <value>
        /// The answer
        /// </value>
        public IDbSet<TASQAssessmentAnswer> TASQAssessmentAnswer
        {
            get
            {
                if (_tasqassessmentanswer == null)
                    _tasqassessmentanswer = base.Set<TASQAssessmentAnswer>();

                return _tasqassessmentanswer;
            }
        }

        IDbSet<TASQAssessment> _tasqassessment;
        /// <summary>
        /// Gets the TASQ Assessments
        /// </summary>
        /// <value>
        /// The Assessment
        /// </value>
        public IDbSet<TASQAssessment> TASQAssessment
        {
            get
            {
                if (_tasqassessment == null)
                    _tasqassessment = base.Set<TASQAssessment>();

                return _tasqassessment;
            }
        }

        IDbSet<SubModule> _submodule;
        /// <summary>
        /// Gets the TASQ SubModules
        /// </summary>
        /// <value>
        /// The SubModule
        /// </value>
        public IDbSet<SubModule> SubModule
        {
            get
            {
                if (_submodule == null)
                    _submodule = base.Set<SubModule>();

                return _submodule;
            }
        }

        IDbSet<Module> _module;
        /// <summary>
        /// Gets the TASQ Modules
        /// </summary>
        /// <value>
        /// The Module
        /// </value>
        public IDbSet<Module> Module
        {
            get
            {
                if (_module == null)
                    _module = base.Set<Module>();

                return _module;
            }
        }

        IDbSet<FlagIndicator> _flagindicator;
        /// <summary>
        /// Gets the TASQ Modules
        /// </summary>
        /// <value>
        /// The Module
        /// </value>
        public IDbSet<FlagIndicator> FlagIndicator
        {
            get
            {
                if (_flagindicator == null)
                    _flagindicator = base.Set<FlagIndicator>();

                return _flagindicator;
            }
        }

        IDbSet<TASQCriteriaType> _tasqcriteriatype;
        /// <summary>
        /// Gets the TASQ Modules
        /// </summary>
        /// <value>
        /// The Module
        /// </value>
        public IDbSet<TASQCriteriaType> TASQCriteriaType
        {
            get
            {
                if (_tasqcriteriatype == null)
                    _tasqcriteriatype = base.Set<TASQCriteriaType>();

                return _tasqcriteriatype;
            }
        }

        IDbSet<FertilizerInformation> _fertilizerinformation;
        /// <summary>
        /// Gets the fertilizers information
        /// </summary>
        /// <value>
        /// The Module
        /// </value>
        public IDbSet<FertilizerInformation> FertilizerInformation
        {
            get
            {
                if (_fertilizerinformation == null)
                    _fertilizerinformation = base.Set<FertilizerInformation>();

                return _fertilizerinformation;
            }
        }

        IDbSet<AverageExtraction> _averageextraction;
        /// <summary>
        /// Gets the Average extraction for the fertilization proccess
        /// </summary>
        /// <value>
        /// The Module
        /// </value>
        public IDbSet<AverageExtraction> AverageExtraction
        {
            get
            {
                if (_averageextraction == null)
                    _averageextraction = base.Set<AverageExtraction>();

                return _averageextraction;
            }
        }

        IDbSet<CoffeeType> _coffeetype;
        /// <summary>
        /// Gets the Average extraction for the fertilization proccess
        /// </summary>
        /// <value>
        /// The Module
        /// </value>
        public IDbSet<CoffeeType> CoffeeType
        {
            get
            {
                if (_coffeetype == null)
                    _coffeetype = base.Set<CoffeeType>();

                return _coffeetype;
            }
        }

        #endregion

        #region IQueryableUnitOfWork Members

        /// <summary>
        /// Return a bool indicating that the database is up and running
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return this.Database.Exists();
        }

        /// <summary>
        /// Returns a IDbSet instance for access to entities of the given type in the context,
        /// the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DbSet<T> CreateSet<T>() where T : class
        {
            return base.Set<T>();
        }

        /// <summary>
        /// Attaches the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        public void Attach<T>(T item) where T : class
        {
            //attach and set as unchanged
            base.Entry<T>(item).State = System.Data.Entity.EntityState.Unchanged;
        }

        /// <summary>
        /// Set object as modified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The entity item to set as modifed</param>
        public void SetModified<T>(T item) where T : class
        {
            //this operation also attach item in object state manager
            base.Entry<T>(item).State = System.Data.Entity.EntityState.Modified;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        public void Add<T>(T item) where T : class
        {
            base.Entry<T>(item).State = EntityState.Added;
        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        public void Remove<T>(T item) where T : class
        {
            base.Entry<T>(item).State = EntityState.Deleted;
        }

        /// <summary>
        /// Reload item from database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void Refresh<T>(T item) where T : class
        {
            base.Entry<T>(item).Reload();
        }

        /// <summary>
        /// Apply current values in <paramref name="original" />
        /// </summary>
        /// <typeparam name="T">The type of entity</typeparam>
        /// <param name="original">The original entity</param>
        /// <param name="current">The current entity</param>
        public void ApplyCurrentValues<T>(T original, T current) where T : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<T>(original).CurrentValues.SetValues(current);
        }

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        /// <exception cref="System.Data.Entity.Validation.DbEntityValidationException"></exception>
        /// <remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then an exception is thrown
        /// </remarks>
        public void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch(DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                RollbackChanges();

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        /// <remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then an exception is thrown
        /// </remarks>
        public void CommitWithoutTracking()
        {
            base.Configuration.AutoDetectChangesEnabled = false;
            base.SaveChanges();
        }

        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        /// <remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then 'client changes' are refreshed - Client wins
        /// </remarks>
        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }

        /// <summary>
        /// Rollback tracked changes. See references of UnitOfWork pattern
        /// </summary>
        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = System.Data.Entity.EntityState.Unchanged);
        }

        /// <summary>
        /// Execute specific query with underliying persistence store
        /// </summary>
        /// <typeparam name="T">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">Dialect Query
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer &gt; {0}
        /// </example></param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        /// Enumerable results
        /// </returns>
        public IEnumerable<T> ExecuteQuery<T>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<T>(sqlQuery, parameters);
        }

        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer &gt; {0}
        /// </example></param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        /// The number of affected records
        /// </returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

        #region DbContext Overrides
        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remove unused conventions
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //Add entity configurations in a structured way using 'TypeConfiguration’ classes
            modelBuilder.Configurations.Add(new FarmConfiguration());
            modelBuilder.Configurations.Add(new WorkerConfiguration());
            modelBuilder.Configurations.Add(new ProductivityConfiguration());
            modelBuilder.Configurations.Add(new SoilAnalysisConfiguration());
            modelBuilder.Configurations.Add(new PlantationConfiguration());
            modelBuilder.Configurations.Add(new FloweringPeriodConfiguration());
            modelBuilder.Configurations.Add(new FertilizerConfiguration());
            modelBuilder.Configurations.Add(new FarmOtherActivityConfiguration());
            modelBuilder.Configurations.Add(new FamilyUnitMemberConfiguration());

            modelBuilder.Configurations.Add(new LotConfiguration());
            modelBuilder.Configurations.Add(new InvoiceConfiguration());

            modelBuilder.Configurations.Add(new ProjectConfiguration());

            modelBuilder.Configurations.Add(new AssessmentTemplateConfiguration());

            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new IndicatorConfiguration());
            modelBuilder.Configurations.Add(new CriteriaConfiguration());
            modelBuilder.Configurations.Add(new CriteriaOptionConfiguration());
            modelBuilder.Configurations.Add(new ImpactAssessmentConfiguration());

            modelBuilder.Configurations.Add(new SensoryProfileConfiguration());
            modelBuilder.Configurations.Add(new SensoryProfileAnswerConfiguration());
            modelBuilder.Configurations.Add(new QualityAttributeConfiguration());
            modelBuilder.Configurations.Add(new RangeAttributeConfiguration());
            modelBuilder.Configurations.Add(new OptionAttributeConfiguration());
            modelBuilder.Configurations.Add(new OpenTextAttributeConfiguration());
            modelBuilder.Configurations.Add(new QualityProfileConfiguration());
            modelBuilder.Configurations.Add(new QualityRecommendationsConfiguration());

            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new SupplierConfiguration());
            modelBuilder.Configurations.Add(new SupplyChainConfiguration());
            modelBuilder.Configurations.Add(new SupplyChainStatusConfiguration());

            modelBuilder.Configurations.Add(new DepartmentConfiguration());
            modelBuilder.Configurations.Add(new MunicipalityConfiguration());
            modelBuilder.Configurations.Add(new VillageConfiguration());

            modelBuilder.Configurations.Add(new FarmStatusConfiguration());
            modelBuilder.Configurations.Add(new FarmSubstatusConfiguration());
            modelBuilder.Configurations.Add(new OwnershipTypeConfiguration());
            modelBuilder.Configurations.Add(new CooperativesConfiguration());

            modelBuilder.Configurations.Add(new SoilTypeConfiguration());
            modelBuilder.Configurations.Add(new PlantationTypeConfiguration());
            modelBuilder.Configurations.Add(new PlantationVarietyConfiguration());
            modelBuilder.Configurations.Add(new PlantationStatusConfiguration());
            modelBuilder.Configurations.Add(new FloweringPeriodQualificationConfiguration());
            modelBuilder.Configurations.Add(new OtherActivityConfiguration());

            modelBuilder.Entity<CoffeeType>().ToTable("CoffeeTypes");

            //<atoro> Configuration without using the Entity Configuration
            modelBuilder.Entity<Client>().ToTable("ComercialClients");
            modelBuilder.Entity<Agent>().ToTable("ComercialAgents");
            modelBuilder.Entity<Agreement>().ToTable("ComercialAgreements");
            modelBuilder.Entity<Seller>().ToTable("ComercialSellers");
            modelBuilder.Entity<MoreInformation>().ToTable("ComercialMoreInformation");
            modelBuilder.Entity<Fixation>().ToTable("ComercialFixations");
            modelBuilder.Entity<FixationType>().ToTable("ComercialFixationTypes");
            modelBuilder.Entity<NYPosition>().ToTable("ComercialNYPositions");
            modelBuilder.Entity<ContractInvoice>().ToTable("ComercialInvoices");
            modelBuilder.Entity<ContractLot>().ToTable("ComercialLots");
            modelBuilder.Entity<DocumentReference>().ToTable("ComercialReferences");
            modelBuilder.Entity<ReferenceRelationShip>().HasKey(t => new { t.DocumentId, t.DocumentReferenceId }).ToTable("ComercialReferencesRelationShip");
            modelBuilder.Entity<MDType>().ToTable("ComercialMDType");
            modelBuilder.Entity<MDOrigin>().ToTable("ComercialMDOrigin");
            modelBuilder.Entity<Shipment>().ToTable("ComercialShipment");

            //Sustainability contact entities
            modelBuilder.Entity<Contact>().ToTable("SustainabilityContacts");
            modelBuilder.Entity<Topic>().ToTable("SustainabilityContactTopics");
            modelBuilder.Entity<ContactType>().ToTable("SustainabilityContactTypes");
            modelBuilder.Entity<Location>().ToTable("SustainabilityContactLocations");
            //Sustainability TASQ entities
            modelBuilder.Entity<TASQCriteria>().ToTable("TASQCriterias");
            modelBuilder.Entity<TASQAssessmentAnswer>().ToTable("TASQAssessmentAnswers");
            modelBuilder.Entity<TASQAssessment>().ToTable("TASQAssessments");
            modelBuilder.Entity<SubModule>().ToTable("TASQSubModules");
            modelBuilder.Entity<Module>().ToTable("TASQModules");
            modelBuilder.Entity<FlagIndicator>().ToTable("TASQFlagIndicators");
            modelBuilder.Entity<TASQCriteriaType>().ToTable("TASQCriteriaTypes");

            //Fertilizers calculator
            modelBuilder.Entity<FertilizerInformation>().ToTable("FertilizerInformation");
            modelBuilder.Entity<AverageExtraction>().ToTable("AverageExtraction");

            //Many to Many relationship between Attribute and Recommendations
            modelBuilder.Entity<OptionAttribute>()
                    .HasMany<QualityRecommendations>(c => c.QualityRecommendations)
                    .WithMany(t => t.OptionAttribute)
                    .Map(ct =>
                    {
                        ct.MapLeftKey("OptionAttributeId");
                        ct.MapRightKey("RecommendationId");
                        ct.ToTable("QualityOptionAttributeRecommendation");
                    });

            //Many to Many relationship between Contact and Topic
            modelBuilder.Entity<Contact>()
                    .HasMany<Topic>(c => c.Topics)
                    .WithMany(t => t.Contacts)
                    .Map(ct =>
                        {
                            ct.MapLeftKey("ContactId");
                            ct.MapRightKey("TopicId");
                            ct.ToTable("SustainabilityContactTopic");
                        });

            //Many to Many relationship between Contact and Farm
            modelBuilder.Entity<Contact>()
                    .HasMany<Farm>(c => c.Farms)
                    .WithMany(f => f.Contacts)
                    .Map(cf =>
                    {
                        cf.MapLeftKey("ContactId");
                        cf.MapRightKey("FarmId");
                        cf.ToTable("SustainabilityContactFarm");
                    });

            //This configuration is done only for authentication
            modelBuilder.Entity<User>()
                 .HasMany(u => u.Roles)
                 .WithMany(r => r.Users)
                 .Map(m =>
                 {
                     m.ToTable("UserRoles");
                     m.MapLeftKey("UserId");
                     m.MapRightKey("RoleId");
                 });


            //Many to Many relationship between User and Project
            modelBuilder.Entity<User>()
                    .HasMany<Project>(c => c.Projects)
                    .WithMany(f => f.Users)
                    .Map(cf =>
                    {
                        cf.MapLeftKey("UserId");
                        cf.MapRightKey("ProjecId");
                        cf.ToTable("UserProjects");
                    });

            modelBuilder.Entity<User>()
                .HasMany(u => u.SensoryProfileAssessments)
                .WithRequired(spa => spa.User)
                .HasForeignKey(spa => spa.UserId);
        }
        #endregion

    }
}
