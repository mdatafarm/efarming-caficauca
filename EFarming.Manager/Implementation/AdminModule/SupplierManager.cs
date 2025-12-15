using AutoMapper;
using EFarming.Core.AdminModule.SupplierAggregate;
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
    /// Supplier Manager
    /// </summary>
    public class SupplierManager : AdminManager<SupplierDTO, SupplierRepository, Supplier>, ISupplierManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private ISupplierRepository _repository;
        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public SupplierManager(SupplierRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
