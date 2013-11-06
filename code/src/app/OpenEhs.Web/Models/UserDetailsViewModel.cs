using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using OpenEhs.Data;
using OpenEhs.Domain;

namespace OpenEhs.Web.Models
{
    /// <summary>
    /// User Details View model contains the data for creating or modifying a user
    /// </summary>
    public class UserDetailsViewModel
    {
        private User _user;

        public int UserId
        {
            get { return _user.Id; }
        }

        public string Username
        {
            get
            {
                return _user.Username;
            }
            set
            {
                _user.Username = value;
            }
        }

        public string Password
        {
            get
            {
                return _user.Password;
            }
            set
            {
                _user.Password = value;
            }
        }

        [Display(Name = "Email Address")]
        public string EmailAddress
        {
            get
            {
                return _user.EmailAddress;
            }
            set
            {
                _user.EmailAddress = value;
            }
        }

        [Display(Name = "First Name")]
        public string FirstName
        {
            get
            {
                return _user.FirstName;
            }
            set
            {
                _user.FirstName = value;
            }
        }

        [Display(Name = "Middle Name")]
        public string MiddleName
        {
            get
            {
                return _user.MiddleName;
            }
            set
            {
                _user.MiddleName = value;
            }
        }

        [Display(Name = "Last Name")]
        public string LastName
        {
            get
            {
                return _user.LastName;
            }
            set
            {
                _user.LastName = value;
            }
        }

        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get
            {
                return _user.PhoneNumber;
            }
            set
            {
                _user.PhoneNumber = value;
            }
        }

        public Address Address
        {
            get
            {
                return _user.Address;
            }
            set
            {
                _user.Address = value; 
            }
        }

        [Required(ErrorMessage = "User's Region is required.")]
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

        [Required(ErrorMessage = "User's Country is required.")]
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


        public IList<Role> Roles
        {
            get { return _user.Roles; }
        }

        [Display(Name = "Role")]
        public Role SelectedRole { get; set; }

        [Display(Name = "Roles")]
        public SelectList AvailableRoles
        {
            get
            {
                var roles = new List<Role>(new RoleRepository().GetAll());
                roles.Sort();

                return new SelectList(roles, "Id", "Name");
            }
        }

        public UserDetailsViewModel(User user)
        {
            _user = user;
        }
    }
}