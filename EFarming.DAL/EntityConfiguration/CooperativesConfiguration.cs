using EFarming.Core.AdminModule.CooperativeAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class CooperativesConfiguration : BasicConfiguration<Cooperative>
    {
        public CooperativesConfiguration()
        {
            this.Property(c => c.Name).IsRequired().HasMaxLength(32);
            this.ToTable("cooperatives");
        }
    }
}
