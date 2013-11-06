using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using OpenEhs.Data.Common;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    /// <summary>
    /// Allergy Repository that handles the management and access of allergies
    /// </summary>
    public class AllergyRepository : IAllergyRepository
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
        /// Get a Allergy with a given id.
        /// </summary>
        /// <param name="id">The Id of the Allergy to be retrieved.</param>
        /// <returns></returns>
        public Allergy Get(int id)
        {
            return Session.Get<Allergy>(id);
        }

        /// <summary>
        /// Gets all the Allergies in the Repository.
        /// </summary>
        /// <returns>An IList containing all Allergies in the Repository.</returns>
        public IList<Allergy> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Allergy>();
            return criteria.List<Allergy>();
        }

        public PagedList<Allergy> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a Allergy to the Repository.
        /// </summary>
        /// <param name="entity">The Allergy to add to the Repository.</param>
        public void Add(Allergy entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a Allergy from the Repository.
        /// </summary>
        /// <param name="entity">The Allergy to remove from the Repository.</param>
        public void Remove(Allergy entity)
        {
            Session.Delete(entity);
        }
    }
}
