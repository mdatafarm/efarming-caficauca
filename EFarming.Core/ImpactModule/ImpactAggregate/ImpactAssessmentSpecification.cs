using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.ImpactModule.ImpactAggregate
{
    /// <summary>
    /// ImpactAssessment Specification
    /// </summary>
    public static class ImpactAssessmentSpecification
    {
        /// <summary>
        /// Filters the dashboard.
        /// </summary>
        /// <param name="supplyChainId">The supply chain identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>the result</returns>
        public static Specification<ImpactAssessment> FilterDashboard(Guid? supplyChainId, Guid? supplierId, Guid? countryId)
        {
            Specification<ImpactAssessment> spec = new TrueSpecification<ImpactAssessment>();

            if (supplyChainId.HasValue && !supplyChainId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<ImpactAssessment>(ia => ia.Farm.SupplyChain.Id.Equals(supplyChainId.Value));
            }
            else if (supplierId.HasValue && !supplierId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<ImpactAssessment>(ia => ia.Farm.SupplyChain.Supplier.Id.Equals(supplierId.Value));
            }
            else if (countryId.HasValue && !countryId.Value.Equals(Guid.Empty))
            {
                spec &= new DirectSpecification<ImpactAssessment>(ia => ia.Farm.SupplyChain.Supplier.Country.Id.Equals(countryId.Value));
            }

            return spec;
        }

        /// <summary>
        /// Filters the by farm.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the result</returns>
        public static Specification<ImpactAssessment> FilterByFarm(Guid id)
        {
            Specification<ImpactAssessment> spec = new TrueSpecification<ImpactAssessment>();

            if (Guid.Empty != id)
            {
                spec &= new DirectSpecification<ImpactAssessment>(ia => ia.FarmId.Equals(id));
            }

            return spec;
        }

        /// <summary>
        /// Filters the by year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>the result</returns>
        public static Specification<ImpactAssessment> FilterByYear(int year)
        {
            Specification<ImpactAssessment> spec = new TrueSpecification<ImpactAssessment>();
            spec &= new DirectSpecification<ImpactAssessment>(ia => ia.Date.Year == year);
            return spec;
        }

        /// <summary>
        /// Tracks the by location.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static Specification<ImpactAssessment> TrackByLocation(int year)
        {
            Specification<ImpactAssessment> spec = new TrueSpecification<ImpactAssessment>();
            spec &= new DirectSpecification<ImpactAssessment>(ia => ia.Date.Year == year);
            return spec;
        }

        /// <summary>
        /// Tracks the by department.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="departmentId">The department identifier.</param>
        /// <returns>the result</returns>
        public static Specification<ImpactAssessment> TrackByDepartment(int year, Guid departmentId)
        {
            Specification<ImpactAssessment> spec = TrackByLocation(year);
            spec &= new DirectSpecification<ImpactAssessment>(ia => ia.Farm.Village.Municipality.DepartmentId.Equals(departmentId));
            return spec;
        }
    }
}
