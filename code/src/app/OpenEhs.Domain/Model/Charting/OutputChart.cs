using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// OutputChart represents a chart entry that helps track what the patient outputs
    /// </summary>
    public class OutputChart : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the Output Chart
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Time of the output from the patient
        /// </summary>
        public virtual DateTime ChartTime { get; set; }

        /// <summary>
        /// The suction amount
        /// </summary>
        public virtual string NGSuctionAmount { get; set; }

        /// <summary>
        /// The suction color
        /// </summary>
        public virtual string NGSuctionColor { get; set; }

        /// <summary>
        /// Amount of urine output
        /// </summary>
        public virtual string UrineAmount { get; set; }

        /// <summary>
        /// Amount of Stool output
        /// </summary>
        public virtual string StoolAmount { get; set; }

        /// <summary>
        /// Color of the stool sample
        /// </summary>
        public virtual string StoolColor { get; set; }

        /// <summary>
        /// The patient check in that this output is being tracked for
        /// </summary>
        public virtual PatientCheckIn PatientCheckIn { get; set; }

        #endregion
    }
}
