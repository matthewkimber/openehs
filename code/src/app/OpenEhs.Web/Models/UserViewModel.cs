using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using OpenEhs.Data;
using OpenEhs.Data.Common;
using OpenEhs.Domain;

namespace OpenEhs.Web.Models
{
    /// <summary>
    /// User View Model contains the data for getting roles and assigning roles
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Paged list of users that allows you to view users in a paged list in a grid
        /// </summary>
        public PagedList<User> Users { get; set; }

        /// <summary>
        /// Currently selected role
        /// </summary>
        public Role SelectedRole { get; set; }

        /// <summary>
        /// Select list of all roles
        /// </summary>
        public SelectList Roles
        {
            get
            {
                var roles = new List<Role>(new RoleRepository().GetAll());
                roles.Sort();
                roles.Insert(0, new Role("All", "", DateTime.Now));

                return new SelectList(roles, "Id", "Name");
            }
        }

        /// <summary>
        /// Search string that is what we're searching users on
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// Constructor that recieves a paged list of users
        /// </summary>
        /// <param name="users">paged list of users</param>
        public UserViewModel(PagedList<User> users)
        {
            Users = users;
        }

        /// <summary>
        /// RolesToString generates a concatinated string of the users roles
        /// </summary>
        /// <param name="roles">the users roles</param>
        /// <returns>a concatinated string of the users roles</returns>
        public static string RolesToString(IList<Role> roles)
        {
            var result = new StringBuilder();

            for (var index = 0; index < roles.Count; index++)
            {
                if (index == roles.Count - 1)
                {
                    result.Append(roles[index].Name);
                }

                result.AppendFormat("{0}, ", roles[index].Name);
            }

            return result.ToString();
        }

    }
}