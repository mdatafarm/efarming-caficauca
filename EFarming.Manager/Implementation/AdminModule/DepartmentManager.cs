using AutoMapper;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using System.Linq.Expressions;
using EFarming.DTO.APIModule;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// Department Manager
    /// </summary>
    public class DepartmentManager : AdminManager<DepartmentDTO,DepartmentRepository, Department>, IDepartmentManager
    {
        /// <summary>
        /// The _department repository
        /// </summary>
        private IDepartmentRepository _departmentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentManager" /> class.
        /// </summary>
        /// <param name="departmentRepository">The department repository.</param>
        public DepartmentManager(DepartmentRepository departmentRepository): base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty">The type of the property.</typeparam>
        /// <param name="filter">The filter.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>
        /// ICollection DepartmentDTO
        /// </returns>
        public ICollection<DepartmentDTO> GetAll<KProperty>(string filter, Expression<Func<Department, KProperty>> orderByExpression)
        {
            var result = _departmentRepository
                .GetFiltered(d => d.Name.ToUpper().Contains(filter.ToUpper()))
                .OrderBy(orderByExpression);

            return Mapper.Map<ICollection<DepartmentDTO>>(result);
        }


        /// <summary>
        /// Counts the farms.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns>
        /// int
        /// </returns>
        public int CountFarms(Guid departmentId)
        {
            var department = _departmentRepository.Get(departmentId);
            return department.Municipalities.SelectMany(m => m.Villages).Sum(v => v.Farms.Count());
        }

        /// <summary>
        /// Counts the hectares.
        /// </summary>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns>
        /// double
        /// </returns>
        public double CountHectares(Guid departmentId)
        {
            var department = _departmentRepository.Get(departmentId);
            return department.Municipalities.SelectMany(m => m.Villages).SelectMany(v => v.Farms).Sum(f => Convert.ToDouble(f.Productivity.TotalHectares));
        }

        /// <summary>
        /// Loads the full data.
        /// </summary>
        /// <returns>
        /// ICollection Department
        /// </returns>
        public ICollection<DepartmentAPIDTO> LoadFullData()
        {
            return Mapper.Map<ICollection<DepartmentAPIDTO>>(_departmentRepository.GetFullData());
        }
    }
}
