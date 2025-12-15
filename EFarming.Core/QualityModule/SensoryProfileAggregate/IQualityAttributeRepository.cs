namespace EFarming.Core.QualityModule.SensoryProfileAggregate
{
    /// <summary>
    /// QualityAttributeRepository Interface
    /// </summary>
    public interface IQualityAttributeRepository : IRepository<QualityAttribute>
    {
        /// <summary>
        /// Updates the attribute.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="persisted">The persisted.</param>
        void UpdateAttribute(QualityAttribute current, QualityAttribute persisted);

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="current">The current.</param>
        void RemoveAttribute(QualityAttribute current);
    }
}
