/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-17-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;

namespace OpenEhs.Infrastructure.Logging.Fakes
{
    public class MyFakeLogManager : ILogManager
    {
        public ILogger GetLogger(string name)
        {
            throw new NotImplementedException();
        }

        public ILogger GetCurrentClassLogger()
        {
            throw new NotImplementedException();
        }
    }
}
