using NUnit.Framework;

namespace OpenEhs.Data.Tests
{
    [TestFixture]
    public class MySqlUtilitiesTests
    {
        [Test]
        public void CanBackupDatabase()
        {
            MySqlUtilities.Backup("openehs_database", "", @"C:\OpenEhsBackup");
        }

        [Test]
        public void CanRestoreDatabase()
        {
            MySqlUtilities.Restore("openehs_database", "", @"C:\OpenEhsBackup\OpenEHS-Backup(20110224).bak");
        }
    }
}
