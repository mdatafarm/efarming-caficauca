using EFarming.Common;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.TasqModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.AdminModule.AssessmentAggregate
{
    /// <summary>
    /// Assesment Template Entity
    /// </summary>
    public class AssessmentTemplate : Entity
    {
        /// <summary>
        /// The cupping identifier
        /// </summary>
        public static Guid CuppingId = Guid.Parse("7B01B167-B114-4D6A-8174-8E45571A9216");
        /// <summary>
        /// The punto zero identifier
        /// </summary>
        public static Guid PuntoZeroId = Guid.Parse("208633F3-7FE8-46F7-BCC4-448CCD3DD9C5");

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
        public string Name
        {
            get { return _name; }
            set { _name = SanitizeString(value); }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the expired on.
        /// </summary>
        /// <value>
        /// The expired on.
        /// </value>
        public DateTime? ExpiredOn { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the published.
        /// </summary>
        /// <value>
        /// The published.
        /// </value>
        public bool? Published { get; set; }

        /// <summary>
        /// Gets or sets the public.
        /// </summary>
        /// <value>
        /// The public.
        /// </value>
        public bool? Public { get; set; }

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
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public virtual ICollection<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the quality attributes.
        /// </summary>
        /// <value>
        /// The quality attributes.
        /// </value>
        public virtual ICollection<QualityAttribute> QualityAttributes { get; set; }

        /// <summary>
        /// Gets or sets the modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        public virtual ICollection<Module> Modules { get; set; }
    }
}
