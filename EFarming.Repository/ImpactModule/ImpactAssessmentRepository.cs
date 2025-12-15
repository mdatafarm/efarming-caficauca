using EFarming.Common;
using EFarming.Core.ImpactModule.ImpactAggregate;
using EFarming.Core.ImpactModule.IndicatorAggregate;
using EFarming.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Repository.ImpactModule
{
    /// <summary>
    /// ImpactAssessment Repository
    /// </summary>
    public class ImpactAssessmentRepository : Repository<ImpactAssessment>, IImpactAssessmentRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImpactAssessmentRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public ImpactAssessmentRepository(UnitOfWork uow) : base(uow) { }

        /// <summary>
        /// Updates the answers.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="persisted">The persisted.</param>
        public void UpdateAnswers(ImpactAssessment current, ImpactAssessment persisted)
        {
            var added = current.Answers.Except(persisted.Answers, new EntityComparer<CriteriaOption>());
            var removed = persisted.Answers.Except(current.Answers, new EntityComparer<CriteriaOption>()).ToList();

            foreach (var item in added)
            {
                var toAdd = ((UnitOfWork)UnitOfWork).CriteriaOptions.Find(item.Id);
                persisted.Answers.Add(toAdd);
            }
            foreach (var item in removed)
            {
                var toRemove = ((UnitOfWork)UnitOfWork).CriteriaOptions.Find(item.Id);
                persisted.Answers.Remove(toRemove);
            }
        }
    }
}
