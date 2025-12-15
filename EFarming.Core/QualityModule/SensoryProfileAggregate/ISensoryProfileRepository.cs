namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// SensoryProfileRepository Interface
    /// </summary>
    public interface ISensoryProfileRepository : IRepository<SensoryProfileAssessment>
    {
        /// <summary>
        /// Updates the answers.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="persisted">The persisted.</param>
        void UpdateAnswers(SensoryProfileAssessment current, SensoryProfileAssessment persisted);
    }
}
