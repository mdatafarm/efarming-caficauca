using EFarming.Core.FarmModule.FarmAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.DAL;
using EFarming.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    [CustomAuthorize(Roles = "Admin,Reader")]
    public class TechnicianReportsController : Controller
    {
        private UnitOfWork db = new UnitOfWork();
        // GET: TechnicianReports
        public ActionResult Index(Guid? ProjectId, DateTime? start, DateTime? end)
        {
            List<ResumeElement> FarmsInformation = new List<ResumeElement>();
            var User = HttpContext.User as CustomPrincipal;
            GetDates(ref start, ref end);
            List<Project> ListProjects = new List<Project>();
            var Projects = db.Projects.Where(d => d.DeletedAt == null).ToList();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();

            foreach (var projectUser in ListProjectsUser)
            {
                foreach (var project in Projects)
                {
                    if (project.Id == projectUser.Id)
                    {
                        ListProjects.Add(project);
                    }
                }
            }
            if (ListProjects.Count() == 0)
            {
                Guid? userId = null;
                ViewBag.Projects = db.Projects.Where(d => d.DeletedAt == null).ToList();
                ViewBag.ProjectId = ProjectId;
                FarmsInformation = GetFarmsInformation(ProjectId, start, end, userId);               
            }
            else
            {
                Guid? userId = User.UserId;
                ViewBag.Projects = ListProjects;
                ViewBag.ProjectId = ProjectId;
                FarmsInformation = GetFarmsInformation(ProjectId, start, end, userId);
            }
            //Grouping the information to be showed
            var Grouped = FarmsInformation.GroupBy(t => t.TechnicianName)
                .Select(fi => new GroupedInformation
                {
                    TechnicianName = fi.First().TechnicianName,
                    ContactsNumber = fi.Sum(c => c.ContactsQuantity),
                    ContactsByFarm = fi.Sum(c => c.ContactsByFarm),
                    CoffeeArea = fi.Sum(ca => ca.CoffeeArea),
                    EstimatedProduction = fi.Sum(ep => ep.EstimatedProduction),
                    Saleskg = fi.Sum(skg => skg.Saleskg),
                    SalesValue = fi.Sum(sv => sv.SalesValue),
                    FertilizersQuantity = fi.Sum(fq => fq.FertilizersQuantity),
                        //FertilizersValue = fi.Sum(fv => fv.FertilizersValue),
                        NumberOfFarms = fi.Count(),
                    Percentage = (fi.Sum(c => c.ContactsByFarm) * 1.0 / fi.Count() * 1.0) * 100.0
                });
            Grouped = Grouped.OrderByDescending(p => p.Percentage);
            return View(Grouped);
        }

        // GET: TechnicianReports/EstimatedProduction
        public ActionResult EstimatedProduction(Guid? ProjectId, DateTime? start, DateTime? end)
        {
            List<ResumeElement> FarmsInformation = new List<ResumeElement>();
            var User = HttpContext.User as CustomPrincipal;
            GetDates(ref start, ref end);
            List<Project> ListProjects = new List<Project>();
            var Projects = db.Projects.Where(d => d.DeletedAt == null).ToList();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();

            foreach (var projectUser in ListProjectsUser)
            {
                foreach (var project in Projects)
                {
                    if (project.Id == projectUser.Id)
                    {
                        ListProjects.Add(project);
                    }
                }
            }

            if (ListProjects.Count() == 0)
            {
                Guid? userId = null;
                ViewBag.Projects = db.Projects.Where(d => d.DeletedAt == null).ToList();
                ViewBag.ProjectId = ProjectId;
                FarmsInformation = GetFarmsInformation(ProjectId, start, end, userId);
            }
            else
            {
                Guid? userId = User.UserId;
                ViewBag.Projects = ListProjects;
                ViewBag.ProjectId = ProjectId;
                FarmsInformation = GetFarmsInformation(ProjectId, start, end, userId);
            }
            //Grouping the information to be showed
            var Grouped = FarmsInformation.GroupBy(t => t.TechnicianName)
                .Select(fi => new GroupedInformation
                {
                    TechnicianName = fi.First().TechnicianName,
                    ContactsNumber = fi.Sum(c => c.ContactsQuantity),
                    ContactsByFarm = fi.Sum(c => c.ContactsByFarm),
                    CoffeeArea = fi.Sum(ca => ca.CoffeeArea),
                    EstimatedProduction = fi.Sum(ep => ep.EstimatedProduction),
                    Saleskg = fi.Sum(skg => skg.Saleskg),
                    SalesValue = fi.Sum(sv => sv.SalesValue),
                    FertilizersQuantity = fi.Sum(fq => fq.FertilizersQuantity),
                        //FertilizersValue = fi.Sum(fv => fv.FertilizersValue),
                        NumberOfFarms = fi.Count(),
                    CoffeeAreaSalesKg = (fi.Sum(skg => skg.Saleskg) * 1.0 / fi.Sum(ca => ca.CoffeeArea)),
                    by125kg = (fi.Sum(skg => skg.Saleskg) * 1.0 / fi.Sum(ca => ca.CoffeeArea)) / 125,
                    Percentage = (fi.Sum(skg => skg.Saleskg) * 1.0 / fi.Sum(ep => ep.EstimatedProduction) * 1.0) * 100.0
                });
            Grouped = Grouped.OrderByDescending(p => p.Percentage);
            return View(Grouped);
        }

        // GET: TechnicianReports/Fertilizers
        public ActionResult Fertilizers(Guid? ProjectId, DateTime? start, DateTime? end)
        {
            List<ResumeElement> FarmsInformation = new List<ResumeElement>();
            var User = HttpContext.User as CustomPrincipal;
            GetDates(ref start, ref end);
            List<Project> ListProjects = new List<Project>();
            var Projects = db.Projects.Where(d => d.DeletedAt == null).ToList();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();

            foreach (var projectUser in ListProjectsUser)
            {
                foreach (var project in Projects)
                {
                    if (project.Id == projectUser.Id)
                    {
                        ListProjects.Add(project);
                    }
                }
            }

            if (ListProjects.Count() == 0)
            {
                Guid? userId = null;
                ViewBag.Projects = db.Projects.Where(d => d.DeletedAt == null).ToList();
                ViewBag.ProjectId = ProjectId;
                FarmsInformation = GetFarmsInformation(ProjectId, start, end, userId);
            }
            else
            {
                Guid? userId = User.UserId;
                ViewBag.Projects = ListProjects;
                ViewBag.ProjectId = ProjectId;
                FarmsInformation = GetFarmsInformation(ProjectId, start, end, userId);
            }
            //Grouping the information to be showed
            var Grouped = FarmsInformation.GroupBy(t => t.TechnicianName)
                .Select(fi => new GroupedInformation
                {
                    TechnicianName = fi.First().TechnicianName,
                    ContactsNumber = fi.Sum(c => c.ContactsQuantity),
                    ContactsByFarm = fi.Sum(c => c.ContactsByFarm),
                    CoffeeArea = fi.Sum(ca => ca.CoffeeArea),
                    EstimatedProduction = fi.Sum(ep => ep.EstimatedProduction),
                    Saleskg = fi.Sum(skg => skg.Saleskg),
                    SalesValue = fi.Sum(sv => sv.SalesValue),
                    FertilizersQuantity = fi.Sum(fq => fq.FertilizersQuantity),
                        //FertilizersValue =  fi.Sum(fv => fv.FertilizersValue),
                        NumberOfFarms = fi.Count(),
                    CoffeeAreaFertilizersQuantity = (fi.Sum(fq => fq.FertilizersQuantity) / fi.Sum(ca => ca.CoffeeArea)),
                    Percentage = (fi.Sum(skg => skg.Saleskg) * 1.0 / fi.Sum(ep => ep.EstimatedProduction) * 1.0) * 100.0
                });
            Grouped = Grouped.OrderByDescending(p => p.Percentage);
            return View(Grouped);
        }

        // GET: TechnicianReports/GeneralStatus
        public ActionResult GeneralStatus(int? start)
        {
            if (start.Equals(null))
            {
                start = 0;
            }
            List<DetailElement> FarmsInformation = new List<DetailElement>();
            IEnumerable<DetailElement> GroupedInformation = new List<DetailElement>();
            var User = HttpContext.User as CustomPrincipal;
            List<Project> ListProjects = new List<Project>();
            var Projects = db.Projects.Where(d => d.DeletedAt == null).ToList();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();

            foreach (var projectUser in ListProjectsUser)
            {
                foreach (var project in Projects)
                {
                    if (project.Id == projectUser.Id)
                    {
                        ListProjects.Add(project);
                    }
                }
            }

            //if (start > 0)
            //{
                if (ListProjects.Count() == 0)
                {
                    FarmsInformation = db.ExecuteQuery<DetailElement>("GeneralStatus @firstYear", new SqlParameter("firstYear", start)).ToList();
                }
                else
                {
                    Guid? userId = User.UserId;
                    FarmsInformation = db.ExecuteQuery<DetailElement>("GeneralStatusByUserProject @firstYear,@userId", new SqlParameter("firstYear", start), new SqlParameter("userId", userId)).ToList();
                }

                GroupedInformation = FarmsInformation.GroupBy(a => a.Agency).Select(e => new DetailElement
                {
                    Agency = e.Key,
                    Municipality = e.Select(m => m.Municipality).First(),
                    Farms = e.Select(f => f.Farms).First(),
                    CoffeArea = e.Sum(ca => ca.CoffeArea),
                    Castillo = e.Sum(cas => cas.Castillo),
                    Colombia = e.Sum(col => col.Colombia),
                    Caturra = e.Sum(cat => cat.Caturra),
                    Tabi = e.Sum(tab => tab.Tabi),
                    Other = e.Sum(ot => ot.Other),
                    CeroDosAnios = e.Sum(ce => ce.CeroDosAnios),
                    DosCincoAnios = e.Sum(dos => dos.DosCincoAnios),
                    SeisNueveAnios = e.Sum(sei => sei.SeisNueveAnios),
                    MasNueveanios = e.Sum(nue => nue.MasNueveanios),
                    Estimativokg = e.Sum(est => est.Estimativokg),
                    Ventaskg = e.Sum(ven => ven.Ventaskg),
                    ConsumoSacos = e.Sum(con => con.ConsumoSacos)
                });

            if (FarmsInformation.Count() > 0)
            {
                var MunicipalityGroupedInformation = FarmsInformation.GroupBy(a => a.Municipality).Select(e => new DetailElement
                {
                    Agency = e.Key,
                    Municipality = e.Select(m => m.Municipality).First(),
                    Farms = GroupedInformation.Where(mun => mun.Municipality == e.Select(m => m.Municipality).First()).Sum(f => f.Farms),
                    CoffeArea = e.Sum(ca => ca.CoffeArea),
                    Castillo = e.Sum(cas => cas.Castillo),
                    Colombia = e.Sum(col => col.Colombia),
                    Caturra = e.Sum(cat => cat.Caturra),
                    Tabi = e.Sum(tab => tab.Tabi),
                    Other = e.Sum(ot => ot.Other),
                    CeroDosAnios = e.Sum(ce => ce.CeroDosAnios),
                    DosCincoAnios = e.Sum(dos => dos.DosCincoAnios),
                    SeisNueveAnios = e.Sum(sei => sei.SeisNueveAnios),
                    MasNueveanios = e.Sum(nue => nue.MasNueveanios),
                    Estimativokg = e.Sum(est => est.Estimativokg),
                    Ventaskg = e.Sum(ven => ven.Ventaskg),
                    ConsumoSacos = e.Sum(con => con.ConsumoSacos)
                });

                if (MunicipalityGroupedInformation.Count() > 0)
                {
                    var totalInformation = MunicipalityGroupedInformation.Select(t => new DetailElement
                    {
                        Farms = MunicipalityGroupedInformation.Sum(f => f.Farms),
                        CoffeArea = MunicipalityGroupedInformation.Sum(ca => ca.CoffeArea),
                        Castillo = MunicipalityGroupedInformation.Sum(cas => cas.Castillo),
                        Colombia = MunicipalityGroupedInformation.Sum(col => col.Colombia),
                        Caturra = MunicipalityGroupedInformation.Sum(cat => cat.Caturra),
                        Tabi = MunicipalityGroupedInformation.Sum(tab => tab.Tabi),
                        Other = MunicipalityGroupedInformation.Sum(ot => ot.Other),
                        CeroDosAnios = MunicipalityGroupedInformation.Sum(ce => ce.CeroDosAnios),
                        DosCincoAnios = MunicipalityGroupedInformation.Sum(dos => dos.DosCincoAnios),
                        SeisNueveAnios = MunicipalityGroupedInformation.Sum(sei => sei.SeisNueveAnios),
                        MasNueveanios = MunicipalityGroupedInformation.Sum(nue => nue.MasNueveanios),
                        Estimativokg = MunicipalityGroupedInformation.Sum(est => est.Estimativokg),
                        Ventaskg = MunicipalityGroupedInformation.Sum(ven => ven.Ventaskg),
                        ConsumoSacos = MunicipalityGroupedInformation.Sum(con => con.ConsumoSacos)
                    }).First();
                    ViewBag.totalInformation = totalInformation;
                }
                ViewBag.MunicipalityGroupedInformation = MunicipalityGroupedInformation;
            }
            
           
            if (start == 0)
            {
                ViewBag.year = string.Format("{0:yyyy}", DateTime.Now);
            }
           
            else
            {
                ViewBag.year = start;
            }
                
           // }

            return View(GroupedInformation.OrderBy(M => M.Municipality));
        }

        public void ToExcel(int? start)
        {
            DataTable _dataTable = new DataTable();
            var User = HttpContext.User as CustomPrincipal;
            List<Project> ListProjects = new List<Project>();
            var Projects = db.Projects.Where(d => d.DeletedAt == null).ToList();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();

            foreach (var projectUser in ListProjectsUser)
            {
                foreach (var project in Projects)
                {
                    if (project.Id == projectUser.Id)
                    {
                        ListProjects.Add(project);
                    }
                }
            }
            if (ListProjects.Count() == 0)
            {
                Guid? userId = null;
                _dataTable = GeneralStatusTable(start,userId);
            }
            else
            {
                Guid? userId = User.UserId;
                _dataTable = GeneralStatusTable(start,userId);
            }
            Response.AddHeader("content-disposition", "attachment; filename=Estatus general.xls");

            if (_dataTable == null)
                return;

            Encoding encoding = Encoding.UTF8;
            Response.Charset = encoding.EncodingName;
            Response.ContentEncoding = Encoding.Unicode;
            Response.ClearContent();
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in _dataTable.Columns)
            {
                if (dc.ColumnName == "CoffeArea")
                {
                    Response.Write(tab + "Area cafe");
                    tab = "\t";
                }
                else if (dc.ColumnName == "Other")
                {
                    Response.Write(tab + "Otros");
                    tab = "\t";
                }
                else if (dc.ColumnName == "CeroDosAnios")
                {
                    Response.Write(tab + "0-2 años");
                    tab = "\t";
                }
                else if (dc.ColumnName == "DosCincoAnios")
                {
                    Response.Write(tab + "3-5 años");
                    tab = "\t";
                }
                else if (dc.ColumnName == "SeisNueveAnios")
                {
                    Response.Write(tab + "6-9 años");
                    tab = "\t";
                }
                else if (dc.ColumnName == "MasNueveanios")
                {
                    Response.Write(tab + "> 9 años");
                    tab = "\t";
                }
                else if (dc.ColumnName == "Estimativokg")
                {
                    Response.Write(tab + "Estimado");
                    tab = "\t";
                }
                else if (dc.ColumnName == "Ventaskg")
                {
                    Response.Write(tab + "Ventas kg");
                    tab = "\t";
                }
                else if (dc.ColumnName == "ConsumoSacos")
                {
                    Response.Write(tab + "Consumo Sacos");
                    tab = "\t";
                }
                else {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in _dataTable.Rows)
            {
                tab = "";
                for (i = 0; i < _dataTable.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        public void ToExcelContacts(DateTime? start2, DateTime? end2)
        {
            DataTable _dataTable = new DataTable();
            var User = HttpContext.User as CustomPrincipal;
            int type = 0;


            if (User.IsAdmin())
            {
                Guid? userId = null;
                type = 1;
                _dataTable = ContactsTable(start2, end2, userId, type);
            }
            else {
                Guid? userId = User.UserId;
                type = 2;
                _dataTable = ContactsTable(start2, end2, userId, type);
            }

            Response.AddHeader("content-disposition", "attachment; filename=Reporte de contactos.xls");

            if (_dataTable == null)
                return;

            Encoding encoding = Encoding.UTF8;
            Response.Charset = encoding.EncodingName;
            Response.ContentEncoding = Encoding.Unicode;
            Response.ClearContent();
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in _dataTable.Columns)
            {
                
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";

            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in _dataTable.Rows)
            {
                tab = "";
                for (i = 0; i < _dataTable.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString().Replace("\r\n", ""));
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        public static DataTable ContactsTable(DateTime? start2, DateTime? end2, Guid? userId, int type)
        {
            DataTable dt = new DataTable();
            dt = FillDataTableByProcedure2("ContactsReportExcel", start2, end2, userId, type);
            return dt;
        }

        public static DataTable GeneralStatusTable(int? start, Guid? userId)
        {
            DataTable dt = new DataTable();
            if (userId != null)
            {
                dt = FillDataTableByProcedure("GeneralStatusByUserProject", start, userId);
            }
            else 
            {
                dt = FillDataTableByProcedure("GeneralStatus", start, userId);
            }
            return dt;
        }

        private static DataTable FillDataTableByProcedure2(string _NameProcedure, DateTime? start2, DateTime? end2, Guid? userId, int type)
        {
            DataTable _dataTable = new DataTable();
            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter _sqlAdapter = new SqlDataAdapter(_NameProcedure, _sqlConnection))
                    {
                        if (start2 != null)
                        {
                            _sqlAdapter.SelectCommand.Parameters.AddWithValue("@firstDate", start2);
                        }
                        else {
                            start2 = DateTime.Now.AddDays(-30);
                            _sqlAdapter.SelectCommand.Parameters.AddWithValue("@firstDate", start2);
                        }

                        if (end2 != null)
                        {
                            _sqlAdapter.SelectCommand.Parameters.AddWithValue("@endDate", end2);
                        }
                        else{
                            end2 = DateTime.Now;
                            _sqlAdapter.SelectCommand.Parameters.AddWithValue("@endDate", end2);
                        }

                        if (userId != null)
                        {
                            _sqlAdapter.SelectCommand.Parameters.AddWithValue("@userId", userId);
                        }

                        _sqlAdapter.SelectCommand.Parameters.AddWithValue("@type", type);
                        _sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _sqlAdapter.Fill(_dataTable);
                        return _dataTable;
                    }
                }

            }
            catch (Exception) { throw; }
        }

        private static DataTable FillDataTableByProcedure(string _NameProcedure, int? startYear, Guid? userId)
        {
            DataTable _dataTable = new DataTable();
            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {
                using (SqlConnection _sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter _sqlAdapter = new SqlDataAdapter(_NameProcedure, _sqlConnection))
                    {
                        if (startYear > 0 && userId == null)
                        {
                            _sqlAdapter.SelectCommand.Parameters.AddWithValue("@firstYear", startYear);
                        }
                        else if (userId != null && startYear > 0)
                        {
                            _sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter ("@firstYear", startYear));
                            _sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter("@userId", userId)); 
                        }
                        else
                        {
                            _sqlAdapter.SelectCommand.Parameters.AddWithValue("@firstYear", DateTime.Now.Year);
                        }
                        _sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _sqlAdapter.Fill(_dataTable);
                        return _dataTable;
                    }
                }

            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Gets the dates.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        private void GetDates(ref DateTime? start, ref DateTime? end)
        {
            if (start == null)
            {
                start = DateTime.Now.AddDays(-30);
                ViewBag.SelectedStart = start;
            }
            else
            {
                ViewBag.SelectedStart = start;
            }

            if (end == null)
            {
                end = DateTime.Now;
                ViewBag.SelectedEnd = end;
            }
            else
            {
                ViewBag.SelectedEnd = end;
            }
        }

        /// <summary>
        /// Gets the farms information acording to the parameters
        /// </summary>
        /// <param name="ProjectId">The project identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="userId">The userId.</param>
        /// <returns></returns>
        private List<ResumeElement> GetFarmsInformation(Guid? ProjectId, DateTime? start, DateTime? end, Guid? userId)
        {
            List<ResumeElement> FarmsInformation;
            if (ProjectId != null)
            {
                ViewBag.Project = db.Projects.Where(c => c.Id == ProjectId).Select(c => c.Name).First().ToString();
                FarmsInformation = db.ExecuteQuery<ResumeElement>("TechnicianReportByProjectAndDate @firstDate, @endDate, @ProjectId", new SqlParameter("firstDate", start), new SqlParameter("endDate", end), new SqlParameter("ProjectId", ProjectId)).ToList();
            }
            else if (userId != null)
            {
                FarmsInformation = db.ExecuteQuery<ResumeElement>("TechnicianReportByUserAndDate @firstDate, @endDate, @userId", new SqlParameter("firstDate", start), new SqlParameter("endDate", end), new SqlParameter("userId", userId)).ToList();
            }
            else
            {
                FarmsInformation = db.ExecuteQuery<ResumeElement>("TechnicianReportByDate @firstDate, @endDate", new SqlParameter("firstDate", start), new SqlParameter("endDate", end)).ToList();
            }

            return FarmsInformation;
        }

        public class ResumeElement
        {
            public string FarmName { get; set; }
            public string Code { get; set; }
            public string TechnicianName { get; set; }
            public int? ContactsQuantity { get; set; }
            public int? ContactsByFarm { get; set; }
            public double? EstimatedProduction { get; set; }
            public double? Saleskg { get; set; }
            public double? SalesValue { get; set; }
            public int? FertilizersQuantity { get; set; }
            public int? FertilizersValue { get; set; }
            public double? CoffeeArea { get; set; }
        }

        public class DetailElement
        {
            public string Municipality { get; set; }
            public string Agency { get; set; }
            public int? Farms { get; set; }
            public decimal? CoffeArea { get; set; }
            public decimal? Castillo { get; set; }
            public decimal? Colombia { get; set; }
            public decimal? Caturra { get; set; }
            public decimal? Tabi { get; set; }
            public decimal? Other { get; set; }
            public decimal? CeroDosAnios { get; set; }
            public decimal? DosCincoAnios { get; set; }
            public decimal? SeisNueveAnios { get; set; }
            public decimal? MasNueveanios { get; set; }
            public decimal? Estimativokg { get; set; }
            public decimal? Ventaskg { get; set; }
            public decimal? ConsumoSacos { get; set; }
            public virtual List<Project> ProjectList { get; set; }
        }

        //public class GroupedElement
        //{
        //    public string Municipality { get; set; }
        //    public string Agency { get; set; }
        //    public int? Farms { get; set; }
        //    public decimal? CoffeArea { get; set; }
        //    public decimal? Castillo { get; set; }
        //    public decimal? Colombia { get; set; }
        //    public decimal? Caturra { get; set; }
        //    public decimal? Other { get; set; }
        //    public decimal? CeroDosAnios { get; set; }
        //    public decimal? DosCincoAnios { get; set; }
        //    public decimal? SeisNueveAnios { get; set; }
        //    public decimal? MasNueveanios { get; set; }
        //    public decimal? Estimativokg { get; set; }
        //    public decimal? Ventaskg { get; set; }
        //    public decimal? ConsumoSacos { get; set; }
        //}

        public class GroupedInformation
        {
            public string TechnicianName { get; set; }
            public int? ContactsNumber { get; set; }
            public int? ContactsByFarm { get; set; }
            public double? EstimatedProduction { get; set; }
            public double? Saleskg { get; set; }
            public double? SalesValue { get; set; }
            public int? FertilizersQuantity { get; set; }
            public int? FertilizersValue { get; set; }
            public int NumberOfFarms { get; set; }
            public double? Percentage { get; set; }
            public double? CoffeeArea { get; set; }
            public double? CoffeeAreaSalesKg { get; set; }
            public double? CoffeeAreaFertilizersQuantity { get; set; }
            public double? by125kg { get; set; }
        }
    }
}