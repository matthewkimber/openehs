/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Hospital Team
 * Date: 13-Feb-2011
 * 
 * Author: Peter Litster (aholibamah@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;
using OpenEhs.Domain;
using NHibernate;

namespace OpenEhs.Data
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Payment Get(int id);
        IList<Payment> GetAll();
        void Add(Payment entity);
        void Remove(Payment entity);
        IList<Payment> FindByPatientId(int PatientId);
    }
}
