using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using OpenEhs.Domain;
using System;
using System.Web.Security;
using OpenEhs.Data;
using OpenEhs.Web.Models;

namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// Admin Controller contains the necessary functions to control the views of the admin page and 
    /// to manage admin options
    /// </summary>
    [Authorize(Roles="Administrators")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new AdminViewModel();
            return View(viewModel);
        }

        public ActionResult BackupDatabase()
        {
            MySqlUtilities.Backup("openehs_database", ConfigurationManager.AppSettings["MySql.RootPassword"], ConfigurationManager.AppSettings["Backup.Directory"]);

            return RedirectToAction("Index");
        }

        #region JsonResults


        #region ProductMethods

        /// <summary>
        /// Add product to the system
        /// </summary>
        /// <returns>Result of the add</returns>
        public JsonResult AddProduct()
        {
            try
            {
                CategoryRepository categoryRepo = new CategoryRepository();
                ProductRepository productRepo = new ProductRepository();

                Product product = new Product();
                product.Name = Request.Form["Name"];
                product.Unit = Request.Form["Unit"];
                product.Category = categoryRepo.Get(int.Parse(Request.Form["CategoryId"]));
                product.Price = Decimal.Parse(Request.Form["Price"]);
                product.QuantityOnHand = int.Parse(Request.Form["QuantityOnHand"]);
                product.Counter = 0;
                product.IsActive = true;

                productRepo.Add(product);

                return Json(new
                {
                    error = false,
                    Name = product.Name,
                    Id = product.Id
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }

        }

        /// <summary>
        /// Edit a product and return results
        /// </summary>
        /// <returns>Result of the update</returns>
        public JsonResult EditProduct()
        {
            try
            {
                ProductRepository productRepo = new ProductRepository();
                CategoryRepository categoryRepo = new CategoryRepository();
                Product product = productRepo.Get(int.Parse(Request.Form["ProductId"]));
                product.Name = Request.Form["Name"];
                product.Unit = Request.Form["Unit"];
                product.Category = categoryRepo.Get(int.Parse(Request.Form["CategoryId"]));
                product.Price = decimal.Parse(Request.Form["Price"]);
                return Json(new
                {
                    error = false
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }
        }

        /// <summary>
        /// Remove product from the system
        /// </summary>
        /// <returns>result of the delete</returns>
        public JsonResult RemoveProduct()
        {
            try
            {
                ProductRepository productRepo = new ProductRepository();
                Product product = productRepo.Get(int.Parse(Request.Form["ProductId"]));
                product.IsActive = false;
                UnitOfWork.CurrentSession.Flush();

                return Json(new
                {
                    error = false
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }
        }

        /// <summary>
        /// Generates a list of products
        /// </summary>
        /// <returns></returns>
        public JsonResult ProductList()
        {
            try
            {
                int productID = int.Parse(Request.Form["ID"]);
                ProductRepository productRepo = new ProductRepository();
                var product = productRepo.Get(productID);

                var resultSet = new List<object>();
                var jsonResult = new JsonResult();

                resultSet.Add(new
                {
                    error = false,
                    Name = product.Name,
                    Unit = product.Unit,
                    Category = product.Category.Name,
                    Price = product.Price,
                    Quantity = product.QuantityOnHand
                });

                jsonResult.Data = resultSet;

                return jsonResult;
            }
            catch (Exception)
            {

                return Json(new
                {
                    error = true
                });
            }
        }

        /// <summary>
        /// Generate a list of services
        /// </summary>
        /// <returns></returns>
        public JsonResult ServiceList()
        {
            try
            {
                int serviceID = int.Parse(Request.Form["ID"]);
                ServiceRepository serviceRepo = new ServiceRepository();
                var service = serviceRepo.Get(serviceID);

                var resultSet = new List<object>();
                var jsonResult = new JsonResult();

                resultSet.Add(new
                {
                    error = false,
                    Price = service.Price
                });

                jsonResult.Data = resultSet;

                return jsonResult;
            }
            catch (Exception)
            {

                return Json(new
                {
                    error = true
                });
            }
        }

        #endregion

        #region InventoryMethods

        public JsonResult AddInventory()
        {
            try
            {
                ProductRepository productRepo = new ProductRepository();
                Product product = productRepo.Get(int.Parse(Request.Form["Product"]));
                product.QuantityOnHand += int.Parse(Request.Form["Quantity"]);
                return Json(new
                {
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

        public JsonResult EditInventory()
        {
            try
            {
                ProductRepository productRepo = new ProductRepository();
                Product product = productRepo.Get(int.Parse(Request.Form["Product"]));
                product.QuantityOnHand = int.Parse(Request.Form["Quantity"]);
                return Json(new
                {
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

        #region LocationMethods

        public JsonResult AddLocation()
        {
            try
            {
                Location loc = new Location();

                loc.Department = Request.Form["Department"];
                loc.RoomNumber = Request.Form["RoomNumber"];
                loc.IsActive = true;

                LocationRepository locationRepo = new LocationRepository();
                locationRepo.Add(loc);

                return Json(new
                {
                    error = false,
                    Id = loc.Id,
                    Name = loc.Department
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }
        }

        public JsonResult DeleteLocation()
        {
            try
            {
                LocationRepository locationRepo = new LocationRepository();
                Location location = locationRepo.Get(int.Parse(Request.Form["Department"]));
                location.IsActive = false;

                return Json(new
                {
                    error = false
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }
        }

        #endregion

        #region CategoryMethods

        public JsonResult AddCategory()
        {
            try
            {
                CategoryRepository categoryRepo = new CategoryRepository();
                Category cata = new Category();

                cata.Name = Request.Form["Name"];
                cata.IsActive = true;
                cata.Description = Request.Form["Description"];
                cata.DateCreated = DateTime.Now;

                categoryRepo.Add(cata);

                return Json(new
                {
                    Id = cata.Id,
                    Name = cata.Name,
                    error = false
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }
        }

        public JsonResult DeleteCategory()
        {
            try
            {
                CategoryRepository categoryRepo = new CategoryRepository();
                Category category = categoryRepo.Get(int.Parse(Request.Form["Category"]));
                category.IsActive = false;

                return Json(new
                {
                    error = false
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }
        }

        #endregion

        #region ServiceMethods

        public JsonResult AddService()
        {
            try
            {
                ServiceRepository serviceRepo = new ServiceRepository();

                Service service = new Service();
                service.IsActive = true;
                service.Name = Request.Form["Name"];
                service.Price = decimal.Parse(Request.Form["Cost"]);

                serviceRepo.Add(service);

                return Json(new
                {
                    error = false,
                    Name = service.Name,
                    Id = service.Id
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }
        }

        public JsonResult EditService()
        {
            try
            {
                ServiceRepository serviceRepo = new ServiceRepository();
                Service service = serviceRepo.Get(int.Parse(Request.Form["Service"]));
                service.Price = decimal.Parse(Request.Form["Cost"]);

                return Json(new
                {
                    error = false
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }
        }

        public JsonResult DeleteService()
        {
            try
            {
                ServiceRepository serviceRepo = new ServiceRepository(); 
                Service service = serviceRepo.Get(int.Parse(Request.Form["Service"]));
                service.IsActive = false;

                return Json(new
                {
                    error = false
                });
            }
            catch
            {
                return Json(new
                {
                    error = true
                });
            }
        }

        #endregion

        #region AllergyMethods

        public JsonResult EditAllergy()
        {
            try
            {
                AllergyRepository allergyRepo = new AllergyRepository();
                Allergy allergy = allergyRepo.Get(int.Parse(Request.Form["Allergy"]));
                allergy.Name = Request.Form["EditAllergy"];

                return Json(new { 
                    error = "false"
                });
            }
            catch
            {
                return Json(new { 
                    error = "true"
                });
            }
        }

        public JsonResult DeleteAllergy()
        {
            try
            {
                AllergyRepository allergyRepo = new AllergyRepository();
                allergyRepo.Remove(allergyRepo.Get(int.Parse(Request.Form["Allergy"])));

                return Json(new
                {
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

        #region ImmunizationMethods

        public JsonResult EditImmunization()
        {
            try
            {
                ImmunizationRepository immunizationRepo = new ImmunizationRepository();
                Immunization immunization = immunizationRepo.Get(int.Parse(Request.Form["Immunization"]));
                immunization.VaccineType = Request.Form["EditImmunization"];

                return Json(new
                {
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

        public JsonResult DeleteImmunization()
        {
            try
            {
                ImmunizationRepository immunizationRepo = new ImmunizationRepository();
                immunizationRepo.Remove(immunizationRepo.Get(int.Parse(Request.Form["Immunization"])));

                return Json(new
                {
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

        #region MedicationMethods

        public JsonResult EditMedication()
        {
            try
            {
                MedicationRepository medicationRepo = new MedicationRepository();
                Medication medication = medicationRepo.Get(int.Parse(Request.Form["Medication"]));
                medication.Name = Request.Form["EditMedication"];

                return Json(new
                {
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

        public JsonResult DeleteMedication()
        {
            try
            {
                MedicationRepository medicationRepo = new MedicationRepository();
                medicationRepo.Remove(medicationRepo.Get(int.Parse(Request.Form["Medication"])));

                return Json(new
                {
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

        #region TemplateCategoryMethods

        public JsonResult AddTemplateCategory()
        {
            try
            {
                NoteTemplateRepository ntRepo = new NoteTemplateRepository();
                NoteTemplateCategory ntCategory = new NoteTemplateCategory();
                ntCategory.Name = Request.Form["Template"];
                ntRepo.Add(ntCategory);

                return Json(new {
                    ID = ntCategory.Id,
                    Name = ntCategory.Name,
                    error = "false"
                });
            }
            catch
            {
                return Json(new {
                    error = "true"
                });
            }
        }

        public JsonResult DeleteTemplateCategory()
        {
            try
            {
                NoteTemplateRepository ntRepo = new NoteTemplateRepository();
                ntRepo.Remove(ntRepo.Get(int.Parse(Request.Form["Template"])));
                return Json(new
                {
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

        #endregion
    }
}
