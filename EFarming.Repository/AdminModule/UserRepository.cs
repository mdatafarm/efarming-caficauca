using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFarming.Repository.AdminModule
{
    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public UserRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
