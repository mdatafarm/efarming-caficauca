using AutoMapper;
using EFarming.DTO.AdminModule;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Manager.Implementation.AdminModule;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EFarming.Web.Areas.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PendingUsersController : ApiController
    {
        /// <summary>
        /// The _manager
        /// </summary>
        private IUserManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="PendingUsersController"/> class.
        /// </summary>
        /// <param name="manager">The manager.</param>
        public PendingUsersController(UserManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ICollection<UserDTO> Index()
        {
            return _manager.GetAll(UserSpecification.DisbledUsers(), u => u.LastName);
        }

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPut]
        public ICollection<UserDTO> Edit(Guid id)
        {
            _manager.Approve(id);
            return _manager.GetAll(UserSpecification.DisbledUsers(), u => u.LastName);
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        public ICollection<UserDTO> Delete(Guid id)
        {
            _manager.Deny(id);
            return _manager.GetAll(UserSpecification.DisbledUsers(), u => u.LastName);
        }
    }
}
