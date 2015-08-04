using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GulfRuby.Web.Core;
using GulfRuby.Web.Models;

namespace GulfRuby.Web.Controllers.MVC
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("account")]
    public class AccountController : ViewControllerBase
    {
        [ImportingConstructor]
        public AccountController(ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }

        private ISecurityAdapter _securityAdapter; 

        [HttpGet]
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            _securityAdapter.Initialize();
            return View(new AccountLoginModel(){ReturnUrl = returnUrl});
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            _securityAdapter.Logout();
            return RedirectToAction("Index", "Home");
        }


        

    }
}