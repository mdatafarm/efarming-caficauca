using EFarming.Core.DashboardModule.BasicColumn;
using EFarming.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// Dashboard Controller API
    [RoutePrefix("api/DashboardFarm")]
    public class DashboardFarmController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();
        #region Dashboard By Modules
        /// <summary>
        /// Overviews the criteria report by module.
        /// </summary>
        /// <param name="id">Id Farm.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [Route("ByModule")]
        [HttpGet]
        [ActionName("ByModule")]
        public string ByModule(string id, DateTime startDate, DateTime endDate)
        {
            //var affectedRows1 = db.ExecuteQuery<ResultColumnChart>("AssessmentCoocentralModule @AsId, @ModuName, @stardate, @enddate, @Department, @Municipality, @Village", new SqlParameter("AsId", id), new SqlParameter("ModuName", moduname), new SqlParameter("stardate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("enddate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("Department", dep), new SqlParameter("Municipality", mun), new SqlParameter("Village", vill)).ToList();

            //var affectedRows1 = db.ExecuteQuery<ResultColumnChart>("DashboardFarmModule @IdFarm,@stardate,@enddate", new SqlParameter("IdFarm", id), new SqlParameter("stardate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("enddate", endDate.ToString("yyyy-MM-dd"))).ToList();
            var sd= startDate.ToString("yyyy-MM-dd");
            var ed= endDate.ToString("yyyy-MM-dd");

            var affectedRows1 = db.ExecuteQuery<ResultColumnChart>("DashboardFarmModule @IdFarm, @stardate, @enddate", new SqlParameter("IdFarm", id), new SqlParameter("stardate", startDate), new SqlParameter("enddate", endDate)).ToList();

            var chart = new BasicColumn(false);

            chart.title = new title() { text = "Overview by module" };

            chart.subtitle = new subtitle() { text = "" };

            chart.xAxis = new xAxis() { categories = affectedRows1.OrderByDescending(x=> x.Cantidad).Distinct().Select(x => x.Name).ToList().Distinct().ToList(), crosshair = true };

            chart.yAxis = new yAxis() { min = 0, title = new title() { text = "quantity" }  };

            List<double> listsi = new List<double>();
            List<double> listno = new List<double>();
            foreach (var item in chart.xAxis.categories)
            {
                int cantyes = 0;
                int cantno = 0;
                //Los valores deben corresponder con la posición de cada categoria
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "True" || x.Value == "CUMPLE")).FirstOrDefault() != null)
                    cantyes = affectedRows1.Where(x => x.Name == item && (x.Value == "True" || x.Value == "CUMPLE")).FirstOrDefault().Cantidad;
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault() != null)
                    cantno = affectedRows1.Where(x => x.Name == item && (x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault().Cantidad;
                listsi.Add(cantyes);
                listno.Add(cantno);
            }

            chart.series.Add(new series() { name = " SI CUMPLE", data = listsi });
            chart.series.Add(new series() { name = " NO CUMPLE", data = listno });

            return JsonConvert.SerializeObject(chart);
        }
        #endregion
        #region Dashboard By SubModules
        /// <summary>
        /// Overviews the criteria report by Submodule.
        /// </summary>
        /// <param name="farmId">Id Farm.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="Id">So that the system differentiates the functions.</param>
        /// <returns></returns>
        
        [Route("BySubModule")]
        [HttpGet]
        [ActionName("BySubModule")]
        public string BySubModule( DateTime startDate, DateTime endDate, string farmId, int aux)
        {
            var affectedRows1 = db.ExecuteQuery<ResultColumnChart>("DashboardFarmSubModule @IdFarm, @stardate, @enddate", new SqlParameter("IdFarm", farmId), new SqlParameter("stardate", startDate), new SqlParameter("enddate", endDate)).ToList();
            
            var chart = new BasicColumn(false);

            chart.title = new title() { text = "Overview by submodule" };

            chart.subtitle = new subtitle() { text = "" };

            chart.xAxis = new xAxis() { categories = affectedRows1.OrderByDescending(x => x.Cantidad).Distinct().Select(x => x.Name).ToList().Distinct().ToList(), crosshair = true };

            chart.yAxis = new yAxis() { min = 0, title = new title() { text = "quantity" } };

            List<double> listsi = new List<double>();
            List<double> listno = new List<double>();
            foreach (var item in chart.xAxis.categories)
            {
                int cantyes = 0;
                int cantno = 0;
                //Los valores deben corresponder con la posición de cada categoria
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "True" || x.Value == "CUMPLE")).FirstOrDefault() != null)
                    cantyes = affectedRows1.Where(x => x.Name == item && (x.Value == "True" || x.Value == "CUMPLE")).FirstOrDefault().Cantidad;
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault() != null)
                    cantno = affectedRows1.Where(x => x.Name == item && (x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault().Cantidad;
                listsi.Add(cantyes);
                listno.Add(cantno);
            }

            chart.series.Add(new series() { name = " Si cumple", data = listsi });
            chart.series.Add(new series() { name = " No cumple", data = listno });

            return JsonConvert.SerializeObject(chart);
        }
        #endregion
        #region Dashboard By SubModules for year
        /// <summary>
        /// Overviews the criteria report by submodule for year.
        /// </summary>
        /// <param name="Timelineid">Id Farm.</param>
        /// <param name="IniDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [Route("TimelineBysubModule")]
        [HttpGet, ActionName("TimelineBysubModule")]
        public string TimelineBysubModule(string Timelineid, DateTime IniDate, DateTime endDate)
        {
            var affectedRows1 = db.ExecuteQuery<ResultColumnChartTimeline>("TimeLineFarmSubModule @IdFarm, @stardate, @enddate", new SqlParameter("IdFarm", Timelineid),  new SqlParameter("stardate", IniDate), new SqlParameter("enddate", endDate)).ToList();

            var chart = new BasicColumn(true);

            chart.title = new title() { text = "Timeline by submodule" };

            chart.subtitle = new subtitle() { text = "" };

            chart.xAxis = new xAxis() { categories = affectedRows1.OrderByDescending(x => x.Cantidad).Distinct().Select(x => x.Name).ToList().Distinct().ToList(), crosshair = true };

            chart.yAxis = new yAxis() { min = 0, title = new title() { text = "percentage" } };

            List<int> listyear = new List<int>();

            listyear = affectedRows1.Distinct().Select(x => x.DateYear).ToList().Distinct().ToList();

            List<double> listyes = new List<double>();
            foreach (var year in listyear)
            {
                listyes = new List<double>();
                foreach (var item in chart.xAxis.categories)
                {
                    int cantyes = 0;
                    int cantno = 0;
                    //Los valores deben corresponder con la posición de cada categoria
                    if (affectedRows1.Where(x => x.Name == item && (x.Value == "True" || x.Value == "CUMPLE") && x.DateYear == year).FirstOrDefault() != null)
                        cantyes = affectedRows1.Where(x => x.Name == item && (x.Value == "True" || x.Value == "CUMPLE") && x.DateYear == year).FirstOrDefault().Cantidad;
                    if (affectedRows1.Where(x => x.Name == item && (x.Value == "False" || x.Value == "NO CUMPLE") && x.DateYear == year).FirstOrDefault() != null)
                        cantno = affectedRows1.Where(x => x.Name == item && (x.Value == "False" || x.Value == "NO CUMPLE") && x.DateYear == year).FirstOrDefault().Cantidad;
                    double porcentajeyes = 0;
                    if ((cantyes + cantno) > 0)
                        porcentajeyes = (cantyes * 100) / (cantyes + cantno);
                    listyes.Add(porcentajeyes);
                }
                //Se agregan las series por cada año
                chart.series.Add(new series() { name = year.ToString(), data = listyes });
            }

            return JsonConvert.SerializeObject(chart);
        }
        #endregion
        #region Dashboard By SubModules GAP
        /// <summary>
        /// Overviews the criteria report by Submodule.
        /// </summary>
        /// <param name="GAPId">Id Farm.</param>
        /// <param name="InitialDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        [Route("BySubModuleGAP")]
        [HttpGet, ActionName("BySubModuleGAP")]
        public string BySubModuleGAP(DateTime InitialDate, DateTime endDate, string GAPId)
        {
            var affectedRows1 = db.ExecuteQuery<ResultColumnChart>("DashboardFarmGAP @IdFarm, @stardate, @enddate", new SqlParameter("stardate", InitialDate), new SqlParameter("enddate", endDate), new SqlParameter("IdFarm", GAPId)).ToList();

            var chart = new BasicColumn(true);

            chart.title = new title() { text = "GAP by submodule" };

            chart.subtitle = new subtitle() { text = "" };

            chart.xAxis = new xAxis() { categories = affectedRows1.OrderByDescending(x => x.Cantidad).Distinct().Select(x => x.Name).ToList().Distinct().ToList(), crosshair = true };

            chart.yAxis = new yAxis() { min = 0, title = new title() { text = "percentage" } };

            List<double> listno = new List<double>();
            foreach (var item in chart.xAxis.categories)
            {
                int cantno = 0;
                //Los valores deben corresponder con la posición de cada categoria
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault() != null)
                    cantno = affectedRows1.Where(x => x.Name == item && (x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault().Cantidad;
                listno.Add(cantno);
            }

            chart.series.Add(new series() { name = " NO CUMPLE", data = listno });

            return JsonConvert.SerializeObject(chart);
        }
        #endregion
    }
    public class ResultColumnChart
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Cantidad { get; set; }
    }
    public class ResultColumnChartTimeline
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Cantidad { get; set; }
        public int DateYear { get; set; }
    }
}
