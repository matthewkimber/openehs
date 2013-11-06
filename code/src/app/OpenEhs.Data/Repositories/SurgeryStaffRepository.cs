using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenEhs.Data.Common;
using OpenEhs.Domain;
using NHibernate;

namespace OpenEhs.Data
{
    /// <summary>
    /// Surgery Staff Repository that handles the management and access of staff in a surgery
    /// </summary>
    public class SurgeryStaffRepository : ISurgeryStaffRepository
    {
        /// <summary>
        /// Get the current session from the unit of work
        /// </summary>
        private ISession Session
        {
            get
            {
                return UnitOfWork.CurrentSession;
            }
        }

        /// <summary>
        /// Get a surgery staff member by id
        /// </summary>
        /// <param name="id">the id of the staff member you wish to retrieve</param>
        /// <returns>the surgery staff member with an id that matches the one you passed in</returns>
        public SurgeryStaff Get(int id)
        {
            return Session.Get<SurgeryStaff>(id);
        }

        /// <summary>
        /// Get all surgery staff members
        /// </summary>
        /// <returns>a list of all surgery staff members</returns>
        public IList<SurgeryStaff> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<SurgeryStaff>();
            return criteria.List<SurgeryStaff>();
        }

        public PagedList<SurgeryStaff> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a surgery staff member to the database
        /// </summary>
        /// <param name="entity">the surgery staff member to add</param>
        public void Add(SurgeryStaff entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Remove a surgery staff member from the database
        /// </summary>
        /// <param name="entity">the surgery staff member to remove</param>
        public void Remove(SurgeryStaff entity)
        {
            Session.Delete(entity);
        }
    }
}
