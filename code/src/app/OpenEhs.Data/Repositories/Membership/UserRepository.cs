using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using OpenEhs.Data.Common;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// the current session from the unit of work
        /// </summary>
        private ISession Session
        {
            get
            {
                return UnitOfWork.CurrentSession;
            }
        }

        /// <summary>
        /// Get a User with a given id.
        /// </summary>
        /// <param name="id">The Id of the User to be retrieved.</param>
        /// <returns></returns>
        public User Get(int id)
        {
            return Session.Get<User>(id);
        }

        /// <summary>
        /// Get a User with a given username.
        /// </summary>
        /// <param name="username">The username of the User to be retrieved.</param>
        /// <returns></returns>
        public User Get(string username)
        {
            ICriteria criteria = Session.CreateCriteria<User>()
                                        .Add(Restrictions.Eq("Username", username));

            return criteria.UniqueResult<User>();
        }

        /// <summary>
        /// Gets all the Users in the Repository.
        /// </summary>
        /// <returns>An IList containing all Users in the Repository.</returns>
        public IList<User> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<User>();

            return criteria.List<User>();
        }

        /// <summary>
        /// Gets the paged list of all the Users in the Repository.
        /// </summary>
        /// <returns>A paged list containing the page size in the Repository.</returns>
        public PagedList<User> GetPaged(int pageIndex, int pageSize)
        {
            var rowCount = Session.CreateCriteria<User>()
                .SetProjection(Projections.RowCount())
                .FutureValue<Int32>();

            ICriteria criteria = Session.CreateCriteria<User>()
                .SetFirstResult((pageIndex - 1)*pageSize)
                .AddOrder(Order.Asc("Username"))
                .SetMaxResults(pageSize);

            return new PagedList<User>(criteria.List<User>(), pageSize, pageSize, rowCount.Value);
        }

        /// <summary>
        /// Adds a User to the Repository.
        /// </summary>
        /// <param name="entity">The User to add to the Repository.</param>
        public void Add(User entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a User from the Repository.
        /// </summary>
        /// <param name="entity">The User to remove from the Repository.</param>
        public void Remove(User entity)
        {
            Session.Delete(entity);
        }

        /// <summary>
        /// Find a user with the given username and password combination
        /// </summary>
        /// <param name="username">username to search for</param>
        /// <param name="password">password to search for</param>
        /// <returns></returns>
        public IList<User> Find(string username, string password)
        {
            ICriteria criteria = Session.CreateCriteria<User>()
                                        .Add(Restrictions.Eq("Username", username))
                                        .Add(Restrictions.Eq("Password", password));

            return criteria.List<User>();
        }

        /// <summary>
        /// Check if a given username already exists in the repository
        /// </summary>
        /// <param name="username">username to check</param>
        /// <returns>a boolean that tells whether the given username is available for use.</returns>
        public bool CheckForUsernameAvailability(string username)
        {
            ICriteria criteria = Session.CreateCriteria<User>()
                .Add(Restrictions.Eq("Username", username));

            if (criteria.UniqueResult<User>() == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get last name initial for the given last name
        /// </summary>
        /// <example>
        /// Olson as the initial string would return O
        /// </example>
        /// <param name="initial">last name that we want to get the initial for</param>
        /// <returns></returns>
        public IList<User> GetByLastNameInitial(string initial)
        {
            if (initial.Length != 1)
            {
                initial = initial.Substring(0, 1);
            }

            ICriteria criteria = Session.CreateCriteria<User>()
                .CreateCriteria("Staff")
                .Add(Restrictions.Like("LastName", initial, MatchMode.Start));

            return criteria.List<User>();
        }

        /// <summary>
        /// Get users that are in the given role
        /// </summary>
        /// <param name="role">role to get users in</param>
        /// <returns></returns>
        public IList<User> GetByRole(Role role)
        {
            ICriteria criteria = Session.CreateCriteria<User>()
                .CreateCriteria("Roles")
                .Add(Restrictions.Eq("Id", role.Id))
                .Add(Restrictions.Eq("Name", role.Name));

            return criteria.List<User>();
        }

        /// <summary>
        /// Get users that are in the given staff type
        /// </summary>
        /// <param name="staffType">staff type to get users in</param>
        /// <returns></returns>
        public IList<User> FindByType(StaffType staffType)
        {
            ICriteria criteria = Session.CreateCriteria<User>().Add(Restrictions.Eq("StaffType", staffType));

            return criteria.List<User>();
        }
    }
}
