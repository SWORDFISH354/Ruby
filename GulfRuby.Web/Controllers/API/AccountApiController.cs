using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using GulfRuby.Web.Core;
using GulfRuby.Web.Models;

namespace GulfRuby.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public AccountApiController(ISecurityAdapter securityAdapter)
        {
            _securityAdapter = securityAdapter;
        }
        private ISecurityAdapter _securityAdapter;

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(HttpRequestMessage request, [FromBody]AccountLoginModel accountModel)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

            //   _securityAdapter.Register(accountModel.UserName, accountModel.Password, null);
                var success = _securityAdapter.Login(accountModel.UserName, accountModel.Password, accountModel.RememberMe);
                response = success ? request.CreateResponse(HttpStatusCode.OK) : request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized login");
                return response;
            });
        }
    }
}
