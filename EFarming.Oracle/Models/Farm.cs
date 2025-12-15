using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFarming.Oracle.Models
{
    public class Farm
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Village { get; set; }
        public int MunicipalityId { get; set; }
        public string Cooperative { get; set; }
        public string Status { get; set; }
        public string OwnerShipType { get; set; }

        //Farmer information
        public string FarmerName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirthday { get; set; }
        public string EducationLevel { get; set; }
        public string CivilStatus { get; set; }
    }
}