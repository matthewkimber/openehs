/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 1-Feb-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using NHibernate;

namespace OpenEhs.Data
{

    /// <summary>
    /// This represents a generic transaction that is used as database transaction
    /// A transaction allows you to obtain a database connection
    /// and ensures that if anything goes wrong during an insert, update, or delete that
    /// the data integrity won't be compromised.
    /// </summary>
    public class GenericTransaction : IGenericTransaction
    {
        private readonly ITransaction _transaction;

        public GenericTransaction(ITransaction transaction)
        {
            _transaction = transaction;
        }

        /// <summary>
        /// Dispose the current transaction
        /// </summary>
        public void Dispose()
        {
            _transaction.Dispose();
        }

        /// <summary>
        /// Attempt to commit the changes to the database
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
        }

        /// <summary>
        /// Rollback the latest attempted commits
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}