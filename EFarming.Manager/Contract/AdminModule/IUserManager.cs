using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.ProjectModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.Manager.Contract.AdminModule
{
    /// <summary>
    /// UserManager Interface
    /// </summary>
    public interface IUserManager : IAdminManager<UserDTO, User>
    {
        /// <summary>
        /// Verifies the login.
        /// </summary>
        /// <param name="loginDTO">The login dto.</param>
        /// <returns>UserDTO</returns>
        UserDTO VerifyLogin(LoginDTO loginDTO);

        /// <summary>
        /// Initials the configuration.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <returns>bool</returns>
        bool InitialConfiguration(UserDTO userDTO);

        /// <summary>
        /// Adds the role.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <param name="roleDTO">The role dto.</param>
        /// <returns></returns>
        bool AddRole(UserDTO userDTO, RoleDTO roleDTO);
        /// <summary>
        /// Removes the role.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <param name="roleDTO">The role dto.</param>
        /// <returns></returns>
        bool RemoveRole(UserDTO userDTO, RoleDTO roleDTO);

        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <param name="projectDTO">The project dto.</param>
        /// <returns></returns>
        bool AddProject(UserDTO userDTO, ProjectDTO projectDTO);

        /// <summary>
        /// Removes the project.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <param name="projectDTO">The project dto.</param>
        /// <returns></returns>
        bool RemoveProject(UserDTO userDTO, ProjectDTO projectDTO);

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <returns>ICollection RoleDTO</returns>
        ICollection<RoleDTO> GetRoles();

        /// <summary>
        /// Gets the Projects.
        /// </summary>
        /// <returns>ICollection ProjectsDTO</returns>
        ICollection<ProjectDTO> GetProjects();


        /// <summary>
        /// Gets the technicians.
        /// </summary>
        /// <returns>ICollection UserDTO</returns>
        ICollection<UserDTO> GetTechnicians();

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>ICollection UserDTO</returns>
        ICollection<UserDTO> GetAllUsers();

        ICollection<UserDTO> GetTasters();

        /// <summary>
        /// Determines whether this instance is configured.
        /// </summary>
        /// <returns>bool</returns>
        bool IsConfigured();

        /// <summary>
        /// Counts the pending.
        /// </summary>
        /// <returns>int</returns>
        int CountPending();

        /// <summary>
        /// Approves the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        void Approve(Guid Id);

        /// <summary>
        /// Denies the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Deny(Guid id);
    }
}
