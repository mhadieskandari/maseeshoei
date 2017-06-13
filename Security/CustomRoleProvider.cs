using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MaaseShooei.Areas.Management.Models;

namespace MaaseShooei.Security
{
    public class CustomRoleProvider : RoleProvider
    {

        public override string[] GetRolesForUser(string username)
        {
            using (MaaseDBEntities db = new MaaseDBEntities())
            {
                T_Users user = db.T_Users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));

                var roles = from ur in user.T_UsersInRoles
                            from r in db.T_UserRoles
                            where ur.RoleId == r.RoleId
                            select r.RoleName;
                if (roles != null)
                    return roles.ToArray();
                else
                    return new string[] { }; ;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (MaaseDBEntities db = new MaaseDBEntities())
            {
                T_Users user = db.T_Users.FirstOrDefault(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));

                var roles = from ur in user.T_UsersInRoles
                            from r in db.T_UserRoles
                            where ur.RoleId == r.RoleId
                            select r.RoleName;
                if (user != null)
                    return roles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
                else
                    return false;
            }
        }


        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
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
    }
}