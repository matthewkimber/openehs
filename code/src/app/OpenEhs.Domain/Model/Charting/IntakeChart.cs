using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Intake Chart represents in intake chart to track how much liquids the patient has consumed
    /// </summary>
    public class IntakeChart : IEntity
    {
        #region Properties

        /// <summary>
        /// The Id of the Intake Chart
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// The time of intake
        /// </summary>
        public virtual DateTime ChartTime { get; set; }

        /// <summary>
        /// The kind of fluid consumed
        /// </summary>
        public virtual string KindOfFluid { get; set; }

        /// <summary>
        /// The amount of fluid consumed
        /// </summary>
        public virtual string Amount { get; set; }

        /// <summary>
        /// The patient check-in this intake was consumed for
        /// </summary>
        public virtual PatientCheckIn PatientCheckIn { get; set; }

        #endregion
    }
}
