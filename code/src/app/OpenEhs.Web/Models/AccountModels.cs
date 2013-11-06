using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using OpenEhs.Domain;

namespace OpenEhs.Web.Models
{
    #region Models

    /// <summary>
    /// Change password model contains all the form data and validation rules for changing a password
    /// </summary>
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// LogOn model contains all the form data and validation rules for logging on
    /// </summary>
    public class LogOnModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    /// <summary>
    /// Register Model contains all the form data and validation rules for registering a new user
    /// </summary>
    public class RegisterModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Title")]
        public StaffType Title { get; set; }

        public SelectList StaffTypes
        {
            get
            {
                var types = from StaffType s in Enum.GetValues(typeof (StaffType))
                            select new {ID = s, Name = s.ToString()};

                return new SelectList(types, "ID", "Name");
            }
        }

        [Display(Name = "License No.")]
        public string LicenseNumber { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Street 1")]
        public string Street1 { get; set; }

        [Display(Name = "Street 2")]
        public string Street2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required(ErrorMessage = "Users's Region is required.")]
        [Display(Name = "Region")]
        public SelectList Regions
        {
            get
            {
                var regions = new List<object>
                                  {
                                      new {Value = "Eastern", Text = "Eastern"},
                                      new {Value = "Central", Text = "Central"},
                                      new {Value = "Western", Text = "Western"},
                                      new {Value = "Northern", Text = "Northern"},
                                      new {Value = "Ashanti", Text = "Ashanti"},
                                      new {Value = "Volta", Text = "Volta"},
                                      new {Value = "Brong-Ahafo", Text = "Brong-Ahafo"},
                                      new {Value = "Upper East", Text = "Upper East"},
                                      new {Value = "Upper West", Text = "Upper West"},
                                      new {Value = "Greater Accra", Text = "Greater Accra"},
                                      new {Value = "Other", Text = "Other"}
                                  };

                return new SelectList(regions, "Value", "Text", new { Value = "Greater Accra", Text = "Greater Accra" });
            }
        }

        [Required]
        public string Region { get; set; }

        [Required(ErrorMessage = "User's Country is required")]
        [Display(Name = "Country")]
        public SelectList Countries
        {
            get
            {
                var types = from Country t in Enum.GetValues(typeof(Country))
                            select new { Id = t, Name = t.ToString() };

                return new SelectList(types, "Id", "Name", "Ghana");
            }
        }

        [Required]
        public Country Country { get; set; }

        public string Username { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    #endregion

    #region Services
    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    /// <summary>
    /// IMembershipService contains all the required members for changing password and creating a user
    /// </summary>
    /// <originalSummary>
    /// The FormsAuthentication type is sealed and contains static members, so it is difficult to
    /// unit test code that calls its members. The interface and helper class below demonstrate
    /// how to create an abstract wrapper around such a type in order to make the AccountController
    /// code unit testable.
    /// </originalSummary>
    public interface IMembershipService
    {
        int MinPasswordLength { get; }
        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    /// <summary>
    /// Account membership service 
    /// </summary>
    public class AccountMembershipService : IMembershipService
    {
        /// <summary>
        /// Membership provider (via .NET framework)
        /// </summary>
        private readonly MembershipProvider _provider;


        /// <summary>
        /// Default constructor
        /// </summary>
        public AccountMembershipService()
            : this(null)
        {
        }

        /// <summary>
        /// Constructor that takes in a membership provider
        /// </summary>
        /// <param name="provider">.NET membership provider</param>
        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        /// <summary>
        /// Minimum password length for creating or changing password 
        /// </summary>
        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        /// <summary>
        /// Validate user through .NET provider
        /// </summary>
        /// <param name="userName">username to validate</param>
        /// <param name="password">password of the username to validate</param>
        /// <returns>Whether or not the user is valid</returns>
        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return _provider.ValidateUser(userName, password);
        }

        /// <summary>
        /// Create a user through the .NET membership provider
        /// </summary>
        /// <param name="username">username of the user to create</param>
        /// <param name="password">password of the user account to create</param>
        /// <param name="email">email of the user account to create</param>
        /// <returns>status of the membership creation</returns>
        public MembershipCreateStatus CreateUser(string username, string password, string email)
        {
            if (String.IsNullOrEmpty(username)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");
            //if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            MembershipCreateStatus status;

            _provider.CreateUser(username, password, email, null, null, false, null, out status);

            return status;
        }

        /// <summary>
        /// Change password for a user
        /// </summary>
        /// <param name="userName">username to change the password for</param>
        /// <param name="oldPassword">the old password for the user</param>
        /// <param name="newPassword">the new password to change it to</param>
        /// <returns></returns>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Forms authentication service that provides sign in and sign out functionality
    /// </summary>
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    /// <summary>
    /// Implementation of a forms authentication service that provides sign in and sign out functionality
    /// </summary>
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        /// <summary>
        /// Sign in and create persitant cookie for the user 
        /// </summary>
        /// <param name="userName">username to log in</param>
        /// <param name="createPersistentCookie">whether or not the cookie should be persistant</param>
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        /// <summary>
        /// Sign out of the forms authentication
        /// </summary>
        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
    #endregion

    #region Validation

    /// <summary>
    /// Validation for account access
    /// </summary>
    public static class AccountValidation
    {
        /// <summary>
        /// Get the string representation of an error code returned by member creation
        /// </summary>
        /// <param name="createStatus">the error status</param>
        /// <returns></returns>
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }

    /// <summary>
    /// Password validation rules
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute, IClientValidatable
    {
        private const string _defaultErrorMessage = "'{0}' must be at least {1} characters long.";
        private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                name, _minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]{
                new ModelClientValidationStringLengthRule(FormatErrorMessage(metadata.GetDisplayName()), _minCharacters, int.MaxValue)
            };
        }
    }
    #endregion

}
