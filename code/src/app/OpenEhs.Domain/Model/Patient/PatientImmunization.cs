using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// PatientImmunization represents Immunizations for a Patient
    /// </summary>
    public class PatientImmunization : IEntity
    {
        #region Fields

        /// <summary>
        /// Id of the PatientImmunization
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Patient that has been administered this immunization
        /// </summary>
        public virtual Patient Patient { get; set; }

        /// <summary>
        /// The immunization that was adminstered
        /// </summary>
        public virtual Immunization Immunization { get; set; }

        /// <summary>
        /// The date that the immunization was adminstered
        /// </summary>
        public virtual DateTime DateAdministered { get; set; }

        #endregion
    }
}
