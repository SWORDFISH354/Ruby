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
    [RoutePrefix("hr")]
    [Authorize]
    public class EmployeeDetailsController : ViewControllerBase
    {
        // GET: EmployeeDetails
        [HttpGet]
        [Route("employee/{id}")]
        [Authorize]
        public ActionResult EmployeeDetails(int id)
        {
           
            return View(new EmployeeDetailModel() {Id = id});
        }


        // GET: GetAllEmployees 
        [HttpGet]
        [Route("employees")]
        [Authorize]
        public ActionResult GetAllEmployees()
        {
            return View("AllEmployees");
        }


        // GET: CompanyInformation
        [HttpGet]
        [Route("companyinformation")]
        [Authorize]
        public ActionResult CompanyInformation()
        {
            return View();
        }



    }
}