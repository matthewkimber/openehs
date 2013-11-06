using System;
using System.Collections.Generic;
using NHibernate;
using OpenEhs.Data.Common;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    /// <summary>
    /// Category Repository that handles the management and access of categories
    /// </summary>
    public class CategoryRepository : ICategoryRepository
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
        /// Get a Category with a given id.
        /// </summary>
        /// <param name="id">The Id of the Category to be retrieved.</param>
        /// <returns></returns>
        public Category Get(int id)
        {
            return Session.Get<Category>(id);
        }

        /// <summary>
        /// Gets all the Categories in the Repository.
        /// </summary>
        /// <returns>An IList containing all Categories in the Repository.</returns>
        public IList<Category> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Category>();

            return criteria.List<Category>();
        }

        public PagedList<Category> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a Category to the Repository.
        /// </summary>
        /// <param name="entity">The Category to add to the Repository.</param>
        public void Add(Category entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a Category from the Repository.
        /// </summary>
        /// <param name="entity">The Category to remove from the Repository.</param>
        public void Remove(Category entity)
        {
            Session.Delete(entity);
        }
    }
}
