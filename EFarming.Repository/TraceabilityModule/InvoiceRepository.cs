using EFarming.Common.SharedClasses;
using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using EFarming.DAL;
using System.Linq;

namespace EFarming.Repository.TraceabilityModule
{
    /// <summary>
    /// Invoice Repository
    /// </summary>
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public InvoiceRepository(UnitOfWork uow) : base(uow) { }
    }
}
