using EFarming.Core.AdminModule.OtherActivitiesAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class OtherActivityConfiguration : BasicConfiguration<OtherActivity>
    {
        public OtherActivityConfiguration()
        {
            this.Property(oa => oa.Name).IsRequired();
            this.ToTable("OtherActivities");
        }
    }
}
