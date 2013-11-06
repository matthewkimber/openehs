/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 26-Jan-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System.Collections.Generic;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    public interface IUserRepository : IRepository<User>
    {
        new User Get(int id);
        User Get(string username);
        IList<User> Find(string username, string password);
        bool CheckForUsernameAvailability(string username);
        IList<User> GetByLastNameInitial(string initial);
        IList<User> GetByRole(Role role);
    }
}
