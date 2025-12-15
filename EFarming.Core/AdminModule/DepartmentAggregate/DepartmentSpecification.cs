using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.AdminModule.DepartmentAggregate
{
    /// <summary>
    /// Department Specification
    /// </summary>
    public static class DepartmentSpecification
    {
        /// <summary>
        /// Filters the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the results</returns>
        public static Specification<Department> FilterByName(string name)
        {
            Specification<Department> spec = new TrueSpecification<Department>();

            if (!string.IsNullOrEmpty(name))
                spec &= new DirectSpecification<Department>(p => p.Name.Contains(name));

            return spec;
        }
    }
}
