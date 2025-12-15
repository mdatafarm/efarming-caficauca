namespace EFarming.Core.AdminModule.OwnershipTypeAggregate
{
    /// <summary>
    /// OwnerShipType Factory
    /// </summary>
    public static class OwnershipTypeFactory
    {
        /// <summary>
        /// Owners the type of the ship.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static OwnershipType OwnerShipType(string name)
        {
            return new OwnershipType
            {
                Name = name
            };
        }
    }
}
