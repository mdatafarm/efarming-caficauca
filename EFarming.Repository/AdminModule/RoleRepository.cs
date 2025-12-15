using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.DAL;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// Role Repository
    /// </summary>
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="uow">The uow.</param>
        public RoleRepository(UnitOfWork uow) : base(uow) { }
    }
}
