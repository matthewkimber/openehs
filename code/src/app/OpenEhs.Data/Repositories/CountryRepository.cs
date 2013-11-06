using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenEhs.Domain;
using OpenEhs.Data.Common;
using NHibernate;

namespace OpenEhs.Data
{
    /// <summary>
    /// Country Repository that handles the management and access of countries
    /// </summary>
    public class CountryRepository : ICountryRepository
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
        /// Get a Country with a given id.
        /// </summary>
        /// <param name="id">The Id of the Country to be retrieved.</param>
        /// <returns></returns>
        public Country Get(int id)
        {
            return Session.Get<Country>(id);
        }

        /// <summary>
        /// Gets all the Countries in the Repository.
        /// </summary>
        /// <returns>An IList containing all Countries in the Repository.</returns>
        public IList<Country> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Country>();
            return criteria.List<Country>();
        }

        public PagedList<Country> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a Country to the Repository.
        /// </summary>
        /// <param name="entity">The Country to add to the Repository.</param>
        public void Add(Country entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a Country from the Repository.
        /// </summary>
        /// <param name="entity">The Country to remove from the Repository.</param>
        public void Remove(Country entity)
        {
            Session.Delete(entity);
        }
    }
}
