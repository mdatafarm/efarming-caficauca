namespace EFarming.Core.AdminModule.SoilTypeAggregate
{
    /// <summary>
    /// SoilType Factory
    /// </summary>
    public static class SoilTypeFactory
    {
        /// <summary>
        /// Soils the type.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static SoilType SoilType(string name)
        {
            return new SoilType { Name = name };
        }
    }
}
