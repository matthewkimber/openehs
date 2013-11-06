/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 26-Jan-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

namespace OpenEhs.Domain
{
    /// <summary>
    /// IEntity is an interface that is used to ensure that each business object has an Id property
    /// </summary>
    public interface IEntity
    {
        int Id { get; }
    }
}
