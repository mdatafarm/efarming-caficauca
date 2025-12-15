using AutoMapper;
using EFarming.Core.AdminModule.DepartmentAggregate;
using EFarming.DTO.AdminModule;
using EFarming.Repository.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// Departments Controller
    /// </summary>
    public class DepartmentsController : ApiController
    {
        /// <summary>
        /// The _department repository
        /// </summary>
        private IDepartmentRepository _departmentRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="DepartmentsController"/> class.
        /// </summary>
        /// <param name="departmentRepository">The department repository.</param>
        public DepartmentsController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// List of departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<DepartmentDTO> Index()
        {
            return Mapper.Map<List<DepartmentDTO>>(_departmentRepository.GetAll().ToList());
        }
    }
}
