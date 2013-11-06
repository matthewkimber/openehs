/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Surgery represents a surgery that is to be performed upon a patient
    /// </summary>
    public class Surgery : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the Surgery
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Location of the Surgery
        /// </summary>
        public virtual Location Location { get; set; }

        /// <summary>
        /// Start time of the Surgery
        /// </summary>
        public virtual DateTime StartTime { get; set; }

        /// <summary>
        /// End time of the Surgery
        /// </summary>
        public virtual DateTime EndTime { get; set; }

        /// <summary>
        /// List of surgery staff that are to administer the surgery
        /// </summary>
        public virtual IList<SurgeryStaff> Staff { get; set; }

        /// <summary>
        /// Type of surgery
        /// </summary>
        public virtual CaseType CaseType { get; set; }

        /// <summary>
        /// Check in associated with the patient that is to get surgergy performed on
        /// </summary>
        public virtual PatientCheckIn CheckIn { get; set; }

        #endregion

    }
}
