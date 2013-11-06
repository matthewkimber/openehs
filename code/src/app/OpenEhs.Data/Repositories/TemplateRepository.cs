using System;
using System.Collections.Generic;
using NHibernate;
using OpenEhs.Data.Common;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    /// <summary>
    /// Template Repository that handles the management and access of templates
    /// </summary>
    public class TemplateRepository : ITemplateRepository
    {
        /// <summary>
        /// Gets the Current Session from the Unit of Work
        /// </summary>
        private ISession Session
        {
            get { return UnitOfWork.CurrentSession; }
        }

        /// <summary>
        /// Get a template by id
        /// </summary>
        /// <param name="id">id of the temple you wish to retrieve</param>
        /// <returns>the template that has the id you passed in</returns>
        public Template Get(int id)
        {
            return Session.Get<Template>(id);
        }

        /// <summary>
        /// Get all templates
        /// </summary>
        /// <returns>list of all templates</returns>
        public IList<Template> GetAll()
        {
            var criteria = Session.CreateCriteria<Template>();
            return criteria.List<Template>();
        }

        public PagedList<Template> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add a template to the database
        /// </summary>
        /// <param name="entity">template to add</param>
        public void Add(Template entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Remove a template from the database
        /// </summary>
        /// <param name="entity">template to remove</param>
        public void Remove(Template entity)
        {
            Session.Delete(entity);
        }

        public IList<Template> GetAllSurgeryTemplates()
        {
            throw new NotImplementedException();
        }

        public IList<Template> GetAllDiagnosisTemplates()
        {
            throw new NotImplementedException();
        }

        public IList<Template> GetAllNoteTemplates()
        {
            throw new NotImplementedException();
        }

        public IList<Template> GetAllReasonTemplates()
        {
            throw new NotImplementedException();
        }
    }
}
