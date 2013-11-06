using System;
using System.Collections.Generic;
using OpenEhs.Data.Common;
using OpenEhs.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace OpenEhs.Data
{
    /// <summary>
    /// Invoice Repository that handles the management and access of invoices
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
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

        #region Invoice

        /// <summary>
        /// Get an Invoice with a given id.
        /// </summary>
        /// <param name="id">The Id of the invoice to be retrieved.</param>
        /// <returns></returns>
        public Invoice Get(int id)
        {
            return Session.Get<Invoice>(id);
        }

        /// <summary>
        /// Gets all the Invoices in the Repository.
        /// </summary>
        /// <returns>An IList containing all Invoices in the Repository.</returns>
        public IList<Invoice> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Invoice>();
            return criteria.List<Invoice>();
        }

        public PagedList<Invoice> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the top 25 invoices ordered by the most recently created invoice
        /// </summary>
        /// <returns></returns>
        public IList<Invoice> GetTop25()
        {
            ICriteria criteria = Session.CreateCriteria<Invoice>();
            criteria.SetMaxResults(25);
            criteria.AddOrder(Order.Desc("Id"));

            return criteria.List<Invoice>();
        }

        /// <summary>
        /// Gets a list of all the InvoiceItems.
        /// </summary>
        /// <returns>Returns an IList of InvoiceItems.</returns>
        public IList<InvoiceItem> GetAllItems()
        {
            ICriteria criteria = Session.CreateCriteria<InvoiceItem>();

            return criteria.List<InvoiceItem>();
        }

        /// <summary>
        /// Get item from an invoice that matches the item id passed in
        /// </summary>
        /// <param name="itemId">id of the item</param>
        /// <returns></returns>
        public InvoiceItem GetItem(int itemId)
        {
            IList<InvoiceItem> lineItems = GetAllItems();
            for (int i = 0; i < lineItems.Count; i++)
            {
                if (lineItems[i].Id == itemId)
                {
                    return lineItems[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Adds an Invoice to the Repository.
        /// </summary>
        /// <param name="entity">The Invoice to add to the Repository.</param>
        public void Add(Invoice entity)
        {
            Session.Save(entity);
        }

        /// <summary>
        /// Removes an invoice from the Repository.
        /// </summary>
        /// <param name="entity">The Invoice to remove from the Repository.</param>
        public void Remove(Invoice entity)
        {
            Session.Delete(entity);
        }

        /// <summary>
        /// Finds a list of Invoices for a given PatientId.
        /// </summary>
        /// <param name="PatientId">The ID of the Patient Object to match Invoices to.</param>
        /// <returns>IList of Invoices for the given PatientId.</returns>
        public IList<Invoice> FindByPatientId(int PatientId)
        {
            //ICriteria criteria = Session.CreateCriteria<InvoiceItem>();
            //criteria.AddOrder(Order.Desc(


            //return criteria.List<InvoiceItem>();



            IList<Invoice> invoices = GetAll();
            IList<Invoice> returnInvoices = new List<Invoice>();
            for (int i = 0; i < invoices.Count;i++)
            {
                if (invoices[i].PatientCheckIn.Patient.Id == PatientId)
                {
                    returnInvoices.Add(invoices[i]);
                }
            }
            return returnInvoices;
        }

        /// <summary>
        /// Adds an InvoiceItem to the database.
        /// </summary>
        /// <param name="lineItem">The item to be added.</param>
        public void AddLineItem(InvoiceItem lineItem)
        {
            Session.SaveOrUpdate(lineItem);
        }

        /// <summary>
        /// Removes an InvoiceItem from the database.
        /// </summary>
        /// <param name="lineItem">the InvoiceItem to be removed.</param>
        public void RemoveLineItem(InvoiceItem lineItem)
        {
            Session.Delete(lineItem);
        }

        #endregion
    }
}
