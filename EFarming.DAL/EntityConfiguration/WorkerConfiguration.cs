using EFarming.Core.FarmModule.FarmAggregate;

namespace EFarming.DAL.EntityConfiguration
{
    class WorkerConfiguration : BasicConfiguration<Worker>
    {
        public WorkerConfiguration()
        {
            this.HasRequired(w => w.Farm)
                .WithOptional(f => f.Worker);
            this.ToTable("workers");
        }
    }
}
