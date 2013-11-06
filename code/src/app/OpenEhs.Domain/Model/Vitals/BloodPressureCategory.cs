/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-17-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

namespace OpenEhs.Domain
{
    /// <summary>
    /// Blood Pressure Category represents the category a bloodpressure can be contained in
    /// </summary>
    public enum BloodPressureCategory
    {
        None,
        Normal,
        Prehypertension,
        StageOneHypertension,
        StageTwoHypertension,
        Crisis
    }
}