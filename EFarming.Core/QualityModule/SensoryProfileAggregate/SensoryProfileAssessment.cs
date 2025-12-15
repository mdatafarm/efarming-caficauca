using EFarming.Common;
using EFarming.Common.Resources;
using EFarming.Core.AdminModule.AssessmentAggregate;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.QualityModule.MicrolotAggregate;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// SensoryProfileAssessment 
    /// </summary>
    public class SensoryProfileAssessment : Historical
    {
        /// <summary>
        /// The farm
        /// </summary>
        public static string FARM = FarmMessages.Farm;
        /// <summary>
        /// The microlot
        /// </summary>
        public static string MICROLOT = QualityMessage.Microlot;
        /// <summary>
        /// The transaction
        /// </summary>
        public static string TRANSACTION = TraceabilityMessage.Transaction;

        /// <summary>
        /// The types
        /// </summary>
        public static List<string> TYPES = new List<string> { FARM, MICROLOT, TRANSACTION };

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Required]
        [MaxLength(64)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the assessment template identifier.
        /// </summary>
        /// <value>
        /// The assessment template identifier.
        /// </value>
        [Required]
        public Guid AssessmentTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public Guid? FarmId { get; set; }

        /// <summary>
        /// Gets or sets the invoice identifier.
        /// </summary>
        /// <value>
        /// The invoice identifier.
        /// </value>
        public Guid? InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets the microlot identifier.
        /// </summary>
        /// <value>
        /// The microlot identifier.
        /// </value>
        public Guid? MicrolotId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public virtual Farm Farm { get; set; }

        /// <summary>
        /// Gets or sets the invoice.
        /// </summary>
        /// <value>
        /// The invoice.
        /// </value>
        public virtual Invoice Invoice { get; set; }

        /// <summary>
        /// Gets or sets the microlot.
        /// </summary>
        /// <value>
        /// The microlot.
        /// </value>
        public virtual Microlot Microlot { get; set; }

        /// <summary>
        /// Gets or sets the assessment template.
        /// </summary>
        /// <value>
        /// The assessment template.
        /// </value>
        public virtual AssessmentTemplate AssessmentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile answers.
        /// </summary>
        /// <value>
        /// The sensory profile answers.
        /// </value>
        public virtual ICollection<SensoryProfileAnswer> SensoryProfileAnswers { get; set; }
    }
}
