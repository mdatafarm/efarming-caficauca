using EFarming.Core.AdminModule.VillageAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class VillageConfiguration : BasicConfiguration<Village>
    {
        public VillageConfiguration()
        {
            this.Property(m => m.Name).IsRequired().HasMaxLength(64);
            this.Property(m => m.MunicipalityId).IsRequired();
            this.HasRequired(m => m.Municipality);
            this.ToTable("villages");
        }
    }
}
