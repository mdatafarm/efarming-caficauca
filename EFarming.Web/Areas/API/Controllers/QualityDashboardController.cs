using EFarming.Core.DashboardModule;
using EFarming.Core.QualityModule.DashboardAggregate;
using EFarming.DAL;
using EFarming.Manager.Contract;
using EFarming.Manager.Implementation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// QualityDashboard Controller API
    /// </summary>
    public class QualityDashboardController : ApiController
    {
        /// <summary>
        /// The database
        /// </summary>
        private UnitOfWork db = new UnitOfWork();

        #region General_Reports

        [HttpGet]
        public PieChart AssessmentType(Guid? CooperativeId, DateTime startDate, DateTime endDate)
        {
            List<Clasification> affectedRows = new List<Clasification>();
            if (CooperativeId.HasValue == true)
                affectedRows = db.ExecuteQuery<Clasification>("AssessmentsByTypeByCooperative @cooperativeId, @firstDate, @endDate", new SqlParameter("cooperativeId", CooperativeId.ToString()), new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();
            else
                affectedRows = db.ExecuteQuery<Clasification>("AssessmentsByType @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();


            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Microlot vs Individually";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        [HttpGet]
        public QDAObject QDA(Guid? CooperativeId, DateTime startDate, DateTime endDate)
        {

            List<Clasification> affectedRows = new List<Clasification>();
            if (CooperativeId.HasValue == true)
                affectedRows = db.ExecuteQuery<Clasification>("QDAByCooperative @cooperativeId, @firstDate, @endDate", new SqlParameter("cooperativeId", CooperativeId.ToString()), new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();
            else
                affectedRows = db.ExecuteQuery<Clasification>("QDA @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();

            var chart = new QDAObject();
            chart.Series = new List<QDASerie>();

            chart.Categories = new List<string>(new string[] { "FRAGANCIA/AROMA", "SABOR", "SABOR RESIDUAL", "ACIDEZ", "CUERPO", "BALANCE", "DULZOR", "PUNTAJE CATADOR", "TAZA LIMPIA", "UNIFORMIDAD" });

            QDASerie Serie = new QDASerie
            {
                name = "QDA",
            };

            foreach (var item in affectedRows)
            {
                switch (item.Answer)
                {
                    case "FRAGANCIA/AROMA":
                        item.Cooperative = 1.ToString();
                        break;
                    case "SABOR":
                        item.Cooperative = 2.ToString();
                        break;
                    case "SABOR RESIDUAL":
                        item.Cooperative = 3.ToString();
                        break;
                    case "ACIDEZ":
                        item.Cooperative = 4.ToString();
                        break;
                    case "CUERPO":
                        item.Cooperative = 5.ToString();
                        break;
                    case "BALANCE":
                        item.Cooperative = 6.ToString();
                        break;
                    case "DULZOR":
                        item.Cooperative = 7.ToString();
                        break;
                    case "PUNTAJE CATADOR":
                        item.Cooperative = 8.ToString();
                        break;
                    case "TAZA LIMPIA":
                        item.Cooperative = 9.ToString();
                        break;
                    case "UNIFORMIDAD":
                        item.Cooperative = 10.ToString();
                        break;
                }

            };

            //affectedRows.Add(new Clasification
            //{
            //    Cooperative = 2.ToString(),
            //    Quantity = affectedRows.Where(a => a.Answer == "FRAGANCIA/AROMA").Select(q => q.Quantity).FirstOrDefault(),
            //    Answer = "FRAGANCIA/AROMA"
            //});

            Serie.data = affectedRows.ToList().OrderBy(c => c.Cooperative).Select(d => d.Quantity).ToList();
            chart.Series.Add(Serie);

            return chart;
        }

        public class QDAObject
        {
            public List<string> Categories { get; set; }
            public List<QDASerie> Series { get; set; }
        };

        public class QDASerie
        {
            public string name { get; set; }
            public List<int> data { get; set; }
        };

        /// <summary>
        /// Clasifications the report.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart ClasificationReport(Guid? CooperativeId, DateTime startDate, DateTime endDate)
        {
            List<Clasification> affectedRows = new List<Clasification>();
            if (CooperativeId.HasValue == true)
                affectedRows = db.ExecuteQuery<Clasification>("Quality_clasificationByCooperative @cooperativeId, @firstDate, @endDate", new SqlParameter("cooperativeId", CooperativeId.ToString()), new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();
            else
                affectedRows = db.ExecuteQuery<Clasification>("Quality_clasification @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Clasification";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Clasifications the report overview.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart ClasificationReportOverview(DateTime startDate, DateTime endDate)
        {
            var affectedRows1 = db.ExecuteQuery<Clasification>("Overview_Quality_clasification @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Clasification";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows1)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Defectses the report.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart DefectsReport(Guid? CooperativeId, DateTime startDate, DateTime endDate)
        {
            List<Clasification> affectedRows = new List<Clasification>();
            if (CooperativeId.HasValue == true)
                affectedRows = db.ExecuteQuery<Clasification>("Quality_defectsByCooperative @cooperativeId, @firstDate, @endDate", new SqlParameter("cooperativeId", CooperativeId.ToString()), new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();
            else
                affectedRows = db.ExecuteQuery<Clasification>("Quality_defects @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Defects";

            List<List<Object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Defectses the report overview.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart DefectsReportOverview(DateTime startDate, DateTime endDate)
        {
            var affectedRows = db.ExecuteQuery<Clasification>("Overview_Quality_defects @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Defects";

            List<List<Object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Decisions the report.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ColumnChart DecisionReport()
        {
            var decisions = db.ExecuteQuery<Clasification>("Quality_decision");

            var chart = new ColumnChart();
            chart.Title = "Decision";
            var Seriedata = new ColumnSerieItem();

            List<List<Object>> Categories = new List<List<object>>();
            //chart.Categories = decisions.Select(Coo => Coo.Cooperative.ToString()).ToList();

            //foreach (var item1 in decisions)
            //{
            //    Seriedata.name = item1.Answer;
            //    foreach(var Cooperative in decisions.ToList())
            //    {
            //        if(item1.Cooperative == Cooperative)
            //        {
            //            Seriedata.data.Add(item1.Quantity);
            //        }
            //    }
            //}

            //data.data = data1;

            //chart.Items.Add(data);

            return chart;
        }

        #endregion

        #region ByCooperative_Reports

        /// <summary>
        /// Clasifications the report by cooperative.
        /// </summary>
        /// <param name="cooperative">The cooperative.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart ClasificationReportByCooperative(string cooperative, DateTime startDate, DateTime endDate)
        {
            var affectedRows1 = db.ExecuteQuery<Clasification>("Quality_clasificationByCooperative @Cooperative", new SqlParameter("Cooperative", cooperative));

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Clasification";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows1)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Overviews the clasification report by cooperative.
        /// </summary>
        /// <param name="cooperative">The cooperative.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewClasificationReportByCooperative(string cooperative, DateTime startDate, DateTime endDate)
        {
            var affectedRows1 = db.ExecuteQuery<Clasification>("Overview_Quality_clasificationByCooperative @Cooperative, @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("Cooperative", cooperative)).ToList();

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Clasification";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows1)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Clasifications the report by.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart ClasificationReportBy(string type, Guid? id, DateTime startDate, DateTime endDate)
        {
            List<Clasification> affectedRows1 = new List<Clasification>();
            switch (type)
            {
                case "Country":
                    affectedRows1 = db.ExecuteQuery<Clasification>("Overview_clasificationByCountry @CountryId, @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("CountryId", id)).ToList();
                    break;
                case "Supplier":
                    affectedRows1 = db.ExecuteQuery<Clasification>("Overview_clasificationBySupplier @SupplierId, @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("SupplierId", id)).ToList();
                    break;
            }


            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Clasification";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows1)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Defectses the report by cooperative.
        /// </summary>
        /// <param name="cooperative">The cooperative.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart DefectsReportByCooperative(string cooperative, DateTime startDate, DateTime endDate)
        {
            var affectedRows1 = db.ExecuteQuery<Clasification>("Quality_defectsByCooperative @Cooperative", new SqlParameter("Cooperative", cooperative));

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Defects";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows1)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Overviews the defects report by cooperative.
        /// </summary>
        /// <param name="cooperative">The cooperative.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart OverviewDefectsReportByCooperative(string cooperative, DateTime startDate, DateTime endDate)
        {
            var affectedRows1 = db.ExecuteQuery<Clasification>("Overview_Quality_defectsByCooperative @Cooperative, @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("Cooperative", cooperative)).ToList();

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Defects";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows1)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Defectses the report by.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart DefectsReportBy(string type, Guid? id, DateTime startDate, DateTime endDate)
        {
            List<Clasification> affectedRows1 = new List<Clasification>();
            switch (type)
            {
                case "Country":
                    affectedRows1 = db.ExecuteQuery<Clasification>("Overview_defectsByCountry @CountryId, @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("CountryId", id)).ToList();
                    break;
                case "Supplier":
                    affectedRows1 = db.ExecuteQuery<Clasification>("Overview_defectsBySupplier @SupplierId, @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("SupplierId", id)).ToList();
                    break;
            }

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Defects";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in affectedRows1)
            {
                data1.Add(new List<object> { item1.Answer, item1.Quantity });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        #endregion
    }
}
