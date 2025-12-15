namespace EFarming.Common.Caching
{
    /// <summary>
    /// Interface to manage Cache factory
    /// </summary>
    public interface ICacheFactory
    {
        /// <summary>
        /// Creates a new cache object
        /// </summary>
        /// <returns>ICache object</returns>
        ICache Create();
    }
}
