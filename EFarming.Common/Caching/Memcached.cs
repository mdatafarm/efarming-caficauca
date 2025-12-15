using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace EFarming.Common.Caching
{
    /// <summary>
    /// Memcached
    /// </summary>
    public sealed class Memcached : ICache
    {
        /// <summary>
        /// Store a new object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Identifier of the object</param>
        /// <param name="toBeCached">Object to be stored in cache</param>
        public void Set<T>(string key, T toBeCached)
        {
            using (MemcachedClient client = new MemcachedClient())
            {
                client.Store(StoreMode.Set, key, toBeCached);
            }
        }

        /// <summary>
        /// Gets a stored object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Identifier of the object</param>
        /// <returns>
        /// Object stored
        /// </returns>
        public T Get<T>(string key)
        {
            T obj;
            using (MemcachedClient client = new MemcachedClient())
            {
                obj = client.Get<T>(key);
            }
            return obj;
        }

        /// <summary>
        /// Removes an object identified by the jkey
        /// </summary>
        /// <param name="key">Identifier of the object</param>
        public void Delete(string key)
        {
            using (MemcachedClient client = new MemcachedClient())
            {
                client.Remove(key);
            }
        }
    }
}
