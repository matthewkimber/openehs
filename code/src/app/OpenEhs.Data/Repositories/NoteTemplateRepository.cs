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
    /// Note Template Repository that handles the management and access of note templates
    /// </summary>
    public class NoteTemplateRepository : INoteTemplateRepository
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
        /// Get a NoteTemplateCategory with a given id.
        /// </summary>
        /// <param name="id">The Id of the NoteTemplateCategory to be retrieved.</param>
        /// <returns></returns>
        public NoteTemplateCategory Get(int id)
        {
            return Session.Get<NoteTemplateCategory>(id);
        }

        /// <summary>
        /// Gets all the NoteTemplateCategorys in the Repository.
        /// </summary>
        /// <returns>An IList containing all NoteTemplateCategorys in the Repository.</returns>
        public IList<NoteTemplateCategory> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<NoteTemplateCategory>();
            return criteria.List<NoteTemplateCategory>();
        }

        public PagedList<NoteTemplateCategory> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a NoteTemplateCategory to the Repository.
        /// </summary>
        /// <param name="entity">The NoteTemplateCategory to add to the Repository.</param>
        public void Add(NoteTemplateCategory entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a NoteTemplateCategory from the Repository.
        /// </summary>
        /// <param name="entity">The NoteTemplateCategory to remove from the Repository.</param>
        public void Remove(NoteTemplateCategory entity)
        {
            Session.Delete(entity);
        }
    }
    
}
