namespace OpenEhs.Domain
{
    /// <summary>
    /// PatientProblem represents problems a patient has
    /// </summary>
    public class PatientProblem : IEntity
    {
        #region Fields

        /// <summary>
        /// Id of the PatientProblem
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Problem that the patient has
        /// </summary>
        public virtual Problem Problem { get; set; }

        /// <summary>
        /// Patient that has the problem
        /// </summary>
        public virtual Patient Patient { get; set; }

        #endregion
    }
}
