/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Vitals Type represents the type of vitals taken
    /// </summary>
    public enum VitalsType
    {
        Initial = 0,
        Discharge = 1,
        Maintenance = 2,
        PreSurgery = 3,
        PostSurgery = 4
    }
}
