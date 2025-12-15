using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.ComercialModule
{
    public partial class Fixation : Entity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key, ForeignKey("Agreement")]
        public override Guid Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        /// <summary>
        /// Gets or sets the fixation date.
        /// </summary>
        /// <value>
        /// The fixation date.
        /// </value>
        public DateTime? FixationDate { get; set; }

        /// <summary>
        /// Gets or sets the fixation level.
        /// </summary>
        /// <value>
        /// The fixation level.
        /// </value>
        public decimal? FixationLevel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Fixation"/> is fixed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if fixed; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool Fixed { get; set; }

        /// <summary>
        /// Gets or sets the fixation type identifier.
        /// </summary>
        /// <value>
        /// The fixation type identifier.
        /// </value>
        [Display(Name = "FixationType")]
        public int FixationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the fixation.
        /// </summary>
        /// <value>
        /// The type of the fixation.
        /// </value>
        public virtual FixationType FixationType { get; set; }

        /// <summary>
        /// Gets or sets the agreement.
        /// </summary>
        /// <value>
        /// The agreement.
        /// </value>
        public virtual Agreement Agreement { get; set; }
    }
}
