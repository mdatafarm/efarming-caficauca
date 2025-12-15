using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFarming.Oracle.Models
{
    public class FamilyMember
    {
        public int Identification { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public string dob { get; set; }
        public string EducationLevel { get; set; }
        public string FarmerRelationship { get; set; }
        public string CivilStatus { get; set; }
        public int FarmerIdentification { get; set; }
    }
}