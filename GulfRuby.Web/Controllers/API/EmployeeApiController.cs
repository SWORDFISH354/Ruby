using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Common.Contracts;
using GulfRuby.Web.Core;
using GulfRuby.Web.Models;
using GulRuby.Business.Entities;
using GulRuby.Data.Contracts.Repository_Interfaces;
using WebMatrix.WebData;

namespace GulfRuby.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RoutePrefix("api/hr")]
    public class EmployeeApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public EmployeeApiController(ISecurityAdapter securityAdapter, IDataRepositoryFactory dataRepository)
        {
            _securityAdapter = securityAdapter;
            _dataRepositoryFactory = dataRepository;
        }
        private ISecurityAdapter _securityAdapter;

        [Import]
        private IDataRepositoryFactory _dataRepositoryFactory;

        [HttpGet]
        [Route("getemployees")]
        [Authorize]
        public HttpResponseMessage GetAllEmployees(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                var employeeSet = employeeRepository.Get().ToList();
                var minifiedEmployee = from employee in employeeSet
                                       select new
                                              {
                                                  employee.Id,
                                                  employee.UserName,
                                                  employee.Name,
                                                  employee.Position,
                                                  employee.PassportNumber,
                                                  employee.VisaNumber,
                                                  employee.EmiratesID,
                                                  employee.BasicSalary
                                              };
                return request.CreateResponse(HttpStatusCode.OK, minifiedEmployee);
            });

        }



        [HttpGet]
        [Route("getemployeeById/{employeeId}")]
        [Authorize]
        public HttpResponseMessage GetEmployeeById(HttpRequestMessage request, int employeeId)
        {
            return GetHttpResponse(request, () =>
            {
                IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();
                var employee = employeeRepository.Get().FirstOrDefault(a => a.Id == employeeId);
                return request.CreateResponse(HttpStatusCode.OK, employee);
            });
        }


        [HttpPost]
        [Route("addemployee")]
        [Authorize]
        public HttpResponseMessage AddEmployee(HttpRequestMessage request, [FromBody]EmployeeDetailModel model)
        {

            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                IEmployeeRepository employeeRepository = _dataRepositoryFactory.GetDataRepository<IEmployeeRepository>();

                if (model.Id == 0)
                {
                    var employee = new Employee()
                                   {
                                       Id = 0,
                                       Name = model.Name,
                                       UserName = model.UserName,
                                       Position = model.Position,
                                       PassportNumber = model.PassportNumber,
                                       PassportExpiry = String.IsNullOrEmpty(model.PassportExpiry) ? (DateTime?)null : DateTime.ParseExact(model.PassportExpiry, "dd/MM/yyyy", null),
                                       EmiratesID = model.EmiratesID,
                                       EmiratesIDExpiry = String.IsNullOrEmpty(model.EmiratesIDExpiry) ? (DateTime?)null : DateTime.ParseExact(model.EmiratesIDExpiry, "dd/MM/yyyy", null),
                                       VisaNumber = model.VisaNumber,
                                       VisaExpiry = String.IsNullOrEmpty(model.VisaExpiry) ? (DateTime?)null : DateTime.ParseExact(model.VisaExpiry, "dd/MM/yyyy", null),
                                       InsuranceNumber = model.InsuranceNumber,
                                       InsuranceExpiry = String.IsNullOrEmpty(model.InsuranceExpiry) ? (DateTime?)null : DateTime.ParseExact(model.InsuranceExpiry, "dd/MM/yyyy", null),
                                       BasicSalary = model.BasicSalary,
                                       JoiningDate = String.IsNullOrEmpty(model.JoiningDate) ? (DateTime?)null : DateTime.ParseExact(model.JoiningDate, "dd/MM/yyyy", null),
                                       Allowance = model.Allowance,
                                       ModifiedBy = User.Identity.Name,
                                       AddedTime = DateTime.UtcNow
                                   };

                    if (!WebSecurity.Initialized)
                        WebSecurity.InitializeDatabaseConnection("GulfRuby", "Account", "AccountId", "UserName", autoCreateTables: true);
                    if (!_securityAdapter.UserExists(model.UserName))
                    {
                        _securityAdapter.Register(model.UserName, "GulfRubyPassword00971", null);
                        employeeRepository.Add(employee);
                        response = request.CreateResponse(HttpStatusCode.OK);

                    }
                    else
                        response = request.CreateResponse<string[]>(HttpStatusCode.BadRequest, new List<string>() { "An account is already registered with this email address." }.ToArray());
                }
                else
                {

                    var employee = new Employee()
                                   {
                                       Id = model.Id,
                                       Name = model.Name,
                                       UserName = model.UserName,
                                       Position = model.Position,
                                       PassportNumber = model.PassportNumber,
                                       PassportExpiry = String.IsNullOrEmpty(model.PassportExpiry) ? (DateTime?)null : DateTime.ParseExact(model.PassportExpiry, "dd/MM/yyyy", null),
                                       EmiratesID = model.EmiratesID,
                                       EmiratesIDExpiry = String.IsNullOrEmpty(model.EmiratesIDExpiry) ? (DateTime?)null : DateTime.ParseExact(model.EmiratesIDExpiry, "dd/MM/yyyy", null),
                                       VisaNumber = model.VisaNumber,
                                       VisaExpiry = String.IsNullOrEmpty(model.VisaExpiry) ? (DateTime?)null : DateTime.ParseExact(model.VisaExpiry, "dd/MM/yyyy", null),
                                       InsuranceNumber = model.InsuranceNumber,
                                       InsuranceExpiry = String.IsNullOrEmpty(model.InsuranceExpiry) ? (DateTime?)null : DateTime.ParseExact(model.InsuranceExpiry, "dd/MM/yyyy", null),
                                       BasicSalary = model.BasicSalary,
                                       JoiningDate = String.IsNullOrEmpty(model.JoiningDate) ? (DateTime?)null : DateTime.ParseExact(model.JoiningDate, "dd/MM/yyyy", null),
                                       Allowance = model.Allowance,
                                       ModifiedBy = User.Identity.Name,
                                       AddedTime = DateTime.UtcNow
                                   };

                    employeeRepository.Update(employee);
                }
                response = request.CreateResponse(HttpStatusCode.OK);
                return response;
            });

        }


        [HttpGet]
        [Route("companyinformation")]
        [Authorize]
        public HttpResponseMessage GetCompanyInformation(HttpRequestMessage request)
        {
            return GetHttpResponse(request, () =>
            {
                ICompanyInformationRepository companyInformationRepository = _dataRepositoryFactory.GetDataRepository<ICompanyInformationRepository>();
                var employee = companyInformationRepository.Get().FirstOrDefault();
                return request.CreateResponse(HttpStatusCode.OK, employee);
            });
        }




        [HttpPost]
        [Route("submitcompanyinformation")]
        [Authorize]
        public HttpResponseMessage SubmitCompanyInformation(HttpRequestMessage request, [FromBody] CompanyInformationModel model)
        {

            return GetHttpResponse(request, () =>
                                            {
                                                HttpResponseMessage response = null;

                                                ICompanyInformationRepository companyInformationRepository =
                                                    _dataRepositoryFactory.GetDataRepository<ICompanyInformationRepository>();
                                                if (model.ID > 0)
                                                {
                                                    var companyInfo = new CompanyInformation()
                                                                      {
                                                                          ID = model.ID,
                                                                          TourismLicense = model.TourismLicense,
                                                                          TourismExpiry = DateTime.ParseExact(model.TourismExpiry, "dd/MM/yyyy", null),
                                                                          Rent = model.Rent,
                                                                          RentExpiry = DateTime.ParseExact(model.RentExpiry, "dd/MM/yyyy", null),
                                                                          WasteNumber = model.WasteNumber,
                                                                          WasteNumberExpiry = DateTime.ParseExact(model.WasteNumberExpiry, "dd/MM/yyyy", null),
                                                                          Sponsorship = model.Sponsorship,
                                                                          SponsorshipExpiry = DateTime.ParseExact(model.SponsorshipExpiry, "dd/MM/yyyy", null),
                                                                          CivilDefence = model.CivilDefence,
                                                                          CivilDefenceExpiry = DateTime.ParseExact(model.CivilDefenceExpiry, "dd/MM/yyyy", null),
                                                                          AddedTime = DateTime.UtcNow,
                                                                          LastModifiedBy = User.Identity.Name
                                                                      };



                                                    companyInformationRepository.Update(companyInfo);
                                                    response = request.CreateResponse(HttpStatusCode.OK);
                                                }
                                                return response;
                                            });

        }





    }
}
