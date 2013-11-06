using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Security;
using OpenEhs.Data;
using OpenEhs.Domain;

namespace OpenEhs.Infrastructure.Security
{
    /// <summary>
    /// OpenEHS Role Provider enforces the security of this project
    /// </summary>
    public class OpenEhsRoleProvider : RoleProvider
    {
        #region Fields

        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        #endregion


        #region Properties

        public override string ApplicationName
        {
            get
            {
                return System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            set
            {}
        }

        #endregion


        #region Constructor

        public OpenEhsRoleProvider()
        {
            _roleRepository = new RoleRepository();
            _userRepository = new UserRepository();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Check if a given username is in a given role
        /// </summary>
        /// <param name="username">username to check</param>
        /// <param name="roleName">role to check if username is in</param>
        /// <returns></returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            var role = _roleRepository.Get(roleName);

            if (role == null)
                throw new ProviderException("Role does not exist.");

            return role.Users.Any(user => user.Username == username);
        }

        /// <summary>
        /// Get array of roles that a user is in with a given username
        /// </summary>
        /// <param name="username">username to get the roles for</param>
        /// <returns>array of roles</returns>
        public override string[] GetRolesForUser(string username)
        {
            var userRepository = new UserRepository();
            var user = userRepository.Get(username);

            var roles = user.Roles.Select(role => role.Name).ToList();

            return roles.ToArray();
        }

        /// <summary>
        /// Create a new role
        /// </summary>
        /// <param name="roleName">name of the role</param>
        public override void CreateRole(string roleName)
        {
            var role = new Role {Name = roleName, Description = String.Empty, DateCreated = DateTime.Now};
            _roleRepository.Add(role);
        }

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="roleName">name of role to delete</param>
        /// <param name="throwOnPopulatedRole">whether or not to throw an exception if the role to delete has users with that role</param>
        /// <returns></returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            var role = _roleRepository.Get(roleName);

            if (!RoleExists(roleName))
                throw new ProviderException("Role does not exist.");

            if (role.Users.Count > 0)
                if (throwOnPopulatedRole)
                    throw new ProviderException("Cannot delete a populated role.");

            _roleRepository.Remove(role);

            return true;
        }

        /// <summary>
        /// Check if role exists
        /// </summary>
        /// <param name="roleName">role to check</param>
        /// <returns></returns>
        public override bool RoleExists(string roleName)
        {
            return _roleRepository.Get(roleName) != null;
        }

        /// <summary>
        /// Add given roles to the users with the given usernames
        /// </summary>
        /// <param name="usernames">users to get the roles</param>
        /// <param name="roleNames">roles to give to the users</param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            var roles = FillRoles(roleNames);
            var users = FillUsers(usernames);

            foreach (var role in roles)
            {
                role.AddUsers(users);
            }
        }

        /// <summary>
        /// Remove given roles from the users with the given usernames
        /// </summary>
        /// <param name="usernames">users to remove the roles from</param>
        /// <param name="roleNames">roles to remove from the users</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            var users = FillUsers(usernames);
            var roles = FillRoles(roleNames);

            foreach(var role in roles)
            {
                role.RemoveUsers(users);
            }
        }

        /// <summary>
        /// Get users that are in the role passed in
        /// </summary>
        /// <param name="roleName">role to check</param>
        /// <returns></returns>
        public override string[] GetUsersInRole(string roleName)
        {
            var role = _roleRepository.Get(roleName);

            return role.Users.Select(user => user.Username).ToList().ToArray();
        }

        /// <summary>
        /// Get all roles that exist
        /// </summary>
        /// <returns></returns>
        public override string[] GetAllRoles()
        {
            var roles = _roleRepository.GetAll();

            return roles.Select(role => role.Name).ToList().ToArray();
        }

        // TODO: Get this one figured out.
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }


        #region Private Methods

        /// <summary>
        /// Create roles, if they don't already exist
        /// </summary>
        /// <param name="roleNames">roles to create</param>
        /// <returns></returns>
        private IList<Role> FillRoles(string[] roleNames)
        {
            var roles = new List<Role>();

            foreach (var name in roleNames)
            {
                var role = _roleRepository.Get(name);

                if (role != null)
                    roles.Add(role);
            }

            return roles;
        }

        /// <summary>
        /// Create users, if they don't already exist
        /// </summary>
        /// <param name="usernames">usernames to create</param>
        /// <returns></returns>
        private IList<User> FillUsers(string[] usernames)
        {
            var users = new List<User>();

            foreach (var name in usernames)
            {
                var user = _userRepository.Get(name);

                if (user != null)
                    users.Add(user);
            }

            return users;
        }

        #endregion

        #endregion
    }
}
