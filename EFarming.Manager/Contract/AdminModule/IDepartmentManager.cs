using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.APIModule;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// DepartmentManager Interface
    /// </summary>
    public interface IDepartmentManager : IAdminManager<DepartmentDTO, Department>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>ICollection DepartmentDTO</returns>
        ICollection<DepartmentDTO> GetAll<KProperty>(string filter, Expression<Func<Department, KProperty>> orderByExpression);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="orderByExpression"></param>
        /// <returns>ICollection DepartmentDTO</returns>
        ICollection<DepartmentDTO> GetAll<KProperty>(Expression<Func<Department, KProperty>> orderByExpression);

        /// <summary>
        /// Counts the farms.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns>int</returns>
        int CountFarms(Guid departmentId);

        /// <summary>
        /// Counts the hectares.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns>double</returns>
        double CountHectares(Guid departmentId);

        /// <summary>
        /// Loads the full data.
        /// </summary>
        /// <returns>ICollection Department</returns>
        ICollection<DepartmentAPIDTO> LoadFullData() ;
    }
}
