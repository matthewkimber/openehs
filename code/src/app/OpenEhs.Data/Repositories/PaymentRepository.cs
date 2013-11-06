using System;
using System.Collections.Generic;
using OpenEhs.Data.Common;
using OpenEhs.Domain;
using NHibernate;

namespace OpenEhs.Data
{
    /// <summary>
    /// Payment Repository that handles the management and access of payments
    /// </summary>
    public class PaymentRepository : IPaymentRepository
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

        #region Payment

        /// <summary>
        /// Get an Payment with a given id.
        /// </summary>
        /// <param name="id">The Id of the Payment to be retrieved.</param>
        /// <returns></returns>
        public Payment Get(int id)
        {
            return Session.Get<Payment>(id);
        }

        /// <summary>
        /// Gets all the Payments in the Repository.
        /// </summary>
        /// <returns>An IList containing all Payments in the Repository.</returns>
        public IList<Payment> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria<Payment>();
            return criteria.List<Payment>();
        }

        public PagedList<Payment> GetPaged(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of the Payments related to a specific Invoice.
        /// </summary>
        /// <param name="invoice">The Invoice to return the Payment from.</param>
        /// <returns>A List of Payments for invoice.</returns>
        public List<Payment> GetPaymentsFor(int InvoiceId)
        {
            IList<Payment> allPayments = GetAll();
            List<Payment> payments = new List<Payment>();

            for (int i = 0; i < allPayments.Count; i++)
            {
                if (allPayments[i].Invoice.Id == InvoiceId)
                {
                    payments.Add(allPayments[i]);
                }
            }
            return payments;
        }

        /// <summary>
        /// Adds an Payment to the Repository.
        /// </summary>
        /// <param name="entity">The Payment to add to the Repository.</param>
        public void Add(Payment entity)
        {
            //is this correct? I copied the ProductRepository.
            Session.Save(entity);
        }

        /// <summary>
        /// Removes a Payment from the Repository.
        /// </summary>
        /// <param name="entity">The Payment to remove from the Repository.</param>
        public void Remove(Payment entity)
        {
            //is this correct? I copied the ProductRepository again.
            Session.Delete(entity);
        }

        /// <summary>
        /// Finds a list of Payments for a given PatientId.
        /// </summary>
        /// <param name="PatientId">The ID of the Patient Object to match Payments to.</param>
        /// <returns>IList of Payments for the given PatientId.</returns>
        public IList<Payment> FindByPatientId(int PatientId)
        {
            string q = String.Format("from Payment where Invoice.PatientCheckIn.Patient.Id = {0}", PatientId);
            IQuery query = Session.CreateQuery(q);
            return query.List<Payment>();
        }

        #endregion
    }
}
