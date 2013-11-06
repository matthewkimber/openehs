namespace OpenEhs.Data
{
    /// <summary>
    /// Structure that follows the 'Unit of Work' pattern that manages the transactions and commits to the database
    /// </summary>
    public interface IUnitOfWorkImplementor : IUnitOfWork
    {
        void IncrementUsages();
    }
}