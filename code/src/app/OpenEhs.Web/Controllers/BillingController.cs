using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OpenEhs.Domain;
using OpenEhs.Web.Models;
using OpenEhs.Data;
using System.Globalization;

namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// Billing Controller contains the necessary functions to control the views of the billing page and 
    /// to manage billing options
    /// </summary>
    [Authorize(Roles="Administrators")]
    public class BillingController : Controller
    {
        //
        // GET: /Invoice/

        public ActionResult Index()
        {
            return Top25();
        }

        private ActionResult Top25()
        {
            var invoices = new InvoiceRepository().GetTop25();
            var billings = new List<BillingViewModel>();

            foreach (Invoice invoice in invoices)
            {
                billings.Add(new BillingViewModel(invoice.Id));
            }

            return View(billings);
        }

        //
        // GET: /Invoice/Details/5

        public ActionResult Details(int id)
        {
            BillingViewModel billing = new BillingViewModel(id);

            return View(billing);
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Invoice/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Invoice/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Invoice/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public RedirectResult AddProductLineItem(int invoiceId)
        {
            InvoiceItem lineItem = new InvoiceItem();
            lineItem.Invoice = new InvoiceRepository().Get(invoiceId);
            lineItem.IsActive = true;
            lineItem.Product = new ProductRepository().Get(1);
            lineItem.Quantity = 1;

            new InvoiceRepository().AddLineItem(lineItem);
            return new RedirectResult("/Billing/Edit/" + invoiceId);
        }

        public ActionResult Service(int invoiceId)
        {
            var lineItem = new InvoiceItem();
            var repo = new InvoiceRepository();
            lineItem.Service = new ServiceRepository().Get(1);
            lineItem.Invoice = repo.Get(invoiceId);
            lineItem.IsActive = true;
            repo.AddLineItem(lineItem);
            

            return View(lineItem);
        }

        [HttpPost]
        public ActionResult Service(InvoiceItem lineItem)
        {
            //add save code here?
            try
            {
                BillingEditViewModel invoice = new BillingEditViewModel(lineItem.Invoice.Id);
                invoice.Save();
                return RedirectToAction("Edit");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }

        public RedirectResult SaveLineItem(int itemId, int productId, int serviceId, int quantity)
        {
            InvoiceItem lineItem = new InvoiceRepository().GetItem(itemId);
            if (serviceId == 0)
            {
                lineItem.Product = new ProductRepository().Get(productId);
            }
            if (productId == 0)
            {
                lineItem.Service = new ServiceRepository().Get(serviceId);
            }
            lineItem.Quantity = quantity;
            new InvoiceRepository().AddLineItem(lineItem);

            return new RedirectResult("/Billing/Edit/" + lineItem.Invoice.Id);
        }

        /// <summary>
        /// Add a payment to the Billing invoice.
        /// </summary>
        /// <returns>Json object used to update the billing cshtml view through javascript.</returns>
        public JsonResult AddPayment()
        {
            try
            {
                PaymentRepository paymentRepo = new PaymentRepository();

                Payment payment = new Payment();
                payment.IsActive = true;
                payment.CashAmount = decimal.Parse(Request.Form["Amount"]);
                payment.Invoice = new InvoiceRepository().Get(int.Parse(Request.Form["InvoiceId"]));
                BillingViewModel billing = new BillingViewModel(payment.Invoice.Id);
                payment.PaymentDate = DateTime.Now;

                if (payment.CashAmount > billing.Total - billing.PaymentTotal)
                {
                    return Json(new { 
                        error = true, 
                        message = "Please do not pay more than is owed." 
                    });
                }

                paymentRepo.Add(payment);


                return Json(new
                {
                    error = false,
                    Date = payment.PaymentDate.ToString(),
                    Amount = payment.CashAmount.ToString("c"),
                    Balance = String.Format("{0:c}", billing.Total - billing.PaymentTotal - payment.CashAmount),
                    Payments = String.Format("{0:c}", billing.PaymentTotal + payment.CashAmount)
                });
            }
            catch(Exception e)
            {
                return Json(new
                {
                    error = true,
                    message = e.Message
                });
            }
        }

        #region PatientSearch

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

        [HttpPost]
        public ActionResult Index(FormCollection values)
        {
            string searchCriteria = values["BillingSearchTextBox"];    //Get the value entered in the 'Search' field

            //If the search field is empty then return the top 25 default results
            if (string.IsNullOrEmpty(searchCriteria))
                return Top25();

            string[] criteriaItems = searchCriteria.Split('[');
            string[] criteriaItems2 = criteriaItems[1].Split(']');

            //Get a list of invoices for this person
            IList<Invoice> invoices = new InvoiceRepository().FindByPatientId(Convert.ToInt32(criteriaItems2[0]));

            var billings = new List<BillingViewModel>();

            foreach (Invoice invoice in invoices)
            {
                billings.Add(new BillingViewModel(invoice.Id, "test"));
            }

            return View(billings);
        }
    }
}
