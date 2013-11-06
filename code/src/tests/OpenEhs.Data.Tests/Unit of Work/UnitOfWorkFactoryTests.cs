/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 1-Feb-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using NHibernate;
using NUnit.Framework;

namespace OpenEhs.Data.Tests.Unit_of_Work
{
    [TestFixture]
    public class UnitOfWorkFactoryTests
    {
        private IUnitOfWorkFactory _factory;

        [SetUp]
        public void SetupContext()
        {
            _factory = (IUnitOfWorkFactory) Activator.CreateInstance(typeof (UnitOfWorkFactory), true);
        }

        [Test]
        public void CanCreateUnitOfWork()
        {
            IUnitOfWork implementor = _factory.Create();
            Assert.IsNotNull(implementor);
            Assert.IsNotNull(_factory.CurrentSession);
            Assert.AreEqual(FlushMode.Commit, _factory.CurrentSession.FlushMode);
        }

        [Test]
        public void CanConfigureNHibernate()
        {
            var configuration = _factory.Configuration;
            Assert.IsNotNull(configuration);
            Assert.AreEqual("NHibernate.Connection.DriverConnectionProvider",
                            configuration.Properties["connection.provider"]);
            Assert.AreEqual("NHibernate.Dialect.MySQLDialect",
                            configuration.Properties["dialect"]);
            Assert.AreEqual("NHibernate.Driver.MySqlDataDriver",
                            configuration.Properties["connection.driver_class"]);
            Assert.AreEqual("Server=127.0.0.1;Database=openehs_database;Uid=OpenEHS_admin;Pwd=password;",
                            configuration.Properties["connection.connection_string"]);
        }

        [Test]
        public void CanCreateAndAccessSessionFactory()
        {
            var sessionFactory = _factory.SessionFactory;
            Assert.IsNotNull(sessionFactory);
        }

        [Test]
        public void AccessingCurrentSessionWhenNoSessionOpenThrows()
        {
            try
            {
                var session = _factory.CurrentSession;
            }
            catch (InvalidOperationException)
            {}
        }
    }
}
