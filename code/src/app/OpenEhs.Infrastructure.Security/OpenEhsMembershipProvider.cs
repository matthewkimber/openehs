/*****************************************************************************
 * Project: Open Electronic Healthcare System
 * Group: Ghana Team
 * Date: Jan-19-2011
 * 
 * Author: Matthew Kimber (matthew.kimber@gmail.com)
 *****************************************************************************/

using System;
using System.Configuration.Provider;
using System.Web.Security;
using OpenEhs.Data;
using OpenEhs.Domain;

namespace OpenEhs.Infrastructure.Security
{
    public class OpenEhsMembershipProvider : MembershipProvider
    {
        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion


        #region Properties

        public override string ApplicationName
        {
            get
            {
                return System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            set {}
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 10; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 30; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        /// <summary>
        /// Password strength regular expression that ensures a users password meets the requirement
        /// </summary>
        public override string PasswordStrengthRegularExpression
        {
            get { return @"(?=.{6,})(?=(.*\d){1,})(?=(.*\W){1,})"; }
        }

        #endregion


        #region Constructor

        public OpenEhsMembershipProvider()
        {
            _userRepository = new UserRepository();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Create a new membership user
        /// </summary>
        /// <param name="username">username for the new user</param>
        /// <param name="password">password for the new user</param>
        /// <param name="email">email for the new user</param>
        /// <param name="passwordQuestion">password question that can help the user get their password if they forget</param>
        /// <param name="passwordAnswer">answer to the password question</param>
        /// <param name="isApproved">whether or not the user is already approved</param>
        /// <param name="providerUserKey">provider user key</param>
        /// <param name="status">membership create status</param>
        /// <returns>The new membership</returns>
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var user = new MembershipUser("OpenEhsMembershipProvider", username, null, email, passwordQuestion, "", false, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue);

            if (_userRepository.CheckForUsernameAvailability(username))
            {
                status = MembershipCreateStatus.Success;
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return user;
            }

            if (password.Length < MinRequiredPasswordLength)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return user;
            }

            return user;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update user from membership user
        /// </summary>
        /// <param name="user">membership user to save to user</param>
        public override void UpdateUser(MembershipUser user)
        {
            var existing = _userRepository.Get(user.UserName);

            if (existing == null)
                throw new ProviderException("That user does not exist.");

            existing.Username = user.UserName;
            existing.EmailAddress = user.Email;
            existing.PasswordQuestion = user.PasswordQuestion;
            existing.IsApproved = user.IsApproved;
            existing.IsLockedOut = user.IsLockedOut;
            existing.LastLogin = user.LastLoginDate;
            existing.IsOnline = user.IsOnline;
        }

        /// <summary>
        /// Validate if username and password are a valid combination
        /// </summary>
        /// <param name="username">username to check</param>
        /// <param name="password">password to check</param>
        /// <returns></returns>
        public override bool ValidateUser(string username, string password)
        {
            var users = _userRepository.Find(username.ToLower(), password);
            return users.Count != 0;
        }

        /// <summary>
        /// Unlock user from being locked out of their account
        /// </summary>
        /// <param name="userName">username to unlock</param>
        /// <returns></returns>
        public override bool UnlockUser(string userName)
        {
            var user = _userRepository.Get(userName);
            user.IsLockedOut = false;

            return true;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get membership user from username
        /// </summary>
        /// <param name="username">username to get membership user for</param>
        /// <param name="userIsOnline">whether or not the user is online</param>
        /// <returns></returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = _userRepository.Get(username);

            if (user == null)
                throw new ProviderException("The specified user does not exist.");

            user.IsOnline = userIsOnline;
            return TransformUser(user);
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if username exists
        /// </summary>
        /// <param name="username">username to check</param>
        /// <returns></returns>
        public bool UsernameExists(string username)
        {
            return _userRepository.CheckForUsernameAvailability(username);
        }


        #region Private Methods

        /// <summary>
        /// Create MembershipUser from User
        /// </summary>
        /// <param name="user">User to create a membershipUser from</param>
        /// <returns></returns>
        private static MembershipUser TransformUser(User user)
        {
            return new MembershipUser("OpenEhsMembershipProvider",
                                      user.Username,
                                      null,
                                      user.EmailAddress,
                                      user.PasswordQuestion,
                                      String.Empty,
                                      user.IsApproved,
                                      user.IsLockedOut,
                                      user.DateCreated,
                                      user.LastLogin,
                                      user.LastActivity,
                                      user.LastPasswordChange,
                                      DateTime.MinValue);
        }

        #endregion

        #endregion
    }
}
