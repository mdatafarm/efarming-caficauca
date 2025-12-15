using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.DashboardModule.BasicColumn
{
    /// <summary>
    /// Chart Basic Column https://www.highcharts.com/demo/column-basic
    /// </summary>
    [Serializable]
    public class BasicColumn
    {
        public BasicColumn(bool ispercent)
        {
            this.series = new List<series>();
            this.chart = new Chart();
            this.tooltip = new tooltip(ispercent);
            this.plotOptions = new plotOptions();
            chart.type = "column";
        }
        public Chart chart { get; set; }
        public title title { get; set; }
        public subtitle subtitle { get; set; }
        public xAxis xAxis { get; set; }
        public yAxis yAxis { get; set; }
        public tooltip tooltip { get; set; }
        public plotOptions plotOptions { get; set; }
        public List<series> series { get; set; }
    }
    [Serializable]
    public class Chart
    {
        public string type { get; set; }
    }
    [Serializable]
    public class title
    {
        public string text { get; set; }
    }
    [Serializable]
    public class subtitle
    {
        public string text { get; set; }
    }
    [Serializable]
    public class xAxis
    {
        public List<string> categories { get; set; }
        public bool crosshair { get; set; }
    }
    [Serializable]
    public class yAxis
    {
        public int min { get; set; }
        public title title { get; set; }
    }
    [Serializable]
    public class tooltip
    {
        public bool _ispercent { get; set; }

        public tooltip(bool ispercent)
        {
            _ispercent = ispercent;
        }
        public string headerFormat
        {
            get
            {
                return "<span style='font - size:10px'>{point.key}</span><table>";
            }
        }
        public string pointFormat
        {
            get
            {
                if(this._ispercent)
                    return "<tr><td style='color: { series.color}; padding: 0'>{series.name}: </td><td style='padding:0'><b>{point.y:.1f}%  </b></td></tr>";
                else
                    return "<tr><td style='color: { series.color}; padding: 0'>{series.name}: </td><td style='padding:0'><b>{point.y:.1f} </b></td></tr>";
            }
        }
        public string footerFormat
        {
            get
            {
                return "</table>";
            }
        }
        public bool shared
        {
            get
            {
                return true;
            }
        }
        //public bool useHTML
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}
    }
    [Serializable]
    public class series
    {
        public string name { get; set; }
        public List<double> data { get; set; }
    }
    [Serializable]
    public class plotOptions
    {
        public plotOptions()
        {
            this.column = new column();
        }
        public column column { get; set; }
    }
    [Serializable]
    public class column
    {
        public double pointPadding
        {
            get
            {
                return 0.2;
            }
        }
        public int borderWidth
        {
            get
            {
                return 0;
            }
        }
    }
}
