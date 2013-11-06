/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 26-Jan-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

namespace OpenEhs.Domain
{
    public class Template
    {
        public virtual int Id { get; private set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual User Staff { get; set; }
        public virtual NoteTemplateCategory NoteTemplateCategory { get; set; }
        public virtual bool IsActive { get; set; }
    }
}
