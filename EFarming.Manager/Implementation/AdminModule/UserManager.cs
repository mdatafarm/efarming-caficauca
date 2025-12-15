using AutoMapper;
using EFarming.Common.Encription;
using EFarming.Core.AuthenticationModule.AutenticationAggregate;
using EFarming.Core.ProjectModule.ProjectAggregate;
using EFarming.DTO.AdminModule;
using EFarming.DTO.ProjectModule;
using EFarming.Manager.Contract;
using EFarming.Manager.Contract.AdminModule;
using EFarming.Repository.AdminModule;
using EFarming.Repository.ProjectModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFarming.Manager.Implementation.AdminModule
{
    /// <summary>
    /// User Manager
    /// </summary>
    public class UserManager : AdminManager<UserDTO, UserRepository, User>, IUserManager
    {
        /// <summary>
        /// The _user repository
        /// </summary>
        private IUserRepository _userRepository;
        /// <summary>
        /// The _role repository
        /// </summary>
        private IRoleRepository _roleRepository;
        /// <summary>
        /// The _project repository
        /// </summary>
        private IProjectRepository _projectRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        public UserManager(UserRepository userRepository, RoleRepository roleRepository, ProjectRepository projectRepository) : base(userRepository){
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// Metodo para verificar los usuarios
        /// </summary>
        /// <param name="loginDTO">DTO entidad de Login</param>
        /// <returns>
        /// UserDTO
        /// </returns>
        public UserDTO VerifyLogin(LoginDTO loginDTO)
        {
            var user = _userRepository.GetFiltered(UserSpecification.VerifyLoginInfo(loginDTO.Username).SatisfiedBy()).FirstOrDefault();          
	  
	  if (user != null)
            {
	      (_userRepository as UserRepository).UOW.Refresh(user);

                var password = EncriptorFactory.CreateEncriptor().HashPassword(loginDTO.Password, user.Salt);
                
	      if (!password.Equals(user.Password))                
                    user = null;
                
            }
            return Mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="entityDTO">Entity UserDTO To save</param>
        /// <returns>
        /// bool state creation
        /// </returns>
        public override bool Create(UserDTO entityDTO)
        {
            try
            {
                entityDTO.Roles.Add(new RoleDTO { Id = entityDTO.Role });
                User entity = Mapper.Map<User>(entityDTO);
                entity.IsActive = false;
                entity.Username = entity.Email;
                entity.Salt = EncriptorFactory.CreateEncriptor().GenerateSalt();
                entity.Password = EncriptorFactory.CreateEncriptor().HashPassword(entity.Password, entity.Salt);
                entity.Roles.Clear();

                foreach (var role in entityDTO.Roles){
                    entity.Roles.Add(_roleRepository.Get(role.Id));
                }

                _userRepository.Add(entity);
                _userRepository.UnitOfWork.Commit();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Edits the specified entity dto.
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public override bool Edit(UserDTO entityDTO)
        {
            try
            {
                var edited = false;
                var persisted = _userRepository.Get(entityDTO.Id);
                
                entityDTO.Username = persisted.Username;
                entityDTO.Email = persisted.Email;
                entityDTO.Password = persisted.Password;
                entityDTO.Salt = persisted.Salt;
                entityDTO.IsActive = persisted.IsActive;

                var actualPassword = EncriptorFactory.CreateEncriptor().HashPassword(entityDTO.OldPassword, persisted.Salt);

                if (actualPassword.Equals(persisted.Password))
                {
                    if (!string.IsNullOrEmpty(entityDTO.NewPassword) && !string.IsNullOrEmpty(entityDTO.ConfirmPassword))
                    {
                        entityDTO.Salt = EncriptorFactory.CreateEncriptor().GenerateSalt();
                        entityDTO.Password = EncriptorFactory.CreateEncriptor().HashPassword(entityDTO.NewPassword, entityDTO.Salt);
                    }
                    edited = true;
                }
                
                var entity = Mapper.Map<User>(entityDTO);
                _userRepository.Merge(persisted, entity);
                _userRepository.UnitOfWork.Commit();
                return edited;
            }
            catch { return false; }
        }

        /// <summary>
        /// Adds the role to the specific user
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <param name="entityRoleDTO">The entity role dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool AddRole(UserDTO entityDTO, RoleDTO entityRoleDTO)
        {
            try
            {
                var persisted = _userRepository.Get(entityDTO.Id);
                var role = _roleRepository.Get(entityRoleDTO.Id);
                persisted.Roles.Add(role);

                var entity = Mapper.Map<User>(entityDTO);
                _userRepository.UnitOfWork.Commit();
                return true;
            }
            catch { return false; }
        }
        
        /// <summary>
        /// Removes the role to the specific user
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <param name="entityRoleDTO">The entity role dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool RemoveRole(UserDTO entityDTO, RoleDTO entityRoleDTO)
        {
            try
            {
                var persisted = _userRepository.Get(entityDTO.Id);
                var role = _roleRepository.Get(entityRoleDTO.Id);
                persisted.Roles.Remove(role);

                var entity = Mapper.Map<User>(entityDTO);
                _userRepository.UnitOfWork.Commit();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Adds the project to the specific user
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <param name="entityProjectDTO">The entity project dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool AddProject(UserDTO entityDTO, ProjectDTO entityProjectDTO)
        {
            try
            {
                var persisted = _userRepository.Get(entityDTO.Id);
                var project = _projectRepository.Get(entityProjectDTO.Id);
                persisted.Projects.Add(project);

                var entity = Mapper.Map<User>(entityDTO);
                _userRepository.UnitOfWork.Commit();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Removes the Project to the specific user
        /// </summary>
        /// <param name="entityDTO">The entity dto.</param>
        /// <param name="entityProjectDTO">The entity Project dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool RemoveProject(UserDTO entityDTO, ProjectDTO entityProjectDTO)
        {
            try
            {
                var persisted = _userRepository.Get(entityDTO.Id);
                var project = _projectRepository.Get(entityProjectDTO.Id);
                persisted.Projects.Remove(project);

                var entity = Mapper.Map<User>(entityDTO);
                _userRepository.UnitOfWork.Commit();
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns>
        /// ICollection RoleDTO
        /// </returns>
        public ICollection<RoleDTO> GetRoles() {
            return Mapper.Map<ICollection<RoleDTO>>(_roleRepository.GetAll());
        }

        /// <summary>
        /// Get All Project
        /// </summary>
        /// <returns>
        /// ICollection ProjectDTO
        /// </returns>
        public ICollection<ProjectDTO> GetProjects()
        {
            return Mapper.Map<ICollection<ProjectDTO>>(_projectRepository.GetAll());
        }

        /// <summary>
        /// Initials the configuration of system.
        /// </summary>
        /// <param name="userDTO">The user dto.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool InitialConfiguration(UserDTO userDTO)
        {
            try
            {
                userDTO.Roles.Add(new RoleDTO { Id = userDTO.Role });
                User entity = Mapper.Map<User>(userDTO);
                entity.OnInstall = true;
                entity.IsActive = true;
                entity.Username = entity.Email;
                entity.Salt = EncriptorFactory.CreateEncriptor().GenerateSalt();
                entity.Password = EncriptorFactory.CreateEncriptor().HashPassword(entity.Password, entity.Salt);
                entity.Roles.Clear();

                foreach (var role in userDTO.Roles)
                {
                    entity.Roles.Add(_roleRepository.Get(role.Id));
                }

                _userRepository.Add(entity);
                _userRepository.UnitOfWork.Commit();
                return true;
            }
            catch { return false; }
        }


        /// <summary>
        /// Determines whether this instance is configured.
        /// </summary>
        /// <returns>
        /// bool
        /// </returns>
        public bool IsConfigured(){
            return _userRepository.AllMatching(UserSpecification.InstallationUser()).Count() > 0;
        }

        /// <summary>
        /// Count the number of pending users to accept in the system.
        /// </summary>
        /// <returns>
        /// int with the pending users
        /// </returns>
        public int CountPending(){ return _userRepository.AllMatching(UserSpecification.DisbledUsers()).Count(); }


        /// <summary>
        /// Approve User
        /// </summary>
        /// <param name="id">User Id</param>
        public void Approve(Guid id)
        {
            var user = _userRepository.Get(id);
            user.IsActive = true;
            _userRepository.Modify(user);
            _userRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Deny User
        /// </summary>
        /// <param name="id">Denegar determinado Usuario</param>
        public void Deny(Guid id)
        {
            var user = _userRepository.Get(id);            
	  _userRepository.Destroy(user);
            _userRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Get all Admin Users
        /// </summary>
        /// <returns>ICollection UserDTO</returns>
        public ICollection<UserDTO> GetAdminUsers() { return Mapper.Map<ICollection<UserDTO>>(_userRepository.AllMatching(UserSpecification.AdminUsers())); }

        /// <summary>
        /// Get all the ActiveUsers
        /// </summary>
        /// <returns>ICollection UserDTO</returns>
        public ICollection<UserDTO> GetAllActiveUsers() { return Mapper.Map<ICollection<UserDTO>>(_userRepository.AllMatching(UserSpecification.AllActiveUsers())); }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>
        /// ICollection UserDTO
        /// </returns>
        public ICollection<UserDTO> GetAllUsers() { return Mapper.Map<ICollection<UserDTO>>(_userRepository.AllMatching(UserSpecification.AllUsers())); }

        /// <summary>
        /// Get all Technicians
        /// </summary>
        /// <returns>
        /// ICollection UserDTO
        /// </returns>
        public ICollection<UserDTO> GetTechnicians(){
            return Mapper.Map<ICollection<UserDTO>>(_userRepository.AllMatching(UserSpecification.TechnicianUsers()));
        }

        public ICollection<UserDTO> GetTasters()
        {
            return Mapper.Map<ICollection<UserDTO>>(_userRepository.AllMatching(UserSpecification.TasterUsers()));
        }

    }
}
