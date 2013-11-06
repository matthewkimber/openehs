/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 1-Feb-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;

namespace OpenEhs.Data
{
    /// <summary>
    /// This represents the structure for a generic transaction that is used as database transaction
    /// </summary>
    public interface IGenericTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}