using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.AdminModule.DepartmentAggregate
{
    /// <summary>
    /// Department Factory
    /// </summary>
    public static class DepartmentFactory
    {
        /// <summary>
        /// Departments the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Name</returns>
        public static Department Department(string name)
        {
            return new Department
            {
                Name = name
            };
        }
    }
}
