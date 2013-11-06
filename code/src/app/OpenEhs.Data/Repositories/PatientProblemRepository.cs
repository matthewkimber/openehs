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
    /// Patient Problem Repository that handles the management and access of patient problems
    /// </summary>
    public class PatientProblemRepository : IPatientProblemRepository
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
        /// Get a PatientProblem with a given id.
        /// </summary>
        /// <param name="id">The Id of the PatientProblem to be retrieved.</param>
        /// <returns></returns>
        public PatientProblem Get(int id)
        {
            return Session.Get<PatientProblem>(id);
        }

        /// <summary>
        /// Gets all the PatientProblems in the Repository.
        /// </summary>
        /// <returns>An IList containing all PatientProblems in the Repository.</returns>
        public IList<PatientProblem> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<PatientProblem>();
            return criteria.List<PatientProblem>();
        }

        public PagedList<PatientProblem> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a PatientProblem to the Repository.
        /// </summary>
        /// <param name="entity">The PatientProblem to add to the Repository.</param>
        public void Add(PatientProblem entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a PatientProblem from the Repository.
        /// </summary>
        /// <param name="entity">The PatientProblem to remove from the Repository.</param>
        public void Remove(PatientProblem entity)
        {
            Session.Delete(entity);
        }
    }
}
