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
    /// PatientCheckIn represents a patients check in and the components involved with the
    /// care of patients including (but not limited to) : vitals, surgeries, and charts.
    /// </summary>
    public class PatientCheckIn : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the patient check in
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Date and time of the check in
        /// </summary>
        public virtual DateTime CheckInTime { get; set; }

        /// <summary>
        /// Patient check in type
        /// </summary>
        public virtual PatientCheckinType PatientType { get; set; }

        /// <summary>
        /// Patient checked in
        /// </summary>
        public virtual Patient Patient { get; set; }

        /// <summary>
        /// Date and time of patient checkout
        /// </summary>
        public virtual DateTime CheckOutTime { get; set; }

        /// <summary>
        /// Whether or not this patient check in is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Invoice for this check in (to track the expenses that the patient owes)
        /// </summary>
        public virtual Invoice Invoice { get; set; }

        /// <summary>
        /// List of Vitals taken throughout the check in
        /// </summary>
        public virtual IList<Vitals> Vitals { get; set; }

        /// <summary>
        /// List of surgeries performed throughout the check in
        /// </summary>
        public virtual IList<Surgery> Surgeries { get; set; }

        /// <summary>
        /// Notes on the patients check in
        /// </summary>
        public virtual IList<Note> Notes { get; set; }

        /// <summary>
        /// Location that the patient was seen at
        /// </summary>
        public virtual Location Location { get; set; }

        /// <summary>
        /// User that is to care for this patient
        /// </summary>
        public virtual User AttendingStaff { get; set; }

        /// <summary>
        /// Feed chart for the patient
        /// </summary>
        public virtual IList<FeedChart> FeedChart { get; set; }

        /// <summary>
        /// Intake chart for the patient
        /// </summary>
        public virtual IList<IntakeChart> IntakeChart { get; set; }

        /// <summary>
        /// Output chart for the patient
        /// </summary>
        public virtual IList<OutputChart> OutputChart { get; set; }

        #endregion
    }
}
