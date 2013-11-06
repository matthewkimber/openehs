/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 1-Feb-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using System.IO;
using System.Xml;
using NHibernate;
using NHibernate.Cfg;

namespace OpenEhs.Data
{
    /// <summary>
    /// Initializes a Unit of Work from the Hibernate Configuration XML file ('hibernate.cfg.xml') to create
    /// a session that can be used in the Unit of Work implementor
    /// </summary>
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        #region Fields

        private const string DefaultHibernateConfig = "hibernate.cfg.xml";

        private static ISession _currentSession;
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        #endregion


        #region Properties

        /// <summary>
        /// Configure the unit of work factory from hibernate configuation xml
        /// </summary>
        public Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new Configuration();
                    var hibernateConfig = DefaultHibernateConfig;

                    // If not rooted, assume path from base directory.
                    if (Path.IsPathRooted(hibernateConfig) == false)
                    {
                        hibernateConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, hibernateConfig);
                    }

                    if (File.Exists(hibernateConfig))
                    {
                        _configuration.Configure(new XmlTextReader(hibernateConfig));
                    }
                }

                return _configuration;
            }
        }

        /// <summary>
        /// Get or set the current session
        /// </summary>
        public ISession CurrentSession
        {
            get
            {
                if (_currentSession == null)
                    throw new InvalidOperationException("You are not in a unit of work.");

                return _currentSession;
            }
            set
            {
                _currentSession = value;
            }
        }

        /// <summary>
        /// Get the session factory, or build a new one if there isn't one currently
        /// </summary>
        public ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    _sessionFactory = Configuration.BuildSessionFactory();

                return _sessionFactory;
            }
        }

        #endregion


        #region Constructor

        internal UnitOfWorkFactory()
        {}

        #endregion


        #region Methods

        /// <summary>
        /// Create a unit of work.  This will create a session and return a unit of work.
        /// </summary>
        /// <returns></returns>
        public IUnitOfWork Create()
        {
            ISession session = CreateSession();
            session.FlushMode = FlushMode.Commit;
            _currentSession = session;

            return new UnitOfWorkImplementor(this, session);
        }

        /// <summary>
        /// Open a new session from the session factory
        /// </summary>
        /// <returns></returns>
        private ISession CreateSession()
        {
            return SessionFactory.OpenSession();
        }

        /// <summary>
        /// Dispose the current session and the unit of work
        /// </summary>
        /// <param name="adapter"></param>
        public void DisposeUnitOfWork(IUnitOfWorkImplementor adapter)
        {
            CurrentSession = null;
            UnitOfWork.DisposeUnitOfWork(adapter);
        }

        #endregion
    }
}
