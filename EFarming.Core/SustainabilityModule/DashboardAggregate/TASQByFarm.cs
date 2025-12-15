using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.SustainabilityModule.DashboardAggregate
{
    /// <summary>
    /// TasqByFarm Entity
    /// </summary>
    public class TASQByFarm
    {
        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>
        /// The answer.
        /// </value>
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets the farm identifier.
        /// </summary>
        /// <value>
        /// The farm identifier.
        /// </value>
        public string FarmId { get; set; }

        /// <summary>
        /// Gets or sets the name of the farm.
        /// </summary>
        /// <value>
        /// The name of the farm.
        /// </value>
        public string FarmName { get; set; }

        /// <summary>
        /// Gets or sets the critic.
        /// </summary>
        /// <value>
        /// The critic.
        /// </value>
        public int Critic { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        /// <value>
        /// The question.
        /// </value>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the question identifier.
        /// </summary>
        /// <value>
        /// The question identifier.
        /// </value>
        public int QuestionId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }
    }
}
