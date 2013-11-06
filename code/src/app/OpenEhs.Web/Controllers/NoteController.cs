using System.Web.Mvc;
using OpenEhs.Domain;

namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// Note Controller contains the necessary functionality to create, edit and delete notes
    /// </summary>
    [Authorize]
    public class NoteController : Controller
    {
        //
        // GET: /Note/

        /// <summary>
        /// Index
        /// URL: /Note/
        /// </summary>
        /// <returns>View for /Note/index.cshtml</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Create new note
        /// URL: /Note/Create
        /// </summary>
        /// <returns>view that allows you to create new note</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Action to create a new note
        /// </summary>
        /// <param name="note">Note to create</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Note note)
        {
            return View();
        }

        /// <summary>
        /// 
        /// URL: /Note/Edit
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// Action to edit note
        /// </summary>
        /// <param name="note">Updated note</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Note note)
        {
            return View();
        }

        /// <summary>
        /// Delete note with specific id
        /// </summary>
        /// <param name="id">id of the note to delete</param>
        /// <returns>redirect to index</returns>
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
