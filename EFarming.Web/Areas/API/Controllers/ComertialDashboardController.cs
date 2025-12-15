using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EFarming.DAL;
using EFarming.Core.DashboardModule;
using System.Data.SqlClient;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// Comertial Dashboard
    /// </summary>
    public class ComertialDashboardController : ApiController
    {
        /// <summary>
        /// The uow
        /// </summary>
        private UnitOfWork uow = new UnitOfWork();

        /// <summary>
        /// Numberofs the farms.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public PieChart NumberofFarms(string type, Guid? id)
        {
            List<TotalFarms> Farms = new List<TotalFarms>();
            switch (type)
            {
                case "Country":
                    Farms = uow.ExecuteQuery<TotalFarms>("Overview_defectsByCountry @CountryId", new SqlParameter("CountryId", id)).ToList();
                    break;
                case "Supplier":
                    Farms = uow.ExecuteQuery<TotalFarms>("Overview_defectsBySupplier @SupplierId", new SqlParameter("SupplierId", id)).ToList();
                    break;
                case "SupplyChain":
                    Farms = uow.ExecuteQuery<TotalFarms>("Overview_FarmsBySupplyChain @SupplyChainId", new SqlParameter("SupplyChainId", id)).ToList();
                    break;
                case "Cooperative":
                    Farms = uow.ExecuteQuery<TotalFarms>("Overview_FarmsByCooperative @CooperativeId", new SqlParameter("CooperativeId", id)).ToList();
                    break;
            }

            var chart = new PieChart();
            var data = new PieSerieItem();
            chart.Title = "Number of Farms";

            List<List<object>> data1 = new List<List<object>>();
            foreach (var item1 in Farms)
            {
                data1.Add(new List<object> { item1.Cooperative, item1.NumberOfFarms });
            }

            data.data = data1;

            chart.Items.Add(data);

            return chart;
        }

        /// <summary>
        /// Comertials the sales1.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart ComertialSales1()
        {
	  var affectedRows1 = uow.ExecuteQuery<Result>("Comertial_Sales");

	  var chart = new PieChart();
	  var data = new PieSerieItem();
	  chart.Title = "% of kg by Cooperative";

	  List<List<object>> data1 = new List<List<object>>();

	  foreach (var item1 in affectedRows1)
	      data1.Add(new List<object> { item1.Code, item1.Total });

	  data.data = data1;
	  chart.Items.Add(data);

	  return chart;
        }

        /// <summary>
        /// Overviews the sales volume.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ColumnChart OverviewSalesVolume(DateTime startDate, DateTime endDate)
        {
            var invoices1 = uow.ExecuteQuery<Result>("Overview_SalesVolume @firstDate, @endDate", new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();
            var invoices = invoices1.ToList();
            var chart = new ColumnChart();
            chart.Categories.Add("Sales");
            chart.Title = "Sales Volume";
            chart.YTitle = "Sales";

            foreach (var invoice in invoices)
            {
                var item = new ColumnSerieItem();
                item.name = invoice.Code;
                item.data.Add(invoice.Total);
                chart.Items.Add(item);
            }

            return chart;
        }

        /// <summary>
        /// Overviews the sales volume.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        public ColumnChart OverviewSalesVolume(string type, Guid? id, DateTime startDate, DateTime endDate)
        {
            string parameterName = "@" + type + "Id";
            
            var idParam = new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = System.Data.SqlDbType.NVarChar,
                Value = id.ToString()
            };
            List<Result> invoices = new List<Result>();
            switch (type)
            {
                case "Country":
                    invoices = uow.ExecuteQuery<Result>("Overview_SalesVolumeByCountry @CountryId, @firstDate, @endDate", idParam, new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();
                    break;
                case "Supplier":
                    invoices = uow.ExecuteQuery<Result>("Overview_SalesVolumeBySupplier @supplierId, @firstDate, @endDate", idParam, new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();
                    break;
                case "SupplierChain":
                    invoices = uow.ExecuteQuery<Result>("Overview_SalesVolumeBySupplierChain @SupplierChainId, @firstDate, @endDate", idParam, new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();
                    break;
                case "Cooperative":
                    invoices = uow.ExecuteQuery<Result>("Overview_SalesVolumeByCooperative @CooperativeId, @firstDate, @endDate", idParam, new SqlParameter("firstDate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("endDate", endDate.ToString("yyyy-MM-dd"))).ToList();
                    break;
                default:
                    break;
            }

            //List<string> Cooperatives = new List<string>();
            var chart = new ColumnChart();
            chart.Categories.Add("Sales");
            chart.Title = "Sales Volume";
            chart.YTitle = "Sales";

            foreach (var invoice in invoices)
            {
                var item = new ColumnSerieItem();
                item.name = invoice.Code;
                item.data.Add(invoice.Total);
                chart.Items.Add(item);
                //Cooperatives.Add(invoice.Code);
            }
            //chart.Categories = Cooperatives.ToList();

            return chart;
        }

        /// <summary>
        /// Comertials the sales2.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public PieChart ComertialSales2()
        {
	  var affectedRows1 = uow.ExecuteQuery<Result>("[Comertial_Sales_By_Farm]");

	  var chart = new PieChart();
	  var data = new PieSerieItem();
	  chart.Title = "Top 20 Farms";

	  List<List<object>> data1 = new List<List<object>>();
	  
	  foreach (var item1 in affectedRows1)	  
	      data1.Add(new List<object> { item1.Code , item1.Total });

	  data.data = data1;
	  chart.Items.Add(data);

	  return chart;
        }

        /// <summary>
        /// Comertials the sales3.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ColumnChart ComertialSales3()
        {
	  var invoices = uow.ExecuteQuery<Result>("Comertial_Sales_By_Farm");

	  var chart = new ColumnChart();
	  chart.Categories.Add("Sales");
	  chart.Title = "Sales by Farm";
	  chart.YTitle = "Total";

	  foreach (var invoice in invoices)
	  {
	      var item = new ColumnSerieItem();
	      item.name = invoice.Code;
	      item.data.Add(invoice.Total);
	      chart.Items.Add(item);
	  }

	  return chart;
        }

        /// <summary>
        /// 
        /// </summary>
        public class Result
        {
	  /// <summary>
	  /// Gets or sets the code.
	  /// </summary>
	  /// <value>
	  /// The code.
	  /// </value>
	  public string Code { get; set; }
	  /// <summary>
	  /// Gets or sets the total.
	  /// </summary>
	  /// <value>
	  /// The total.
	  /// </value>
	  public double Total { get; set; }
        }

        /// <summary>
        /// Get the total of Farms
        /// </summary>
        public class TotalFarms
        {
	  /// <summary>
	  /// Gets or sets the cooperative.
	  /// </summary>
	  /// <value>
	  /// The cooperative.
	  /// </value>
	  public string Cooperative { get; set; }
	  /// <summary>
	  /// Gets or sets the number of farms.
	  /// </summary>
	  /// <value>
	  /// The number of farms.
	  /// </value>
	  public int NumberOfFarms { get; set; }
        }

    }
}
