using System.Collections.Generic;
using System.Linq;
using OpenEhs.Data;
using OpenEhs.Domain;

namespace OpenEhs.Web.Models
{
    /// <summary>
    /// Admin View Model contains the functionality for getting products, categories, locations, services, allergies, immunizations,
    /// medications, and templates from their corresponding repository.
    /// </summary>
    public class AdminViewModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IAllergyRepository _allergyRepository;
        private readonly IImmunizationRepository _immunizationRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly ITemplateRepository _templateRepository;

        public AdminViewModel()
        {
            _productRepository = new ProductRepository();
            _categoryRepository = new CategoryRepository();
            _locationRepository = new LocationRepository();
            _serviceRepository = new ServiceRepository();
            _allergyRepository = new AllergyRepository();
            _immunizationRepository = new ImmunizationRepository();
            _medicationRepository = new MedicationRepository();
            _templateRepository = new TemplateRepository();
        }

        /// <summary>
        /// Get all products
        /// </summary>
        public IList<Product> Products
        {
            get
            {
                return _productRepository.GetAll();
            }
        }

        /// <summary>
        /// Get all active products
        /// </summary>
        public IList<Product> ActiveProducts
        {
            get
            {

                var prod = from activeProd in Products
                           where activeProd.IsActive == true
                           select activeProd;

                return prod.ToList();
            }
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        public IList<Category> Categories
        {
            get
            {
                return _categoryRepository.GetAll();
            }
        }

        /// <summary>
        /// Get all active categories
        /// </summary>
        public IList<Category> ActiveCategories
        {
            get
            {
                var cat = from activeCat in Categories
                          where activeCat.IsActive == true
                          select activeCat;

                return cat.ToList();
            }
        }

        /// <summary>
        /// Get all allergies
        /// </summary>
        public IList<Allergy> Allergies
        {
            get
            {
                return _allergyRepository.GetAll();
            }
        }

        /// <summary>
        /// Get all active allergies
        /// </summary>
        public IList<Allergy> ActiveAllergies
        {
            get
            {
                var allergy = from activeAll in Allergies
                          where activeAll.IsActive == true
                          select activeAll;

                return allergy.ToList();
            }
        }

        /// <summary>
        /// Get all immunizations
        /// </summary>
        public IList<Immunization> Immunizations
        {
            get
            {
                return _immunizationRepository.GetAll();
            }
        }

        /// <summary>
        /// Get all active immunizations
        /// </summary>
        public IList<Immunization> ActiveImmunizations
        {
            get
            {
                var immun = from activeImm in Immunizations
                              where activeImm.IsActive == true
                              select activeImm;

                return immun.ToList();
            }
        }

        /// <summary>
        /// Get all medications
        /// </summary>
        public IList<Medication> Medications
        {
            get
            {
                return _medicationRepository.GetAll();
            }
        }

        /// <summary>
        /// Get all active medications
        /// </summary>
        public IList<Medication> ActiveMedications
        {
            get
            {
                var med = from activeMed in Medications
                            where activeMed.IsActive == true
                            select activeMed;

                return med.ToList();
            }
        }

        /// <summary>
        /// Get all locations
        /// </summary>
        public IList<Location> Locations
        {
            get
            {
                return _locationRepository.GetAll();
            }
        }

        /// <summary>
        /// Get all active locations
        /// </summary>
        public IList<Location> ActiveLocations
        {
            get
            {
                var loc = from activeLoc in Locations
                          where activeLoc.IsActive == true
                          select activeLoc;

                return loc.ToList();
            }
        }

        /// <summary>
        /// Get all templates
        /// </summary>
        public IList<Template> Templates
        {
            get
            {
                return _templateRepository.GetAll();
            }
        }

        /// <summary>
        /// get all active temples
        /// </summary>
        public IList<Template> ActiveTemplates
        {
            get
            {
                var temp = from activeTemp in Templates
                          where activeTemp.IsActive == true
                          select activeTemp;

                return temp.ToList();
            }
        }

        /// <summary>
        /// Get all note template categories
        /// </summary>
        public IList<NoteTemplateCategory> TemplateCategories
        {
            get
            {
                NoteTemplateRepository ntc = new NoteTemplateRepository();
                return ntc.GetAll();
            }
        }

        /// <summary>
        /// get all active template categories
        /// </summary>
        public IList<NoteTemplateCategory> AllTemplateCategories
        {
            get
            {
                var temp = from activeTemp in TemplateCategories
                           select activeTemp;

                return temp.ToList();
            }
        }

        /// <summary>
        /// Get all services
        /// </summary>
        public IList<Service> Services
        {
            get
            {
                return _serviceRepository.GetAll();
            }
        }

        /// <summary>
        /// Get all active services
        /// </summary>
        public IList<Service> ActiveServices
        {
            get
            {
                var ser = from activeSer in Services
                          where activeSer.IsActive == true
                          select activeSer;

                return ser.ToList();
            }
        }
    }
}