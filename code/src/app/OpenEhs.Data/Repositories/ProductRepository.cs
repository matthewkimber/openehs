/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 23-Jan-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using OpenEhs.Data.Common;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    /// <summary>
    /// Product Repository that handles the management and access of products
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// the current session from the unit of work
        /// </summary>
        private ISession Session
        {
            get { return UnitOfWork.CurrentSession; }
        }

        /// <summary>
        /// Get a Product by id
        /// </summary>
        /// <param name="id">id of the Product you wish to retrieve</param>
        /// <returns>the Product that has an id matching the id you passed in</returns>
        public Product Get(int id)
        {
            return Session.Get<Product>(id);
        }

        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns>list of all Products</returns>
        public IList<Product> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Product>();
            return criteria.List<Product>();
        }

        public PagedList<Product> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a Product to the database
        /// </summary>
        /// <param name="entity">Product to add</param>
        public void Add(Product product)
        {
            Session.Save(product);
        }

        /// <summary>
        /// Remove a Product from the database
        /// </summary>
        /// <param name="entity">Product to remove</param>
        public void Remove(Product product)
        {
            Session.Delete(product);
        }

        /// <summary>
        /// Get all Products that are active
        /// </summary>
        /// <returns>a list of Products that are currently active</returns>
        public IList<Product> GetActiveProducts()
        {
            ICriteria criteria = Session.CreateCriteria<Product>()
                .Add(Restrictions.Eq("IsActive", true));

            return criteria.List<Product>();
        }

        /// <summary>
        /// Get all Products that are inactive
        /// </summary>
        /// <returns>a list of Products that are currently inactive</returns>
        public IList<Product> GetInactiveProducts()
        {
            ICriteria criteria = Session.CreateCriteria<Product>()
                .Add(Restrictions.Eq("IsActive", false));

            return criteria.List<Product>();
        }

        public IList<Product> GetByCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
