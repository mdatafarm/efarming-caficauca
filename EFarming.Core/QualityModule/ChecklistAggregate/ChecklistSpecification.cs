using EFarming.Core.Specification;
using EFarming.Core.Specification.Implementation;
using System;

namespace EFarming.Core.QualityModule.ChecklistAggregate
{
    public static class ChecklistSpecification
    {
        public static Specification<Checklist> ByFarm(Guid id)
        {
            Specification<Checklist> spec = new TrueSpecification<Checklist>();

            spec &= new DirectSpecification<Checklist>(c => c.FarmId.Equals(id));
            
            return spec;
        }
    }
}
