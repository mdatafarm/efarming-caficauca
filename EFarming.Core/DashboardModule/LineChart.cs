using System;
using System.Runtime.Serialization;

namespace EFarming.Core.DashboardModule
{
    /// <summary>
    /// LineChart
    /// </summary>
    [Serializable]
    [DataContract]
    public class LineChart : Chart
    {
        /// <summary>
        /// Gets or sets the y title.
        /// </summary>
        /// <value>
        /// The y title.
        /// </value>
        [DataMember(Name = "YTitle")]
        public string YTitle { get; set; }
    }
}
