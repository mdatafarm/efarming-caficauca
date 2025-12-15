using EFarming.Core.AdminModule.SupplierAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class SupplierConfiguration : BasicConfiguration<Supplier>
    {
        public SupplierConfiguration()
        {
            this.Property(s => s.Name).IsRequired();
            this.Property(s => s.LogoUrl).IsOptional();
            this.HasMany(s => s.SupplyChains)
                .WithRequired(sc => sc.Supplier)
                .HasForeignKey(sc => sc.SupplierId);
            this.ToTable("suppliers");
        }
    }
}
