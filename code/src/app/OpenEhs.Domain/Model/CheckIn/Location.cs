/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

using System.Collections.Generic;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Location represents a location in the hospital that a patient can be checked into
    /// </summary>
    public class Location : IEntity
    {
        /// <summary>
        /// Id of the location
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// The locations department
        /// </summary>
        public virtual string Department { get; set; }

        /// <summary>
        /// The locations room number
        /// </summary>
        public virtual string RoomNumber { get; set; }

        /// <summary>
        /// List of patients checked into this location
        /// </summary>
        public virtual IList<PatientCheckIn> PatientCheckIns { get; set; }

        /// <summary>
        /// List of surgeries scheduled in this location
        /// </summary>
        public virtual IList<Surgery> Surgeries { get; set; }

        /// <summary>
        /// Whether or not this location is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
