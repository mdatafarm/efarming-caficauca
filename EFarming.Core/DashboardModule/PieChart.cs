using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EFarming.Core.DashboardModule
{
    /// <summary>
    /// PieChart
    /// </summary>
    [Serializable]
    [DataContract]
    public class PieChart : Chart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PieChart"/> class.
        /// </summary>
        public PieChart()
        {
            Categories = new List<string>();
        }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        [DataMember(Name = "Categories")]
        public List<string> Categories { get; set; }
    }
}
