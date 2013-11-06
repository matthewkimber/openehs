using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Medication represents a medication that a patient can be perscribed
    /// </summary>
    public class Medication : IEntity
    {
        #region Fields

        /// <summary>
        /// Id of the Medication
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Name of the Medication
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Description of the Medication
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// List of patients that have been perscribed the medication
        /// </summary>
        public virtual IList<PatientMedication> Patients { get; set; }

        /// <summary>
        /// Whether or not the Medication is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        #endregion
    }
}
