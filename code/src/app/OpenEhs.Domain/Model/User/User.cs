/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-12-2011
 * 
 * Author: Cameron Harp (charp5257@gmail.com)
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace OpenEhs.Domain
{
    /// <summary>
    /// User represents a user that can login to the system and have a role
    /// </summary>
    public class User : IEntity
    {
        #region Properties

        /// <summary>
        /// Id of the User
        /// </summary>
        public virtual int Id { get; private set; }

        /// <summary>
        /// First name of the User
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Middle name of the user
        /// </summary>
        public virtual string MiddleName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Username of the user
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// Phone number of the user
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// The type of staff this user is
        /// </summary>
        public virtual StaffType StaffType { get; set; }

        /// <summary>
        /// user's license number
        /// </summary>
        public virtual string LicenseNumber { get; set; }

        /// <summary>
        /// Address of the user
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// TODO: figure out what application name is
        /// </summary>
        public virtual string ApplicationName { get; set; }

        /// <summary>
        /// Password to the users account
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// Password question (in case they forget their password)
        /// </summary>
        public virtual string PasswordQuestion { get; set; }

        /// <summary>
        /// Answer to the password question
        /// </summary>
        public virtual string PasswordAnswer { get; set; }

        /// <summary>
        /// Date that the user was created
        /// </summary>
        public virtual DateTime DateCreated { get; set; }

        /// <summary>
        /// Date and time of the last login
        /// </summary>
        public virtual DateTime LastLogin { get; set; }

        /// <summary>
        /// Date and time of last activity
        /// </summary>
        public virtual DateTime LastActivity { get; set; }

        /// <summary>
        /// Date and time of last password change
        /// </summary>
        public virtual DateTime LastPasswordChange { get; set; }

        /// <summary>
        /// Whether or not the user is currently online
        /// </summary>
        public virtual bool IsOnline { get; set; }

        /// <summary>
        /// IP Address of the user
        /// </summary>
        public virtual string IpAddress { get; set; }

        /// <summary>
        /// Whether or not the user is locked out of their account (from too many failed login attempts)
        /// </summary>
        public virtual bool IsLockedOut { get; set; }

        /// <summary>
        /// Count of how many times the user has tried to login unsuccessfully
        /// </summary>
        public virtual int FailedPasswordAttemptCount { get; set; }

        /// <summary>
        /// Whether or not the user is approved
        /// </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>
        /// Whether or not the user is active
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// roles that the user is in
        /// </summary>
        public virtual IList<Role> Roles { get; private set; }

        /// <summary>
        /// Surgeries that this user is assigned to
        /// </summary>
        public virtual IList<Surgery> Surgery { get; set; }

        /// <summary>
        /// Patient checkins that have been administered by this user
        /// </summary>
        public virtual IList<PatientCheckIn> PatientCheckIns { get; set; }

        /// <summary>
        /// Users templates
        /// </summary>
        public virtual IList<Template> Templates { get; set; }
        //public virtual DateTime LastLockout { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public User()
            : this(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false)
        { }

        /// <summary>
        /// Constructor with the essential params
        /// </summary>
        /// <param name="username">username of the user</param>
        /// <param name="password">password for the user's account</param>
        /// <param name="emailAddress">email address of the user</param>
        /// <param name="passwordQuestion">password question for the user, in case they forget password</param>
        /// <param name="passwordAnswer">answer to the password question</param>
        /// <param name="isApproved">whether or not the user is approved yet</param>
        public User(string username, string password, string emailAddress, string passwordQuestion, string passwordAnswer, 
            bool isApproved)
        {
            Username = username;
            Password = password;
            EmailAddress = emailAddress;
            PasswordQuestion = passwordQuestion;
            PasswordAnswer = passwordAnswer;
            IsApproved = isApproved;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add role to the user
        /// </summary>
        /// <param name="role">role that the user is to be added to</param>
        public virtual void AddRole(Role role)
        {
            if (!Roles.Contains(role))
                Roles.Add(role);
            else
                throw new ArgumentException("This user is already in that role.");
        }

        /// <summary>
        /// Remove role from the user
        /// </summary>
        /// <param name="role">Role to be removed from this user</param>
        public virtual void RemoveRole(Role role)
        {
            Roles.Remove(role);
        }

        #endregion
    }
}
