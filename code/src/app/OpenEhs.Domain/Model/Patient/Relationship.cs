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
    /// Relationship represents the relationship the emergency contact has with the patient
    /// </summary>
    public enum Relationship
    {
        Spouse = 0,
        Father = 1,
        Mother = 2,
        Brother = 3,
        Sister = 4,
        InLaw = 5,
        Aunt = 6,
        Cousin = 7,
        Other = 8
    }
}
