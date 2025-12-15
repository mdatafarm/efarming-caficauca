using EFarming.Common;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.AdminModule.VillageAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using System;
using System.Collections.Generic;

namespace EFarming.Core.QualityModule.MicrolotAggregate
{
    /// <summary>
    /// Microlot Entity
    /// </summary>
    public class Microlot : Entity
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the municipality identifier.
        /// </summary>
        /// <value>
        /// The municipality identifier.
        /// </value>
        public Guid? MunicipalityId { get; set; }

        /// <summary>
        /// Gets or sets the village identifier.
        /// </summary>
        /// <value>
        /// The village identifier.
        /// </value>
        public Guid? VillageId { get; set; }

        /// <summary>
        /// Gets or sets the municipality.
        /// </summary>
        /// <value>
        /// The municipality.
        /// </value>
        public virtual Municipality Municipality { get; set; }

        /// <summary>
        /// Gets or sets the village.
        /// </summary>
        /// <value>
        /// The village.
        /// </value>
        public virtual Village Village { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile assessments.
        /// </summary>
        /// <value>
        /// The sensory profile assessments.
        /// </value>
        public virtual ICollection<SensoryProfileAssessment> SensoryProfileAssessments { get; set; }
    }
}
