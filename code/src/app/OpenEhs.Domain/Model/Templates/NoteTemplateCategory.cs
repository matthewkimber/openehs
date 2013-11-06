using System.Collections.Generic;
namespace OpenEhs.Domain
{
    /// <summary>
    /// Note Template Category represents the category for a note template
    /// </summary>
    public class NoteTemplateCategory
    {
        /// <summary>
        /// Id of the note template category
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Name of the note template category
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// List of templates in the note template category
        /// </summary>
        public virtual IList<Template> Templates { get; set; }

        /// <summary>
        /// List of notes in the note template category
        /// </summary>
        public virtual IList<Note> Notes { get; set; }
    }
}
