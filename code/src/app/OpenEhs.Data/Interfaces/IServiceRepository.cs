/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 14-Feb-2011
 * 
 * Author: Peter Litster (aholibamah@gmail.com)
 *****************************************************************************/

using System.Collections.Generic;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    public interface IServiceRepository : IRepository<Service>
    {
        IList<Service> GetActiveServices();
        IList<Service> GetInactiveServices();
        IList<Service> GetByCategory(Category category);
    }
}