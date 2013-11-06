/* OpenEHS System
 * Billing View Models
 * Peter Litster (aholibamah@gmail.com)
 * 17-Feb-2011
 */

using System;
using System.Collections.Generic;
using System.Linq;
using OpenEhs.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using OpenEhs.Data;

namespace OpenEhs.Web.Models
{

    #region Models

    public class BillingViewModel
    {

        private Invoice _invoice;
        private List<Payment> _payments;
        public string SearchTerm { get; set; }

        #region Billing

        /// <summary>
        /// Create a viewmodel for the specified InvoiceId.
        /// </summary>
        /// <param name="InvoiceId">The ID of the invoice to view.</param>
        public BillingViewModel(int InvoiceId)
        {
            _invoice = new InvoiceRepository().Get(InvoiceId);
            _payments = new PaymentRepository().GetPaymentsFor(InvoiceId);
        }

        public BillingViewModel(int InvoiceId, string searchTerms)
        {
            _invoice = new InvoiceRepository().Get(InvoiceId);
            _payments = new PaymentRepository().GetPaymentsFor(InvoiceId);
        }

        [Required]
        [DisplayName("Patient ID")]
        public int PatientId
        {
            get
            {
                return _invoice.PatientCheckIn.Patient.Id;
            }

            set
            {
                _invoice.PatientCheckIn.Patient = new PatientRepository().Get(value);
            }
        }

        [DisplayName("Patient First Name")]
        public string PatientFirstName
        {
            get
            {
                return _invoice.PatientCheckIn.Patient.FirstName;
            }
        }

        [DisplayName("Patient Last Name")]
        public string PatientLastName
        {
            get
            {
                return _invoice.PatientCheckIn.Patient.LastName;
            }
        }

        [DisplayName("Patient Middle Name")]
        public string PatientMiddleName
        {
            get
            {
                return _invoice.PatientCheckIn.Patient.MiddleName;
            }
        }

        public Address BillToAddress
        {
            get { return _invoice.PatientCheckIn.Patient.Address; }
        }

        [Required]
        [DisplayName("Invoice ID")]
        public int Id
        {
            get
            {
                return _invoice.Id;
            }
        }

        [Required]
        [DisplayName("Total")]
        public decimal Total
        {
            get
            {
                return _invoice.Total;
            }
        }

        [Required]
        [DisplayName("Line Items")]
        public IList<InvoiceItem> LineItems
        {
            get
            {
                return _invoice.Items;
            }

            set
            {
                _invoice.Items = value;
            }
        }

        [Required]
        [DisplayName("Date")]
        public DateTime Date
        {
            get
            {
                return _invoice.Date;
            }

            set
            {
                _invoice.Date = value;
            }
        }

        [DisplayName("Payment Total")]
        public decimal PaymentTotal
        {
            get
            {
                decimal tot = 0;
                foreach (Payment payment in _payments)
                {
                    tot += payment.CashAmount;
                }
                return tot;
            }
        }

