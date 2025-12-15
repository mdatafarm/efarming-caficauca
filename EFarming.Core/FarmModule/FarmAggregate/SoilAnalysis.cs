using EFarming.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.FarmModule.FarmAggregate
{
    /// <summary>
    /// SoilAnalysis 
    /// </summary>
    public class SoilAnalysis : Historical
    {
        /// <summary>
        /// The _comment
        /// </summary>
        private string _comment;
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        [Required]
        public string Comment
        {
            get { return _comment; }
            set { _comment = SanitizeString(value); }
        }

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        public int Depth { get; set; }

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
    }
}
