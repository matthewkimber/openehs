/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 23-Jan-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;
using OpenEhs.Data.Common;
using OpenEhs.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace OpenEhs.Data
{
    /// <summary>
    /// Immunization Repository that handles the management and access of immunizations
    /// </summary>
    public class ImmunizationRepository : IImmunizationRepository 
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
        /// Get a Immunization with a given id.
        /// </summary>
        /// <param name="id">The Id of the Immunization to be retrieved.</param>
        /// <returns></returns>
        public Immunization Get(int id) 
        {
            return Session.Get<Immunization>(id);
        }

        /// <summary>
        /// Gets all the Immunizations in the Repository.
        /// </summary>
        /// <returns>An IList containing all Immunizations in the Repository.</returns>
        public IList<Immunization> GetAll() 
        {
            ICriteria criteria = Session.CreateCriteria<Immunization>();
            return criteria.List<Immunization>();
        }

        public PagedList<Immunization> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a Immunization to the Repository.
        /// </summary>
        /// <param name="entity">The Immunization to add to the Repository.</param>
        public void Add(Immunization entity) 
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a Immunization from the Repository.
        /// </summary>
        /// <param name="entity">The Immunization to remove from the Repository.</param>
        public void Remove(Immunization entity) 
        {
            Session.Delete(entity);
        }

        public Boolean ImmunizationExists(string vaccineType) 
        {
            ICriteria criteria = Session.CreateCriteria<Immunization>().Add(Restrictions.Eq("VaccineType", vaccineType));
            Immunization Immunization = criteria.UniqueResult<Immunization>();
            if (Immunization == null)
                return false;
            else
                return true;
        }
    }
}
