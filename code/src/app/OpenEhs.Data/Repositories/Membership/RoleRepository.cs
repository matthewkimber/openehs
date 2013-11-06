using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using OpenEhs.Data.Common;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// the current session from the unit of work
        /// </summary>
        private ISession Session
        {
            get { return UnitOfWork.CurrentSession; }
        }

        /// <summary>
        /// Get a User with a given id.
        /// </summary>
        /// <param name="id">The Id of the User to be retrieved.</param>
        /// <returns></returns>
        public Role Get(int id)
        {
            return Session.Get<Role>(id);
        }

        /// <summary>
        /// Get a User with a given name.
        /// </summary>
        /// <param name="name">The name of the User to be retrieved.</param>
        /// <returns></returns>
        public Role Get(string name)
        {
            ICriteria criteria = Session.CreateCriteria<Role>()
                .Add(Restrictions.Eq("Name", name));

            return criteria.UniqueResult<Role>();
        }

        /// <summary>
        /// Gets all the Roles in the Repository.
        /// </summary>
        /// <returns>An IList containing all Roles in the Repository.</returns>
        public IList<Role> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Role>();

            return criteria.List<Role>();
        }

        public PagedList<Role> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a Role to the Repository.
        /// </summary>
        /// <param name="entity">The Role to add to the Repository.</param>
        public void Add(Role entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a Role from the Repository.
        /// </summary>
        /// <param name="entity">The Role to remove from the Repository.</param>
        public void Remove(Role entity)
        {
            Session.Delete(entity);
        }
    }
}
