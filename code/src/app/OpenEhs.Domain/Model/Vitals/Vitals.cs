/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

using System;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Vitals represents the vitals that are taken from a patient
    /// </summary>
    public class Vitals : IEntity
    {

        #region Properties

        /// <summary>
        /// Id of the Vitals
        /// </summary>
        public virtual int Id {get; private set;}

        /// <summary>
        /// The type of vitals taken
        /// </summary>
        public virtual VitalsType Type { get; set; }

        /// <summary>
        /// Time that the vitals were taken
        /// </summary>
        public virtual DateTime Time { get; set; }

        /// <summary>
        /// Height of the patient
        /// </summary>
        public virtual double Height { get; set; }

        /// <summary>
        /// Weight of the patient
        /// </summary>
        public virtual double Weight { get; set; }

        /// <summary>
        /// Heart rate of the patient
        /// </summary>
        public virtual int HeartRate { get; set; }

        /// <summary>
        /// Body temperature of the patient
        /// </summary>
        public virtual float Temperature { get; set; }

        /// <summary>
        /// The patient's blood pressure
        /// </summary>
        public virtual BloodPressure BloodPressure { get; set; }

        /// <summary>
        /// Respiratory rate of the patient
        /// </summary>
        public virtual int RespiratoryRate { get; set; }

        /// <summary>
        /// The patient check in that this vitals was taken for
        /// </summary>
        public virtual PatientCheckIn PatientCheckIn { get; set; }

        /// <summary>
        /// Whether or not the vitals are active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        #endregion


    }
}
