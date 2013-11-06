using System.ComponentModel.DataAnnotations;
using System.Linq;
using OpenEhs.Domain;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using OpenEhs.Data;
using System.Web;
using System.Web.Mvc;

namespace OpenEhs.Web.Models
{
    #region Models

    /// <summary>
    /// Patient View Model Contains the patient data and the validation rules for patient creation and modifications
    /// </summary>
    public class PatientViewModel
    {
        
        private Patient _patient;

        public PatientViewModel(int patientId)
        {
            _patient = new PatientRepository().Get(patientId);
        }

        #region Patient Properties

        [Required]
        [DisplayName("Patient ID")]
        public int PatientId
        {
            get
            {
                return _patient.Id;
            }
        }

        [Required]
        [DisplayName("First Name")]
        public string FirstName
        {
            get
            {
                return _patient.FirstName;
            }
            set
            {
                _patient.FirstName = value;
            }
        }

        [DisplayName("Middle Name")]
        public string MiddleName
        {
            get
            {
                return _patient.MiddleName;
            }
            set
            {
                _patient.MiddleName = value;
            }
        }

        [Required]
        [DisplayName("Surname")]
        public string LastName
        {
            get
            {
                return _patient.LastName;
            }
            set
            {
                _patient.LastName = value;
            }
        }

        [DisplayName("Occupation")]
        public string Occupation
        {
            get
            {
                return _patient.Occupation;
            }
            set
            {
                _patient.Occupation = value;
            }
        }

        [DisplayName("Insurance Number")]
        public string InsuranceNumber
        {
            get
            {
                return _patient.InsuranceNumber;
            }
            set
            {
                _patient.InsuranceNumber = value;
            }
        }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Creation Date")]
        public DateTime CreationDate
        {
            get
            {
                return _patient.CreationDate;
            }
            set
            {
                _patient.CreationDate = value;
            }
        }

        [DisplayName("Insurance Expiration")]
        public DateTime InsuranceExpiration
        {
            get
            {
                return _patient.InsuranceExpiration;
            }
            set
            {
                _patient.InsuranceExpiration = value;
            }
        }

        [DisplayName("Place of Birth")]
        public string PlaceOfBirth
        {
            get
            {
                return _patient.PlaceOfBirth;
            }
            set
            {
                _patient.PlaceOfBirth = value;
            }
        }

        [Required]
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth
        {
            get
            {
                return _patient.DateOfBirth;
            }
            set
            {
                _patient.DateOfBirth = value;
            }
        }

        [Required]
        [DisplayName("Age")]
        public int Age
        {
            get
            {
                return _patient.Age;
            }
        }

        [Required]
        [DisplayName("Gender")]
        public Gender Gender
        {
            get
            {
                return _patient.Gender;
            }
            set
            {
                _patient.Gender = value;
            }
        }

        [DisplayName("Marital Status")]
        public MaritalStatus MaritalStatus
        {
            get
            {
                return _patient.MaritalStatus;
            }
            set
            {
                _patient.MaritalStatus = value;
            }
        }

        [DisplayName("Education")]
        public Education Education
        {
            get
            {
                return _patient.Education;
            }
            set
            {
                _patient.Education = value;
            }
        }

        [DisplayName("Phone Number")]
        [RegularExpression(@"^([0-9]{3})[ ]{1}([0-9]{3})[ ]{1}([0-9]{4})$", ErrorMessage = "Please format phone to XXX XXX XXXX")]
        public string PhoneNumber
        {
            get
            {
                return _patient.PhoneNumber;
            }
            set
            {
                _patient.PhoneNumber = value;
            }
        }

        [DisplayName("Emergency Contact")]
        public EmergencyContact EmergencyContact
        {
            get
            {
                return _patient.EmergencyContact;
            }
            set
            {
                _patient.EmergencyContact = value;
            }
        }

        [Required]
        [DisplayName("Address")]
        public Address Address
        {
            get
            {
                return _patient.Address;
            }
            set
            {
                _patient.Address = value;
            }
        }

        [DisplayName("Blood Type")]
        public BloodTypes BloodType
        {
            get
            {
                return _patient.BloodType;
            }
            set
            {
                _patient.BloodType = value;
            }
        }

