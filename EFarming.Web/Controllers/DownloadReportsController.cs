using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.DAL;
using EFarming.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style.XmlAccess;

namespace EFarming.Web.Controllers
{
    [CustomAuthorize(Roles = "Admin,Reader")]
    public class DownloadReportsController : Controller
    {
        private UnitOfWork db = new UnitOfWork();
        // GET: DownloadReports
        public ActionResult Index(int? start)
        {
           
            if (start == 0 || start.Equals(null))
            {
                ViewBag.year = string.Format("{0:yyyy}", DateTime.Now);
            }

            else
            {
                ViewBag.year = start;
            }
            return View();
        }

        public ActionResult ReporteGeneral()
        {
                        
            return View();
        }

        public void ToExcelReporteGeneral()
        {
            SqlCommand Sqlcmd;
            DataSet ds = new DataSet();
            SqlDataAdapter Sqlda;
            SqlConnection Sqlcx;
            string SqlQuery = "";

            var User = HttpContext.User as CustomPrincipal;

            

            Sqlcx = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            try
            {



                Sqlcx.Open();
                Sqlcmd = new SqlCommand();

                SqlQuery = "GeneralReport2021";

                Sqlcmd.Parameters.Add(new SqlParameter("@user", SqlDbType.UniqueIdentifier));
                Sqlcmd.Parameters["@user"].Value = User.UserId;


                Sqlcmd.Connection = Sqlcx;
                Sqlcmd.CommandText = SqlQuery;
                Sqlcmd.CommandType = CommandType.StoredProcedure;
                Sqlcmd.CommandTimeout = 10000;

                Sqlda = new SqlDataAdapter(Sqlcmd);
                Sqlda.Fill(ds);


            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally
            {
                Sqlcx.Close();
            }


            if (ds.Tables.Count == 5)
            {




                string ruta = string.Empty;
                FileInfo newFile;
                ExcelPackage pck;
                byte[] bin;


                //ruta = Server.MapPath("~/Content/Templates/general_report_template.xlsx");
                //newFile = new FileInfo(ruta);
                //pck = new ExcelPackage(newFile);


                ///*****************************HOJA FINCAS*******************************/


                //var ws = pck.Workbook.Worksheets["Fincas"];

                //for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                //{
                //    for (int cols = 0; cols < 18; cols++)
                //    {
                //        //ws.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                //        try
                //        {
                //            ws.Cells[rows + 2, cols + 1].Value = ds.Tables[0].Rows[rows][cols].ToString().Replace(",", ".");
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}
                //for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                //{
                //    for (int cols = 20; cols < 23; cols++)
                //    {
                //        //ws.Cells[rows + 2, cols + 12].Style.Numberformat.Format = "#,##0.00";
                //        try
                //        {
                //            ws.Cells[rows + 2, cols + 12].Value = ds.Tables[0].Rows[rows][cols].ToString().Replace(",", ".");
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}



                ///*****************************HOJA FAMILIARES*******************************/

                //var wsFam = pck.Workbook.Worksheets["Familiar"];

                //for (int rows = 0; rows < ds.Tables[1].Rows.Count; rows++)
                //{
                //    for (int cols = 0; cols < ds.Tables[1].Columns.Count; cols++)
                //    {
                //        //wsFam.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                //        try
                //        {
                //            wsFam.Cells[rows + 2, cols + 1].Value = ds.Tables[1].Rows[rows][cols].ToString().Replace(",", ".");
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}


                ///*****************************HOJA PERSONAS*******************************/

                //var wsPer = pck.Workbook.Worksheets["Personas"];

                //for (int rows = 0; rows < ds.Tables[2].Rows.Count; rows++)
                //{
                //    for (int cols = 0; cols < ds.Tables[2].Columns.Count; cols++)
                //    {
                //        //wsPer.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                //        try
                //        {
                //            wsPer.Cells[rows + 2, cols + 1].Value = ds.Tables[2].Rows[rows][cols].ToString().Replace(",", ".");
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}


                ///*****************************HOJA PLANTACIONES*******************************/

                //var wsPlan = pck.Workbook.Worksheets["Plantaciones"];

