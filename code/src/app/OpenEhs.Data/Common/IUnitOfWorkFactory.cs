/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 1-Feb-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using NHibernate;
using NHibernate.Cfg;

namespace OpenEhs.Data
{
    /// <summary>
    /// Contains the structure for a class that can initialize a Unit of Work from the Hibernate Configuration XML file 
    /// ('hibernate.cfg.xml') to create a session that can be used in the Unit of Work implementor
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        Configuration Configuration { get; }
        ISession CurrentSession { get; set; }
        ISessionFactory SessionFactory { get; }
        IUnitOfWork Create();
        void DisposeUnitOfWork(IUnitOfWorkImplementor adapter);
    }
}