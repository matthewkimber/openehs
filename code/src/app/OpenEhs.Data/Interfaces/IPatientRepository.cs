/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 16-Jan-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;
using OpenEhs.Domain;

namespace OpenEhs.Data
{
    public interface IPatientRepository : IRepository<Patient>
    {
        IList<Patient> GetAll();
        IList<Patient> FindByPhoneNumber(string phoneNumber);
        IList<Patient> FindByFirstName(string name);
        IList<Patient> FindByMiddleName(string name);
        IList<Patient> FindByLastName(string name);
        IList<Patient> FindByDateOfBirth(DateTime dateOfBirth);
        IList<Patient> FindByOldPhysicalRecord(string number);
    }
}
