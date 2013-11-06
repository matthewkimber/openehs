using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OpenEhs.Domain;
using System.Collections.Generic;
using OpenEhs.Data;

namespace OpenEhs.Web.Models
{
    /// <summary>
    /// Patient Search View Model contains the data for patient searching and retrieval
    /// </summary>
    public class PatientSearchViewModel
    {
        private IList<Patient> _patients;

        public PatientSearchViewModel()
        {
        }

        public PatientSearchViewModel(IEnumerable<Patient> patients)
        {
            // TODO: Complete member initialization
            Patients = new List<Patient>(patients);
        }

        public PatientSearchViewModel(IEnumerable<Patient> patients, string searchTerm)
        {
            Patients = new List<Patient>(patients);
            SearchTerm = searchTerm;
        }
        
        #region Properties

        public string SearchTerm { get; set; }
        public IList<Patient> Patients { get; set; }
        
        #endregion

    }
}