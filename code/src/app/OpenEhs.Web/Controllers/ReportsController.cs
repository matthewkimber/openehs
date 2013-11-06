using System.Web.Mvc;
using System;
using OpenEhs.Web.Models;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using MySql;
using MySql.Data;

namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// Reports Controller contains functionality for generating reports
    /// </summary>
    [Authorize(Roles="Administrators, OPDAdministrators")]
    public class ReportsController : Controller
    {
        private string dateRegExpression = @"(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})|(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))|(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})";
        
        /// <summary>
        /// Gets the index for reports
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// returns the user report model
        /// </summary>
        /// <returns></returns>
        public ActionResult UserReport()
        {
            return View(new UserReportViewModel());
        }

        /// <summary>
        /// Generate report based on form data
        /// </summary>
        /// <param name="collection">form data to get date param from</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserReport(FormCollection collection)
        {
            //use dateRegExpression to check to see if the date in the "Select a Date" box is a valid date.
            string dateSource = collection["selectedDate"];
            Regex dobRegEx = new Regex(this.dateRegExpression);
            Match dateRE = dobRegEx.Match(dateSource);
            if (dateRE.Success)
            {
                string selectStatement = "SELECT  u.LastName, " +
                            "u.Firstname, " +
                            "COUNT(c.CheckinTime) " +
                            "FROM user u, patient p, patientcheckin c " +
                            "WHERE u.UserID = c.UserID AND c.PatientID = p.PatientID AND DATE(c.CheckinTime) = @date " +
                            "GROUP BY u.LastName, u.FirstName;";

                string ConnectionString = ConfigurationManager.ConnectionStrings["OpenEhs.ConnectionString"].ConnectionString;
                DateTime date = Convert.ToDateTime(collection["selectedDate"]);

                MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString);
                string select = selectStatement;
                MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(select, connection);
                command.Parameters.AddWithValue("date", date);

                //Connecting to the database and performing the query
                connection.Open();
                MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
                System.Data.DataTable tableReport = new System.Data.DataTable();
                tableReport.Load(reader);
                connection.Close();

                System.Data.DataView dv1 = new System.Data.DataView(tableReport);

                return View(new UserReportViewModel(date, dv1));
            }
            else
                return View(new UserReportViewModel()); //Return a blank/new report if the date is invalid.
        }

        /// <summary>
        /// Get location report model
        /// </summary>
        /// <returns></returns>
        public ActionResult LocationReport()
        {
            return View(new LocationReportViewModel());
        }

        /// <summary>
        /// Generate location report based on form params
        /// </summary>
        /// <param name="collection">form data to generate the report with</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LocationReport(FormCollection collection)
        {
            //use dateRegExpression to check to see if the date in the "Select a Date" box is a valid date.
            string dateSource = collection["selectedDate"];
            Regex dobRegEx = new Regex(this.dateRegExpression);
            Match dateRE = dobRegEx.Match(dateSource);
            if (dateRE.Success)
            {
                string selectStatement = "SELECT  l.Department, " +
                                            "COUNT(c.CheckinTime) " +
                                            "FROM patientcheckin c, location l " +
                                            "WHERE l.LocationID = c.LocationID AND DATE(c.CheckinTime) = @date " +
                                            "GROUP BY l.Department;";

                string ConnectionString = ConfigurationManager.ConnectionStrings["OpenEhs.ConnectionString"].ConnectionString;
                DateTime date = Convert.ToDateTime(collection["selectedDate"]);

                MySql.Data.MySqlClient.MySqlConnection connection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString);
                string select = selectStatement;
                MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(select, connection);
                command.Parameters.AddWithValue("date", date);

                //Connecting to the database and performing the query
                connection.Open();
                MySql.Data.MySqlClient.MySqlDataReader reader = command.ExecuteReader();
                System.Data.DataTable tableReport = new System.Data.DataTable();
                tableReport.Load(reader);
                connection.Close();

                System.Data.DataView dv1 = new System.Data.DataView(tableReport);

                return View(new LocationReportViewModel(date, dv1));
            }
            else
                return View(new LocationReportViewModel()); //Return a blank/new report if the date is invalid.
        }
    }
}