        [DisplayName("Payments")]
        public IList<Payment> Payments
        {
            get
            {
                return _payments;
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Adds an InvoiceLineItem to this Invoice.
        /// </summary>
        /// <param name="lineItem">The InvoiceLineItem to add to this Invoice.</param>
        public void AddLineItem(InvoiceItem lineItem)
        {
            _invoice.Items.Add(lineItem);
        }

        /// <summary>
        /// Removes one InvoiceLineItem from this Invoice.
        /// </summary>
        /// <param name="lineItem">The InvoiceLineItem to remove from the Invoice.</param>
        public void RemoveLineItem(InvoiceItem lineItem)
        {
            _invoice.Items.Remove(lineItem);
        }

        /*public void GetByPatientId(int patientId)
        {
            IList<Invoice> invoices = new InvoiceRepository().FindByPatientId(patientId);
            foreach (Invoice invoice in invoices)
            {
                if (invoice.Total > 0)
                {
                    _invoice = invoice;
                }
            }
        }

        public void GetByInvoiceId(int invoiceId)
        {
            _invoice = new InvoiceRepository().Get(invoiceId);
        }
         */

        #endregion
    }

    public class BillingEditViewModel
    {

        private Invoice _invoice;
        private List<Payment> _payments;

        #region Billing

        public BillingEditViewModel(int InvoiceId)
        {
            _invoice = new InvoiceRepository().Get(InvoiceId);
            _payments = new PaymentRepository().GetPaymentsFor(InvoiceId);
        }

        [Required]
        [DisplayName("Patient ID")]
        public int PatientId
        {
            get
            {
                return _invoice.PatientCheckIn.Patient.Id;
            }

            set
            {
                _invoice.PatientCheckIn.Patient = new PatientRepository().Get(value);
            }
        }

        [DisplayName("Patient First Name")]
        public string PatientFirstName
        {
            get
            {
                return _invoice.PatientCheckIn.Patient.FirstName;
            }
        }

        [DisplayName("Patient Last Name")]
        public string PatientLastName
        {
            get
            {
                return _invoice.PatientCheckIn.Patient.LastName;
            }
        }

        [DisplayName("Patient Middle Name")]
        public string PatientMiddleName
        {
            get
            {
                return _invoice.PatientCheckIn.Patient.MiddleName;
            }
        }

        public Address BillToAddress
        {
            get { return _invoice.PatientCheckIn.Patient.Address; }
        }

        [Required]
        [DisplayName("Invoice ID")]
        public int Id
        {
            get
            {
                return _invoice.Id;
            }
        }

        [Required]
        [DisplayName("Total")]
        public decimal Total
        {
            get
            {
                return _invoice.Total;
            }
        }

        [Required]
        [DisplayName("Line Items")]
        public IList<InvoiceItem> LineItems
        {
            get
            {
                return _invoice.Items;
            }

            set
            {
                _invoice.Items = value;
            }
        }

        [Required]
        [DisplayName("Date")]
        public DateTime Date
        {
            get
            {
                return _invoice.Date;
            }

            set
            {
                _invoice.Date = value;
            }
        }

        [DisplayName("Products")]
        public IList<Product> Products
        {
            get
            {
                return new ProductRepository().GetAll();
            }
        }

        [DisplayName("Services")]
        public IList<Service> Services
        {
            get
            {
                return new ServiceRepository().GetAll();
            }
        }

        [DisplayName("Payment Total")]
        public decimal PaymentTotal
        {
            get
            {
                decimal tot = 0;
                foreach (Payment payment in _payments)
                {
                    tot += payment.CashAmount;
                }
                return tot;
            }
        }

        public IList<Payment> Payments
        {
            get
            {
                return _payments;
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Adds an InvoiceLineItem to this Invoice.
        /// </summary>
        /// <param name="lineItem">The InvoiceLineItem to add to this Invoice.</param>
        public void AddLineItem(InvoiceItem lineItem)
        {
            _invoice.Items.Add(lineItem);
        }

        /// <summary>
        /// Removes one InvoiceLineItem from this Invoice.
        /// </summary>
        /// <param name="lineItem">The InvoiceLineItem to remove from the Invoice.</param>
        public void RemoveLineItem(InvoiceItem lineItem)
        {
            _invoice.Items.Remove(lineItem);
        }

        /// <summary>
        /// Used to add a service that isn't yet defined (empty)
        /// </summary>
        public void AddEmptyService()
        {
            InvoiceItem lineItem = new InvoiceItem();
            lineItem.Service = new Service();
            lineItem.Quantity = 1;
            _invoice.Items.Add(lineItem);
        }

        /// <summary>
        /// Used to add a product that isn't yet defined (empty)
        /// </summary>
        public void AddEmptyProduct()
        {
            InvoiceItem lineItem = new InvoiceItem();
            lineItem.Product = new Product();
            lineItem.Quantity = 1;
            _invoice.Items.Add(lineItem);
        }

        /// <summary>
        /// Save the current invoice with each line item and payment
        /// </summary>
        public void Save()
        {
            var repo = new InvoiceRepository();
            repo.Add(_invoice);
            foreach (InvoiceItem lineItem in _invoice.Items)
            {
                repo.AddLineItem(lineItem);
            }
            foreach (Payment payment in _payments)
            {
                new PaymentRepository().Add(payment);
            }
        }

        #endregion

    }

    #endregion
}
