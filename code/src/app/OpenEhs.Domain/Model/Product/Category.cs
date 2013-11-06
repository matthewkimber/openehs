/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-17-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Category represents a category that contains products
    /// </summary>
    public class Category : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the category
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Name of the category
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Description of the category
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Products in the category
        /// </summary>
        public virtual IList<Product> Products { get; set; }

        /// <summary>
        /// Creation date of the category
        /// </summary>
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// Whether or not the category is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        #endregion

        /// <summary>
        /// Add a product to this category
        /// </summary>
        /// <param name="product">product to add to the category</param>
        public virtual void AddProduct(Product product)
        {
            product.Category = this;
            Products.Add(product);
        }
    }
}
