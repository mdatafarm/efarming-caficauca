namespace EFarming.Core.ImpactModule.ImpactAggregate
{
    /// <summary>
    /// Inpact assesment Reopository Interface
    /// </summary>
    public interface IImpactAssessmentRepository : IRepository<ImpactAssessment>
    {
        void UpdateAnswers(ImpactAssessment current, ImpactAssessment persisted);
    }
}
