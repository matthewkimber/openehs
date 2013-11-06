using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Medication Route of Administration Type is an enumeration that describes how a medication is administered
    /// </summary>
    public enum MedicationRouteOfAdministrationType
    {
        Orally,
        Rectally,
        Intravenously,
        Inhalational,
        Nasally,
        Other
    }
}
