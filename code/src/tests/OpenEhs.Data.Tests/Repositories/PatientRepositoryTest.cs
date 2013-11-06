using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenEhs.Domain;

namespace OpenEhs.Data.Tests.Repositories
{
    [TestFixture]
    public class PatientRepositoryTest
    {
        public PatientRepositoryTest()
        {
            UnitOfWork.Start();
        }

        [Test]
        public void CanGetAllPatients()
        {
            var repo = new PatientRepository();
            var patients = repo.GetAll();

            Assert.AreNotEqual(0, patients.Count);
        }

        [Test]
        public void CanAddNewPatient()
        {
            var repo = new PatientRepository();
            var patient = new Patient
                              {
                                  Address =
                                      new Address
                                          {
                                              Street1 = "123 Main Street",
                                              City = "Accra",
                                              Region = "Greater Accra",
                                              //Country = "Ghana",
                                              IsActive = true
                                          },
                                  //Allergies = new List<Allergy> {new Allergy {Name = "Peanut Butter"}},
                                  DateOfBirth = new DateTime(1980, 03, 17),
                                  //BloodType = "O+",
                                  Gender = Gender.Male,
                                  FirstName = "Matthew",
                                  MiddleName = "Scott",
                                  LastName = "Kimber",
                                  EmergencyContact = new EmergencyContact
                                                         {
                                                             FirstName = "Jan",
                                                             LastName = "Kimber",
                                                             PhoneNumber = "801-479-1717",
                                                             Relationship = Relationship.Mother,
                                                             Address =
                                                                 new Address
                                                                     {
                                                                         Street1 = "1288 E. 4790 N.",
                                                                         City = "Ogden",
                                                                         Region = "Utah",
                                                                         //Country = "USA"
                                                                     }
                                                         },
                                  PhoneNumber = "801-726-8585"
                                  //Religion = "Muslim",
                                  //TribeRace = "Anglo-Saxon"
                              };

            repo.Add(patient);

            UnitOfWork.CurrentSession.Flush();
            UnitOfWork.CurrentSession.Clear();

            repo.Get(100002);
        }
    }
}
