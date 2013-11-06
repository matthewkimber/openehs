/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

namespace OpenEhs.Domain
{
    /// <summary>
    /// EmergencyContact represents who should be notified in the case of an emergency
    /// </summary>
    public class EmergencyContact: IEntity // NOTE: Should we just call this class "Contact"?
    {
        #region Properties

        /// <summary>
        /// Id of the emergency contact
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// First name of the emergency contact
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Last name of the emergency contact
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Phone number of the emergency contact
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Relationship the emergency contact has with the patient
        /// </summary>
        public virtual Relationship Relationship { get; set; }

        /// <summary>
        /// Address of the emergency contact
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// Whether or not the emergency contact is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        #endregion
    }
}
