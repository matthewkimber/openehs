namespace OpenEhs.Domain
{
    /// <summary>
    /// PatientAllergy represents Allergies for a Patient
    /// </summary>
    public class PatientAllergy : IEntity
    {
        #region Fields

        /// <summary>
        /// Id of the patient allergy
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Patient that has this allergy
        /// </summary>
        public virtual Patient Patient { get; set; }

        /// <summary>
        /// Allergy that the patient is allergic to
        /// </summary>
        public virtual Allergy Allergy { get; set; }

        #endregion
    }
}
