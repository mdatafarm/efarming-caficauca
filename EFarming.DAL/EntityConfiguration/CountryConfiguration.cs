using EFarming.Core.AdminModule.CountryAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class CountryConfiguration : BasicConfiguration<Country>
    {
        public CountryConfiguration()
        {
            this.Property(c => c.Name).IsRequired().HasMaxLength(64);
            this.HasMany(c => c.Suppliers)
                .WithRequired(s => s.Country)
                .HasForeignKey(s => s.CountryId);
            this.ToTable("countries");
        }
    }
}
