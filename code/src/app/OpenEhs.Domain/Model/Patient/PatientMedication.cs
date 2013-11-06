using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// PatientMedication represents the medications prescribed to a patient
    /// </summary>
    public class PatientMedication : IEntity
    {
        #region Fields

        /// <summary>
        /// Id of the PatientMedication
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Instructions on how to take the medication
        /// </summary>
        public virtual string Instruction { get; set; }

        /// <summary>
        /// The dose of medication prescribed
        /// </summary>
        public virtual string Dose { get; set; }

        /// <summary>
        /// The frequency that the patient prescribed the medication needs to take it
        /// </summary>
        public virtual string Frequency { get; set; }

        /// <summary>
        /// The way the medication was administered
        /// </summary>
        public virtual MedicationRouteOfAdministrationType Administration { get; set; }

        /// <summary>
        /// Beginning date of the medication presription
        /// </summary>
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the medication prescription
        /// </summary>
        public virtual DateTime ExpDate { get; set; }

        /// <summary>
        /// The patient prescribed this medication
        /// </summary>
        public virtual Patient Patient { get; set; }

        /// <summary>
        /// The medication that the patient is to take
        /// </summary>
        public virtual Medication Medication { get; set; }

        #endregion
    }
}
