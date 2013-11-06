using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OpenEhs.Data;
using OpenEhs.Domain;

namespace OpenEhs.Web.Models
{
    /// <summary>
    /// Dashboard View Model contains the data for viewing recent checkins and retrieving them
    /// </summary>
    public class DashboardViewModel
    {
        private Patient _patient;
        private User _user;

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

        /// <summary>
        /// Get first open checkin found
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

                    return query.First();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get all locations
        /// </summary>
        public IList<Location> GetLocations
        {
            get
            {
                var locations = new LocationRepository();
                return locations.GetAll();
            }
        }

        /// <summary>
        /// Get all patients in ward 1
        /// </summary>
        public IList<Patient> PatientsInMyWard
        {
            get 
            { 
                var repo = new PatientRepository();
                return repo.FindByLocation(new Location { Department = "WARD 1" });
            }
        }
    }
}