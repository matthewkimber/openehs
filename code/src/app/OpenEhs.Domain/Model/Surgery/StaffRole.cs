using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Staff role represents the role that the staff member will have in the surgery
    /// </summary>
    public enum StaffRole
    {
        Surgeon = 0,
        SurgeonAssistant = 1,
        Anaesthetist = 2,
        AnaesthetistAssistant = 3,
        Nurse = 4,
        Consultant = 5
    }
}