                //for (int rows = 0; rows < ds.Tables[3].Rows.Count; rows++)
                //{
                //    for (int cols = 0; cols < ds.Tables[3].Columns.Count; cols++)
                //    {
                //        //wsPlan.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                //        try
                //        {
                //            wsPlan.Cells[rows + 2, cols + 1].Value = ds.Tables[3].Rows[rows][cols].ToString().Replace(",", ".");
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}


                ///*****************************HOJA CONTACTOS*******************************/

                //var wsCon = pck.Workbook.Worksheets["Contactos"];

                //for (int rows = 0; rows < ds.Tables[4].Rows.Count; rows++)
                //{
                //    for (int cols = 0; cols < ds.Tables[4].Columns.Count; cols++)
                //    {
                //        //wsCon.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                //        try
                //        {
                //            wsCon.Cells[rows + 2, cols + 1].Value = ds.Tables[4].Rows[rows][cols].ToString().Replace(",", ".");
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}

                ///*****************************HOJA NOVEDADES*******************************/

                //var wsNov = pck.Workbook.Worksheets["Novedades reportadas"];

                //for (int rows = 0; rows < ds.Tables[5].Rows.Count; rows++)
                //{
                //    for (int cols = 0; cols < ds.Tables[5].Columns.Count; cols++)
                //    {
                //        //wsNov.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                //        try
                //        {
                //            wsNov.Cells[rows + 2, cols + 1].Value = ds.Tables[5].Rows[rows][cols].ToString().Replace(",", ".");
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}


                using (var Excel = new ExcelPackage())
                {
                    //    FileStream fs = new FileStream(Server.MapPath("/Content/Reports/reporteGeneral2021.xlsx"), FileMode.Create);
                    //ExcelPackage Excel = new ExcelPackage(fs);

                    /* Creación del estilo. */
                    //Excel.Workbook.Styles.CreateNamedStyle("Moneda");
                    //ExcelNamedStyleXml moneda = Excel.Workbook.Styles.NamedStyles[1];// 0 = Normal, 1 (El que acabamos de agregar).

                    //moneda.Style.Numberformat.Format = "_-$* #,##0.00.00_-;-$* #,##0.00.00_-;_-$* \"-\"??_-;_-@_-";
                    //moneda.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //moneda.Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                    /* Creación de hoja de trabajo. */
                    Excel.Workbook.Worksheets.Add("FINCAS");
                    ExcelWorksheet hoja = Excel.Workbook.Worksheets["FINCAS"];

                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        hoja.Cells[1, j + 1].Style.Font.Bold = true;
                        hoja.Cells[1, j + 1].Value = ds.Tables[0].Columns[j].ColumnName;
                    }

                    for (int rows = 0; rows < ds.Tables[0].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < ds.Tables[0].Columns.Count; cols++)
                        {
                            hoja.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                            try
                            {
                                hoja.Cells[rows + 2, cols + 1].Value = ds.Tables[0].Rows[rows][cols].ToString().Replace(",", ".");
                            }
                            catch
                            {

                            }
                        }
                    }

                    Excel.Workbook.Worksheets.Add("PERSONAS");
                    hoja = Excel.Workbook.Worksheets["PERSONAS"];

                    for (int j = 0; j < ds.Tables[1].Columns.Count; j++)
                    {
                        hoja.Cells[1, j + 1].Style.Font.Bold = true;
                        hoja.Cells[1, j + 1].Value = ds.Tables[1].Columns[j].ColumnName;
                    }

                    for (int rows = 0; rows < ds.Tables[1].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < ds.Tables[1].Columns.Count; cols++)
                        {
                            hoja.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                            try
                            {
                                hoja.Cells[rows + 2, cols + 1].Value = ds.Tables[1].Rows[rows][cols].ToString().Replace(",", ".");
                            }
                            catch
                            {

                            }
                        }
                    }

                    Excel.Workbook.Worksheets.Add("PLANTACIONES");
                    hoja = Excel.Workbook.Worksheets["PLANTACIONES"];

                    for (int j = 0; j < ds.Tables[2].Columns.Count; j++)
                    {
                        hoja.Cells[1, j + 1].Style.Font.Bold = true;
                        hoja.Cells[1, j + 1].Value = ds.Tables[2].Columns[j].ColumnName;
                    }

                    for (int rows = 0; rows < ds.Tables[2].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < ds.Tables[2].Columns.Count; cols++)
                        {
                            hoja.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                            try
                            {
                                hoja.Cells[rows + 2, cols + 1].Value = ds.Tables[2].Rows[rows][cols].ToString().Replace(",", ".");
                            }
                            catch
                            {

                            }
                        }
                    }

                    Excel.Workbook.Worksheets.Add("CONTACTOS");
                    hoja = Excel.Workbook.Worksheets["CONTACTOS"];

                    for (int j = 0; j < ds.Tables[3].Columns.Count; j++)
                    {
                        hoja.Cells[1, j + 1].Style.Font.Bold = true;
                        hoja.Cells[1, j + 1].Value = ds.Tables[3].Columns[j].ColumnName;
                    }

                    for (int rows = 0; rows < ds.Tables[3].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < ds.Tables[3].Columns.Count; cols++)
                        {
                            hoja.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                            try
                            {
                                hoja.Cells[rows + 2, cols + 1].Value = ds.Tables[3].Rows[rows][cols].ToString().Replace(",", ".");
                            }
                            catch
                            {

                            }
                        }
                    }

                    Excel.Workbook.Worksheets.Add("NOVEDADES");
                    hoja = Excel.Workbook.Worksheets["NOVEDADES"];

                    for (int j = 0; j < ds.Tables[4].Columns.Count; j++)
                    {
                        hoja.Cells[1, j + 1].Style.Font.Bold = true;
                        hoja.Cells[1, j + 1].Value = ds.Tables[4].Columns[j].ColumnName;
                    }

                    for (int rows = 0; rows < ds.Tables[4].Rows.Count; rows++)
                    {
                        for (int cols = 0; cols < ds.Tables[4].Columns.Count; cols++)
                        {
                            hoja.Cells[rows + 2, cols + 1].Style.Numberformat.Format = "#,##0.00";
                            try
                            {
                                hoja.Cells[rows + 2, cols + 1].Value = ds.Tables[4].Rows[rows][cols].ToString().Replace(",", ".");
                            }
                            catch
                            {

                            }
                        }
                    }

                    /* Num Caracteres + 1.29 de Margen.
                    Los índices de columna empiezan desde 1. */
                    //hoja.Column(1).Width = 11.29f;

                    //ExcelRange rango = hoja.Cells["A1"];

                    //rango.Value = 1.0M;
                    //rango.StyleName = "Moneda";

                    //Excel.Save();

                    //// No estoy seguro de ésta parte, pero mejor cerramos el stream de archivo.
                    //fs.Close();
                    //fs.Dispose();

                    //Excel.Dispose();
                    Response.ClearContent();
                    Response.BinaryWrite(Excel.GetAsByteArray());
                    Response.AddHeader("content-disposition", "attachment;filename=reporteGeneral2021.xlsx");

                    Response.ContentType = "application/excel";
                    Response.Flush();
                    Response.End();
                }

                

            }
        }

        public static DataTable GeneralReports(int? start, Guid? userId)
        {
            DataTable dt = new DataTable();
            if (userId != null)
            {
                dt = FillDataTableByProcedure("GeneralReportsByUserProjects", start, userId);
            }
            else
            {
                dt = FillDataTableByProcedure("GeneralReports", start, userId);
            }
            return dt;
        }

        public static DataTable PlantationReports(int? start, Guid? userId)
        {
            DataTable dt = new DataTable();
            if (userId != null)
            {
                dt = FillDataTableByProcedure("PlantationReportsByUserProjects", 0, userId);
            }
            else
            {
                dt = FillDataTableByProcedure("PlantationReports", 0, userId);
            }
            return dt;
        }

        public static DataTable ProductivitiesReports(int? start, Guid? userId)
        {
            DataTable dt = new DataTable();
            if (userId != null)
            {
                dt = FillDataTableByProcedure("ProductivitiesReportsByUserProjects", 0, userId);
            }
            else
            {
                dt = FillDataTableByProcedure("ProductivitiesReports", 0, userId);
            }
            return dt;
        }

        public static DataTable floweringPeriodsReport(Guid? userId)
        {
            DataTable dt = new DataTable();
            if (userId != null)
            {
                dt = FillDataTableByProcedure("floweringPeriodsReportByUserProjects", 0,userId);
            }
            else
            {
                dt = FillDataTableByProcedure("floweringPeriodsReport", 0, userId);
            }
            return dt;
        }

        public void ToExcel(int reports, int? start)
        {
            DataTable _dataTable = new DataTable();
            var User = HttpContext.User as CustomPrincipal;
            List<Project> ListProjects = new List<Project>();
            var Projects = db.Projects.Where(d => d.DeletedAt == null).ToList();
            var UserList = db.Users.Where(u => u.Id == User.UserId).FirstOrDefault();
            var ListProjectsUser = UserList.Projects.ToList();

            string reportName = string.Empty;

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
            switch (reports)
            {
                case 1:
                    if (ListProjects.Count() == 0)
                    {
                        Guid? userId = null;
                        _dataTable = GeneralReports(start, userId);
                    }
                    else
                    {
                        Guid? userId = User.UserId;
                        _dataTable = GeneralReports(start, userId);
                    }
                    //Response.AddHeader("content-disposition", "attachment; filename=Reporte general.xls");
                    reportName = "Reporte general.xls";
                    break;
                case 2:
                    if (ListProjects.Count() == 0)
                    {
                        Guid? userId = null;
                        _dataTable = PlantationReports(start,userId);
                    }
                    else
                    {
                        Guid? userId = User.UserId;
                        _dataTable = PlantationReports(start,userId);
                    }
                    //Response.AddHeader("content-disposition", "attachment; filename=Reporte plantaciones.xls");
                    reportName = "Reporte plantaciones.xls";
                    break;
                case 3:
                    if (ListProjects.Count() == 0)
                    {
                        Guid? userId = null;
                        _dataTable = ProductivitiesReports(start, userId);
                    }
                    else
                    {
                        Guid? userId = User.UserId;
                        _dataTable = ProductivitiesReports(start,userId);
                    }
                    //Response.AddHeader("content-disposition", "attachment; filename=Reporte productividad.xls");
                    reportName = "Reporte productividad.xls";
                    break;
                case 4:
                    if (ListProjects.Count() == 0)
                    {
                        Guid? userId = null;
                        _dataTable = floweringPeriodsReport(userId);
                    }
                    else
                    {
                        Guid? userId = User.UserId;
                        _dataTable = floweringPeriodsReport(userId);
                    }
                    //Response.AddHeader("content-disposition", "attachment; filename=Reporte floraciones.xls");
                    reportName = "Reporte floraciones.xls";
                    break;
                default:
                    break;
            }

            if (_dataTable == null)
                return;

            DumpExcel(_dataTable, reportName);
            //Encoding encoding = Encoding.UTF8;
            //Response.Charset = encoding.EncodingName;
            //Response.ContentEncoding = Encoding.Unicode;
            //Response.ClearContent();
            //Response.ContentType = "application/vnd.ms-excel";
            //string tab = "";
            //foreach (DataColumn dc in _dataTable.Columns)
            //{
            //    Response.Write(tab + dc.ColumnName);
            //    tab = "\t";
            //}
            //Response.Write("\n");
            //int i;
            //foreach (DataRow dr in _dataTable.Rows)
            //{
            //    tab = "";
            //    for (i = 0; i < _dataTable.Columns.Count; i++)
            //    {
            //        Response.Write(tab + dr[i].ToString());
            //        tab = "\t";
            //    }
            //    Response.Write("\n");
            //}
            //Response.End();

        }

        public void DumpExcel(DataTable tbl, string filename)
        {



            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = tbl;
            dgGrid.DataBind();

            //Get the HTML for the control.
            dgGrid.RenderControl(hw);



            //Write the HTML back to the browser.
            //Response.ContentType = application/vnd.ms-excel;
            Response.Clear();
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            //this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.End();

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
                            _sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter("@firstYear", startYear));
                            _sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter("@userId", userId));
                        }
                        else if(userId != null && startYear == 0)
                        {
                            _sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter("@userId", userId));
                        }

                        _sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _sqlAdapter.Fill(_dataTable);
                        return _dataTable;
                    }
                }
               
            }
            catch (Exception) { throw; }
        }
    }
}
