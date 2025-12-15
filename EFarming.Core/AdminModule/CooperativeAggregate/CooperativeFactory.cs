namespace EFarming.Core.AdminModule.CooperativeAggregate
{
    /// <summary>
    /// 
    /// </summary>
    public static class CooperativeFactory
    {
        /// <summary>
        /// Cooperatives the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Cooperative Cooperative(string name)
        {
            return new Cooperative
            {
                Name = name
            };
        }
    }
}