        [DisplayName("Tribe")]
        public Tribes Tribe
        {
            get
            {
                return _patient.Tribe;
            }
            set
            {
                _patient.Tribe = value;
            }
        }

        [DisplayName("Race")]
        public Races Race
        {
            get
            {
                return _patient.Race;
            }
            set
            {
                _patient.Race = value;
            }
        }

        [DisplayName("Religion")]
        public Religions Religion
        {
            get
            {
                return _patient.Religion;
            }
            set
            {
                _patient.Religion = value;
            }
        }

        [DisplayName("Old Record Number")]
        public string OldPhysicalRecordNumber
        {
            get
            {
                return _patient.OldPhysicalRecordNumber;
            }
            set
            {
                _patient.OldPhysicalRecordNumber = value;
            }
        }

        [DisplayName("Active")]
        public bool IsActive
        {
            get
            {
                return _patient.IsActive;
            }
            set
            {
                _patient.IsActive = value;
            }
        }

        [DisplayName("Note")]
        public string Note
        {
            get
            {
                return _patient.Note;
            }
            set
            {
                _patient.Note = value;
            }
        }

        [DisplayName("CheckIns")]
        public IList<PatientCheckIn> PatientCheckIns
        {
            get
            {
                return _patient.PatientCheckIns;
            }
            set
            {
                _patient.PatientCheckIns = value;
            }
        }

        [DisplayName("Problems")]
        public IList<PatientProblem> Problems
        {
            get
            {
                return _patient.Problems;
            }
            set
            {
                _patient.Problems = value;
            }
        }


        [DisplayName("Allergies")]
        public IList<PatientAllergy> PatientAllergies
        {
            get
            {
                return _patient.Allergies;
            }
            set
            {
                _patient.Allergies = value;
            }
        }

        [DisplayName("Immunizations")]
        public IList<PatientImmunization> Immunizations
        {
            get
            {
                return _patient.Immunizations;
            }
            set
            {
                _patient.Immunizations = value;
            }
        }

