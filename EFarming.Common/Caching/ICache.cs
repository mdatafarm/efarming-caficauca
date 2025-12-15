namespace EFarming.Common.Caching
{
    /// <summary>
    /// This interface handles all the data that is going to be
    /// cached
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Store a new object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Identifier of the object</param>
        /// <param name="toBeCached">Object to be stored in cache</param>
        void Set<T>(string key, T toBeCached);

        /// <summary>
        /// Gets a stored object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">Identifier of the object</param>
        /// <returns>
        /// Object stored
        /// </returns>
        T Get<T>(string key);

        /// <summary>
        /// Removes an object identified by the jkey
        /// </summary>
        /// <param name="key">Identifier of the object</param>
        void Delete(string key);
    }
}
