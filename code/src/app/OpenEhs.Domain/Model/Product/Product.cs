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
    /// Product represents a product that a patient can be billed for during their check in
    /// </summary>
    public class Product: IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the product
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// The products name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The category that the product is in
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// The units of products
        /// </summary>
        public virtual string Unit { get; set; }

        /// <summary>
        /// The price of the product
        /// </summary>
        public virtual decimal Price { get; set; }

        /// <summary>
        /// The quantity of this product that is in stock
        /// </summary>
        public virtual int QuantityOnHand { get; set; }

        /// <summary>
        /// TODO: figure out what this is
        /// </summary>
        public virtual int Counter { get; set; }

        /// <summary>
        /// Whether or not the product is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        #endregion
    }
}
