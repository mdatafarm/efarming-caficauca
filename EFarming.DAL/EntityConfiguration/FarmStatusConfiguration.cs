using EFarming.Core.AdminModule.FarmStatusAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class FarmStatusConfiguration : BasicConfiguration<FarmStatus>
    {
        public FarmStatusConfiguration()
        {
            this.Property(fs => fs.Name).HasMaxLength(32).IsRequired();
            this.HasMany(fs => fs.FarmSubstatuses);
            this.ToTable("farmStatuses");
        }
    }
}
