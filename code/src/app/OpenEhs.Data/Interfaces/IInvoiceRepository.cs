/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 7-Feb-2011
 * 
 * Author: Peter Litster (aholibamah@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;
using OpenEhs.Domain;
using NHibernate;

namespace OpenEhs.Data
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Invoice Get(int id);
        IList<Invoice> GetAll();
        IList<InvoiceItem> GetAllItems();
        void Add(Invoice entity);
        void Remove(Invoice entity);
        IList<Invoice> FindByPatientId(int PatientId);
        IList<Invoice> GetTop25();
    }
}
