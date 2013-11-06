using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Immunization represents an immunization that a patient is administered
    /// </summary>
    public class Immunization : IEntity
    {
        #region Fields

        /// <summary>
        /// Id of the Immunization
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// The type of vaccine administered
        /// </summary>
        public virtual string VaccineType { get; set; }

        /// <summary>
        /// Whether or not the immunization is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// List of patients that have been given this immunization
        /// </summary>
        public virtual IList<PatientImmunization> Patients { get; set; }

        #endregion
    }
}
