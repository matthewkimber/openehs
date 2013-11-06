/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 26-Jan-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using OpenEhs.Domain;

namespace OpenEhs.Data
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role Get(string name);
    }
}
