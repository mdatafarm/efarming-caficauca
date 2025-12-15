using EFarming.Common;
using EFarming.Core.QualityModule.SensoryProfileAggregate;
using EFarming.DAL;
using System.Linq;

namespace EFarming.Repository.QualityModule
{
    /// <summary>
    /// SensoryProfile Repository
    /// </summary>
    public class SensoryProfileRepository : Repository<SensoryProfileAssessment>, ISensoryProfileRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SensoryProfileRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public SensoryProfileRepository(UnitOfWork uow) : base(uow) { }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="item">Item to delete</param>
        public override void Destroy(SensoryProfileAssessment item){
            foreach(var ans in item.SensoryProfileAnswers.ToList())
                UOW.Remove(ans);
            base.Destroy(item);
        }

        /// <summary>
        /// Metodo para Actulizar Sensory Profile Assessment // Perfil de evaluacion sensorial
        /// </summary>
        /// <param name="current">Actual SensoryProfileAssessment</param>
        /// <param name="persisted">The persisted.</param>
        public void UpdateAnswers(SensoryProfileAssessment current, SensoryProfileAssessment persisted){
            
            var added = current.SensoryProfileAnswers.Except(persisted.SensoryProfileAnswers, new EntityComparer<SensoryProfileAnswer>());
            
            var removed = persisted.SensoryProfileAnswers.Except(current.SensoryProfileAnswers, new EntityComparer<SensoryProfileAnswer>());
            
            var edited = current.SensoryProfileAnswers.Except(added, new EntityComparer<SensoryProfileAnswer>());

            added.ToList().ForEach(p => persisted.SensoryProfileAnswers.Add(p));
            
            removed.ToList().ForEach(p => UOW.SensoryProfileAnswers.Remove(p));
            
            foreach (var item in edited.ToList())
            {
                var actual = persisted.SensoryProfileAnswers.First(p => p.Id.Equals(item.Id));
                UOW.ApplyCurrentValues(actual, item);
            }
        }
    }
}
