using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenEhs.Data.Common;
using OpenEhs.Domain;
using NHibernate;

namespace OpenEhs.Data {

    /// <summary>
    /// Patient Immunization Repository that handles the management and access of patient immunizations
    /// </summary>
    public class PatientImmunizationRepository : IPatientImmunizationRepository 
    {
        /// <summary>
        /// the current session from the unit of work
        /// </summary>
        private ISession Session {
            get 
            {
                return UnitOfWork.CurrentSession;
            }
        }

        /// <summary>
        /// Get a PatientImmunization with a given id.
        /// </summary>
        /// <param name="id">The Id of the PatientImmunization to be retrieved.</param>
        /// <returns></returns>
        public PatientImmunization Get(int id) 
        {
            return Session.Get<PatientImmunization>(id);
        }

        /// <summary>
        /// Gets all the PatientImmunizations in the Repository.
        /// </summary>
        /// <returns>An IList containing all PatientImmunizations in the Repository.</returns>
        public IList<PatientImmunization> GetAll() 
        {
            ICriteria criteria = Session.CreateCriteria<PatientImmunization>();
            return criteria.List<PatientImmunization>();
        }

        public PagedList<PatientImmunization> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a PatientImmunization to the Repository.
        /// </summary>
        /// <param name="entity">The PatientImmunization to add to the Repository.</param>
        public void Add(PatientImmunization entity) 
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a PatientImmunization from the Repository.
        /// </summary>
        /// <param name="entity">The PatientImmunization to remove from the Repository.</param>
        public void Remove(PatientImmunization entity) 
        {
            Session.Delete(entity);
        }
    }
}
