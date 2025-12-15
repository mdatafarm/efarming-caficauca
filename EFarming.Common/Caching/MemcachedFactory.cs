namespace EFarming.Common.Caching
{
    /// <summary>
    /// Memcached Factory
    /// </summary>
    public class MemcachedFactory : ICacheFactory
    {
        /// <summary>
        /// Creates a new cache object
        /// </summary>
        /// <returns>
        /// ICache object
        /// </returns>
        public ICache Create()
        {
            return new Memcached();
        }
    }
}
