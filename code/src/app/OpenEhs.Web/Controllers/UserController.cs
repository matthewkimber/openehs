using System;
using System.Web.Mvc;
using OpenEhs.Data;
using OpenEhs.Domain;
using OpenEhs.Web.Models;

namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// User Controller contains functionality for user creation, approving, viewing, and role management
    /// </summary>
    [Authorize(Roles="Administrators")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
        }

        /// <summary>
        /// Get index model for user crud
        /// </summary>
        /// <param name="page">page index to display</param>
        /// <returns></returns>
        public ActionResult Index(int? page)
        {
            var pageIndex = page ?? 1;
            var users = _userRepository.GetPaged(pageIndex, 10);

            return View(new UserViewModel(users));
        }

        /// <summary>
        /// Get details model for a specific user
        /// </summary>
        /// <param name="id">user id to show the details for</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            return View(new UserDetailsViewModel(_userRepository.Get(id)));
        }

        /// <summary>
        /// Update details for user
        /// </summary>
        /// <param name="id">id of user to update</param>
        /// <param name="collection">form collection that contains the new user details</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(int id, FormCollection collection)
        {
            var user = _userRepository.Get(id);

            user.Password = collection["Password"];
            user.EmailAddress = collection["EmailAddress"];
            user.FirstName = collection["FirstName"];
            user.MiddleName = collection["MiddleName"];
            user.LastName = collection["LastName"];
            user.Address.Street1 = collection["Address.Street1"];
            user.Address.Street2 = collection["Address.Street2"];
            user.Address.City = collection["Address.City"];
            user.Address.Region = collection["Address.Region"];
            Country selectedCountry;
            Enum.TryParse(collection["Address.Country"], out selectedCountry);
            user.Address.Country = selectedCountry;
            user.PhoneNumber = collection["PhoneNumber"];

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Add role to user
        /// </summary>
        /// <param name="id">id of role to add to user</param>
        /// <param name="userId">user to add the role to</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRole(int id, int userId)
        {
            var roleToAdd = _roleRepository.Get(id);
            var user = _userRepository.Get(userId);

            try
            {
                user.AddRole(roleToAdd);
            }
            catch (ArgumentException ex)
            {
                return Json(new {success = false, error = ex.Message});
            }

            UnitOfWork.CurrentSession.Flush();

            return Json(new {success = true, RoleName = roleToAdd.Name, RoleId = roleToAdd.Id});
        }

        /// <summary>
        /// Remove role from a user
        /// </summary>
        /// <param name="id">id of role to remove</param>
        /// <param name="userId">user id of the user to remove the role from</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveRole(int id, int userId)
        {
            var roleToRemove = _roleRepository.Get(id);
            var user = _userRepository.Get(userId);

            user.RemoveRole(roleToRemove);

            UnitOfWork.CurrentSession.Flush();

            return Json(new {success = true});
        }

        /// <summary>
        /// Approve a user
        /// </summary>
        /// <param name="id">id of user to approve</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Approve(int id)
        {
            var user = _userRepository.Get(id);

            user.IsApproved = !user.IsApproved;

            return Json(new { success = true });
        }
    }
}
