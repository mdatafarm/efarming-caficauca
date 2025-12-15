using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EFarming.Core.DashboardModule
{
    /// <summary>
    /// SerieItem
    /// </summary>
    [Serializable]
    [DataContract]
    public class SerieItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SerieItem"/> class.
        /// </summary>
        public SerieItem()
        {
            data = new List<List<object>>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember(Name="name")]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DataMember(Name="data")]
        public List<List<object>> data { get; set; }
    }

    /// <summary>
    /// ColumSerieItem
    /// </summary>
    [Serializable]
    [DataContract]
    public class ColumnSerieItem : SerieItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnSerieItem"/> class.
        /// </summary>
        public ColumnSerieItem()
        {
            type = "column";
            data = new List<double>();
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember(Name = "type")]
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DataMember(Name = "data")]
        public new List<double> data { get; set; }
    }

    /// <summary>
    /// PieSerieItem
    /// </summary>
    [Serializable]
    [DataContract]
    public class PieSerieItem : SerieItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PieSerieItem"/> class.
        /// </summary>
        public PieSerieItem()
        {
            type = "pie";
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember(Name = "type")]
        public string type { get; set; }
    }

    public class drillObject
    {
        public string name { get; set; }
        public decimal y { get; set; }
        public string drilldown { get; set; }
    }

    /// <summary>
    /// PieSerieItem
    /// </summary>
    [Serializable]
    [DataContract]
    public class SerieDrillDownItem : SerieItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PieSerieItem"/> class.
        /// </summary>
        public SerieDrillDownItem()
        {
            name = "pie";
            data = new List<drillObject>();

        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember(Name = "name")]
        public string type { get; set; }

        [DataMember(Name = "data")]
        public new List<drillObject> data { get; set; }
    }

    /// <summary>
    /// PolarSerieItem
    /// </summary>
    [DataContract]
    [Serializable]
    public class PolarSerieItem : SerieItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PolarSerieItem"/> class.
        /// </summary>
        public PolarSerieItem()
        {
            data = new List<double>();
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DataMember(Name = "data")]
        public new List<double> data { get; set; }

        /// <summary>
        /// Gets or sets the point placement.
        /// </summary>
        /// <value>
        /// The point placement.
        /// </value>
        [DataMember(Name = "pointPlacement")]
        public string pointPlacement { get; set; }
    }

    /// <summary>
    /// LineSerieItem
    /// </summary>
    [Serializable]
    [DataContract]
    public class LineSerieItem : SerieItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LineSerieItem"/> class.
        /// </summary>
        public LineSerieItem()
        {
            data = new List<double>();
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [DataMember(Name = "data")]
        public new List<double> data { get; set; }
    }
}
