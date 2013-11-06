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
    /// Patient Allergy Repository that handles the management and access of patient allergies
    /// </summary>
    public class PatientAllergyRepository : IPatientAllergyRepository
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
        /// Get a PatientAllergy with a given id.
        /// </summary>
        /// <param name="id">The Id of the PatientAllergy to be retrieved.</param>
        /// <returns></returns>
        public PatientAllergy Get(int id)
        {
            return Session.Get<PatientAllergy>(id);
        }

        /// <summary>
        /// Gets all the PatientAllergys in the Repository.
        /// </summary>
        /// <returns>An IList containing all PatientAllergys in the Repository.</returns>
        public IList<PatientAllergy> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<PatientAllergy>();
            return criteria.List<PatientAllergy>();
        }

        public PagedList<PatientAllergy> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a PatientAllergy to the Repository.
        /// </summary>
        /// <param name="entity">The PatientAllergy to add to the Repository.</param>
        public void Add(PatientAllergy entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a PatientAllergy from the Repository.
        /// </summary>
        /// <param name="entity">The PatientAllergy to remove from the Repository.</param>
        public void Remove(PatientAllergy entity)
        {
            Session.Delete(entity);
        }
    }
}
