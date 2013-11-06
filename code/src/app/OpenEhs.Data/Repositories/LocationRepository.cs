using System;
using System.Collections.Generic;
using NHibernate;
using OpenEhs.Data.Common;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    /// <summary>
    /// Location Repository that handles the management and access of locations
    /// </summary>
    public class LocationRepository : ILocationRepository
    {
        /// <summary>
        /// the current session from the unit of work
        /// </summary>
        private ISession Session
        {
            get { return UnitOfWork.CurrentSession; }
        }

        /// <summary>
        /// Get a Location with a given id.
        /// </summary>
        /// <param name="id">The Id of the Location to be retrieved.</param>
        /// <returns></returns>
        public Location Get(int id)
        {
            return Session.Get<Location>(id);
        }

        /// <summary>
        /// Gets all the Locations in the Repository.
        /// </summary>
        /// <returns>An IList containing all Locations in the Repository.</returns>
        public IList<Location> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Location>();
            return criteria.List<Location>();
        }

        public PagedList<Location> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a Location to the Repository.
        /// </summary>
        /// <param name="entity">The Location to add to the Repository.</param>
        public void Add(Location entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a Location from the Repository.
        /// </summary>
        /// <param name="entity">The Location to remove from the Repository.</param>
        public void Remove(Location entity)
        {
            Session.Delete(entity);
        }
    }
}