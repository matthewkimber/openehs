/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-17-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

namespace OpenEhs.Infrastructure.Logging
{
    public interface ILogManager
    {
        ILogger GetLogger(string name);
        ILogger GetCurrentClassLogger();
    }
}
