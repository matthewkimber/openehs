using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace OpenEhs.Web.Models
{
    /// <summary>
    /// User Report View Model contains the validation rules for report creation
    /// </summary>
    public class UserReportViewModel
    {
        /// <summary>
        /// Selected date to filter the report on
        /// </summary>
        [Required(ErrorMessage = "A date is required for the report")]
        [DataType(DataType.Date)]
        [Display(Name = "Report Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SelectedDate { get; set; }

        /// <summary>
        /// Data to filter report upon
        /// </summary>
        public DataView Data { get; set; }

        /// <summary>
        /// Default constructor that passes todays date and no data to the report
        /// </summary>
        public UserReportViewModel() : this(DateTime.Now, null)
        {}

        /// <summary>
        /// Constructor that allows you to pass in the data you'd like to see a user report view model on
        /// </summary>
        /// <param name="selectedDate">date to filter the report on</param>
        /// <param name="data">data to filter on</param>
        public UserReportViewModel(DateTime selectedDate, DataView data)
        {
            SelectedDate = selectedDate;
            Data = data;
        }
    }
}