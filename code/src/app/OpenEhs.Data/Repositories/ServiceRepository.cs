/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 14-Feb-2011
 * 
 * Author: Peter Litster (aholibamah@gmail.com)
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
    /// Service Repository that handles the management and access of services
    /// </summary>
    public class ServiceRepository : IServiceRepository
    {
        /// <summary>
        /// the current session from the unit of work
        /// </summary>
        private ISession Session
        {
            get { return UnitOfWork.CurrentSession; }
        }

        /// <summary>
        /// Get a service by id
        /// </summary>
        /// <param name="id">id of the service you wish to retrieve</param>
        /// <returns>the service that has an id matching the id you passed in</returns>
        public Service Get(int id)
        {
            return Session.Get<Service>(id);
        }

        /// <summary>
        /// Get all services
        /// </summary>
        /// <returns>list of all services</returns>
        public IList<Service> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Service>();
            return criteria.List<Service>();
        }

        public PagedList<Service> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a service to the database
        /// </summary>
        /// <param name="entity">service to add</param>
        public void Add(Service service)
        {
            Session.Save(service);
        }

        /// <summary>
        /// Remove a service from the database
        /// </summary>
        /// <param name="entity">service to remove</param>
        public void Remove(Service service)
        {
            Session.Delete(service);
        }

        /// <summary>
        /// Get all services that are active
        /// </summary>
        /// <returns>a list of services that are currently active</returns>
        public IList<Service> GetActiveServices()
        {
            ICriteria criteria = Session.CreateCriteria<Service>()
                .Add(Restrictions.Eq("IsActive", true));

            return criteria.List<Service>();
        }

        /// <summary>
        /// Get all services that are inactive
        /// </summary>
        /// <returns>a list of services that are currently inactive</returns>
        public IList<Service> GetInactiveServices()
        {
            ICriteria criteria = Session.CreateCriteria<Service>()
                .Add(Restrictions.Eq("IsActive", false));

            return criteria.List<Service>();
        }

        public IList<Service> GetByCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
