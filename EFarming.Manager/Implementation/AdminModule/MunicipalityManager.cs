using AutoMapper;
using EFarming.Core.AdminModule.MunicipalityAggregate;
using EFarming.Core.Specification.Implementation;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// Municipality Manager
    /// </summary>
    public class MunicipalityManager : AdminManager<MunicipalityDTO, MunicipalityRepository, Municipality>, IMunicipalityManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IMunicipalityRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MunicipalityManager"/> class.
        /// </summary>
        /// <param name="municipalityRepository">The municipality repository.</param>
        public MunicipalityManager(MunicipalityRepository municipalityRepository) : base(municipalityRepository)
        {
            _repository = municipalityRepository;
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="filterSpecification"></param>
        /// <param name="orderByExpression"></param>
        /// <returns>
        /// ICollection
        /// </returns>
        public ICollection<MunicipalityDTO> GetAll<KProperty>(Specification<Municipality> filterSpecification, Expression<Func<Municipality, KProperty>> orderByExpression)
        {
            var result = _repository
                .AllMatching(filterSpecification)
                .OrderBy(orderByExpression);
            return Mapper.Map<ICollection<MunicipalityDTO>>(result);
        }
    }
}
