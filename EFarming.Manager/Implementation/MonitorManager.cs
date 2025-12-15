using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Manager.Contract;

namespace EFarming.Manager.Implementation
{
    /// <summary>
    /// Monitor Manager
    /// </summary>
    public class MonitorManager : IMonitorManager
    {
        /// <summary>
        /// The _repository
        /// </summary>
        private IUserRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MonitorManager(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Determines whether [is database up].
        /// </summary>
        /// <returns>
        /// boot with the state of the database
        /// </returns>
        public bool IsDatabaseUp()
        {
            return _repository.CheckDatabase();
        }
    }
}
