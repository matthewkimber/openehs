using System.Web.Mvc;

namespace OpenEhs.Web.Controllers {

    /// <summary>
    /// Help Controller provides the views and functionality of navigating the help pages
    /// </summary>
    public class HelpController : Controller {
        //
        // GET: /Help/

        /// <summary>
        /// Get the index for help
        /// /Help/
        /// </summary>
        /// <returns>View for the help page</returns>
        public ActionResult Index() {
            return View();
        }

        /// <summary>
        /// Show the help for the Dashboard.
        /// Nothing more than a link to Dashboard.cshtml.
        /// </summary>
        /// <returns>ActionResult to display.</returns>
        public ActionResult Dashboard()
        {
            return View();
        }

        /// <summary>
        /// Show the patient search help model
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientSearch()
        {
            return View();
        }

        /// <summary>
        /// Show the patient creation help model
        /// </summary>
        /// <returns></returns>
        public ActionResult PatientCreation() {
            return View();
        }

        /// <summary>
        /// Show the help for Patient Resources. Basically just a link to the 
        /// PatientResources.cshtml in the help.
        /// </summary>
        /// <returns>ActionResult to display.</returns>
        public ActionResult PatientResources() {
            return View();
        }

        /// <summary>
        /// Show the billing search help model
        /// </summary>
        /// <returns></returns>
        public ActionResult BillingSearch() {
            return View();
        }

        /// <summary>
        /// Show the help for Billing Details.
        /// Nothing more than a link to BillingDetails.cshtml.
        /// </summary>
        /// <returns></returns>
        public ActionResult BillingDetails()
        {
            return View();
        }

        /// <summary>
        /// Show the printer setup help model
        /// </summary>
        /// <returns></returns>
        public ActionResult PrinterSetup()
        {
            return View();
        }

        /// <summary>
        /// Show the print existing patient barcode help model
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintExistingPatientBarcode()
        {
            return View();
        }

    }
}
