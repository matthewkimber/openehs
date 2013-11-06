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
    /// Patient Medication Repository that handles the management and access of patient medications
    /// </summary>
    public class PatientMedicationRepositiry : IPatientMedicationRepository
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
        /// Get a PatientMedication with a given id.
        /// </summary>
        /// <param name="id">The Id of the PatientMedication to be retrieved.</param>
        /// <returns></returns>
        public PatientMedication Get(int id)
        {
            return Session.Get<PatientMedication>(id);
        }

        /// <summary>
        /// Gets all the PatientMedications in the Repository.
        /// </summary>
        /// <returns>An IList containing all PatientMedications in the Repository.</returns>
        public IList<PatientMedication> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<PatientMedication>();
            return criteria.List<PatientMedication>();
        }

        public PagedList<PatientMedication> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a PatientMedication to the Repository.
        /// </summary>
        /// <param name="entity">The PatientMedication to add to the Repository.</param>
        public void Add(PatientMedication entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a PatientMedication from the Repository.
        /// </summary>
        /// <param name="entity">The PatientMedication to remove from the Repository.</param>
        public void Remove(PatientMedication entity)
        {
            Session.Delete(entity);
        }
    }
}
