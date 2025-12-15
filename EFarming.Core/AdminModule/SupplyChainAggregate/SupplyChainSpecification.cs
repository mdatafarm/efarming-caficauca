using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.AdminModule.SupplyChainAggregate
{
    /// <summary>
    /// SuppluChain Specification
    /// </summary>
    public static class SupplyChainSpecification
    {
        /// <summary>
        /// Filters the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns>the result</returns>
        public static Specification<SupplyChain> Filter(string name, Guid? supplierId)
        {
            Specification<SupplyChain> filter = new TrueSpecification<SupplyChain>();
            if (!string.IsNullOrEmpty(name))
            {
                filter &= new DirectSpecification<SupplyChain>(sc => sc.Name.ToUpper().Contains(name.ToUpper()));
            }
            if (supplierId.HasValue && supplierId.Value != Guid.Empty)
            {
                filter &= new DirectSpecification<SupplyChain>(sc => sc.SupplierId.Equals(supplierId.Value));
            }
          
            return filter;
        }
        public static Specification<SupplyChain> FilterByAll(string name, Guid? supplierId, Guid departmentId)
        {
            Specification<SupplyChain> filter = new TrueSpecification<SupplyChain>();
            if (!string.IsNullOrEmpty(name))
            {
                filter &= new DirectSpecification<SupplyChain>(sc => sc.Name.ToUpper().Equals(name.ToUpper()));
            }
            if (supplierId.HasValue && supplierId.Value != Guid.Empty)
            {
                filter &= new DirectSpecification<SupplyChain>(sc => sc.SupplierId.Equals(supplierId.Value));
            }
            if (departmentId != null && Guid.Empty != departmentId)
            {
                filter &= new DirectSpecification<SupplyChain>(sc => sc.DepartmentId.Equals(departmentId));
            }
            return filter;
        }
    }
}
