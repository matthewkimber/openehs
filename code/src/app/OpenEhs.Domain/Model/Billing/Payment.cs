/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Payment represents a payment towards an Invoice
    /// </summary>
    public class Payment: IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the payment
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Cash amount of the payment
        /// </summary>
        public virtual decimal CashAmount { get; set; }

        /// <summary>
        /// Date that this payment was performed on
        /// </summary>
        public virtual DateTime PaymentDate { get; set; }
        //public virtual Invoice InvoiceID { get; set; }

        /// <summary>
        /// Whether or not this payment is active (non-active means deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// The invoice that this payment was applying on
        /// </summary>
        public virtual Invoice Invoice { get; set; }

        #endregion
    }
}
