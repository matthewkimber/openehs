using System.Web.Mvc;

namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// Role Controller contains functionality for getting the main page for role management
    /// </summary>
    [Authorize(Roles="Administrators")]
    public class RoleController : Controller
    {
        /// <summary>
        /// Access the index for roles 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
