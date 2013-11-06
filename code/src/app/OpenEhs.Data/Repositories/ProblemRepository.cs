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
    /// Problem Repository that handles the management and access of problems
    /// </summary>
    public class ProblemRepository : IProblemRepository
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
        /// Get a Problem by id
        /// </summary>
        /// <param name="id">id of the Problem you wish to retrieve</param>
        /// <returns>the Problem that has an id matching the id you passed in</returns>
        public Problem Get(int id)
        {
            return Session.Get<Problem>(id);
        }

        /// <summary>
        /// Get all Problems
        /// </summary>
        /// <returns>list of all Problems</returns>
        public IList<Problem> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Problem>();
            return criteria.List<Problem>();
        }

        public PagedList<Problem> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a Problem to the database
        /// </summary>
        /// <param name="entity">Problem to add</param>
        public void Add(Problem entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Remove a Problem from the database
        /// </summary>
        /// <param name="entity">Problem to remove</param>
        public void Remove(Problem entity)
        {
            Session.Delete(entity);
        }
    }
}
