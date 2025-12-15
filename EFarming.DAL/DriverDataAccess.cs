using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EFarming.DAL.UnitOfWork;

namespace EFarming.DAL
{
    public class DriverDataAccess
    {
        SqlConnection _efConnection = new SqlConnection(new UnitOfWork().Database.Connection.ConnectionString);

        // Open Connection
        void EfarmingConnOpen()
        {
            if (_efConnection.State == ConnectionState.Closed)
            {
                _efConnection.Open();
            }
        }

        // Close Connection
        void EfarmingConnClose()
        {
            if (_efConnection.State == ConnectionState.Open)
            {
                _efConnection.Close();
            }
        }

        // Ejecutar procedimientos (Insert,Update,Delete)
        //public void EjecutarSp(String nombreProcedimiento, List<Parametros> lstParametros)
        public async Task<List<Errores>> EjecutarSp(String nombreProcedimiento, List<Parametros> lstParametros)
        {
            SqlCommand cmd;
            List<Errores> ListError = new List<Errores>();
            try
            {
                EfarmingConnOpen();
                cmd = new SqlCommand(nombreProcedimiento, _efConnection) { CommandType = CommandType.StoredProcedure };
                cmd.CommandTimeout = 900;
                if (lstParametros != null)
                {
                    for (var i = 0; i < lstParametros.Count; i++)
                    {
                        if (lstParametros[i].Direccion == ParameterDirection.Input)
                        {
                            cmd.Parameters.AddWithValue(lstParametros[i].Nombre, lstParametros[i].Valor);
                        }

                        if (lstParametros[i].Direccion == ParameterDirection.Output)
                        {
                            cmd.Parameters.Add(lstParametros[i].Nombre, lstParametros[i].TipoDato, lstParametros[i].Tamanio).Direction = ParameterDirection.Output;
                        }
                    }
                    cmd.ExecuteNonQuery();

                    // recuperar parámetros de salida
                    for (var i = 0; i < lstParametros.Count; i++)
                    {
                        if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                        {
                            lstParametros[i].Valor = cmd.Parameters[i].Value.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {


                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    Errores error = new Errores();
                    error.type = "Procedure";
                    error.Description = ex.Errors[i].Message;
                    error.LineNumber = ex.Errors[i].LineNumber;
                    error.Procedure = ex.Errors[i].Procedure;
                    error.state = 1;
                    ListError.Add(error);
                }

            }
            finally
            {
                EfarmingConnClose();
            }

            if (ListError == null)
            {
                Errores error = new Errores();
                error.state = 0;
            }
            return ListError;
        }

        public class Errores
        {
            public string type { get; set; }
            public string Description { get; set; }
            public int LineNumber { get; set; }
            public string Source { get; set; }
            public string Procedure { get; set; }
            public int state { get; set; }

        }
    }
}
