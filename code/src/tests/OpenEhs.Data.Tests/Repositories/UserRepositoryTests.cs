using System;
using NUnit.Framework;
using OpenEhs.Domain;

namespace OpenEhs.Data.Tests.Repositories
{
    [TestFixture]
    public class UserRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            //UnitOfWork.Start();
        }

        [Test]
        public void CanAddUser()
        {
            var repo = new UserRepository();
            var user = new User
                           {
                               Username = "matthew.kimber",
                               Password = "password",
                               EmailAddress = "matthew.kimber@gmail.com",
                               FirstName = "Matthew",
                               MiddleName = "Scott",
                               LastName = "Kimber",
                               PhoneNumber = "222-1234567",
                               IsActive = true,
                               LicenseNumber = "ABCDEFG123",
                               StaffType = StaffType.Physician,
                               Address = new Address
                                             {
                                                 Street1 = "939 E. 490 N.",
                                                 Street2 = "Apt. 1B",
                                                 City = "Ogden",
                                                 Region = "Utah",
                                                 //Country = "USA",
                                                 IsActive = true
                                             },
                               DateCreated = DateTime.Now,
                               IsApproved = true
                           };

            repo.Add(user);

            UnitOfWork.CurrentSession.Flush();
            UnitOfWork.CurrentSession.Clear();

            var retrievedUser = repo.Get("matthew.kimber");

            Assert.AreEqual(user.Username, retrievedUser.Username);
        }

        [Test]
        public void CanDeleteUser()
        {
            var repo = new UserRepository();
            var user = repo.Get("matthew.kimber");
            repo.Remove(user);
        }
    }
}
