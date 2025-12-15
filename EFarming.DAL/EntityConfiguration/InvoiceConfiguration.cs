using EFarming.Core.TraceabilityModule.InvoicesAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.DAL.EntityConfiguration
{
    class InvoiceConfiguration : BasicConfiguration<Invoice>
    {
        public InvoiceConfiguration()
        {
            Property(i => i.Identification).IsRequired().HasMaxLength(32);
            Property(i => i.DateInvoice).IsRequired();
            Property(i => i.InvoiceNumber).IsRequired();

            HasMany(i => i.SensoryProfileAssessments)
                .WithOptional(spa => spa.Invoice)
                .HasForeignKey(spa => spa.InvoiceId);

            ToTable("invoices");
        }
    }
}
