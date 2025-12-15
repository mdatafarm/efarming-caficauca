namespace EFarming.Core.AdminModule.PlantationStatusAggregate
{
    /// <summary>
    /// PlantationStatus Factory
    /// </summary>
    public static class PlantationStatusFactory
    {
        /// <summary>
        /// Plantations the status.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static PlantationStatus PlantationStatus(string name)
        {
            return new PlantationStatus { Name = name };
        }
    }
}
