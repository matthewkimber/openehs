using System.Collections.Generic;
using OpenEhs.Domain;

namespace OpenEhs.Web.Models
{
    /// <summary>
    /// Role View Model allows you to store a list of roles
    /// </summary>
    public class RoleViewModel
    {
        /// <summary>
        /// List of all roles
        /// </summary>
        public IList<Role> Roles { get; set; }
    }
}