using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.AuthenticationModule.AutenticationAggregate
{
    public static class UserSpecification
    {
        /// <summary>
        /// Check the login by Username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>User</returns>
        public static Specification<User> VerifyLoginInfo(string username)
        {
            Specification<User> specUser = new TrueSpecification<User>();

            if (!string.IsNullOrEmpty(username))
                specUser &= new DirectSpecification<User>(p => p.Username.Equals(username));

            specUser &= new DirectSpecification<User>(u => u.IsActive);

            return specUser;
        }

        /// <summary>
        /// Get all installation User
        /// </summary>
        /// <returns>User</returns>
        public static Specification<User> InstallationUser()
        {
            Specification<User> specUser = new TrueSpecification<User>();
            specUser &= new DirectSpecification<User>(u => u.OnInstall);
            return specUser;
        }

        /// <summary>
        /// Get all Disbled user of the system
        /// </summary>
        /// <returns>User</returns>
        public static Specification<User> DisbledUsers()
        {
            Specification<User> specUser = new TrueSpecification<User>();
            specUser &= new DirectSpecification<User>(u => !u.IsActive);
            return specUser;
        }

        /// <summary>
        /// Get all Technician Users
        /// </summary>
        /// <returns></returns>
        public static Specification<User> TechnicianUsers(){
            Specification<User> specUser = new TrueSpecification<User>();
            specUser &= new DirectSpecification<User>(u => u.Roles.Select(r => r.Id).Contains(Role.TechnicianId));
            return specUser;
        }

        public static Specification<User> TasterUsers()
        {
            Specification<User> specUser = new TrueSpecification<User>();
            specUser &= new DirectSpecification<User>(u => u.Roles.Select(r => r.Id).Contains(Role.TasterId));
            return specUser;
        }

        /// <summary>
        /// Get all Admin Users
        /// </summary>
        /// <returns></returns>
        public static Specification<User> AdminUsers() {
            Specification<User> specUser = new TrueSpecification<User>();
            specUser &= new DirectSpecification<User>(u => u.Roles.Select(r => r.Id).Contains(Role.AdminId));
            return specUser;
        }

        /// <summary>
        /// Get all Sunstentability Users
        /// </summary>
        /// <returns></returns>
        public static Specification<User> SustentabilityUsers() {
            Specification<User> specUser = new TrueSpecification<User>();
            specUser &= new DirectSpecification<User>(u => u.Roles.Select(r => r.Id).Contains(Role.SustainabilityId));
            return specUser;
        }

        /// <summary>
        /// Get all Report Users
        /// </summary>
        /// <returns></returns>
        public static Specification<User> ReportsUsers() {
            Specification<User> specUser = new TrueSpecification<User>();
            specUser &= new DirectSpecification<User>(u => u.Roles.Select(r => r.Id).Contains(Role.ReportsId));
            return specUser;
        }

        /// <summary>
        /// Get all Project users.
        /// </summary>
        /// <returns></returns>
        public static Specification<User> ProjectUsers()
        {
            Specification<User> specUser = new TrueSpecification<User>();
            specUser &= new DirectSpecification<User>(u => u.Roles.Select(r => r.Id).Contains(Role.ProjectId));
            return specUser;
        }

        /// <summary>
        /// Get all the users of the system
        /// </summary>
        /// <returns></returns>
        public static Specification<User> AllUsers(){ 
            Specification<User> allUsers = new TrueSpecification<User>();
            allUsers &= new DirectSpecification<User>(u => u.IsActive == true );
            return allUsers;
        }

        /// <summary>
        /// Get all the active User of the system
        /// </summary>
        /// <returns></returns>
        public static Specification<User> AllActiveUsers(){
            Specification<User> allActiveUsers = new TrueSpecification<User>();
            allActiveUsers &= new DirectSpecification<User>(u => u.IsActive == true);
            return allActiveUsers;
        }
    }
}
