using EFarming.Core.TasqModule;
using EFarming.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EFarming.Web.Controllers
{
    public class AssessmentsReportController : Controller
    {
        private UnitOfWork db = new UnitOfWork();
        // GET: AssessmentsReport
        public ActionResult Index()
        {
            var Assessments = db.AssessmentTemplates.Where(s => s.DeletedAt == null).ToList();
            ViewBag.TypeAssessment = Assessments;

            return View();
        }

        public class Answers
        {
            public Guid TASQAssessmentId { get; set; }
            public int CriteriaID { get; set; }
            public string Answer { get; set; }

            public int modOrder { get; set; }

            public int subModOrder { get; set; }

            public int criteriaOrder { get; set; }
        }

        public class Answers1
        {
            public DateTime? Date { get; set; }
            public string TituloEncuesta { get; set; }
            public string NombreFinca { get; set; }
            public string CodigoFinca { get; set; }
            public string CedulaProd { get; set; }
            public string NombProd { get; set; }
            public string Vereda { get; set; }
            public string Municipio { get; set; }
            public string Departamento { get; set; }
            public string Cooperativa { get; set; }
            public string Tecnico { get; set; }


            public virtual ICollection<Answers> AnswersItems { get; set; }
        }

        public class Questions
        {
            public int CriteriaID { get; set; }

            public string Question { get; set; }

            public int modOrder { get; set; }

            public int subModOrder { get; set; }

            public int criteriaOrder { get; set; }
        }

        [HttpPost]
        public void ToExcel(Guid idAssessment, DateTime? searchStartDate, DateTime? searchEndDate)
        {
            var id = db.TASQAssessment.Where(x => x.AssessmentTemplateId == idAssessment).Include(a => a.AssessmentTemplate).Include(f => f.Farm).Include(fa => fa.Farm.FamilyUnitMembers).Include(v => v.Farm.Village).Include(m => m.Farm.Village.Municipality).Include(d => d.Farm.Village.Municipality.Department).Include(f => f.Farm.Cooperative).ToList(); 
            if (searchStartDate != null && searchEndDate!= null)
            {
                //consulto todas las encuestas diligenciadas de acuerdo al id que llega y lo guardo en una variable
                id = id.Where(x => x.Date >= searchStartDate && x.Date <= searchEndDate).ToList();
            }
            else if (searchStartDate != null && searchEndDate == null)
            {
                //consulto todas las encuestas diligenciadas de acuerdo al id que llega y lo guardo en una variable
                id = id.Where(x => x.Date >= searchStartDate && x.Date <= DateTime.Now).ToList();
            }
            else if (searchStartDate == null && searchEndDate != null)
            {
                //consulto todas las encuestas diligenciadas de acuerdo al id que llega y lo guardo en una variable
                id = id.Where(x => x.Date >= DateTime.Now.AddDays(-15) && x.Date <= searchEndDate).ToList();
            }


            //me creo una lista de tipo respuestas para almacenar dinamicamente las respuestas de las preguntas de las encuestas
            List<Answers1> ListRespuestas1 = new List<Answers1>();

            //consulto todas las preguntas segun el id de la encuesta 
            var Questions = db.TASQCriteria.Where(x => x.SubModule.Module.AssessmentTemplateId == idAssessment).OrderBy(o => o.SubModule.Module.ModuleOrder).ThenBy(o => o.SubModule.SubModuleOrder).ThenBy(o=>o.CriteriaOrder).ToList();

            // me creo una lista para almacenar las preguntas
            List<Questions> ListPreguntas = new List<Questions>();

            // por cada pregunta la almaceno en la lista de preguntas
            foreach (var item in Questions)
            {
                Questions aq1 = new Questions();

                aq1.CriteriaID = item.Id;
                aq1.Question = item.SubModule.Module.ModuleOrder.ToString() + "-" + item.SubModule.SubModuleOrder.ToString() + "-" + item.CriteriaOrder.ToString() + " - "+item.Description;
                aq1.modOrder = item.SubModule.Module.ModuleOrder;
                aq1.subModOrder = item.SubModule.SubModuleOrder;
                aq1.criteriaOrder = item.CriteriaOrder;
                ListPreguntas.Add(aq1);

            }

            //int cont = 1;

            //por cada encuesta diligenciada entra a este foreach
            foreach (var item in id)
            {
                //consulta los datos del propietario por cada finca que haya respondido la encuesta
                var DatosFamily = db.FamilyUnitMembers.Where(x => x.FarmId == item.FarmId && x.IsOwner == true).FirstOrDefault();
                var Techni = db.Farms.Include(x => x.AssociatedPeople).Where(x => x.Id == item.FarmId).Select(x => x.AssociatedPeople).FirstOrDefault();

                //instancio el objeto respuestas para guardar los datos 
                Answers1 aqP = new Answers1();
                aqP.Date = item.Date;
                aqP.TituloEncuesta = item.AssessmentTemplate.Name;
                aqP.NombreFinca = item.Farm.Name;
                aqP.CodigoFinca = item.Farm.Code;
                if (DatosFamily != null)
                {
                    if (DatosFamily.Identification != "" || DatosFamily.Identification != null)
                    {
                        aqP.CedulaProd = DatosFamily.Identification;
                    }
                    else
                    {
                        aqP.CedulaProd = "";
                    }

                    if (DatosFamily.FirstName != "" || DatosFamily.FirstName != null && DatosFamily.LastName != "" || DatosFamily.LastName != null)
                    {
                        aqP.NombProd = DatosFamily.FirstName + " " + DatosFamily.LastName;
                    }
                    else
                    {
                        aqP.NombProd = "";
                    }
                }
                else
                {
                    aqP.CedulaProd = "";
                    aqP.NombProd = "";
                }

                aqP.Vereda = item.Farm.Village.Name;
                aqP.Municipio = item.Farm.Village.Municipality.Name;
                aqP.Departamento = item.Farm.Village.Municipality.Department.Name;
                aqP.Cooperativa = item.Farm.Cooperative.Name;

                foreach (var item2 in Techni)
                {
                    aqP.Tecnico = item2.FirstName + " " + item2.LastName;
                }

                //if (cont <= 2)
                //{
                // consulto las respuestas para cada encuesta
                //var respuestas = db.TASQAssessmentAnswer.Where(x => x.TASQAssessmentId == item.Id).OrderBy(x => x.CriteriaId).ToList();
                var respuestas = (from a in db.TASQAssessmentAnswer
                                  join e in db.TASQCriteria on a.CriteriaId equals e.Id
                                  where a.TASQAssessmentId == item.Id
                                  select a
                                    ).ToList().OrderBy(x=>x.Criteria.CriteriaOrder);

                //var respuestas = db.TASQAssessmentAnswer.Where(x => x.TASQAssessmentId == new Guid("{624A20E3-9295-41E1-8057-000CE734FB80}")).OrderBy(x => x.CriteriaId).ToList();

                //creo una lista de tipo respuestas 
                List<Answers> ListRespuestas = new List<Answers>();

                // por cada respuesta la almaceno en la lista de respuestas
                foreach (var item2 in respuestas)
                {
                    Answers aq2 = new Answers();
                    
                    TASQCriteria criteria = db.TASQCriteria.Where(c => c.Id == item2.CriteriaId).FirstOrDefault();
                    if(criteria != null)
                    {
                        aq2.TASQAssessmentId = item2.TASQAssessmentId;
                        aq2.CriteriaID = item2.CriteriaId;
                        aq2.modOrder = criteria.SubModule.Module.ModuleOrder;
                        aq2.subModOrder = criteria.SubModule.SubModuleOrder;
                        aq2.criteriaOrder = criteria.CriteriaOrder;

                        if (item2.Value != null && item2.Value != "" && item2.Value != " ")
                        {
                            aq2.Answer = item2.Value;
                        }
                        else
                        {
                            aq2.Answer = "RESPUESTA VACIA";
                        }
                        ListRespuestas.Add(aq2);

                        //cada respuesta la guardo en la variable AnswersItems
                        aqP.AnswersItems = ListRespuestas;
                    }
                    
                }

                //finalmente guardo todos los campos 
                ListRespuestas1.Add(aqP);
                //    cont++;
                //}
                //else
                //{
                //    break;
                //}
            }

            Response.ContentEncoding = System.Text.Encoding.UTF32;
            Response.AddHeader("content-disposition", "attachment; filename=Reporte de evaluaciones.xls");

            Encoding encoding = Encoding.UTF8;
            Response.Charset = encoding.EncodingName;
            Response.ContentEncoding = Encoding.Unicode;
            Response.ClearContent();
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";

            int conta = 0;

            ListPreguntas = ListPreguntas.OrderBy(o => o.modOrder).ToList();//.OrderBy(s => s.subModOrder).OrderBy(p => p.criteriaOrder)
            foreach (var item in ListPreguntas)
            {
                if (conta == 0)
                {
                    Response.Write(tab + "Fecha");
                    tab = "\t";

                    Response.Write(tab + "Tipo Encuesta");
                    tab = "\t";

                    Response.Write(tab + "Nombre de la finca");
                    tab = "\t";

                    Response.Write(tab + "Codigo de la finca");
                    tab = "\t";

                    Response.Write(tab + "Cedula del productor");
                    tab = "\t";

                    Response.Write(tab + "Nombre del productor");
                    tab = "\t";

                    Response.Write(tab + "Tecnico");
                    tab = "\t";

                    Response.Write(tab + "Vereda");
                    tab = "\t";

                    Response.Write(tab + "Municipio");
                    tab = "\t";

                    Response.Write(tab + "Departamento");
                    tab = "\t";

                    Response.Write(tab + "Cooperativa");
                    tab = "\t";

                    Response.Write(tab + item.Question);
                    tab = "\t";

                    conta++;
                }
                else
                {
                    Response.Write(tab + item.Question );
                    tab = "\t";
                }
            }

            Response.Write(tab + "");
            Response.Write("\n");

            int cont2 = 0;
            int contP = 0;
            int limite = ListPreguntas.Count();


            foreach (var item2 in ListRespuestas1)
            {
                Response.Write(item2.Date);
                tab = "\t";
                Response.Write(tab + item2.TituloEncuesta);
                tab = "\t";
                Response.Write(tab + item2.NombreFinca);
                tab = "\t";
                Response.Write(tab + item2.CodigoFinca);
                tab = "\t";
                Response.Write(tab + item2.CedulaProd);
                tab = "\t";
                Response.Write(tab + item2.NombProd);
                tab = "\t";
                Response.Write(tab + item2.Tecnico);
                tab = "\t";
                Response.Write(tab + item2.Vereda);
                tab = "\t";
                Response.Write(tab + item2.Municipio);
                tab = "\t";
                Response.Write(tab + item2.Departamento);
                tab = "\t";
                Response.Write(tab + item2.Cooperativa);
                tab = "\t";

                item2.AnswersItems = item2.AnswersItems.OrderBy(o => o.modOrder).OrderBy(s=>s.subModOrder).ToList();

                foreach (var item in item2.AnswersItems)
                {
                    Response.Write(tab + item.Answer);
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        private static DataTable FillDataTableByProcedure(string _NameProcedure, Guid idAssessment, DateTime? searchStartDate, DateTime? searchEndDate)
        {
            DataTable _dataTable = new DataTable();
            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {

                using (SqlConnection _sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter _sqlAdapter = new SqlDataAdapter(_NameProcedure, _sqlConnection))
                    {
                        _sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter("@AssessmentTemplateId", idAssessment));
                        _sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter("@startdate", searchStartDate));
                        _sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter("@enddate", searchEndDate));

                        _sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        _sqlAdapter.Fill(_dataTable);

                        return _dataTable;
                    }
                }

            }
            catch (Exception) { throw; }
        }

        private static DataTable FillDataTableByProcedure2(string _NameProcedure, string Id)
        {
            DataTable _dataTable = new DataTable();
            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {

                using (SqlConnection _sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter _sqlAdapter = new SqlDataAdapter(_NameProcedure, _sqlConnection))
                    {
                        _sqlAdapter.SelectCommand.Parameters.Add(new SqlParameter("@id", Id));

                        _sqlAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                        _sqlAdapter.Fill(_dataTable);

                        return _dataTable;
                    }
                }

            }
            catch (Exception) { throw; }
        }


        public ActionResult MigrarEncuesta()
        {
            DataTable _dataTable = new DataTable();

            StringBuilder saveQueryToDbUpd = new StringBuilder();

            string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            try
            {

                using (SqlConnection _sqlConnection = new SqlConnection(_connectionString))
                {
                    using (SqlDataAdapter _sqlAdapter = new SqlDataAdapter("SELECT * FROM EncuestaMejoramiento200824_2", _sqlConnection))
                    {
                        

                        _sqlAdapter.SelectCommand.CommandType = CommandType.Text;

                        _sqlAdapter.Fill(_dataTable);

                       
                    }
                }

                //List<trasparency_buybusiness> businessList = db.ExecuteQuery<trasparency_buybusiness>(transparencyQuery.ToString()).ToList();
                for (int i= 0; i < _dataTable.Rows.Count; i++)
                {
                    saveQueryToDbUpd.Clear();
                    for (int j = 2; j < _dataTable.Columns.Count; j++)
                    {
                        if (!_dataTable.Rows[i][0].ToString().Equals("#N/A"))
                        {
                            saveQueryToDbUpd.Append("INSERT INTO [dbo].[TASQAssessmentAnswers] ([Id] ,[CriteriaId] ,[Value] ,[CreatedAt] ," +
                                "[TASQAssessmentId]) VALUES (NEWID() ," + _dataTable.Columns[j].ColumnName + ",'" + _dataTable.Rows[i][j].ToString() + "',GETDATE(),'" + _dataTable.Rows[i][0].ToString() + "');\n ");
                        }
                    }
                    try
                    {
                        db.ExecuteCommand(saveQueryToDbUpd.ToString());
                    }
                    catch(Exception exs)
                    {

                    }
                    //INSERT INTO [dbo].[TASQAssessmentAnswers] ([Id] ,[CriteriaId] ,[Value] ,[CreatedAt] ,[TASQAssessmentId]) VALUES (NEWID() ,<CriteriaId, int,> ,<Value, nvarchar(max),> ,GETDATE(),<TASQAssessmentId, uniqueidentifier,>)
                }


            }
            catch (Exception) { throw; }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        public class AnswersAssements
        {
            public Guid? Id_Assement { get; set; }
            public Guid? Id_Finca { get; set; }
            public DateTime? Fecha { get; set; }
            public string Prueba { get; set; }
            public string Tecnico { get; set; }
            public string Nomb_Finca { get; set; }
            public string Code_Finca { get; set; }
            public string CC_Producer { get; set; }
            public string Name_Producer { get; set; }
            public string Vereda { get; set; }
            public string Municipio { get; set; }
            public string Departamento { get; set; }
            public string Cooperativa { get; set; }
            public int? Total_Q { get; set; }
            public int? A_NULLS { get; set; }
            public int? A_NOT_NULLS { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class groupedAssessments
        {
            public List<TASQAssessmentAnswer> Answers { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class ListOptionAnswer
        {
            public string OptionAnswer { get; set; }
            public bool SelectOption { get; set; }
        }
    }
}
