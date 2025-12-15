using EFarming.Core.TraceabilityModule.LotAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class LotConfiguration : BasicConfiguration<Lot>
    {
        public LotConfiguration()
        {
            Property(l => l.Code).IsRequired();

            ToTable("lots");
        }
    }
}
