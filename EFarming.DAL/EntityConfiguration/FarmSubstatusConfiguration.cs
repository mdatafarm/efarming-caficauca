using EFarming.Core.AdminModule.FarmSubstatusAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class FarmSubstatusConfiguration : BasicConfiguration<FarmSubstatus>
    {
        public FarmSubstatusConfiguration()
        {
            this.Property(fss => fss.Name).IsRequired().HasMaxLength(32);
            this.Property(fss => fss.FarmStatusId).IsRequired();
            this.HasRequired(fss => fss.FarmStatus);
            this.ToTable("farmSubstatuses");
        }
    }
}
