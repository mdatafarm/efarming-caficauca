using EFarming.Oracle.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EFarming.Oracle.Mapper
{
    public class MapperUtils
    {
        public dynamic query(string SqlQuery, string type, int? FarmerIdentification)
        {
            dynamic NewObject = null;

            OracleConnection conn = new OracleConnection(GetConnectionString());

            conn.Open();

            OracleParameter parm = new OracleParameter();
            parm.OracleDbType = OracleDbType.Int32;
            parm.Value = FarmerIdentification;

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.Parameters.Add(parm);

            cmd.CommandText = SqlQuery;

            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();

            try
            {
                if (type == "Farmer")
                {
                    NewObject = new List<Farmer>();
                    while (dr.Read())
                    {
                        NewObject.Add(new Farmer
                        {
                            Identification = dr.GetInt32(0),
                            LastNames = dr.GetValue(1).ToString(),
                            Names = dr.GetValue(2).ToString(),
                            dob = dr.GetValue(3).ToString(),
                            EducationLevel = dr.GetValue(4).ToString(),
                            CivilStatus = dr.GetValue(5).ToString()
                        });
                    }
                }
                else if (type == "Farm")
                {
                    NewObject = new List<Farm>();
                    while (dr.Read())
                    {
                        NewObject.Add(new Farm
                        {
                            Code = dr.GetInt32(0),
                            Name = dr.GetValue(1).ToString(),
                            Village = dr.GetValue(2).ToString(),
                            MunicipalityId = dr.GetInt32(3),
                            Cooperative = dr.GetValue(4).ToString(),
                            Status = dr.GetValue(5).ToString(),
                            OwnerShipType = dr.GetValue(6).ToString(),
                            FarmerName = dr.GetValue(7).ToString(),
                            LastName = dr.GetValue(8).ToString(),
                            DateOfBirthday = dr.GetValue(9).ToString(),
                            EducationLevel = dr.GetValue(10).ToString(),
                            CivilStatus = dr.GetValue(11).ToString()
                        });
                    }
                }
                else if (type == "Family")
                {
                    NewObject = new List<FamilyMember>();
                    while (dr.Read())
                    {
                        NewObject.Add(new FamilyMember
                        {
                            Identification = dr.GetInt32(0),
                            Names = dr.GetValue(1).ToString(),
                            LastNames = dr.GetValue(2).ToString(),
                            dob = dr.GetValue(3).ToString(),
                            EducationLevel = dr.GetValue(4).ToString(),
                            FarmerRelationship = dr.GetValue(5).ToString(),
                            CivilStatus = dr.GetValue(6).ToString(),
                            FarmerIdentification = dr.GetInt32(7)
                        });
                    }
                }
                else if (type == "Invoices")
                {
                    NewObject = new List<Invoice>();
                    while (dr.Read())
                    {
                        NewObject.Add(new Invoice
                        {
                            FarmerIdentification = dr.GetInt32(0),
                            Weight = dr.GetInt32(1),
                            Date = dr.GetValue(2).ToString(),
                            InvoiceNumber = dr.GetInt32(3),
                            Value = dr.GetInt32(4),
                            DateInvoice = dr.GetValue(5).ToString(),
                            Ubication = dr.GetInt32(6),
                            Hold = dr.GetInt32(7),
                            Cash = dr.GetInt32(8),
                            BaseKg = dr.GetInt32(9),
                            CoffeeTypeId = dr.GetInt32(10),
                        });
                    }
                }
                else if (type == "Fertilizers")
                {
                    NewObject = new List<Fertilizer>();
                    while (dr.Read())
                    {
                        NewObject.Add(new Fertilizer
                        {
                            Name = dr.GetValue(0).ToString(),
                            Date = dr.GetValue(1).ToString(),
                            InvoiceNumber = dr.GetInt32(2),
                            FarmerIdentification = dr.GetInt32(3),
                            Ubication = dr.GetInt32(4),
                            Value = dr.GetInt32(5),
                            Hold = dr.GetInt32(6),
                            CashRegister = dr.GetInt32(7),
                            UnitPrice = dr.GetInt32(8),
                            Quantity = dr.GetInt32(9),
                        });
                    }
                }
            }
            finally
            {
                dr.Close();
            }
            conn.Close();
            conn.Dispose();
            return NewObject;
        }

        static private string GetConnectionString()
        {
            return "User Id=efarming;Password=1234567;Data Source=(DESCRIPTION=" +
                "(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.17.74)(PORT=1521))" +
                "(CONNECT_DATA=(SERVICE_NAME=orcl.coocentral.local)));";
        }
    }
}