using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EFarming.Core.DashboardModule
{
    /// <summary>
    /// Chart
    /// </summary>
    [Serializable]
    [DataContract]
    public abstract class Chart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chart"/> class.
        /// </summary>
        public Chart()
        {
            Categories = new List<string>();
            Items = new List<SerieItem>();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [DataMember(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        [DataMember(Name = "Categories")]
        public List<string> Categories { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [DataMember(Name = "Items")]
        public List<SerieItem> Items { get; set; }
    }
}
