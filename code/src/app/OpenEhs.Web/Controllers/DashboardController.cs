using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OpenEhs.Data;
using OpenEhs.Domain;
using OpenEhs.Web.Models;

namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// Dashboard Controller contains the necessary functions to control the views of the dashboard page and 
    /// to manage the viewing of recent check-ins
    /// </summary>
    [Authorize]
    public class DashboardController : Controller
    {
        private IPatientRepository _patientRepository;

        /// <summary>
        /// Default constructor that initializes the patient repository
        /// </summary>
        public DashboardController()
        {
            _patientRepository = new PatientRepository();
        }

        /// <summary>
        /// Get the index, main page, for the dashboard
        /// /Dashboard/
        /// </summary>
        /// <returns>the main page view</returns>
        public ActionResult Index()
        {
            var viewModel = new DashboardViewModel();

            return View(viewModel);
        }

        /// <summary>
        /// Get the list of active checkins.
        /// </summary>
        /// <returns>A Json object containing the list of active checkins.</returns>
        public JsonResult ActiveCheckIns()
        {
            try
            {
                PatientRepository patientRepo = new PatientRepository();
                

                string department = Request.Form["loc"];

                var myLocation = new Location();
                myLocation.Department = department;

                var list = patientRepo.FindByLocation(myLocation);

                //var resultList = from test in list select test;
                var bob = new List<object>();

                foreach (var patient in list)
                {
                    bob.Add(new {Name=patient.LastName +", " + patient.FirstName, ID=patient.Id });
                }

                return Json(new
                {
                    error = "false",
                    status = "Successfully.",
                    //loc.Department
                    //list
                    //resultList
                    bob
                });
                //return Json(list);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    error = "true",
                    status = "Didnt work"
                });
            }
        }

    }
}
