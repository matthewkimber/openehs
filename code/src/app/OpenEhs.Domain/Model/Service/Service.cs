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
    /// Service represents a service that can be provided to a patient
    /// </summary>
    public class Service: IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the Service
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Name of the Service
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Price of the Service
        /// </summary>
        public virtual decimal Price { get; set; }

        /// <summary>
        /// Whether or not the service is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        #endregion
    }
}
