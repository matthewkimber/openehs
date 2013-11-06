/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: 1-Feb-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using NHibernate;
using NHibernate.Cfg;

namespace OpenEhs.Data
{
    /// <summary>
    /// Used to manage connection to the database.  Keeps track of the current unit of work that can be accessed and
    /// used by a class wanting to access the database.
    /// </summary>
    public static class UnitOfWork
    {
        private static readonly IUnitOfWorkFactory _unitOfWorkFactory = new UnitOfWorkFactory();
        public const string CurrentUnitOfWorkKey = "CurrentUnitOfWork.Key";

        /// <summary>
        /// Get the unit of work factory configuration
        /// </summary>
        public static Configuration Configuration
        {
            get { return _unitOfWorkFactory.Configuration; }
        }

        /// <summary>
        /// Get or set the current unit of work from local data based on key
        /// </summary>
        private static IUnitOfWork CurrentUnitOfWork
        {
            get { return Local.Data[CurrentUnitOfWorkKey] as IUnitOfWork; }
            set { Local.Data[CurrentUnitOfWorkKey] = value; }
        }

        /// <summary>
        /// Get the current unit of work
        /// </summary>
        public static IUnitOfWork Current
        {
            get
            {
                var unitOfWork = CurrentUnitOfWork;

                if (unitOfWork == null)
                {
                    throw new InvalidOperationException("You are not in a unit of work.");
                }

                return unitOfWork;
            }
        }

        /// <summary>
        /// Check if the current unit of work
        /// </summary>
        public static bool IsStarted
        {
            get { return CurrentUnitOfWork != null; }
        }

        /// <summary>
        /// Get or set the current session in the unit of work factory
        /// </summary>
        public static ISession CurrentSession
        {
            get { return _unitOfWorkFactory.CurrentSession; }
            internal set { _unitOfWorkFactory.CurrentSession = value; }
        }

        /// <summary>
        /// Start the current unit of work by initializing a new unit of work from the unit of work factory
        /// </summary>
        /// <returns></returns>
        public static IUnitOfWork Start()
        {
            if (CurrentUnitOfWork != null)
            {
                throw new InvalidOperationException("You cannot start more than one unit of work at the same time.");
            }

            var unitOfWork = _unitOfWorkFactory.Create();
            CurrentUnitOfWork = unitOfWork;

            return unitOfWork;
        }

        /// <summary>
        /// Dispose of the current unit of work
        /// </summary>
        /// <param name="unitOfWork"></param>
        public static void DisposeUnitOfWork(IUnitOfWorkImplementor unitOfWork)
        {
            CurrentUnitOfWork = null;
        }
    }
}
