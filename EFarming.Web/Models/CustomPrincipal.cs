using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace EFarming.Web.Models
{
    public class CustomPrincipal : IPrincipal
    {


        public const string TASTER = "Taster";
        public const string QUALITY = "Quality";

        public const string ADMIN = "Admin";
        public const string TECHNICIAN = "Technician";
        public const string SUSTAINABILITY = "Sustainability";
        public const string REPORTS = "Reports";
        public const string PROJECT = "Project";
        public const string READER = "Reader";

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }

        public IIdentity Identity
        {
            get;
            private set;
        }

        public string FullName
        {
            get { return string.Concat(FirstName, " ", LastName); }
        }

        public bool IsInRole(string role)
        {
            return Roles.Any(r => role.Contains(r));
        }

        public CustomPrincipal(string userName)
        {
            this.Identity = new GenericIdentity(userName);
        }

        public bool IsAdmin()
        {
            return IsInRole(ADMIN);
        }

        public bool IsTechnician()
        {
            return IsInRole(TECHNICIAN);
        }

        public bool IsTaster()
        {
            return IsInRole(TASTER);
        }

        public bool IsSustainability()
        {
            return IsInRole(SUSTAINABILITY);
        }

        public bool IsReports()
        {
            return IsInRole(REPORTS);
        }

        public bool IsProject()
        {
            return IsInRole(PROJECT);
        }

        public bool IsAdminAndOther()
        {
            return IsInRole(ADMIN) && Roles.Count() > 1;
        }

        public bool IsQuality()
        {
            return IsInRole(QUALITY);
        }

        public bool IsReader()
        {
            return IsInRole(READER);
        }
    }
}