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
    /// Allergy represents something someone can be allergic to
    /// </summary>
    public class Allergy : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the Allergy
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Name of the Allergy
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Whether or not the allergy is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// List of patients allergic to this allergy
        /// </summary>
        public virtual IList<PatientAllergy> Patients { get; set; }

        #endregion
    }
}
