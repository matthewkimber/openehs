/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Invoice is a representation of a billing invoice that keeps track of how much is owed for a patients visit
    /// </summary>
    public class Invoice : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the Invoice
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Total amount owed for the current Invoice by adding up all the invoice items in this invoice
        /// </summary>
        public virtual decimal Total
        {
            get
            {
                decimal total = 0.0m;
                if (Items != null && Items.Count > 0)
                {
                    foreach (InvoiceItem item in Items)
                    {
                        total += item.Total;
                    }
                }

                return total;
            }
        }

        /// <summary>
        /// Date of the invoice.  It uses the patients check in time, if one exists, or the current UTC time.
        /// </summary>
        public virtual DateTime Date
        {
            get
            {
                if (PatientCheckIn != null)
                {
                    return PatientCheckIn.CheckInTime;
                }
                return DateTime.UtcNow;
            }
            set
            {
                //Date = value;
            }
        }

        /// <summary>
        /// List of Invoice Items that this Invoice contains
        /// </summary>
        public virtual IList<InvoiceItem> Items { get; set; }

        /// <summary>
        /// Patient Check In that this particular invoice is for
        /// </summary>
        public virtual PatientCheckIn PatientCheckIn { get; set; }

        /// <summary>
        /// List of Payments that have been paid thus far
        /// </summary>
        public virtual IList<Payment> Payments { get; set; }

        /// <summary>
        /// Whether the invoice is active or not
        /// </summary>
        public virtual bool IsActive { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Invoice()
        {
            PatientCheckIn = new PatientCheckIn();
        }

        /// <summary>
        /// Add new invoice item to the list of items on this invoice
        /// </summary>
        /// <param name="item">Invoice item that contains details for the invoice</param>
        public virtual void AddLineItem(InvoiceItem item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Remove an invoice item from the list of items on this invoice
        /// </summary>
        /// <param name="item">Invoice item to remove from the list</param>
        public virtual void RemoveLineItem(InvoiceItem item)
        {
            Items.Remove(item);
        }

        #endregion
    }
}