        [DisplayName("Medications")]
        public IList<PatientMedication> Medications
        {
            get
            {
                return _patient.Medications;
            }
            set
            {
                _patient.Medications = value;
            }
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        public IList<User> Users
        {
            get
            {
                UserRepository userRepo = new UserRepository();
                return userRepo.GetAll();
            }
        }

        /// <summary>
        /// Gets all current medications that aren't expired
        /// </summary>
        public IList<PatientMedication> CurrentMedications
        {
            get
            {
                var currentMeds = from med in Medications
                                  where med.ExpDate >= DateTime.Now
                                  select med;

                return currentMeds.ToList();
            }
        }

        /// <summary>
        /// Gets all medications that are expired
        /// </summary>
        public IList<PatientMedication> PastMedications
        {
            get
            {
                var pastMeds = from med in Medications
                               where med.ExpDate <= DateTime.Now
                               select med;

                return pastMeds.ToList();
            }
        }

        /// <summary>
        /// Get all immunizations
        /// </summary>
        public IList<PatientImmunization> TenImmunization
        {
            get
            {
                var valShots = from immun in Immunizations
                               select immun;

                return valShots.ToList();
            }
        }

        /// <summary>
        /// Gets all immunizations that have been administered ten years or later from now
        /// </summary>
        public IList<PatientImmunization> PriorImmunization
        {
            get
            {
                var valShots = from immun in Immunizations
                               where immun.DateAdministered <= DateTime.Now.AddYears(-10)
                               select immun;

                return valShots.ToList();
            }
        }

        /// <summary>
        /// Gets all immunizations that have been administered over the past ten years
        /// </summary>
        public IList<PatientImmunization> TenYearImmunization
        {
            get
            {
                var valShots = from immun in Immunizations
                               where immun.DateAdministered >= DateTime.Now.AddYears(-10)
                               select immun;

                return valShots.ToList();
            }
        }
         
        /// <summary>
        /// Gets an open checkin
        /// </summary>
        public PatientCheckIn GetOpenCheckin
        {
            get
            {
                try
                {
                    var query = from checkin in PatientCheckIns
                                where checkin.CheckOutTime == DateTime.MinValue
                                select checkin;

                    return query.First<PatientCheckIn>();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Allows you to search the patient check ins
        /// </summary>
        public IList<PatientCheckIn> SearchCheckIns
        {
            get
            {
                var searchVisits = from sci in PatientCheckIns
                                   select sci;

                return searchVisits.ToList();
            }
        }

        /// <summary>
        /// Allows you to search the patient check ins and gets the top patient check in
        /// </summary>
        public IList<PatientCheckIn> SearchCheckInsTop1
        {
            get
            {
                var visitsTopOne = (from sci in PatientCheckIns
                                    orderby sci.Id descending
                                    select sci).Take(1);

                return visitsTopOne.ToList();
            }
        }

        /// <summary>
        /// Get all locations in the hospital
        /// </summary>
        public IList<Location> GetLocations
        {
            get
            {
                LocationRepository locations = new LocationRepository();
                return locations.GetAll();
            }
        }

        /// <summary>
        /// Get all users that can be selected
        /// </summary>
        public IList<User> GetUser
        {
            get
            {
                UserRepository staffMembers = new UserRepository();
                return staffMembers.GetAll();
            }
        }

        /// <summary>
        /// Get all immunizations that can be administered
        /// </summary>
        public IList<Immunization> AllImmunizations
        {
            get
            {
                ImmunizationRepository immun = new ImmunizationRepository();
                return immun.GetAll();
            }
        }

        /// <summary>
        /// Get all problems that a patient can have
        /// </summary>
        public IList<Problem> AllProblems
        {
            get
            {
                ProblemRepository probRepo = new ProblemRepository();
                return probRepo.GetAll();
            }
        }

        /// <summary>
        /// Get all allergies that can be allergic to
        /// </summary>
        public IList<Allergy> AllAllergies
        {
            get
            {
                AllergyRepository allergy = new AllergyRepository();
                return allergy.GetAll();
            }
        }

        /// <summary>
        /// Get all medications that can be selected
        /// </summary>
        public IList<Medication> AllMedications
        {
            get
            {
                MedicationRepository meds = new MedicationRepository();
                return meds.GetAll();
            }
        }

        /// <summary>
        /// Get all active users that have the staff type of physician
        /// </summary>
        public IList<User> GetActivePhysicians
        {
            get
            {
                UserRepository user = new UserRepository();
                return user.FindByType(StaffType.Physician);
            }
        }

        /// <summary>
        /// Get all the invoices for the current patient
        /// </summary>
        public IList<Invoice> Invoices
        {
            get
            {
                InvoiceRepository repo = new InvoiceRepository();
                return repo.FindByPatientId(this._patient.Id);
            }
        }

        /// <summary>
        /// All the products that can be selected
        /// </summary>
        [DisplayName("Products")]
        public IList<Product> Products
        {
            get
            {
                return new ProductRepository().GetAll();
            }
        }

        /// <summary>
        /// All the services that can be selected
        /// </summary>
        [DisplayName("Services")]
        public IList<Service> Services
        {
            get
            {
                return new ServiceRepository().GetAll();
            }
        }

        /// <summary>
        /// Select list for all the different countries
        /// </summary>
        public SelectList Countries
        {
            get
            {
                var types = from Country t in Enum.GetValues(typeof(Country))
                            select new { Id = t, Name = t.ToString() };

                return new SelectList(types, "Id", "Name");
            }
        }

        #endregion



        #region Patient Methods

        /// <summary>
        /// Add encounter to a patient check in
        /// </summary>
        /// <param name="checkIn">check in to add the encounter to</param>
        public void AddEncounter(PatientCheckIn checkIn)
        {
            PatientCheckIns.Add(checkIn);
        }

        /// <summary>
        /// Remove encounter from a patient check in
        /// </summary>
        /// <param name="checkIn">check in to remove the encounter from</param>
        public void RemoveEncounter(PatientCheckIn checkIn)
        {
            PatientCheckIns.Remove(checkIn);
        }

        /// <summary>
        /// Remove the encounter by ID
        /// </summary>
        /// <param name="encounterId">ID of the encounter to remove</param>
        /// <returns>If the encounter was successfully removed</returns>
        public bool RemoveEncounter(int encounterId)
        {
            foreach (PatientCheckIn checkIn in PatientCheckIns)
            {
                if (checkIn.Id == encounterId)
                {
                    PatientCheckIns.Remove(checkIn);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Add an immunization to a patient
        /// </summary>
        /// <param name="immunization"></param>
        public void AddImmunization(PatientImmunization immunization)
        {
            Immunizations.Add(immunization);
        }
        
        /// <summary>
        /// Remove a immunization from a patient
        /// </summary>
        /// <param name="immunization">Immunization to remove</param>
        public void RemoveImmunization(PatientImmunization immunization)
        {
            Immunizations.Remove(immunization);
        }

        /// <summary>
        /// Remove the immunization by ID
        /// </summary>
        /// <param name="immunizationId">ID of the immunization to remove</param>
        /// <returns>If the immunization was successfully removed</returns>
        public bool RemoveImmunization(int immunizationId)
        {
            foreach (PatientImmunization immunization in Immunizations)
            {
                if (immunization.Id == immunizationId)
                {
                    Immunizations.Remove(immunization);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the ID for the invoice on the current PatientCheckIn (not checked out)
        /// </summary>
        /// <returns>The Id of the Invoice.</returns>
        public int GetCheckedInInvoiceId()
        {
            foreach (var checkin in _patient.PatientCheckIns)
            {
                if (checkin.CheckOutTime == null)
                {
                    return checkin.Invoice.Id;
                }
            }
            return 0;
        }

        /// <summary>
        /// Get the current invoice for the patient checked in
        /// </summary>
        public Invoice CurrentInvoice
        {
            get
            {
                foreach (var pci in PatientCheckIns)
                {
                    if (pci.CheckOutTime == DateTime.MinValue)
                    {
                        return pci.Invoice;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Get the current user that is logged into the system
        /// </summary>
        public User CurrentUser
        {
            get
            {
                UserRepository userRepo = new UserRepository();
                return userRepo.Get(HttpContext.Current.User.Identity.Name);
            }
        }

        /// <summary>
        /// Select list for the different types of medication administration types
        /// </summary>
        public SelectList MedicationRouteOfAdministrationType
        {
            get
            {
                var types = from MedicationRouteOfAdministrationType s in Enum.GetValues(typeof(MedicationRouteOfAdministrationType))
                            select new { Id = s, Name = s.ToString() };

                return new SelectList(types, "Id", "Name");
            }
        }

        /// <summary>
        /// Select list of all the different tribes
        /// </summary>
        public SelectList Tribes
        {
            get
            {
                var types = from Tribes s in Enum.GetValues(typeof(Tribes))
                            select new { Id = s, Name = s.ToString() };

                return new SelectList(types, "Id", "Name");
            }
        }

        /// <summary>
        /// Select list of all the different races
        /// </summary>
        public SelectList Races
        {
            get
            {
                var types = from Races s in Enum.GetValues(typeof(Races))
                            select new { Id = s, Name = s.ToString() };

                return new SelectList(types, "Id", "Name");
            }
        }

        /// <summary>
        /// Select list of all the different education levels
        /// </summary>
        public SelectList Educations
        {
            get
            {
                var types = from Education s in Enum.GetValues(typeof(Education))
                            select new { Id = s, Name = s.ToString() };

                return new SelectList(types, "Id", "Name");
            }
        }

        /// <summary>
        /// Select list of all the different marital statuses
        /// </summary>
        public SelectList MaritalStatuses
        {
            get
            {
                var types = from MaritalStatus s in Enum.GetValues(typeof(MaritalStatus))
                            select new { Id = s, Name = s.ToString() };

                return new SelectList(types, "Id", "Name");
            }
        }

        /// <summary>
        /// Select list of all the different genders
        /// </summary>
        public SelectList Genders
        {
            get
            {
                var types = from Gender s in Enum.GetValues(typeof(Gender))
                            select new { Id = s, Name = s.ToString() };

                return new SelectList(types, "Id", "Name");
            }
        }

        #endregion

    }

    #endregion
}