using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.DAL;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EFarming.Core.DashboardModule.BasicColumn;
using Newtonsoft.Json;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// Farm Controller for the API
    /// </summary>
    /// 
    [RoutePrefix("api/Farms")]
    
    public class FarmsController : ApiController
    {
        private IFarmManager _farmManager;
        private UnitOfWork db = new UnitOfWork();

        /// <summary>
        /// Initializes a new instance of the <see cref="FarmsController"/> class.
        /// </summary>
        /// <param name="farmManager">The farm manager.</param>
        public FarmsController(FarmManager farmManager)
        {
            _farmManager = farmManager;
        }

        /// <summary>
        /// Farms
        /// </summary>
        public class Farms
        {
	  /// <summary>
	  /// Gets or sets the identifier.
	  /// </summary>
	  /// <value>
	  /// The identifier.
	  /// </value>
	  public Guid Id { get; set; }
	  /// <summary>
	  /// Gets or sets the code.
	  /// </summary>
	  /// <value>
	  /// The code.
	  /// </value>
            public string Code { get; set; }
	  /// <summary>
	  /// Gets or sets the name.
	  /// </summary>
	  /// <value>
	  /// The name.
	  /// </value>
            public string Name { get; set; }

            public string FarmerName { get; set; }
            public string FarmerLastName { get; set; }


        }

        /// <summary>
        /// Indexes the specified current department.
        /// </summary>
        /// <param name="currentDepartment">The current department.</param>
        /// <param name="currentMunicipality">The current municipality.</param>
        /// <param name="currentVillage">The current village.</param>
        /// <param name="farmerName">Name of the farmer.</param>
        /// <param name="farmName">Name of the farm.</param>
        /// <param name="farmCode">The farm code.</param>
        /// <param name="farmerIdentification">The farmer identification.</param>
        /// <returns></returns>
        [HttpGet]
        public List<Farms> Index(string currentDepartment, string currentMunicipality, string currentVillage, string farmerName, string farmName, string farmCode, string farmerIdentification)
        {
            var villageId = string.IsNullOrEmpty(currentVillage) ? Guid.Empty : Guid.Parse(currentVillage);
            var municipalityId = string.IsNullOrEmpty(currentMunicipality) ? Guid.Empty : Guid.Parse(currentMunicipality);
            var departmentId = string.IsNullOrEmpty(currentDepartment) ? Guid.Empty : Guid.Parse(currentDepartment);

            var farms = _farmManager
                .GetAllQueryable(FarmSpecification.FilterWithFarmerInfo(farmCode, farmName, Guid.Empty, Guid.Empty, Guid.Empty, villageId, municipalityId, departmentId, farmerName),
                                        f => f.Code);
           
           var UserId = HttpContext.Current.User as CustomPrincipal;
           var UserList = db.Users.Where(u => u.Id == UserId.UserId).FirstOrDefault();
           var ListProjectsUser = UserList.Projects.ToList();

            var SelectedFarms = new List<Farms>();
            var ListFarm = new List<Farms>();
            if (ListProjectsUser.Count() > 0)
            {
                foreach (var farm in farms)
                {
                    foreach (var farmProject in farm.Projects)
                    {
                        foreach (var projectsUser in ListProjectsUser)
                        {
                            if (farmProject.Id == projectsUser.Id)
                            {
                                ListFarm.Add(new Farms
                                {
                                    Id = farm.Id,
                                    Code = farm.Code,
                                    Name = farm.Name,
                                    FarmerName = farm.FamilyUnitMembers.Select(x => x.FirstName).FirstOrDefault(),
                                    FarmerLastName = farm.FamilyUnitMembers.Select(x => x.LastName).FirstOrDefault()
                                });
                            }
                        }
                    }
                }
                var FarmsSelect = ListFarm.GroupBy(g => g.Id).Select(s => new { Id = s.Key, Code = s.FirstOrDefault().Code, Name = s.FirstOrDefault().Name }).ToList();
                foreach(var item in FarmsSelect)
                {
                    SelectedFarms.Add(new Farms
                    {
                        Id = item.Id,
                        Code = item.Code,
                        Name = item.Name
                    });
                }
            }
            else
            {
                foreach (var farm in farms)
                {
                    Farms frm = new Farms();

                    frm.Id = farm.Id;
                    frm.Code = farm.Code;
                    frm.Name = farm.Name;
                    var fami = db.FamilyUnitMembers.Where(x=> x.FarmId == farm.Id && x.IsOwner == true).ToList();
                    if (fami.Count == 1)
                    {
                        frm.FarmerName = fami.Select(x => x.FirstName).FirstOrDefault() + " ";
                        frm.FarmerLastName = fami.Select(x => x.LastName).FirstOrDefault();
                    }
                    else {
                        frm.FarmerName = "";
                        frm.FarmerLastName = "";
                    }
                    SelectedFarms.Add(frm);
                }
            }
            return SelectedFarms;
        }
        #region Dashboard By Modules
        /// <summary>
        /// Overviews the criteria report by module.
        /// </summary>

        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="id">Id Farm.</param>
        /// <param name="moduname">Name of module.</param>
        /// <param name="Department">Department.</param>
        /// <param name="Municipality">Department.</param>
        /// <param name="Village">Department.</param>
        /// <returns></returns>
        //[Route("ByModule")]
        //[HttpGet, ActionName("ByModule")]
        //[Authorize]
        //[Route("ByModule")]

        //[ActionName("ByModule")]
        [Route("ByModule")]
        [HttpGet]
        [ActionName("ByModule")]
        //[Route("ByModule")]
        public string ByModule(DateTime startDate, DateTime endDate,string id, string Department, string Municipality, string Village)
        {
            var sd = startDate.ToString("yyyy-MM-dd");
            //var affectedRows1 = db.ExecuteQuery<ResultColumnChart>("AssessmentCoocentralModule @stardate, @enddate, @Department, @Municipality, @Village", new SqlParameter("stardate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("enddate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("Department", this.stringnull(Department)), new SqlParameter("Municipality", this.stringnull(Municipality)), new SqlParameter("Village", this.stringnull(Village))).ToList();
            var dep = this.stringnull(Department);
            var mun = this.stringnull(Municipality);
            var vill = this.stringnull(Village);

             var affectedRows1 = db.ExecuteQuery<ResultColumnChart>("AssessmentCoocentralModule @AsId, @stardate, @enddate, @Department, @Municipality, @Village", new SqlParameter("AsId", id), new SqlParameter("stardate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("enddate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("Department", dep), new SqlParameter("Municipality", mun), new SqlParameter("Village", vill)).ToList();

            var chart = new BasicColumn(false);

            chart.title = new title() { text = "Overview by modules "};

            chart.subtitle = new subtitle() { text = "" };

            chart.xAxis = new xAxis() { categories = affectedRows1.OrderByDescending(x => x.Cantidad).Distinct().Select(x => x.Name).ToList().Distinct().ToList(), crosshair = true };

            chart.yAxis = new yAxis() { min = 0, title = new title() { text = "quantity" } };

           
            List<double> listSi = new List<double>();
            List<double> listNo = new List<double>();
            

            foreach (var item in chart.xAxis.categories)
            {
                int cantSi = 0, cantNo = 0;
                //Los valores deben corresponder con la posición de cada categoria
                
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "Si" || x.Value == "True" || x.Value == "CUMPLE")).FirstOrDefault() != null)
                {
                    cantSi = affectedRows1.Where(x => x.Name == item && (x.Value == "Si" || x.Value == "True" || x.Value == "CUMPLE")).FirstOrDefault().Cantidad;
                }
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "No" || x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault() != null)
                {
                    cantNo = affectedRows1.Where(x => x.Name == item && (x.Value == "No" || x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault().Cantidad;
                }
                
                listSi.Add(cantSi);
                listNo.Add(cantNo);
                
            }
            //Modulos encuesta coocentral
            
                    chart.series.Add(new series() { name = "Sí", data = listSi });
                    chart.series.Add(new series() { name = "No", data = listNo });
                   
               
            
            return JsonConvert.SerializeObject(chart);
        }
        #endregion


        #region Dashboard By SubModules
        /// <summary>
        /// Overviews the criteria report by Submodule.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="id">Id Farm.</param>
        /// <param name="moduname">Name of module.</param>
        /// <param name="Department">Department.</param>
        /// <param name="Municipality">Department.</param>
        /// <param name="Village">Department.</param>
        /// <param name="aux">Department.</param>
        /// <returns></returns>
        /// 

        [Route("BySubModule")]
        [HttpGet]
        [ActionName("BySubModule")]
        public string BySubModule(DateTime startDate, DateTime endDate, string id, string Department, string Municipality, string Village, int aux)
        { 
            var dep = this.stringnull(Department);
            var mun = this.stringnull(Municipality);
            var vill = this.stringnull(Village);
            var affectedRows1 = db.ExecuteQuery<ResultColumnChart>("AssessmentCoocentralSubModule @AsId, @stardate, @enddate, @Department, @Municipality, @Village", new SqlParameter("AsId", id),  new SqlParameter("stardate", startDate.ToString("yyyy-MM-dd")), new SqlParameter("enddate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("Department", dep), new SqlParameter("Municipality", mun), new SqlParameter("Village", vill)).ToList();

            var chart = new BasicColumn(false);

            chart.title = new title() { text = "Submodules"};

            chart.subtitle = new subtitle() { text = "" };

            chart.xAxis = new xAxis() { categories = affectedRows1.OrderByDescending(x => x.Cantidad).Distinct().Select(x => x.Name).ToList().Distinct().ToList(), crosshair = true };

            chart.yAxis = new yAxis() { min = 0, title = new title() { text = "quantity" } };

            List<double> listSi = new List<double>();
            List<double> listNo = new List<double>();
           
            foreach (var item in chart.xAxis.categories)
            {
                int cantSi = 0, cantNo = 0;
                //Los valores deben corresponder con la posición de cada categoria
               
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "Si" || x.Value == "True" || x.Value == "CUMPLE")).FirstOrDefault() != null)
                {
                    cantSi = affectedRows1.Where(x => x.Name == item && (x.Value == "Si" || x.Value == "True" || x.Value == "CUMPLE")).FirstOrDefault().Cantidad;
                }
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "No" || x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault() != null)
                {
                    cantNo = affectedRows1.Where(x => x.Name == item && (x.Value == "No" || x.Value == "False" || x.Value == "NO CUMPLE")).FirstOrDefault().Cantidad;
                }
                listSi.Add(cantSi);
                listNo.Add(cantNo);
                
            }
           
                    chart.series.Add(new series() { name = "Sí", data = listSi });
                    chart.series.Add(new series() { name = "No", data = listNo });
                   

            return JsonConvert.SerializeObject(chart);

        }
        #endregion


        #region Dashboard By TimeLineSubModules for year
        /// <summary>
        /// Overviews the criteria report by submodule for year.
        /// </summary>
        /// <param name = "Asid" > Id Farm.</param>
        /// <param name = "moduname" > Id Farm.</param>
        /// <param name = "Department" > Id Farm.</param>
        /// <param name = "Municipality" > Id Farm.</param>
        /// <param name = "Village" > Id Farm.</param>
        /// <param name = "IniDate" > The start date.</param>
        /// <param name = "EndDate" > The end date.</param>
        /// <returns></returns>
        //string moduname,
        [Route("TimelineBysubModule")]
        [HttpGet]
        [ActionName("TimelineBysubModule")]
        public string TimelineBysubModule(string Asid,  string Department, string Municipality, string Village, DateTime IniDate, DateTime EndDate)
        {
            var dep = this.stringnull(Department);
            var mun = this.stringnull(Municipality);
            var vill = this.stringnull(Village);
            var affectedRows1 = db.ExecuteQuery<ResultColumnChartTimeline>("AssessmentCoocentralSubModuleTimeLine @AsId, @stardate, @enddate, @Department, @Municipality, @Village", new SqlParameter("AsId", Asid), new SqlParameter("stardate", IniDate.ToString("yyyy-MM-dd")), new SqlParameter("enddate", EndDate.ToString("yyyy-MM-dd")), new SqlParameter("Department", dep), new SqlParameter("Municipality", mun), new SqlParameter("Village", vill)).ToList();

            //return "";
            var chart = new BasicColumn(true);

            chart.title = new title() { text = "Timeline by submodules" };

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
                    if (affectedRows1.Where(x => x.Name == item && (x.Value == "Si"|| x.Value == "CUMPLE") && x.DateYear == year).FirstOrDefault() != null)
                        cantyes = affectedRows1.Where(x => x.Name == item && (x.Value == "Si" || x.Value == "CUMPLE") && x.DateYear == year).FirstOrDefault().Cantidad;

                    if (affectedRows1.Where(x => x.Name == item && (x.Value == "No" || x.Value == "NO CUMPLE") && x.DateYear == year).FirstOrDefault() != null)
                        cantno = affectedRows1.Where(x => x.Name == item && (x.Value == "No" || x.Value == "NO CUMPLE") && x.DateYear == year).FirstOrDefault().Cantidad;

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
        /// <param name="moduname">Id Farm.</param>
        /// <param name="InitialDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="Department">The end date.</param>
        /// <param name="Municipality">The end date.</param>
        /// <param name="Village">The end date.</param>
        /// <returns></returns>
        /// string moduname,
        [Route("BySubModuleGAP")]
        [HttpGet, ActionName("BySubModuleGAP")]
        public string BySubModuleGAP(string GAPId,  DateTime InitialDate, DateTime endDate, string Department, string Municipality, string Village)
        {
            var dep = this.stringnull(Department);
            var mun = this.stringnull(Municipality);
            var vill = this.stringnull(Village);
            var affectedRows1 = db.ExecuteQuery<ResultColumnChart>("AssessmentCoocentralGAP @AsId, @stardate, @enddate, @Department, @Municipality, @Village", new SqlParameter("AsId", GAPId), new SqlParameter("stardate", InitialDate.ToString("yyyy-MM-dd")), new SqlParameter("enddate", endDate.ToString("yyyy-MM-dd")), new SqlParameter("Department", dep), new SqlParameter("Municipality", mun), new SqlParameter("Village", vill)).ToList();

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
                if (affectedRows1.Where(x => x.Name == item && (x.Value == "No" || x.Value == "NO CUMPLE")).FirstOrDefault() != null)
                    cantno = affectedRows1.Where(x => x.Name == item && (x.Value == "No" || x.Value == "NO CUMPLE")).FirstOrDefault().Cantidad;
                listno.Add(cantno);
            }

            chart.series.Add(new series() { name = "No", data = listno });

            return JsonConvert.SerializeObject(chart);
        }
        #endregion
        private string stringnull(string variable)
        {
            if (string.IsNullOrEmpty(variable)||variable.Equals("null") || variable.Equals("0"))
                return "";
            else
                return variable;
        }
    }
}
