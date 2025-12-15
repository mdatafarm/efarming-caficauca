using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Core.AdminModule.DepartmentAggregate
{
    /// <summary>
    /// Department Interface
    /// </summary>
    public interface IDepartmentRepository : IRepository<Department>
    {
        /// <summary>
        /// Gets the full data.
        /// </summary>
        /// <returns>List of Deparment</returns>
        List<Department> GetFullData();
    }
}
