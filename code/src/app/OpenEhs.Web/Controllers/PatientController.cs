using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using OpenEhs.Data;
using OpenEhs.Domain;
using OpenEhs.Web.Models;


namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// Patient Controller contains the functionality to navigate through a patient check in,
    /// handles patient edits, create, and allows you to view details within.
    /// </summary>
    [Authorize(Roles = "OPDClerks, OPDAdministrators, Administrators")]
    public class PatientController : Controller
    {
        private IPatientRepository _patientRepository;
        private IAddressRepository _addressRepository;

        public PatientController()
        {
            _patientRepository = new PatientRepository();
            _addressRepository = new AddressRepository();
        }

        #region Regular Expressions

        private string dateRegExpression = @"(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})|(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})";
        private string phoneRegExpression = @"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})";
        private string patientIDRegExpression = @"[0-9]{6}";
        private string physicalIDRegExpression = @"[0-9]{6,10}";
        private string nameRegExpression = @"[a-zA-Z]+";

        #endregion

        #region ActionResults

        // GET: /Patient/

        /// <summary>
        /// Get the index for patient searching
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var psvModel = new PatientSearchViewModel(new PatientRepository().GetTop25());

            return View(psvModel);
        }

        #region CreatePatient

        /// <summary>
        /// Get the create new patient model
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View(new CreatePatientViewModel());
        }

        /// <summary>
        /// Confirmation that the patient was created
        /// </summary>
        /// <param name="id">the patient to confirm</param>
        /// <returns></returns>
        public ActionResult Confirmation(int id)
        {

            var patient = _patientRepository.Get(id);
            return View(patient);
        }

        /// <summary>
        /// Creates a new patient based on the create patient view model
        /// </summary>
        /// <param name="model">create patient view model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreatePatientViewModel model)
        {

            Address PatientAddress = new Address
            {
                Street1 = model.Street1,
                Street2 = model.Street2,
                City = model.City,
                Region = model.Region,
                Country = model.Country,
                IsActive = true
            };
            _addressRepository.Add(PatientAddress);

            Address ECAddress = new Address
            {
                Street1 = model.EcStreet1,
                City = model.EcCity,
                Region = model.EcRegion,
                Country = model.EcCountry,
                IsActive = true
            };
            _addressRepository.Add(ECAddress);

            var patient = new Patient
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                PlaceOfBirth = model.PlaceOfBirth,
                DateOfBirth = model.DateOfBirth,
                DateOfDeath = DateTime.MinValue,
                MaritalStatus = model.SelectedMaritalStatus,
                Gender = model.SelectedGender,
                Tribe = model.SelectedTribe,
                Race = model.SelectedRace,
                Occupation = model.pOccupation,
                Education = model.SelectedEducation,
                Religion = model.SelectedReligion,
                InsuranceNumber = model.InsuranceNumber,
                OldPhysicalRecordNumber = model.OldPhysicalRecordNumber,
                CreationDate = DateTime.Now,
                Address = PatientAddress,
                EmergencyContact = new EmergencyContact
                {
                    FirstName = model.EcFirstName,
                    LastName = model.EcLastName,
                    PhoneNumber = model.EcPhoneNumber,
                    Relationship = model.EcRelationship,
                    Address = ECAddress,
                    IsActive = true
                },
                IsActive = true
            };

            //add insurance expiration if it's been set
            if (model.InsuranceExpiration != DateTime.MinValue)
            {
                patient.InsuranceExpiration = model.InsuranceExpiration;
            }
            _patientRepository.Add(patient);

            UnitOfWork.CurrentSession.Flush();

            return RedirectToAction("Confirmation", new { id = patient.Id });
        }

        #endregion

        /// <summary>
        /// Gets patient details and passes them to the patient view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var patientViewModel = new PatientViewModel(id);

            HttpContext.Session["CurrentPatient"] = id;

            return View(patientViewModel);
        }

        /// <summary>
        /// Search patient records from criteria in 'Search' box
        /// </summary>
        /// <param name="values">Collection of values from the posted form</param>
        /// <returns>List of patients</returns>
        [HttpPost]
        public ActionResult Index(FormCollection values)
        {
            string searchCriteria = values["PatientSearchTextBox"];    //Get the value entered in the 'Search' field

            //If the search field is empty then return the top 25 default results
            if (string.IsNullOrEmpty(searchCriteria))
                return View(new PatientSearchViewModel(new PatientRepository().GetTop25()));

            IEnumerable<Patient> patients = new List<Patient>();
            string searchTerms = null;

            //Check if the search criteria contains a Date of Birth
            Regex dobRegEx = new Regex(this.dateRegExpression);
            Match m = dobRegEx.Match(searchCriteria);
            if (m.Success)
            {
                //Parse the DOB to English (en) Great Britain (GB) format 'DD/MM/YYYY' for Ghana
                DateTime dob = DateTime.Parse(m.ToString(), new CultureInfo("en-GB"));

                IList<Patient> dobPatients = new PatientRepository().FindByDateOfBirth(dob);    //Find any patients with this DOB

                patients = patients.Union<Patient>(dobPatients); //Add them to the result set

                //If there are results that matched then show this in the search criteria
                if (dobPatients.Count != 0)
                    searchTerms += string.Format("{0}|", m.ToString());
            }

            //Check if the search criteria contains a Phone Number
            Regex phoneRegEx = new Regex(this.phoneRegExpression); //Check for phone number
            m = phoneRegEx.Match(searchCriteria); //Check if the search string matches the phone number
            if (m.Success)
            {
                //Format the phone number to 'XXXXXXXXXX' format to search for it
                string formattedPhoneNumber = phoneRegEx.Replace(m.ToString(), "$1$2$3");

                //Find any patients with this Phone Number
                IList<Patient> phonePatients = new PatientRepository().FindByPhoneNumber(formattedPhoneNumber);

                patients = patients.Union<Patient>(phonePatients);  //Add them to the result set

                //If there are results that matched then show this in the search criteria
                if (phonePatients.Count != 0)
                    searchTerms += string.Format("{0}|", m.ToString());
            }

            //Check if the search criteria contains a Patient ID (6 character numeric string)
            Regex idRegEx = new Regex(this.patientIDRegExpression); //Check for Patient ID number
            m = idRegEx.Match(searchCriteria);  //Check if the search string contains the Patient ID
            if (m.Success)
            {
                //Find any patients with a matching ID
                IList<Patient> idPatients = new PatientRepository().FindByPatientId(Convert.ToInt32(m.ToString()));

                patients = patients.Union<Patient>(idPatients); //Add them to the result set

                //If there are results that matched then show this in the search criteria
                if (idPatients.Count != 0)
                    searchTerms += string.Format("{0}|", m.ToString());
            }

            //Check if the search criteria contains a Patient ID (6 character numeric string)
            Regex physicalIdRegEx = new Regex(this.physicalIDRegExpression); //Check for Patient ID number
            m = physicalIdRegEx.Match(searchCriteria);  //Check if the search string contains the Patient ID
            if (m.Success)
            {
                //Find any patients with a matching ID
                IList<Patient> physicalIdPatients = new PatientRepository().FindByOldPhysicalRecord(m.ToString());

                patients = patients.Union<Patient>(physicalIdPatients); //Add them to the result set

                //If there are results that matched then show this in the search criteria
                if (physicalIdPatients.Count != 0)
                    searchTerms += string.Format("{0}|", m.ToString());
            }

            //Check if the search criteria contains a Patient name
            Regex nameRegEx = new Regex(this.nameRegExpression); //Check for Patient name
            string[] names = searchCriteria.Split(' ');
            foreach (string name in names)
            {
                m = nameRegEx.Match(name);  //Check if the search string contains a Patient name
                if (m.Success)
                {
                    //Find any patients with a matching name
                    IList<Patient> namePatients = new PatientRepository().FindByFirstName(m.ToString());
                    patients = patients.Union<Patient>(namePatients); //Add them to the result set

                    //If there are results that matched then show this in the search criteria
                    if (namePatients.Count != 0)
                        searchTerms += string.Format("{0}|", m.ToString());

                    namePatients = new PatientRepository().FindByMiddleName(m.ToString());
                    patients = patients.Union<Patient>(namePatients); //Add them to the result set

                    //If there are results that matched then show this in the search criteria
                    if (namePatients.Count != 0)
                        searchTerms += string.Format("{0}|", m.ToString());

                    namePatients = new PatientRepository().FindByLastName(m.ToString());
                    patients = patients.Union<Patient>(namePatients); //Add them to the result set

                    //If there are results that matched then show this in the search criteria
                    if (namePatients.Count != 0)
                        searchTerms += string.Format("{0}|", m.ToString());
                }
            }

            var psvModel = new PatientSearchViewModel(patients, searchTerms);

            return View(psvModel);  //Return the merged result set with no duplicates
        }

        #endregion

        #region JsonResults

        /// <summary>
        /// Edit patient from form
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult EditPatient()
        {
            try
            {
                PatientRepository patientRepo = new PatientRepository();
                Patient patient = patientRepo.Get(int.Parse(Request.Form["PatientId"]));
                patient.FirstName = Request.Form["FirstName"];
                patient.MiddleName = Request.Form["MiddleName"];
                patient.LastName = Request.Form["LastName"];
                patient.PlaceOfBirth = Request.Form["PlaceOfBirth"];
                patient.DateOfBirth = DateTime.Parse(Request.Form["DateOfBirth"]);
                patient.PhoneNumber = Request.Form["PhoneNumber"];
                patient.Address.Street1 = Request.Form["Address_Street1"];
                patient.Address.Street2 = Request.Form["Address_Street2"];
                patient.Address.City = Request.Form["Address_City"];
                patient.Address.Region = Request.Form["Address_Region"];
                patient.Address.Country = (Country)Enum.Parse(typeof(Country), Request.Form["Address_Country"]);
                patient.EmergencyContact.FirstName = Request.Form["EC_FirstName"];
                patient.EmergencyContact.LastName = Request.Form["EC_LastName"];
                patient.EmergencyContact.PhoneNumber = Request.Form["EC_PhoneNumber"];
                patient.EmergencyContact.Address.Street1 = Request.Form["EC_Address_Street1"];
                patient.EmergencyContact.Address.Street2 = Request.Form["EC_Address_Street2"];
                patient.EmergencyContact.Address.City = Request.Form["EC_Address_City"];
                patient.EmergencyContact.Address.Region = Request.Form["EC_Address_Region"];
                patient.EmergencyContact.Address.Country = (Country)Enum.Parse(typeof(Country), Request.Form["EC_Address_Country"]);
                patient.Note = Request.Form["Note"];
                patient.Tribe = (Tribes)Enum.Parse(typeof(Tribes), Request.Form["Tribe"]);
                patient.MaritalStatus = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), Request.Form["MaritalStatus"]);
                patient.Gender = (Gender)Enum.Parse(typeof(Gender), Request.Form["Gender"]);
                patient.Race = (Races)Enum.Parse(typeof(Races), Request.Form["Race"]);
                patient.Occupation = Request.Form["Occupation"];
                patient.InsuranceNumber = Request.Form["InsuranceNumber"];
                if (Request.Form["InsuranceExpiration"] != "")
                    patient.InsuranceExpiration = DateTime.Parse(Request.Form["InsuranceExpiration"]);
                else
                    patient.InsuranceExpiration = DateTime.MinValue;
                patient.Education = (Education)Enum.Parse(typeof(Education), Request.Form["Education"]);
                patient.IsActive = bool.Parse(Request.Form["IsActive"]);

                return Json(new
                {
                    FirstName = patient.FirstName,
                    MiddleName = patient.MiddleName,
                    LastName = patient.LastName.ToUpper(),
                    Age = patient.Age,
                    error = "false"
                });
            }
            catch
            {
                return Json(new
                {
                    error = "true"
                });
            }
        }

        #region FeedChart

        /// <summary>
        /// Add new feed to the feed chart
        /// </summary>
        /// <returns></returns>
        public JsonResult AddFeed()
        {
            try
            {
                int patientId = int.Parse(Request.Form["patientID"]);
                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientId);

                //Get current open patient checkin 
                var query = from checkin in patient.PatientCheckIns
                            where checkin.CheckOutTime == DateTime.MinValue
                            select checkin;
                PatientCheckIn openCheckIn = query.First<PatientCheckIn>();

                //Create new feed chart object and add appropriate parameters 
                FeedChart feedchart = new FeedChart();

                feedchart.PatientCheckIn = openCheckIn;

                feedchart.FeedTime = DateTime.Now;

                string feedType = Request.Form["feedType"];
                feedchart.FeedType = feedType;

                string amountOffered = Request.Form["amountOffered"];
                feedchart.AmountOffered = amountOffered;

                string amountTaken = Request.Form["amountTaken"];
                feedchart.AmountTaken = amountTaken;

                string vomit = Request.Form["vomit"];
                feedchart.Vomit = vomit;

                string urine = Request.Form["urine"];
                feedchart.Urine = urine;

                string stool = Request.Form["stool"];
                feedchart.Stool = stool;

                string comments = Request.Form["comments"];
                feedchart.Comments = comments;


                //Add new feed chart object to patient
                openCheckIn.FeedChart.Add(feedchart);

                //Return results as JSON
                return Json(new
                {
                    error = "false",
                    status = "Successfully added chart.",
                    Date = feedchart.FeedTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    feedchart.FeedType,
                    feedchart.AmountOffered,
                    feedchart.AmountTaken,
                    feedchart.Vomit,
                    feedchart.Urine,
                    feedchart.Stool,
                    feedchart.Comments
                });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = "Unable to add feed chart successfully",
                    errorMessage = e.Message
                });
            }
        }

        /// <summary>
        /// Add intake to feed chart
        /// </summary>
        /// <returns></returns>
        public JsonResult AddIntake()
        {
            try
            {
                int patientId = int.Parse(Request.Form["patientID"]);
                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientId);

                //Get current open patient checkin 
                var query = from checkin in patient.PatientCheckIns
                            where checkin.CheckOutTime == DateTime.MinValue
                            select checkin;
                PatientCheckIn openCheckIn = query.First<PatientCheckIn>();

                //Create new intake chart object and add appropriate parameters 
                IntakeChart intakechart = new IntakeChart();

                intakechart.PatientCheckIn = openCheckIn;

                intakechart.ChartTime = DateTime.Now;

                string kindoffluid = Request.Form["kindoffluid"];
                intakechart.KindOfFluid = kindoffluid;

                string amount = Request.Form["amount"];
                intakechart.Amount = amount;

                //Add new intake object to patient
                openCheckIn.IntakeChart.Add(intakechart);

                //Return results as JSON
                return Json(new
                {
                    error = "false",
                    status = "Successfully added chart.",
                    Date = intakechart.ChartTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    intakechart.KindOfFluid,
                    intakechart.Amount
                });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = "Unable to add intake chart successfully",
                    errorMessage = e.Message
                });
            }
        }

        /// <summary>
        /// Add output to the feed chart
        /// </summary>
        /// <returns></returns>
        public JsonResult AddOutput()
        {
            try
            {
                int patientId = int.Parse(Request.Form["patientID"]);
                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientId);

                //Get current open patient checkin 
                var query = from checkin in patient.PatientCheckIns
                            where checkin.CheckOutTime == DateTime.MinValue
                            select checkin;
                PatientCheckIn openCheckIn = query.First<PatientCheckIn>();

                //Create new intake chart object and add appropriate parameters 
                OutputChart outputchart = new OutputChart();

                outputchart.PatientCheckIn = openCheckIn;

                outputchart.ChartTime = DateTime.Now;

                string ngAmount = Request.Form["ngAmount"];
                outputchart.NGSuctionAmount = ngAmount;

                string ngColor = Request.Form["ngColor"];
                outputchart.NGSuctionColor = ngColor;

                string urineAmount = Request.Form["urineAmount"];
                outputchart.UrineAmount = urineAmount;

                string stoolAmount = Request.Form["stoolAmount"];
                outputchart.StoolAmount = stoolAmount;

                string stoolColor = Request.Form["stoolColor"];
                outputchart.StoolColor = stoolColor;

                //Add new output object to patient
                openCheckIn.OutputChart.Add(outputchart);

                //Return results as JSON
                return Json(new
                {
                    error = "false",
                    status = "Successfully added chart.",
                    Date = outputchart.ChartTime.ToString("dd/MM/yyyy HH:mm:ss"),
                    outputchart.NGSuctionAmount,
                    outputchart.NGSuctionColor,
                    outputchart.UrineAmount,
                    outputchart.StoolAmount,
                    outputchart.StoolColor
                });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = "Unable to add intake chart successfully",
                    errorMessage = e.Message
                });
            }
        }

        #endregion

        #region Allergy

        /// <summary>
        /// Create new allergy
        /// </summary>
        /// <returns></returns>
        public JsonResult CreateNewAllergy()
        {
            try
            {
                AllergyRepository allergy = new AllergyRepository();
                Allergy newAllergy = new Allergy();

                newAllergy.Name = Request.Form["AllergyName"];
                newAllergy.IsActive = true;

                allergy.Add(newAllergy);

                return Json(new
                {
                    error = false,
                    status = "Added new allergy successfully!",
                    newAllergy.Id,
                    newAllergy.Name
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = "Unable to add allergy!",
                    errorMessage = e.Message
                });
            }
        }

        /// <summary>
        /// Add allergy to the current patient
        /// </summary>
        /// <returns></returns>
        public JsonResult AddAllergyToPatient()
        {
            try
            {
                int patientId = int.Parse(Request.Form["PatientId"]);
                int allergyId = int.Parse(Request.Form["AllergyId"]);

                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientId);

                PatientAllergyRepository patientAllergyRepo = new PatientAllergyRepository();
                AllergyRepository allergyRepo = new AllergyRepository();

                PatientAllergy pallergy = new PatientAllergy();
                pallergy.Allergy = allergyRepo.Get(allergyId);
                pallergy.Patient = patient;
                patientAllergyRepo.Add(pallergy);

                return Json(new
                {
                    error = "false",
                    status = "Added patient allergy successfully",
                    ID = pallergy.Id,
                    Name = pallergy.Allergy.Name
                });

            }
            catch (Exception e)
            {

                return Json(new
                {
                    error = "true",
                    status = "Unable to add patient allergy!",
                    errorMessage = e.Message
                });
            }
        }

        /// <summary>
        /// Remove allergy from patient
        /// </summary>
        /// <returns></returns>
        public JsonResult RemoveAllergy()
        {
            try
            {
                PatientAllergyRepository patientAllergyRepo = new PatientAllergyRepository();
                PatientRepository patientRepo = new PatientRepository();
                AllergyRepository allergyRepo = new AllergyRepository();

                var patientId = patientRepo.Get(int.Parse(Request.Form["patientID"]));
                var allergyId = allergyRepo.Get(int.Parse(Request.Form["allergyID"]));

                PatientAllergy pa = new PatientAllergy();

                pa.Allergy = allergyId;
                pa.Patient = patientId;

                patientAllergyRepo.Remove(pa);

                //UnitOfWork.CurrentSession.Flush();

                return Json(new
                {
                    error = "false",
                    status = "Allergy Deleted"
                });
            }
            catch (Exception e)
            {

                return Json(new
                {
                    error = "true",
                    status = "Unable to remove patient allergy!",
                    errorMessage = e.Message
                });
            }
        }

        #endregion

        #region Vitals

        /// <summary>
        /// Add vitals to patient
        /// </summary>
        /// <returns></returns>
        public JsonResult AddVital()
        {
            try
            {
                //Get current patient object
                int patientID = int.Parse(Request.Form["patientID"]);
                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientID);

                //Get current open patient checkin 
                var query = from checkin in patient.PatientCheckIns
                            where checkin.CheckOutTime == DateTime.MinValue
                            select checkin;
                PatientCheckIn openCheckIn = query.First<PatientCheckIn>();

                //Create new vitals object and add appropriate parameters 
                Vitals vitals = new Vitals();
                vitals.PatientCheckIn = openCheckIn;
                if (Request.Form["height"] != "")
                    vitals.Height = double.Parse(Request.Form["height"]);
                if (Request.Form["weight"] != "")
                    vitals.Weight = double.Parse(Request.Form["weight"]);

                BloodPressure bp = new BloodPressure();
                if (Request.Form["BpDiastolic"] != "" && Request.Form["BpSystolic"] != "")
                {
                    bp.Diastolic = int.Parse(Request.Form["BpDiastolic"]);
                    bp.Systolic = int.Parse(Request.Form["BpSystolic"]);
                }
                vitals.BloodPressure = bp;
                if (Request.Form["HeartRate"] != "")
                    vitals.HeartRate = int.Parse(Request.Form["HeartRate"]);
                if (Request.Form["RespiratoryRate"] != "")
                    vitals.RespiratoryRate = int.Parse(Request.Form["RespiratoryRate"]);
                if (Request.Form["Temperature"] != "")
                    vitals.Temperature = float.Parse(Request.Form["Temperature"]);
                vitals.Type = (VitalsType)Enum.Parse(typeof(VitalsType), Request.Form["type"]);
                vitals.Time = DateTime.Parse(Request.Form["Date"] + " " + Request.Form["Time"]);
                vitals.IsActive = true;

                //Add new vitals object to patient
                openCheckIn.Vitals.Add(vitals);

                //Return results as JSON
                return Json(new
                {
                    error = "false",
                    status = "Successfully added vital.",
                    date = vitals.Time.ToString("MM/dd/yyyy HH:mm:ss"),
                    height = vitals.Height,
                    weight = vitals.Weight,
                    bpDiastolic = vitals.BloodPressure.Diastolic,
                    bpSystolic = vitals.BloodPressure.Systolic,
                    heartRate = vitals.HeartRate,
                    respiratoryRate = vitals.RespiratoryRate,
                    temperature = vitals.Temperature,
                    type = Enum.GetName(typeof(VitalsType), vitals.Type)
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = e.Message
                });
            }
        }

        #endregion

        #region Checkin/Checkout

        /// <summary>
        /// Check in a patient
        /// </summary>
        /// <returns></returns>
        public JsonResult AddCheckIn()
        {
            try
            {
                //Get patient object
                int patientID = int.Parse(Request.Form["patientID"]);
                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientID);

                //Get Staff Object
                int staffId = int.Parse(Request.Form["staffID"]);
                UserRepository userRepo = new UserRepository();
                var staff = userRepo.Get(staffId);

                //Get Location Object
                int locationId = int.Parse(Request.Form["locationID"]);
                LocationRepository locationRepo = new LocationRepository();
                var location = locationRepo.Get(locationId);

                //Build Check In Object
                PatientCheckIn checkin = new PatientCheckIn();
                checkin.Patient = patient;
                checkin.CheckInTime = DateTime.Now;
                checkin.PatientType = (PatientCheckinType)Enum.Parse(typeof(PatientCheckinType), Request.Form["patientType"]);
                checkin.AttendingStaff = staff;
                checkin.Location = location;
                checkin.IsActive = true;

                //Build Invoice Object
                Invoice invoice = new Invoice();
                invoice.PatientCheckIn = checkin;
                checkin.Invoice = invoice;

                patient.PatientCheckIns.Add(checkin);
                new InvoiceRepository().Add(invoice);

                return Json(new
                {
                    error = "false"
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = e.Message
                });
            }
        }

        /// <summary>
        /// Check out a patient
        /// </summary>
        /// <returns></returns>
        public JsonResult CheckOut()
        {
            try
            {
                int patientId = int.Parse(Request.Form["patientID"]);
                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientId);

                var query = from checkin in patient.PatientCheckIns
                            where checkin.CheckOutTime == DateTime.MinValue
                            select checkin;

                PatientCheckIn checkIn = query.First<PatientCheckIn>();

                var surgeryQuery = from surgery in checkIn.Surgeries
                                   where surgery.EndTime == DateTime.MinValue
                                   select surgery;

                if (surgeryQuery.Count<Surgery>() > 0)
                {
                    Surgery openSurgery = surgeryQuery.First<Surgery>();
                    openSurgery.EndTime = DateTime.Now;
                }

                return Json(new
                {
                    error = "false"
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = e.Message
                });
            }
        }

        /// <summary>
        /// Gets the current check in for the patient
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCurrentCheckin()
        {
            try
            {
                //Get patient object
                int patientID = int.Parse(Request.Form["patientID"]);
                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientID);

                var query = from checkin in patient.PatientCheckIns
                            where checkin.CheckOutTime == DateTime.MinValue
                            select checkin;


                if (query.Count<PatientCheckIn>() > 0)
                {
                    PatientCheckIn checkIn = query.First<PatientCheckIn>();
                    return Json(new
                    {
                        error = "false",
                        checkin = checkIn.Id
                    });
                }
                else
                {

                    return Json(new
                    {
                        error = "false",
                        checkin = "null"
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = e.Message
                });
            }
        }

        #endregion

        #region PastMedicalHistory

        /// <summary>
        /// Search patient visits between a time range to get history 
        /// </summary>
        /// <returns></returns>
        public JsonResult SearchVisit()
        {
            int patientID = int.Parse(Request.Form["patientID"]);
            PatientRepository patientRepo = new PatientRepository();
            var patient = patientRepo.Get(patientID);

            DateTime fromDate = DateTime.Parse(Request.Form["from"]);
            DateTime toDate = DateTime.Parse(Request.Form["to"]);

            var query = from checkin in patient.PatientCheckIns
                        where checkin.CheckInTime >= fromDate && checkin.CheckInTime <= toDate
                        select checkin;

            var resultSet = new List<object>();
            var jsonResult = new JsonResult();

            foreach (var result in query)
            {
                IList<object> vitalsList = new List<object>();

                resultSet.Add(new
                {
                    date = result.CheckInTime.ToString("dd/MM/yyyy HH:mm:ss")
                });
            }

            jsonResult.Data = resultSet;

            return jsonResult;
        }

        /// <summary>
        /// Get list of visits with each visits details
        /// </summary>
        /// <returns></returns>
        public JsonResult SearchVisitList()
        {
            try
            {

                int patientID = int.Parse(Request.Form["patientID"]);
                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientID);

                int checkInID = int.Parse(Request.Form["checkInID"]);

                var query = from checkin in patient.PatientCheckIns
                            where checkin.Id == checkInID
                            select checkin;

                var resultSet = new List<object>();
                var jsonResult = new JsonResult();

                foreach (var result in query)
                {
                    IList<object> visitList = new List<object>();

                    foreach (var vitals in result.Vitals)
                    {
                        visitList.Add(new
                        {
                            Time = vitals.Time.ToString("dd/MM/yyyy HH:mm:ss"),
                            //vitals.Type,
                            type = Enum.GetName(typeof(VitalsType), vitals.Type),
                            Height = vitals.Height,
                            Weight = vitals.Weight,
                            Temperature = vitals.Temperature,
                            HeartRate = vitals.HeartRate,
                            BpDiastolic = vitals.BloodPressure.Diastolic,
                            BpSystolic = vitals.BloodPressure.Systolic,
                            RespiratoryRate = vitals.RespiratoryRate
                        });
                    }

                    IList<object> feedList = new List<object>();

                    foreach (var a in result.FeedChart)
                    {
                        feedList.Add(new
                        {
                            Time = a.FeedTime.ToString("dd/MM/yyyy HH:mm:ss"),
                            Type = a.FeedType,
                            AmountOffered = a.AmountOffered,
                            AmountTaken = a.AmountTaken,
                            Vomit = a.Vomit,
                            Urine = a.Urine,
                            Stool = a.Stool,
                            Comments = a.Comments
                        });
                    }

                    IList<object> outputList = new List<object>();

                    foreach (var b in result.OutputChart)
                    {
                        outputList.Add(new
                        {
                            Time = b.ChartTime.ToString("dd/MM/yyyy HH:mm:ss"),
                            b.NGSuctionAmount,
                            b.NGSuctionColor,
                            b.UrineAmount,
                            b.StoolAmount,
                            b.StoolColor
                        });
                    }

                    IList<object> intakeList = new List<object>();

                    foreach (var c in result.IntakeChart)
                    {
                        intakeList.Add(new
                        {
                            Time = c.ChartTime.ToString("dd/MM/yyyy HH:mm:ss"),
                            c.ChartTime,
                            c.KindOfFluid,
                            c.Amount
                        });
                    }

                    IList<object> noteList = new List<object>();

                    foreach (var g in result.Notes.Where(g => g.Type == NoteType.General))
                    {
                        noteList.Add(new
                        {
                            g.Body,
                            g.Author.FirstName,
                            g.Author.LastName
                        });
                    }

                    IList<object> noteSurgeryList = new List<object>();

                    foreach (var g in result.Notes.Where(g => g.Type == NoteType.Surgery))
                    {
                        noteSurgeryList.Add(new
                        {
                            g.Body
                        });
                    }



                    IList<object> surgery = new List<object>();
                    foreach (var o in result.Surgeries)
                    {
                        surgery.Add(new
                        {
                            StartTime = o.StartTime.ToString("dd/MM/yyyy HH:mm:ss"),
                            EndTime = o.EndTime.ToString("dd/MM/yyyy HH:mm:ss"),
                            CaseType = Enum.GetName(typeof(CaseType), o.CaseType),
                            Staff = GetStaff(o)
                        });
                    }


                    resultSet.Add(new
                    {
                        date = result.CheckInTime.ToString("dd/MM/yyyy HH:mm:ss"),
                        firstName = result.AttendingStaff.FirstName,
                        lastName = result.AttendingStaff.LastName,
                        Vitals = visitList,
                        FeedChart = feedList,
                        OutputChart = outputList,
                        IntakeChart = intakeList,
                        Note = noteList,
                        SurgeryNote = noteSurgeryList,
                        Surgery = surgery
                    });
                }

                jsonResult.Data = resultSet;

                return jsonResult;
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = "Unable to fetch list successfully"
                    //errorMessage = e.Message
                });
            }
        }

        /// <summary>
        /// Get the surgury staff for a certain surgery
        /// </summary>
        /// <param name="surgery">sugery to get the staff for</param>
        /// <returns></returns>
        private IList<object> GetStaff(Surgery surgery)
        {
            IList<object> result = new List<object>();
            foreach (var staff in surgery.Staff)
            {
                result.Add(new
                {
                    Role = Enum.GetName(typeof(StaffRole), staff.Role),
                    Name = staff.Staff.FirstName + " " + staff.Staff.LastName

                });
            }
            return result;

        }

        #endregion

        #region Surgery

        /// <summary>
        /// Add new surgery to open check in
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddSurgery()
        {
            try
            {
                //Repositories
                PatientRepository patientRepo = new PatientRepository();
                UserRepository userRepo = new UserRepository();
                LocationRepository locationRepo = new LocationRepository();
                SurgeryStaffRepository ssRepo = new SurgeryStaffRepository();

                //Build new objects
                Surgery surgery = new Surgery();
                Note note = new Note();

                //Get patient object
                int patientID = int.Parse(Request.Form["patientID"]);
                Patient patient = patientRepo.Get(patientID);

                //Get current open patient checkin 
                var query = from checkin in patient.PatientCheckIns
                            where checkin.CheckOutTime == DateTime.MinValue
                            select checkin;
                PatientCheckIn openCheckIn = query.First<PatientCheckIn>();

                surgery.Location = locationRepo.Get(int.Parse(Request.Form["theatreNumber"]));
                surgery.StartTime = DateTime.Parse(Request.Form["startTime"]);
                surgery.EndTime = DateTime.Parse(Request.Form["endTime"]);

                //Add to checkin
                openCheckIn.Surgeries.Add(surgery);
                surgery.CheckIn = openCheckIn;
                surgery.CaseType = (CaseType)Enum.Parse(typeof(CaseType), Request.Form["caseType"]);

                //Build Note
                User author = userRepo.Get(int.Parse(Request.Form["staffID"]));
                note.Author = author;
                note.Body = Request.Form["NoteBody"];
                note.PatientCheckIns = openCheckIn;
                note.Title = "";
                note.Type = NoteType.Surgery;
                note.IsActive = true;
                note.DateCreated = DateTime.Now;
                openCheckIn.Notes.Add(note);

                UnitOfWork.CurrentSession.Flush();

                //Surgeon
                if (Request.Form["surgeon"] != "")
                {
                    SurgeryStaff surgeon = new SurgeryStaff();
                    surgeon.Staff = userRepo.Get(int.Parse(Request.Form["surgeon"]));
                    surgeon.Surgery = surgery;
                    surgeon.Role = StaffRole.Surgeon;
                    ssRepo.Add(surgeon);
                }

                //Surgeon Assistant
                if (Request.Form["surgeonAssistant"] != "")
                {
                    SurgeryStaff surgeonAssistant = new SurgeryStaff();
                    surgeonAssistant.Staff = userRepo.Get(int.Parse(Request.Form["surgeonAssistant"]));
                    surgeonAssistant.Surgery = surgery;
                    surgeonAssistant.Role = StaffRole.SurgeonAssistant;
                    ssRepo.Add(surgeonAssistant);

                }
                //Anaesthetist
                if (Request.Form["anaesthetist"] != "")
                {
                    SurgeryStaff anaesthetist = new SurgeryStaff();
                    anaesthetist.Staff = userRepo.Get(int.Parse(Request.Form["anaesthetist"]));
                    anaesthetist.Role = StaffRole.Anaesthetist;
                    anaesthetist.Surgery = surgery;
                    ssRepo.Add(anaesthetist);
                }
                //Anaesthetist Assistant
                if (Request.Form["anaesthetistAssistant"] != "")
                {
                    SurgeryStaff anaesthetistAssistant = new SurgeryStaff();
                    anaesthetistAssistant.Staff = userRepo.Get(int.Parse(Request.Form["anaesthetistAssistant"]));
                    anaesthetistAssistant.Role = StaffRole.AnaesthetistAssistant;
                    anaesthetistAssistant.Surgery = surgery;
                    ssRepo.Add(anaesthetistAssistant);
                }
                //Nurse
                if (Request.Form["nurse"] != "")
                {
                    SurgeryStaff nurse = new SurgeryStaff();
                    nurse.Staff = userRepo.Get(int.Parse(Request.Form["nurse"]));
                    nurse.Role = StaffRole.Nurse;
                    nurse.Surgery = surgery;
                    ssRepo.Add(nurse);
                }
                //Consultant
                if (Request.Form["consultant"] != "")
                {
                    SurgeryStaff consultant = new SurgeryStaff();
                    consultant.Staff = userRepo.Get(int.Parse(Request.Form["consultant"]));
                    consultant.Role = StaffRole.Consultant;
                    consultant.Surgery = surgery;
                    ssRepo.Add(consultant);
                }

                //Save Template
                if (Request.Form["NoteTitle"] != null)
                {
                    TemplateRepository templateRepo = new TemplateRepository();
                    NoteTemplateRepository noteRepo = new NoteTemplateRepository();
                    NoteTemplateCategory noteCat = noteRepo.Get(2);
                    Template template = new Template();
                    template.Title = Request.Form["NoteTitle"];
                    template.Staff = author;
                    template.Body = note.Body;
                    template.IsActive = true;
                    template.NoteTemplateCategory = noteCat;
                    templateRepo.Add(template);
                    return Json(new
                    {
                        Id = template.Id,
                        Name = template.Title,
                        NoteBody = note.Body,
                        error = "false"
                    });
                }
                return Json(new
                {
                    error = "false"
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = e.Message
                });
            }
        }

        #endregion

        #region Medication

        /// <summary>
        /// Create new medication
        /// </summary>
        /// <returns></returns>
        public JsonResult CreateNewMedication()
        {
            try
            {
                Medication pMed = new Medication();

                pMed.Name = Request.Form["MedicationName"];
                pMed.Description = Request.Form["MedicationDescription"];
                pMed.IsActive = true;

                MedicationRepository medicationRepo = new MedicationRepository();

                medicationRepo.Add(pMed);

                return Json(new
                                {
                                    error = "false",
                                    Name = pMed.Name,
                                    Id = pMed.Id
                                });

            }
            catch (Exception e)
            {
                return Json(new
                                {
                                    error = "true",
                                    status = "Unable to add new medication successfully",
                                    errorMessage = e.Message
                                });
            }
        }

        /// <summary>
        /// Add medication to patient
        /// </summary>
        /// <returns></returns>
        public JsonResult AddMedicationToPatient()
        {
            try
            {
                PatientRepository repo = new PatientRepository();
                Patient patient = repo.Get(int.Parse(Request.Form["patientID"]));
                PatientMedicationRepositiry pmr = new PatientMedicationRepositiry();
                MedicationRepository medRepo = new MedicationRepository();

                PatientMedication pMed = new PatientMedication();
                pMed.Medication = medRepo.Get(int.Parse(Request.Form["name"]));
                pMed.Instruction = Request.Form["instructions"];
                pMed.StartDate = DateTime.Now;
                pMed.ExpDate = DateTime.Parse(Request.Form["expDate"]);
                pMed.Administration = (MedicationRouteOfAdministrationType)Enum.Parse(typeof(MedicationRouteOfAdministrationType), Request.Form["route"]);
                pMed.Dose = Request.Form["dose"];
                pMed.Frequency = Request.Form["frequency"];
                pMed.Patient = patient;

                pmr.Add(pMed);

                return Json(new
                                {
                                    error = "false",
                                    id = pMed.Medication.Id,
                                    name = pMed.Medication.Name,
                                    dose = pMed.Dose,
                                    frequency = pMed.Frequency,
                                    route = Enum.GetName(typeof(OpenEhs.Domain.MedicationRouteOfAdministrationType), pMed.Administration),
                                    instructions = pMed.Instruction,
                                    startDate = pMed.StartDate.ToString("dd/MM/yyyy"),
                                    expDate = pMed.ExpDate.ToString("dd/MM/yyyy")
                                });


            }
            catch (Exception e)
            {
                return Json(new
                                {
                                    error = "true",
                                    status = "Unable to add medication to patient",
                                    errorMessage = e.Message
                                });
            }
        }

        #endregion

        #region Billing

        /// <summary>
        /// Add an item to the patient's invoice.
        /// </summary>
        /// <returns>The Jason object so it can be immediately displayed.</returns>
        public JsonResult AddInvoiceItem()
        {
            try
            {
                //Build Line Item objects
                InvoiceItem lineItem = new InvoiceItem();

                //Get patient object
                int patientID = int.Parse(Request.Form["patientID"]);
                PatientRepository patientRepo = new PatientRepository();
                var patient = patientRepo.Get(patientID);

                //Get current open patient checkin 
                var query = from checkin in patient.PatientCheckIns
                            where checkin.CheckOutTime == DateTime.MinValue
                            select checkin;
                PatientCheckIn openCheckIn = query.First<PatientCheckIn>();

                //Invoice Repository
                InvoiceRepository invoiceRepo = new InvoiceRepository();

                //Product Repository
                ProductRepository productRepo = new ProductRepository();

                //Service Repository
                ServiceRepository serviceRepo = new ServiceRepository();

                //Quantity
                if (Request.Form["quantity"] != "")
                {
                    lineItem.Quantity = int.Parse(Request.Form["quantity"]);
                    lineItem.Invoice = openCheckIn.Invoice;
                    lineItem.IsActive = true;

                    //Product
                    if (Request.Form["product"] != "")
                    {
                        lineItem.Product = productRepo.Get(int.Parse(Request.Form["product"]));
                        lineItem.Service = null;
                        invoiceRepo.AddLineItem(lineItem);
                        UnitOfWork.CurrentSession.Flush();
                        return Json(new
                        {
                            error = "false",
                            lineItem.Product.Name,
                            lineItem.Quantity

                        });
                    } //Service
                    else if (Request.Form["service"] != "")
                    {
                        lineItem.Service = serviceRepo.Get(int.Parse(Request.Form["service"]));
                        lineItem.Product = null;
                        invoiceRepo.AddLineItem(lineItem);
                        UnitOfWork.CurrentSession.Flush();
                        return Json(new
                        {
                            error = "false",
                            lineItem.Service.Name,
                            lineItem.Quantity
                        });
                    }
                }


                return Json(new
                {
                    error = "false"
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = e.Message
                });
            }
        }


        #endregion

        #region Diagnosis

        /// <summary>
        /// Add note to diagnosis
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddNote()
        {
            try
            {
                PatientRepository patientRepo = new PatientRepository();
                UserRepository userRepo = new UserRepository();
                User staff = new User();

                if (Request.Form["StaffId"] != "")
                    staff = userRepo.Get(int.Parse(Request.Form["StaffId"]));

                Patient patient = patientRepo.Get(int.Parse(Request.Form["PatientId"]));

                Note note = new Note();

                var query = from checkin in patient.PatientCheckIns
                            where checkin.CheckOutTime == DateTime.MinValue
                            select checkin;
                PatientCheckIn openCheckIn = query.First<PatientCheckIn>();
                note.Author = staff;
                note.DateCreated = DateTime.Now;
                note.Body = Request.Form["NoteBody"]; //HttpUtility.UrlDecode(Request.Form["NoteBody"], System.Text.Encoding.Default);
                note.PatientCheckIns = openCheckIn;
                note.Title = "";
                note.Type = NoteType.General;
                note.IsActive = true;
                openCheckIn.Notes.Add(note);

                if (Request.Form["TemplateTitle"] != null && Request.Form["TemplateTitle"] != "")
                {
                    TemplateRepository templateRepo = new TemplateRepository();
                    NoteTemplateRepository noteRepo = new NoteTemplateRepository();
                    NoteTemplateCategory noteCat = noteRepo.Get(1);
                    Template template = new Template();
                    template.Title = Request.Form["TemplateTitle"];
                    template.Staff = staff;
                    template.Body = note.Body;
                    template.IsActive = true;
                    template.NoteTemplateCategory = noteCat;
                    templateRepo.Add(template);
                    return Json(new
                    {
                        templateId = template.Id,
                        templateTitle = template.Title,
                        NoteBody = note.Body,
                        error = "false"
                    });
                }
                return Json(new
                {
                    NoteBody = note.Body,
                    error = "false"
                });
            }
            catch
            {
                return Json(new
                {
                    error = "true"
                });
            }
        }

        /// <summary>
        /// Get the detail for a certain template
        /// </summary>
        /// <returns></returns>
        public JsonResult TemplateDetail()
        {
            try
            {
                TemplateRepository templateRepo = new TemplateRepository();
                Template template = templateRepo.Get(int.Parse(Request.Form["ID"]));
                return Json(new
                {
                    body = template.Body,
                    error = "false"
                });
            }
            catch
            {
                return Json(new
                {
                    error = "true"
                });
            }

        }
        #endregion

        #region PatientSearch

        /// <summary>
        /// Get suggestions for patient search autocomplete
        /// </summary>
        /// <param name="term">search term</param>
        /// <returns></returns>
        public JsonResult AutoCompleteSuggestions(string term)
        {
            List<string> suggestions = new List<string>();

            try
            {
                //Parse the DOB to English (en) Great Britain (GB) format 'DD/MM/YYYY' for Ghana
                DateTime dob = DateTime.Parse(term, new CultureInfo("en-GB"));
                IList<Patient> dobPatients = new PatientRepository().FindByDateOfBirth(dob);    //Find any patients with this DOB
                foreach (Patient patient in dobPatients)
                {
                    suggestions.Add(string.Format("[{0}] {1}, {2} {3} ({4})", patient.Id, patient.LastName, patient.FirstName, patient.MiddleName, patient.DateOfBirth.ToShortDateString()));
                }
            }
            catch (Exception e) { }

            try
            {
                IList<Patient> dobPatients = new PatientRepository().FindByDateOfBirthPiece(term);    //Find any patients with this DOB
                foreach (Patient patient in dobPatients)
                {
                    suggestions.Add(string.Format("[{0}] {1}, {2} {3} ({4})", patient.Id, patient.LastName, patient.FirstName, patient.MiddleName, patient.DateOfBirth.ToShortDateString()));
                }
            }
            catch (Exception e) { }


            try
            {
                //Find any patients with this Phone Number
                IList<Patient> phonePatients = new PatientRepository().FindByPhoneNumber(term);
                foreach (Patient patient in phonePatients)
                {
                    string phoneNo = string.Format("{0} {1} {2}", patient.PhoneNumber.Substring(0, 3), patient.PhoneNumber.Substring(3, 3), patient.PhoneNumber.Substring(6, 4));
                    suggestions.Add(string.Format("{5} - [{0}] {1}, {2} {3} ({4})", patient.Id, patient.LastName, patient.FirstName, patient.MiddleName, patient.DateOfBirth.ToShortDateString(), phoneNo));
                }
            }
            catch (Exception e) { }

            try
            {
                //Find any patients with a matching ID
                IList<Patient> idPatients = new PatientRepository().FindByPatientIdPiece(term);
                foreach (Patient patient in idPatients)
                {
                    suggestions.Add(string.Format("[{0}] {1}, {2} {3} ({4})", patient.Id, patient.LastName, patient.FirstName, patient.MiddleName, patient.DateOfBirth.ToShortDateString()));
                }
            }
            catch (Exception e) { }

            try
            {
                //Find any patients with a matching ID
                IList<Patient> physicalIdPatients = new PatientRepository().FindByOldPhysicalRecord(term);
                foreach (Patient patient in physicalIdPatients)
                {
                    suggestions.Add(string.Format("{5} - [{0}] {1}, {2} {3} ({4})", patient.Id, patient.LastName, patient.FirstName, patient.MiddleName, patient.DateOfBirth.ToShortDateString(), patient.OldPhysicalRecordNumber));
                }
            }
            catch (Exception e) { }

            try
            {
                //Find any patients with a matching name
                IList<Patient> firstNamePatients = new PatientRepository().FindByFirstName(term);
                foreach (Patient patient in firstNamePatients)
                {
                    suggestions.Add(string.Format("[{0}] {1}, {2} {3} ({4})", patient.Id, patient.LastName, patient.FirstName, patient.MiddleName, patient.DateOfBirth.ToShortDateString()));
                }
            }
            catch (Exception e) { }

            try
            {
                IList<Patient> middleNamePatients = new PatientRepository().FindByMiddleName(term);
                foreach (Patient patient in middleNamePatients)
                {
                    suggestions.Add(string.Format("[{0}] {1}, {2} {3} ({4})", patient.Id, patient.LastName, patient.FirstName, patient.MiddleName, patient.DateOfBirth.ToShortDateString()));
                }
            }
            catch (Exception e) { }

            try
            {
                IList<Patient> lastNamePatients = new PatientRepository().FindByLastName(term);
                foreach (Patient patient in lastNamePatients)
                {
                    suggestions.Add(string.Format("[{0}] - {1}, {2} {3} - {4}", patient.Id, patient.LastName, patient.FirstName, patient.MiddleName, patient.DateOfBirth.ToShortDateString()));
                }
            }
            catch (Exception e) { }

            return Json(suggestions, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Immunization

        /// <summary>
        /// Add new immunization to patient
        /// </summary>
        /// <returns></returns>
        public JsonResult AddImmunizationToPatient()
        {
            try
            {
                int patientId = int.Parse(Request.Form["patientID"]);
                int immunizationId = int.Parse(Request.Form["vaccineType"]);
                DateTime dateAdministered = DateTime.Parse(Request.Form["dateAdministered"]);

                PatientRepository repo = new PatientRepository();
                var patient = repo.Get(patientId);
                PatientImmunizationRepository patientImmunRepo = new PatientImmunizationRepository();
                ImmunizationRepository immunRepo = new ImmunizationRepository();


                PatientImmunization pImmunization = new PatientImmunization();
                pImmunization.Immunization = immunRepo.Get(immunizationId);
                pImmunization.Patient = patient;
                pImmunization.DateAdministered = dateAdministered;
                patientImmunRepo.Add(pImmunization);

                //UnitOfWork.CurrentSession.Flush();

                return Json(new
                {
                    error = "false",
                    status = "Added immunization: " + pImmunization.Immunization.VaccineType + " to patient successfully",
                    immunization = pImmunization.Immunization.VaccineType,
                    dateAdmin = pImmunization.DateAdministered.ToString("dd/MM/yyyy"),
                    id = pImmunization.Id
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    status = "Unable to add immunization successfully",
                    errorMessage = e.Message
                });
            }
        }

        /// <summary>
        /// Add new immunization
        /// </summary>
        /// <returns></returns>
        public JsonResult AddNewImmunization()
        {
            try
            {
                ImmunizationRepository immunRepo = new ImmunizationRepository();
                Immunization immun = new Immunization();

                immun.VaccineType = Request.Form["VaccieType"];
                immun.IsActive = true;

                immunRepo.Add(immun);

                return Json(new
                {
                    error = "false",
                    immun.Id,
                    immun.VaccineType
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    error = "true",
                    errorMessage = e.Message
                });
            }
        }

        #endregion

        #region Chronic Diseases

        /// <summary>
        /// Add new disease
        /// </summary>
        /// <returns></returns>
        public JsonResult CreateNewDisease()
        {
            try
            {
                ProblemRepository problemRepo = new ProblemRepository();
                var problemName = Request.Form["DiseaseName"];

                Problem problem = new Problem();

                problem.ProblemName = problemName;

                problemRepo.Add(problem);

                return Json(new
                {
                    error = "false",
                    Name = problem.ProblemName
                });

            }
            catch (Exception)
            {
                return Json(new
                {
                    error = "true"
                });
            }
        }

        /// <summary>
        /// Add a disease to a patient
        /// </summary>
        /// <returns></returns>
        public JsonResult AddDiseaseToPatient()
        {
            try
            {
                var patientId = int.Parse(Request.Form["patientId"]);
                var problemId = int.Parse(Request.Form["problemId"]);

                PatientRepository repo = new PatientRepository();
                var patient = repo.Get(patientId);

                PatientProblemRepository patientProbRepo = new PatientProblemRepository();
                ProblemRepository pRepo = new ProblemRepository();
                PatientProblem pProblem = new PatientProblem();

                pProblem.Problem = pRepo.Get(problemId);
                pProblem.Patient = patient;

                patientProbRepo.Add(pProblem);


                return Json(new
                {
                    error = "false",
                    Name = pProblem.Problem.ProblemName
                });


            }
            catch (Exception)
            {
                return Json(new
                {
                    error = "true"
                });
            }
        }

        #endregion

        #endregion

    }
}
