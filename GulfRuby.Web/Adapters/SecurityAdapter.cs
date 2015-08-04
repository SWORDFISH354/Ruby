using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using GulfRuby.Web.Core;
using WebMatrix.WebData;

namespace GulfRuby.Web.Services
{
    [Export(typeof(ISecurityAdapter))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecurityAdapter : ISecurityAdapter
    {
        public void Initialize()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("GulfRuby", "Account", "AccountId", "UserName", autoCreateTables: true);
        }

        public void Register(string loginEmail, string password, object propertyValues)
        {
            WebSecurity.CreateUserAndAccount(loginEmail, password, propertyValues);
        }
        
        public bool Login(string loginEmail, string password, bool rememberMe)
        {
            return WebSecurity.Login(loginEmail, password, persistCookie: rememberMe);
        }

        public bool ChangePassword(string loginEmail, string oldPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(loginEmail, oldPassword, newPassword);
        }

        public bool UserExists(string loginEmail)
        {
            return WebSecurity.UserExists(loginEmail);
        }

        public bool Logout()
        {
            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("GulfRuby", "Account", "AccountId", "UserName", autoCreateTables: true);
             WebSecurity.Logout();
            return true;
        }


    }
}
