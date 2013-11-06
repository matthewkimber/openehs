/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-19-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Role represents a role a user can have
    /// </summary>
    public class Role : IEntity, IComparable<Role>
    {
        #region Properties

        /// <summary>
        /// Id of the Role
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Name of the Role
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Description of the Role
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Users that are in this role
        /// </summary>
        public virtual IList<User> Users { get; private set; }

        /// <summary>
        /// Date the role was created
        /// </summary>
        public virtual DateTime DateCreated { get; set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Role()
            : this(String.Empty, String.Empty, DateTime.Now)
        {}

        /// <summary>
        /// Constructor that accepts details about the role
        /// </summary>
        /// <param name="name">Name of the role</param>
        /// <param name="description">Description of the role</param>
        /// <param name="dateCreated">Date the role was created</param>
        public Role(string name, string description, DateTime dateCreated)
        {
            Name = name;
            Description = description;
            Users = new List<User>();
            DateCreated = dateCreated;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Add user to this role
        /// </summary>
        /// <param name="user">User to add to the role</param>
        public virtual void AddUser(User user)
        {
            Users.Add(user);
        }

        /// <summary>
        /// Add a list of users to this role
        /// </summary>
        /// <param name="users">List of users to be added to this role</param>
        public virtual void AddUsers(IList<User> users)
        {
            foreach(var user in users)
            {
                AddUser(user);
            }
        }

        /// <summary>
        /// Remove user from this role
        /// </summary>
        /// <param name="user">User to remove from this role</param>
        public virtual void RemoveUser(User user)
        {
            Users.Remove(user);
        }

        /// <summary>
        /// Remove list of users from the users in this role
        /// </summary>
        /// <param name="users">List of users to remove from this role</param>
        public virtual void RemoveUsers(IList<User> users)
        {
            foreach(var user in users)
            {
                RemoveUser(user);
            }
        }

        #endregion

        /// <summary>
        /// Compare to another role. Performs comparison based on the name of the role
        /// </summary>
        /// <param name="other">Role to compare this role to</param>
        /// <returns>TODO:add the return</returns>
        public virtual int CompareTo(Role other)
        {
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Gets the string version of the role (which is the name of the role)
        /// </summary>
        /// <returns>Name of the role</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}