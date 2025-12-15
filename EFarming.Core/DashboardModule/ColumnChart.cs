using System;
using System.Runtime.Serialization;

namespace EFarming.Core.DashboardModule
{
    /// <summary>
    /// ColumnChart
    /// </summary>
    [Serializable]
    [DataContract]
    public class ColumnChart : Chart
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
