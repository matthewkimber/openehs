/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 1-Feb-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using System.Data;
using NHibernate;

namespace OpenEhs.Data
{
    /// <summary>
    /// Follows the 'Unit of Work' pattern that manages the transactions and commits to the database
    /// </summary>
    public class UnitOfWorkImplementor : IUnitOfWorkImplementor
    {
        #region Fields

        private readonly UnitOfWorkFactory _factory;
        private readonly ISession _session;

        #endregion


        #region Properties

        /// <summary>
        /// Check if the current transaction is active
        /// </summary>
        public bool IsInActiveTransaction
        {
            get
            {
                return _session.Transaction.IsActive;
            }
        }

        /// <summary>
        /// Get the unit of work factory
        /// </summary>
        public IUnitOfWorkFactory Factory
        {
            get
            {
                return _factory;
            }
        }

        /// <summary>
        /// Get a session
        /// </summary>
        public ISession Session
        {
            get
            {
                return _session;
            }
        }

        #endregion


        #region Constructor

        public UnitOfWorkImplementor(UnitOfWorkFactory factory, ISession session)
        {
            _factory = factory;
            _session = session;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Begin transaction.  Transaction begins to allow you to obtain a database connection
        /// and ensures that if anything goes wrong during an insert, update, or delete that
        /// the data integrity won't be compromised.
        /// </summary>
        /// <returns></returns>
        public IGenericTransaction BeginTransaction()
        {
            return new GenericTransaction(_session.BeginTransaction());
        }

        /// <summary>
        /// Begin transaction with an isolation level.
        /// </summary>
        /// <param name="isolationLevel">specifies the transaction locking behavior for the connection</param>
        /// <returns></returns>
        public IGenericTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return new GenericTransaction(_session.BeginTransaction(isolationLevel));
        }

        /// <summary>
        /// Flush the transaction by trying to commit and if nothing goes wrong then you're good.
        /// If commit goes wrong, the transaction will rollback the database
        /// </summary>
        public void TransactionalFlush()
        {
            TransactionalFlush(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Flush the transaction by trying to commit and if nothing goes wrong then you're good.
        /// If commit goes wrong, the transaction will rollback the database
        /// </summary>
        /// <param name="isolationLevel">level of isolation for the transaction to flush</param>
        public void TransactionalFlush(IsolationLevel isolationLevel)
        {
            IGenericTransaction tx = BeginTransaction(isolationLevel);

            try
            {
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
            finally
            {
                tx.Dispose();
            }
        }

        /// <summary>
        /// Flush the session
        /// </summary>
        public void Flush()
        {
            _session.Flush();
        }

        public void IncrementUsages()
        {
            throw new NotImplementedException();
        }

        #region IDisposable Implementation

        public void Dispose()
        {
            _factory.DisposeUnitOfWork(this);
            _session.Dispose();
        }

        #endregion

        #endregion
    }
}