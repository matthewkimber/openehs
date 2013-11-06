/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 16-Jan-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System.Collections.Generic;
using OpenEhs.Domain;
using System;

namespace OpenEhs.Data {
    public interface IImmunizationRepository : IRepository<Immunization> {
        Boolean ImmunizationExists(string vaccineType);
    }
}