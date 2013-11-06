/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 16-Jan-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System.Collections.Generic;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    public interface IProductRepository : IRepository<Product>
    {
        IList<Product> GetActiveProducts();
        IList<Product> GetInactiveProducts();
        IList<Product> GetByCategory(Category category);
    }
}