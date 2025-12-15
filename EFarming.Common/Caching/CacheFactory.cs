namespace EFarming.Common.Caching
{
    public static class CacheFactory
    {
        #region members
        /// <summary>
        /// The _factory
        /// </summary>
        static ICacheFactory _factory = null;
        #endregion

        #region public methods
        /// <summary>
        /// Establishes the factory to create Cache objects
        /// </summary>
        /// <param name="factory">Factory to use</param>
        public static void SetCurrent(ICacheFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Create a new cache
        /// </summary>
        /// <returns>
        /// Cache object
        /// </returns>
        public static ICache CreateCache()
        {
            return (_factory != null) ? _factory.Create() : null;
        }
        #endregion
    }
}
