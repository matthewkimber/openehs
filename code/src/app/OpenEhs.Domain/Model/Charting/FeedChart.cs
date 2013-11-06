using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// FeedChart represents the chart for a patients feed
    /// </summary>
    public class FeedChart : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the FeedChart
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// The patient check-in that this Feed Chart was taken for
        /// </summary>
        public virtual PatientCheckIn PatientCheckIn { get; set; }

        /// <summary>
        /// The time this chart was performed
        /// </summary>
        public virtual DateTime FeedTime { get; set; }

        /// <summary>
        /// The feed chart type
        /// </summary>
        public virtual string FeedType { get; set; }

        /// <summary>
        /// Amount offered to the patient
        /// </summary>
        public virtual string AmountOffered { get; set; }

        /// <summary>
        /// Amount taken from the patient
        /// </summary>
        public virtual string AmountTaken { get; set; }

        /// <summary>
        /// The amount of vomit that the patient produces
        /// </summary>
        public virtual string Vomit { get; set; }

        /// <summary>
        /// The amount of urine that the patient produces
        /// </summary>
        public virtual string Urine { get; set; }

        /// <summary>
        /// The amount of stool that the patient produces
        /// </summary>
        public virtual string Stool { get; set; }

        /// <summary>
        /// Comments about this Feed Chart
        /// </summary>
        public virtual string Comments { get; set; }

        #endregion
    }
}
