/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

namespace OpenEhs.Domain
{
    /// <summary>
    /// Invoice Item represents an entry that is then added to an invoice to comprise a total bill for a patient's visit
    /// </summary>
    public class InvoiceItem : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the Invoice Item
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Owner of this Invoice Item
        /// </summary>
        public virtual Invoice Invoice { get; set; }

        /// <summary>
        /// The product that is being charged for
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// The service that is being charged for
        /// </summary>
        public virtual Service Service { get; set; }

        /// <summary>
        /// Quantity of the product/service that was given
        /// </summary>
        public virtual decimal Quantity { get; set; }

        /// <summary>
        /// Total amount owed for this item
        /// </summary>
        public virtual decimal Total
        {
            get
            {
                if (Service != null)
                    return Service.Price * Quantity;

                if (Product != null)
                    return Product.Price * Quantity;

                return 0.0m;
            }
        }

        /// <summary>
        /// Whether or not this item is active (non-active means deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        #endregion
    }
}
