using EFarming.Core.AdminModule.SupplierAggregate;
using EFarming.DTO.AdminModule;
using System;
using System.Collections.Generic;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface ISupplierManager : IAdminManager<SupplierDTO, Supplier>
    {
    }
}
