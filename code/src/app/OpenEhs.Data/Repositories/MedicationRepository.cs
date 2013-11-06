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
    /// Medication Repository that handles the management and access of medications
    /// </summary>
    public class MedicationRepository : IMedicationRepository
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
        /// Get a Medication with a given id.
        /// </summary>
        /// <param name="id">The Id of the Medication to be retrieved.</param>
        /// <returns></returns>
        public Medication Get(int id)
        {
            return Session.Get<Medication>(id);
        }

        /// <summary>
        /// Gets all the Medications in the Repository.
        /// </summary>
        /// <returns>An IList containing all Medications in the Repository.</returns>
        public IList<Medication> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Medication>();
            return criteria.List<Medication>();
        }

        public PagedList<Medication> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a Medication to the Repository.
        /// </summary>
        /// <param name="entity">The Medication to add to the Repository.</param>
        public void Add(Medication entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a Medication from the Repository.
        /// </summary>
        /// <param name="entity">The Medication to remove from the Repository.</param>
        public void Remove(Medication entity)
        {
            Session.Delete(entity);
        }
    }
}
