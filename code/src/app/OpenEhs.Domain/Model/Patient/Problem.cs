/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-26-2011
 * 
 * Author: Kevin Russon
 *****************************************************************************/

using System.Collections.Generic;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Problem represents a problem that a patient can have
    /// </summary>
    public class Problem : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the Problem
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Name of the problem
        /// </summary>
        public virtual string ProblemName { get; set; }

        /// <summary>
        /// List of the patients that have this problem
        /// </summary>
        public virtual IList<PatientProblem> Patients { get; set; }

        #endregion

    }
}
