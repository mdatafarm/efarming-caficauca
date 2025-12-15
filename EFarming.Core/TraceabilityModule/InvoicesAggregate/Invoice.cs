using EFarming.Common;
using EFarming.Core.AdminModule.CooperativeAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.Core.TraceabilityModule.LotAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFarming.Core.TraceabilityModule.InvoicesAggregate
{
    /// <summary>
    /// Invoice Entity
    /// </summary>
    public class Invoice : Historical
    {
        /// <summary>
        /// Gets or sets the receipt.
        /// </summary>
        /// <value>
        /// The receipt.
        /// </value>
        [Required]
        public int InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the identification.
        /// </summary>
        /// <value>
        /// The identification.
        /// </value>
        [Required]
        [MaxLength(32)]
        public string Identification { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        [Required]
        public DateTime DateInvoice { get; set; }

        /// <summary>
        /// Gets or sets the ubication.
        /// </summary>
        /// <value>
        /// The ubication.
        /// </value>
        public int Ubication { get; set; }

        /// <summary>
        /// Gets or sets the hold.
        /// </summary>
        /// <value>
        /// The hold.
        /// </value>
        public int Hold { get; set; }

        /// <summary>
        /// Gets or sets the cash.
        /// </summary>
        /// <value>
        /// The cash.
        /// </value>
        public int Cash { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the base kg.
        /// </summary>
        /// <value>
        /// The base kg.
        /// </value>
        public double BaseKg { get; set; }

        public int CoffeeTypeId { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        [Required]
        public Guid FarmId { get; set; }

        /// <summary>
        /// Gets or sets the farm.
        /// </summary>
        /// <value>
        /// The farm.
        /// </value>
        public virtual Farm Farm { get; set; }

        /// <summary>
        /// Gets or sets the sensory profile assessments.
        /// </summary>
        /// <value>
        /// The sensory profile assessments.
        /// </value>
        public virtual ICollection<SensoryProfileAssessment> SensoryProfileAssessments { get; set; }

        public virtual CoffeeType CoffeeType { get; set; }
    }
}
