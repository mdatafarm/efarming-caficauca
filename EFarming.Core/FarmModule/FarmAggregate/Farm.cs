using EFarming.Common;
using EFarming.Core.AdminModule.CooperativeAggregate;
using EFarming.Core.AdminModule.FarmStatusAggregate;
using EFarming.Core.AdminModule.FarmSubstatusAggregate;
using EFarming.Core.AdminModule.OwnershipTypeAggregate;
using EFarming.Core.AdminModule.SoilTypeAggregate;
using EFarming.Core.AdminModule.SupplyChainAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.FarmModule.FamilyUnitAggregate;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.Core.QualityModule.ChecklistAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.SustainabilityModule.ContactAggregate;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// Farm Entity
    /// </summary>
    public partial class Farm : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Farm"/> class.
        /// </summary>
        public Farm()
        {
            //SoilAnalysis = new List<SoilAnalysis>();
            //SoilTypes = new List<SoilType>();
        }

        /// <summary>
        /// The _code
        /// </summary>
        private string _code;

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        [Required]
        [MaxLength(16)]
        public string Code
        {
            get { return _code; }
            set { _code = SanitizeString(value); }
        }

        /// <summary>
        /// The _name
        /// </summary>
        private string _name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [MaxLength(128)]
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }

        /// <summary>
        /// Gets or sets the geo location.
        /// </summary>
        /// <value>
        /// The geo location.
        /// </value>
        public DbGeography GeoLocation { get; set; }

        /// <summary>
        /// Gets or sets the productivity.
        /// </summary>
        /// <value>
        /// The productivity.
        /// </value>
        [Required]
        public virtual Productivity Productivity { get; set; }

        /// <summary>
        /// Gets or sets the worker.
        /// </summary>
        /// <value>
        /// The worker.
        /// </value>
        public virtual Worker Worker { get; set; }

        /// <summary>
        /// Gets or sets the supply chain identifier.
        /// </summary>
        /// <value>
        /// The supply chain identifier.
        /// </value>
        [ForeignKey("SupplyChain")]
        public Guid? SupplyChainId { get; set; }

        /// <summary>
        /// Gets or sets the village identifier.
        /// </summary>
        /// <value>
        /// The village identifier.
        /// </value>
        [Required]
        [ForeignKey("Village")]
        public Guid VillageId { get; set; }

        /// <summary>
        /// Gets or sets the farm status identifier.
        /// </summary>
        /// <value>
        /// The farm status identifier.
        /// </value>
        [ForeignKey("FarmStatus")]
        public Guid? FarmStatusId { get; set; }

        /// <summary>
        /// Gets or sets the farm substatus identifier.
        /// </summary>
        /// <value>
        /// The farm substatus identifier.
        /// </value>
        [ForeignKey("FarmSubstatus")]
        public Guid? FarmSubstatusId { get; set; }

        /// <summary>
        /// Gets or sets the cooperative identifier.
        /// </summary>
        /// <value>
        /// The cooperative identifier.
        /// </value>
        [ForeignKey("Cooperative")]
        public Guid? CooperativeId { get; set; }

        /// <summary>
        /// Gets or sets the ownership type identifier.
        /// </summary>
        /// <value>
        /// The ownership type identifier.
        /// </value>
        [ForeignKey("OwnershipType")]
        public Guid? OwnershipTypeId { get; set; }

        /// <summary>
        /// Gets or sets the supply chain.
        /// </summary>
        /// <value>
        /// The supply chain.
        /// </value>
        public virtual SupplyChain SupplyChain { get; set; }

        /// <summary>
        /// Gets or sets the village.
        /// </summary>
        /// <value>
        /// The village.
        /// </value>
        public virtual Village Village { get; set; }

        /// <summary>
        /// Gets or sets the farm status.
        /// </summary>
        /// <value>
        /// The farm status.
        /// </value>
        public virtual FarmStatus FarmStatus { get; set; }

        /// <summary>
        /// Gets or sets the farm substatus.
        /// </summary>
        /// <value>
        /// The farm substatus.
        /// </value>
        public virtual FarmSubstatus FarmSubstatus { get; set; }

        /// <summary>
        /// Gets or sets the cooperative.
        /// </summary>
        /// <value>
        /// The cooperative.
        /// </value>
        public virtual Cooperative Cooperative { get; set; }

        /// <summary>
        /// Gets or sets the type of the ownership.
        /// </summary>
        /// <value>
        /// The type of the ownership.
        /// </value>
        public virtual OwnershipType OwnershipType { get; set; }

        /// <summary>
        /// Gets or sets the soil analysis.
        /// </summary>
        /// <value>
        /// The soil analysis.
        /// </value>
        public virtual ICollection<SoilAnalysis> SoilAnalysis { get; set; }

        /// <summary>
        /// Gets or sets the soil types.
        /// </summary>
        /// <value>
        /// The soil types.
        /// </value>
        public virtual ICollection<SoilType> SoilTypes { get; set; }

        /// <summary>
        /// Gets or sets the fertilizers.
        /// </summary>
        /// <value>
        /// The fertilizers.
        /// </value>
        public virtual ICollection<Fertilizer> Fertilizers { get; set; }

        /// <summary>
        /// Gets or sets the other activities.
        /// </summary>
        /// <value>
        /// The other activities.
        /// </value>
        public virtual ICollection<FarmOtherActivity> OtherActivities { get; set; }

        /// <summary>
        /// Gets or sets the family unit members.
        /// </summary>
        /// <value>
        /// The family unit members.
        /// </value>
        public virtual ICollection<FamilyUnitMember> FamilyUnitMembers { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>
        /// The images.
        /// </value>
        public virtual ICollection<Image> Images { get; set; }

        /// <summary>
        /// Gets or sets the impact assessments.
        /// </summary>
        /// <value>
        /// The impact assessments.
        /// </value>
        public virtual ICollection<ImpactAssessment> ImpactAssessments { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile assessments.
        /// </summary>
        /// <value>
        /// The sensory profile assessments.
        /// </value>
        public virtual ICollection<SensoryProfileAssessment> SensoryProfileAssessments { get; set; }

        /// <summary>
        /// Gets or sets the associated people.
        /// </summary>
        /// <value>
        /// The associated people.
        /// </value>
        public virtual ICollection<User> AssociatedPeople { get; set; }

        /// <summary>
        /// Gets or sets the invoices.
        /// </summary>
        /// <value>
        /// The invoices.
        /// </value>
        public virtual ICollection<Invoice> Invoices { get; set; }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>
        /// The projects.
        /// </value>
        public virtual ICollection<Project> Projects { get; set; }

        /// <summary>
        /// Gets or sets the checklists.
        /// </summary>
        /// <value>
        /// The checklists.
        /// </value>
        public virtual ICollection<Checklist> Checklists { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        public virtual ICollection<Contact> Contacts { get; set; }

        #region public methods
        /// <summary>
        /// Gets the impact answers.
        /// </summary>
        /// <returns></returns>
        public IDictionary<Indicator, int> GetImpactAnswers()
        {
            var lastAssessment = ImpactAssessments.OrderByDescending(f => f.Date).FirstOrDefault();
            if (lastAssessment == null) // NO assessments yet
                return null;
            return lastAssessment.GetAnswers();
        }

        /// <summary>
        /// Gets the impact track.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public IEnumerable<ImpactAssessment> GetImpactTrack(int year)
        {
            return ImpactAssessments.Where(ia => ia.Date.Year == year);
        }
        #endregion
    }
}
