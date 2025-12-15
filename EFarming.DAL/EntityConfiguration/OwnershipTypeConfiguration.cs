using EFarming.Core.AdminModule.OwnershipTypeAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class OwnershipTypeConfiguration : BasicConfiguration<OwnershipType>
    {
        public OwnershipTypeConfiguration()
        {
            this.Property(ot => ot.Name).IsRequired().HasMaxLength(32);
            this.ToTable("ownershipTypes");
        }
    }
}
