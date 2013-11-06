using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using OpenEhs.Data;
using OpenEhs.Domain;
using OpenEhs.Web.Models;

namespace OpenEhs.Web.Controllers
{
    /// <summary>
    /// Account Controller acts as a controller for user management and contains the management of views for 
    /// the process of user create and edit
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// User repository to be used to access the users in the database
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// Forms service that controls the persistance of the users login
        /// </summary>
        public IFormsAuthenticationService FormsService { get; set; }

        /// <summary>
        /// Membership service that controls that creation of users
        /// </summary>
        public IMembershipService MembershipService { get; set; }

        /// <summary>
        /// Initialize the controller and create new services, if needed.
        /// </summary>
        /// <param name="requestContext">context of the request</param>
        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        /// <summary>
        /// Default constructor that initializes the userRepository
        /// </summary>
        public AccountController()
        {
            _userRepository = new UserRepository();
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        /// <summary>
        /// Login page
        /// URL: /Account/LogOn
        /// </summary>
        /// <returns>The view for Login.cshtml</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Login action that validates whether the login is correct
        /// </summary>
        /// <param name="model">the logon model that contains the fields username and password with the validation rules</param>
        /// <param name="returnUrl">the url to redirect to when login is successful</param>
        /// <returns>Redirects to the returnURL, if login valid, otherwise shows errors</returns>
        [HttpPost]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, false);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        /// <summary>
        /// Action to log off a user
        /// URL: /Account/LogOff
        /// </summary>
        /// <returns>Redirect to the index</returns>
        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Dashboard");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        /// <summary>
        /// Gets the Registeration page
        /// URL: /Account/Register
        /// </summary>
        /// <returns>view of the page</returns>
        [Authorize]
        public ActionResult Register()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(new RegisterModel());
        }

        /// <summary>
        /// Action to register a user
        /// </summary>
        /// <param name="model">registeration model that has all the users form data</param>
        /// <returns>Redirects to the login page if successful, otherwise it will display errors</returns>
        [Authorize]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.Username, model.Password,
                                                                                   model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    var User = new User
                                    {
                                        FirstName = model.FirstName,
                                        MiddleName = model.MiddleName,
                                        LastName = model.LastName,
                                        StaffType = model.Title,
                                        LicenseNumber = model.LicenseNumber,
                                        PhoneNumber = model.PhoneNumber,
                                        Address = new Address
                                        {
                                            Street1 = model.Street1,
                                            Street2 = model.Street2,
                                            City = model.City,
                                            Region = model.Region,
                                            Country = model.Country,
                                            IsActive = true
                                        },
                                        Username = model.Username,
                                        Password = model.Password,
                                        EmailAddress = model.Email,
                                        LastActivity = DateTime.Now,
                                        LastLogin = DateTime.Now,
                                        DateCreated = DateTime.Now,
                                        PasswordQuestion = String.Empty,
                                        PasswordAnswer = String.Empty,
                                        LastPasswordChange = DateTime.MinValue,
                                        ApplicationName = "/",
                                        IpAddress = Request.ServerVariables["REMOTE_ADDR"],
                                        IsActive = true,
                                        IsApproved = false,
                                        IsLockedOut = false,
                                        IsOnline = true,
                                        FailedPasswordAttemptCount = 0
                                    };


                    _userRepository.Add(User);

                    //FormsService.SignIn(User.Username, false);

                    if (Request.IsAjaxRequest())
                    {
                        return Json(new { success = true });
                    }

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;

            return View(model);
        }

        /// <summary>
        /// Check if a username is available
        /// </summary>
        /// <param name="username">username to check if available</param>
        /// <returns>result as to whether is it available</returns>
        public JsonResult CheckForUsernameAvailability(string username)
        {
            username = username.ToLower();
            var isAvailable = _userRepository.CheckForUsernameAvailability(username);
            var suffix = 1;

            // HACK!!!
            if (!isAvailable)
            {
                username = username + suffix;
                if (_userRepository.CheckForUsernameAvailability(username))
                    return Json(new { requestedUsername = username });
                
                throw new ArgumentException("That username has already been taken. Please contact your system administrator.", "username");
            }

            return Json(new { requestedUsername = username });
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        /// <summary>
        /// Action to get the change password page
        /// Url: /Account/ChangePassword
        /// </summary>
        /// <returns>change password view</returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }

        /// <summary>
        /// Action for changing a password and logic for validation of the password rules
        /// </summary>
        /// <param name="model">Model that contains the form elements and new password</param>
        /// <returns>Returns password successfully changed view or the current view with errors</returns>
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        /// <summary>
        /// The action that is called when a password change was successful
        /// URL: /Account/ChangePasswordSuccess
        /// </summary>
        /// <returns>View that shows 'Password changed successfully'</returns>
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}
