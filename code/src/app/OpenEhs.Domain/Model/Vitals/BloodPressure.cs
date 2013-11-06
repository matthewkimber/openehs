/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-17-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
namespace OpenEhs.Domain
{
    /// <summary>
    /// Blood Pressure represents the blood pressure vital that is taken
    /// </summary>
    public class BloodPressure
    {

        #region Properties

        public virtual int Systolic {get; set;}
        public virtual int Diastolic {get; set;}

        /// <summary>
        /// The the category that this blood pressure lies within (normal, prehypertension, stage one hypertension,
        /// stage two hypertension, and crisis
        /// </summary>
        public BloodPressureCategory Category
        {
            get
            {
                // Normal Blood Pressure
                if ((Systolic > 0 && Systolic < 120) && (Diastolic > 0 && Diastolic < 80))
                    return BloodPressureCategory.Normal;
                
                // Prehypertension
                if ((Systolic >= 120 && Systolic <= 139) || (Diastolic >= 80 && Diastolic <= 89))
                    return BloodPressureCategory.Prehypertension;

                // Stage 1 Hypertension
                if ((Systolic >= 140 && Systolic <= 159) || (Diastolic >= 90 && Diastolic <= 99))
                    return BloodPressureCategory.StageOneHypertension;

                // Stage 2 Hypertension
                if ((Systolic >= 160 && Systolic <= 179) || (Diastolic >= 100 && Diastolic <= 109))
                    return BloodPressureCategory.StageTwoHypertension;

                // Hypertensive Crisis
                if (Systolic >= 180 || Systolic >= 110)
                    return BloodPressureCategory.Crisis;

                return BloodPressureCategory.None;
            }
        }

        #endregion

        /// <summary>
        /// Get the blood pressure in a human readable string
        /// </summary>
        /// <returns>Systolic / Diastolic</returns>
        public override string ToString()
        {
            return Systolic + "/" + Diastolic;
        }
    }
}
