using EFarming.Core.AdminModule.FloweringPeriodQualificationAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class FloweringPeriodQualificationConfiguration : BasicConfiguration<FloweringPeriodQualification>
    {
        public FloweringPeriodQualificationConfiguration()
        {
            this.Property(fpq => fpq.Name).IsRequired().HasMaxLength(32);
            this.ToTable("FloweringPeriodQualifications");
        }
    }
}
