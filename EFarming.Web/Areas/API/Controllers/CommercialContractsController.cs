//using EFarming.Core.ComercialModule;
//using EFarming.Core.DashboardModule;
//using EFarming.Core.QualityModule.DashboardAggregate;
//using EFarming.DAL;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace EFarming.Web.Areas.API.Controllers
//{
//    /// <summary>
//    /// Controller for the Commercial activities
//    /// </summary>
//    public class CommercialContractsController : ApiController
//    {
//        private UnitOfWork db = new UnitOfWork();

//        /// <summary>
//        /// Chart for the Contracts (Volume By Quality)
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public ColumnChart VolumeByQuality()
//        {
//            var contracts = db.ExecuteQuery<Result>("Volume_By_Quality");
//            var groupedcontracts = contracts.GroupBy(c => c.Short).OrderBy(g => g.Key);
//            var months = contracts.GroupBy(q => q.ShipmentMonth).OrderBy(q => q.Key).Select(g => g.Key);

//            var chart = new ColumnChart { Title = "Volume by quality", YTitle = "Volume" };
//            chart.Categories = months.Select(q => q.ToString()).ToList();

//            foreach (var contract in groupedcontracts)
//            {
//                //chart.Categories.Add(contract.ShipmentMonth.ToString());
//                var item = new ColumnSerieItem();
//                item.name = contract.Key;

//                foreach (var month in months)
//                {
//                    var formonths = contract.Where(i => i.ShipmentMonth == month);
//                    item.data.Add(formonths.Sum(c => c.Volume));
//                }
//                chart.Items.Add(item);
//            }

//            return chart;
//        }

//        /// <summary>
//        /// Chart for the Contracts (Volume By Quality) filtered by Client
//        /// </summary>
//        /// <param name="ClientId">The client identifier.</param>
//        /// <returns></returns>
//        [HttpGet]
//        public ColumnChart VolumeByQualityByClient(string ClientId)
//        {
//            var chart = new ColumnChart { Title = "Volume by quality", YTitle = "Volume" };
//            try
//            {
//                var ClientParam = new SqlParameter
//                {
//                    ParameterName = "@ClientId",
//                    SqlDbType = System.Data.SqlDbType.NVarChar,
//                    Value = ClientId
//                };
//                var contracts = db.ExecuteQuery<VolumeByQuality>("Volume_By_Quality_By_Client @ClientId", ClientParam).ToList();
//                //var contracts = db.ExecuteQuery<VolumeByQuality>(Sqlstring);
//                var groupedcontracts = contracts.GroupBy(c => c.Short).OrderBy(g => g.Key);
//                var months = contracts.GroupBy(q => q.ShipmentMonth).OrderBy(a => a.Key).Select(b => b.Key);
                
//                chart.Categories = months.Select(m => m.ToString()).ToList();
//                foreach (var contract1 in groupedcontracts)
//                {
//                    //chart.Categories.Add(contract.ShipmentMonth.ToString());
//                    var item = new ColumnSerieItem();
//                    item.name = contract1.Key;

//                    foreach (var month in months)
//                    {
//                        var formonths = contract1.Where(i => i.ShipmentMonth == month);
//                        item.data.Add(formonths.Sum(c => c.Volume));
//                    }
//                    chart.Items.Add(item);
//                }
//            }
//            catch (Exception ex)
//            {

//            }
//            return chart;
//        }

//        /// <summary>
//        /// Entity for the result
//        /// </summary>
//        public class Result
//        {
//            /// <summary>
//            /// Gets or sets the code.
//            /// </summary>
//            /// <value>
//            /// The code.
//            /// </value>
//            public int ShipmentMonth { get; set; }
//            /// <summary>
//            /// Gets or sets the total.
//            /// </summary>
//            /// <value>
//            /// The total.
//            /// </value>
//            public int Volume { get; set; }
//            /// <summary>
//            /// Gets or sets the quality.
//            /// </summary>
//            /// <value>
//            /// The quality.
//            /// </value>
//            public string Short { get; set; }
//        }
//    }
//}
