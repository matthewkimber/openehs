/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace OpenEhs.Domain
{
    /// <summary>
    /// Note represents a note that can be made upon a patients check in
    /// </summary>
    public class Note : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the Note
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// Title of the Note
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Body of the Note
        /// </summary>
        public virtual string Body { get; set; }

        /// <summary>
        /// Date and time that this note was created
        /// </summary>
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// Author of the note
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// Category of the note template used
        /// </summary>
        public virtual NoteTemplateCategory NoteTemplateCategory { get; set; }

        /// <summary>
        /// Whether or not the note is active (non-active is deleted)
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Patient check in that this note is associated with
        /// </summary>
        public virtual PatientCheckIn PatientCheckIns { get; set; }

        /// <summary>
        /// Type of Note
        /// </summary>
        public virtual NoteType Type { get; set; }

        #endregion

    }
}
