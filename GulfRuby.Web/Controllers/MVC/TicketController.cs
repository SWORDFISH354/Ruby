﻿using System;
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
    [RoutePrefix("tickets")]
    [Authorize]
    public class TicketController : ViewControllerBase
    {
        [HttpGet]
        [Route("index")]
        [Authorize]
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        [Route("history")]
        [Authorize]
        public ActionResult History()
        {
            return View();
        }



         [HttpGet]
        [Route("ticket/{id}")]
        [Authorize]
        public ActionResult TicketStep1(int id)
        {
            return View(new TicketDetailModel() {ID = id});
        }


         [HttpGet]
         [Route("ticket/{id}/step2")]
         [Authorize]
         public ActionResult TicketStep2(int id)
         {
             return View();
         }

    }
}