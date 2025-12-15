namespace EFarming.Core.AdminModule.FarmStatusAggregate
{
    /// <summary>
    /// FarmStatusFactory
    /// </summary>
    public static class FarmStatusFactory
    {
        /// <summary>
        /// Farms the status.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static FarmStatus FarmStatus(string name)
        {
            return new FarmStatus { Name = name };
        }
    }
}
