using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.SustainabilityModule.DashboardAggregate
{
    /// <summary>
    /// Tasq By Results
    /// </summary>
    public class TASQResults
    {
        /// <summary>
        /// Gets or sets the yes.
        /// </summary>
        /// <value>
        /// The yes.
        /// </value>
        public decimal YES { get; set; }

        /// <summary>
        /// Gets or sets the no.
        /// </summary>
        /// <value>
        /// The no.
        /// </value>
        public decimal NO { get; set; }

        /// <summary>
        /// Gets or sets the na.
        /// </summary>
        /// <value>
        /// The na.
        /// </value>
        public decimal NA { get; set; }

        /// <summary>
        /// Gets or sets the question identifier.
        /// </summary>
        /// <value>
        /// The question identifier.
        /// </value>
        //public Guid QuestionID { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the flag indicator identifier.
        /// </summary>
        /// <value>
        /// The flag indicator identifier.
        /// </value>
        public int FlagIndicatorID { get; set; }
    }
}
