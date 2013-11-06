using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using OpenEhs.Domain;
using OpenEhs.Data.Common;

namespace OpenEhs.Data
{
    /// <summary>
    /// Address Repository that handles the management and access of addresses
    /// </summary>
    public class AddressRepository : IAddressRepository
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
        /// Get a Address with a given id.
        /// </summary>
        /// <param name="id">The Id of the Address to be retrieved.</param>
        /// <returns></returns>
        public Address Get(int id)
        {
            return Session.Get<Address>(id);
        }

        /// <summary>
        /// Gets all the Addresses in the Repository.
        /// </summary>
        /// <returns>An IList containing all Addresses in the Repository.</returns>
        public IList<Address> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Address>();
            return criteria.List<Address>();
        }

        public PagedList<Address> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a Address to the Repository.
        /// </summary>
        /// <param name="entity">The Address to add to the Repository.</param>
        public void Add(Address entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a Address from the Repository.
        /// </summary>
        /// <param name="entity">The Address to remove from the Repository.</param>
        public void Remove(Address entity)
        {
            Session.Delete(entity);
        }
    }
}
