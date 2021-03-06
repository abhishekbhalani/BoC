using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using BoC.Security.Model;
using BoC.Services;

namespace BoC.Security.Services
{
    public enum PasswordFormat
    {
        Clear,
        Hashed,
        Encrypted
    }

    public interface IUserService : IModelService<User>
    {
        User Insert(User user, String password);

        Boolean UserExists(String login);
        Boolean ChangePassword(User user, String oldPassword, String newPassword);
        void SetPassword(User user, String password);
        String EncodePassword(String password);
        Boolean CheckPassword(String password, String dbpassword);

        IEnumerable<User> FindUsersByPartialLogin(String login);
        Int32 CountUsersByPartialLogin(String login);
        IEnumerable<User> FindUsersByPartialEmail(String email);
        Int32 CountUsersByPartialEmail(String email);

        Int32 CountOnlineUsers(TimeSpan activitySpan);

    	void UnlockUser(User user);
    	void LockUser(User user);

        User FindByEmail(string email);
		User FindByLogin(string login);

        User Authenticate(String login, String password);

    	User GetByPrincipal(IPrincipal principal);

        #region Roles
        void AddUsersToRoles(IEnumerable<User> users, IEnumerable<Role> roles);
        void AddUsersToRoles(IEnumerable<String> logins, IEnumerable<String> roleNames);
        void RemoveUsersFromRoles(IEnumerable<String> logins, IEnumerable<String> roleNames);
        void RemoveUsersFromRoles(IEnumerable<User> users, IEnumerable<Role> roles);
        void CreateRole(String roleName);
        Role GetRole(Int64 id);
        Role FindRole(String roleName);
        Boolean RoleExists(String roleName);
        void DeleteRole(String roleName);
        void DeleteRole(Role role);
        void DeleteRole(String roleName, Boolean throwWhenUsed);
        void DeleteRole(Role role, Boolean throwWhenUsed);
        Role[] GetAllRoles();
        #endregion

        #region settings
        Boolean RequiresUniqueEmail { get; set; }
        Int32 MaxInvalidPasswordAttempts { get; set; }
        Int32 PasswordAttemptWindowMinutes { get; set; }
        PasswordFormat PasswordFormat { get; set; }
        Int32 MinRequiredNonAlphanumericCharacters { get; set; }
        Int32 MinRequiredPasswordLength { get; set; }
        string PasswordStrengthRegularExpression { get; set; }
        #endregion
    }
}