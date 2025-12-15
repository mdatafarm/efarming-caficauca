using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.TraceabilityModule.InvoicesAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public static class InvoiceSpecification
    {
        /// <summary>
        /// Invoiceses by receipt.
        /// </summary>
        /// <param name="receipt">The receipt.</param>
        /// <returns></returns>
        public static Specification<Invoice> InvoicesByReceipt(int InvoiceNumber)
        {
            Specification<Invoice> spec = new TrueSpecification<Invoice>();
            spec &= new DirectSpecification<Invoice>(i => i.InvoiceNumber.Equals(InvoiceNumber));
            return spec;
        }

        /// <summary>
        /// Invoiceses by farm.
        /// </summary>
        /// <param name="farmId">The farm identifier.</param>
        /// <returns></returns>
        public static Specification<Invoice> InvoicesByFarm(Guid farmId)
        {
            Specification<Invoice> spec = new TrueSpecification<Invoice>();
            spec &= new DirectSpecification<Invoice>(i => i.FarmId.Equals(farmId));
            return spec;
        }

        /// <summary>
        /// by year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns></returns>
        public static Specification<Invoice> ByYear(int year)
        {
            Specification<Invoice> spec = new TrueSpecification<Invoice>();
            spec &= new DirectSpecification<Invoice>(i => i.Date.Year.Equals(year));
            return spec;
        }

        /// <summary>
        /// Filters the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="lotId">The lot identifier.</param>
        /// <returns></returns>
        public static Specification<Invoice> Filter(DateTime? start, DateTime? end, Guid? lotId)
        {
            Specification<Invoice> spec = new TrueSpecification<Invoice>();

            if (start.HasValue)
            {
                spec &= new DirectSpecification<Invoice>(i => i.Date >= start.Value);
            }

            if (end.HasValue)
            {
                spec &= new DirectSpecification<Invoice>(i => i.Date <= end.Value);
            }

            return spec;
        }
    }
}
