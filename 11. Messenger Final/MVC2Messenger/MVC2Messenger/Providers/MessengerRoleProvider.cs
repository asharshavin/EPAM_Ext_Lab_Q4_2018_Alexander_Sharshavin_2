using System;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL2Messenger;

namespace MVC2Messenger.Providers
{
    public class MessengerRoleProvider : RoleProvider
    {
        private const string MessengerConnectionString = "MessengerConection";
        private static UnitOfWork repo = new UnitOfWork(MessengerConnectionString);

        public MessengerRoleProvider()
        {
        }

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            var user = repo.Users.GetAll().FirstOrDefault(u => u.name == username);
            if (user != null && user.role != 0)
            {
                roles = new string[] { repo.Roles.Get(user.role).name };
            }
            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = repo.Users.GetAll().FirstOrDefault(u => u.name == username);
            if (user != null && user.role != 0 && repo.Roles.Get(user.role).name == roleName)
                return true;
            else
                return false;
        }
    }
}