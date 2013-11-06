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
    /// Address is a class that contains the street, city, and region for a particular user
    /// </summary>
    public class Address: IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the Address
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Street where the user is located 
        /// </summary>
        public virtual string Street1 { get; set; }

        /// <summary>
        /// Second street where the user is located (if needed)
        /// </summary>
        public virtual string Street2 { get; set; }

        /// <summary>
        /// City where the user is located
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Region where the user is located
        /// </summary>
        public virtual string Region { get; set; }

        /// <summary>
        /// Country where the user is located
        /// </summary>
        public virtual Country Country { get; set; }

        /// <summary>
        /// Whether the Address is active (non-active is associated with a deleted entry)
        /// </summary>
        public virtual bool IsActive { get; set; }

        #endregion
    }
}
