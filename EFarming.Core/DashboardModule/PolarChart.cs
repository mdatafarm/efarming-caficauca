using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EFarming.Core.DashboardModule
{
    /// <summary>
    /// PolarChart
    /// </summary>
    [Serializable]
    [DataContract]
    public class PolarChart : Chart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolarChart"/> class.
        /// </summary>
        public PolarChart()
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
