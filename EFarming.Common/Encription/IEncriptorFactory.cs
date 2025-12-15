namespace EFarming.Common.Encription
{
    /// <summary>
    /// EncriptorFactory Interface
    /// </summary>
    public interface IEncriptorFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        IEncriptor Create();
    }
}
