using System.Web.Mvc;

namespace OpenEhs.Web.Controllers 
{
    /// <summary>
    /// Home Controller contains the navigation for the index
    /// </summary>
    [Authorize]
    public class HomeController : Controller 
    {
        /// <summary>
        /// Get the index for the home
        /// /Home/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() 
        {
            ViewBag.Message = "Oops";

            return View();
        }
    }
}
