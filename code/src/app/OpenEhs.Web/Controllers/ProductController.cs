using System.Web.Mvc;
using OpenEhs.Data;
using OpenEhs.Domain;

namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// Product Controller contains the functionality for creating, deleting, and viewing products
    /// </summary>
    [Authorize(Roles="Administrators")]
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        /// <summary>
        /// Get the index for the products page
        /// /Product/
        /// </summary>
        /// <returns>View for the product index</returns>
        public ActionResult Index()
        {
            var products = new ProductRepository().GetAll();
            return View(products);
        }

        /// <summary>
        /// Get the create product page
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Add a product to the repository
        /// </summary>
        /// <param name="product">product to add</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Product product)
        {
            new ProductRepository().Add(product);
            return View();
        }

        /// <summary>
        /// Get the details for a product
        /// </summary>
        /// <param name="id">id of product to get details for</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            var product = new ProductRepository().Get(id);
            return View(product);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">id to delete</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
