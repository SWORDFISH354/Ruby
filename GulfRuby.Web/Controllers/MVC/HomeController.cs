﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GulfRuby.Web.Controllers.MVC
{

        [Export]
        [PartCreationPolicy(CreationPolicy.NonShared)]
        [RoutePrefix("home")]
        public class HomeController : Controller
        {
            public ActionResult Index()
            {
                return View();
            }

            //[HttpGet]
            //[Route("my")]
            //[Authorize]
            //public ActionResult MyAccount()
            //{
            //    return View();
            //}
        }
  
}