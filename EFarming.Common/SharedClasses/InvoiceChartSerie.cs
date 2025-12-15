using System.Collections.Generic;

namespace EFarming.Common.SharedClasses
{
    /// <summary>
    /// InvoicesChartSerie
    /// </summary>
    public class InvoiceChartSerie
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceChartSerie"/> class.
        /// </summary>
        public InvoiceChartSerie()
        {
            data = new List<List<object>>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public List<List<object>> data { get; set; }
    }
}
