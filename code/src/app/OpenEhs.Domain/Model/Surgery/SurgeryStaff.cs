namespace OpenEhs.Domain
{
    /// <summary>
    /// Surgery Staff represents a staff member assigned to a surgery
    /// </summary>
    public class SurgeryStaff : IEntity
    {
        /// <summary>
        /// Id of the Surgery Staff
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Staff's user account
        /// </summary>
        public virtual User Staff { get; set; }

        /// <summary>
        /// Surgery assigned to
        /// </summary>
        public virtual Surgery Surgery { get; set; }

        /// <summary>
        /// Role the Staff member will play in the surgery
        /// </summary>
        public virtual StaffRole Role { get; set; }
    }
}
