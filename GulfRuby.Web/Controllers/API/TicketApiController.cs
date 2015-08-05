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
    [RoutePrefix("api/ticket")]
    public class TicketApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public TicketApiController(ISecurityAdapter securityAdapter, IDataRepositoryFactory dataRepository)
        {
            _securityAdapter = securityAdapter;
            _dataRepositoryFactory = dataRepository;
        }
        private ISecurityAdapter _securityAdapter;
        [Import]
        private IDataRepositoryFactory _dataRepositoryFactory;



        // Added By Jobin :- Start
        [HttpGet]
        [Route("pendingBookingTickets")]
        [Authorize]
        public HttpResponseMessage GetAllPendingBookingTickets(HttpRequestMessage request)
        {

            return GetHttpResponse(request, () =>
            {
                ITicketRepository ticketRepository = _dataRepositoryFactory.GetDataRepository<ITicketRepository>();

                var ticketList = ticketRepository.Get().ToList();
                var miniTicketList = from ticket in ticketList
                                     select new
                                     {
                                         ticket.ID,
                                         ticket.Status,
                                         ticket.IssueDate,
                                         ticket.InvoiceNumber
                                     };
                return request.CreateResponse(HttpStatusCode.OK, miniTicketList);
                //  return request.CreateResponse(HttpStatusCode.OK,ticketList);
            });

        }

        [HttpPost]
        [Route("save")]
        [Authorize]
        public HttpResponseMessage Save(HttpRequestMessage request, [FromBody]TicketDetailModel model)
        {
            HttpResponseMessage response;

            ITicketRepository ticketRepository = _dataRepositoryFactory.GetDataRepository<ITicketRepository>();

            if (model.ID == 0)
            {
               var booking = AddNewBooking(model, ticketRepository);
               return request.CreateResponse(HttpStatusCode.OK, booking);
              //  response = request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                // upDATE
                //
                response = request.CreateResponse(HttpStatusCode.OK);
            }
            return response;
            /*if (model.Id == 0)
            {
              
             }*/

        }

        private Booking AddNewBooking(TicketDetailModel model, ITicketRepository ticketRepository)
        {
            try
            {

                var ticket = new Booking
                             {
                                 ID = 0,
                                 BaseFare = model.BaseFare,
                                 CareOf = model.CareOf,
                                 ContactNumber = model.ContactNumber,
                                 CorporateClient = model.CorporateClient,
                                 CustomerName = model.CustomerName,
                                 CustomerType = (CustomerTypeEnum)model.CustomerType,
                                 //CustomerType = (CustomerTypeEnum)CustomerType,
                                 // DueDate = String.IsNullOrEmpty(model.IssueDate.ToString()) ? (DateTime?)null : DateTime.ParseExact(model.IssueDate.ToString(), "dd/MM/yyyy", null),
                                 //DueDate = model.DueDate,
                                 Email = model.Email,
                                 InvoiceNumber = model.InvoiceNumber,
                                 IsActive = true,
                                 LastModifiedBy =  User.Identity.Name,
                                 //ModeOfIssue = model.ModeOfIssue,
                                 QuotedFare = model.QuotedFare,
                                 // Status = model.Status,
                                 Tax = model.Tax,
                                 AddedTime = DateTime.UtcNow,
                                 IssueDate = System.DateTime.Now,
                                 DueDate = System.DateTime.Now
                             };


                //  List<Itinerary> itineraryies = new List<Itinerary>();
                foreach (ItineraryDetailModel itinerary in model.Itineraries)
                {
                    Itinerary tempItinerary = new Itinerary()
                                              {
                                                  ID = 0,
                                                  //ArrTime = string.IsNullOrEmpty(itinerary.ArrTime) ? (DateTime?) null : DateTime.ParseExact(itinerary.ArrTime,"dd/mm/yyyy",null),
                                                  Carrier = itinerary.Carrier,
                                                  // Date = string.IsNullOrEmpty(itinerary.Date)? (DateTime?) null : DateTime.ParseExact(itinerary.Date,"dd/mm/yyyy",null),
                                                  DateIsOpen = itinerary.DateIsOpen,
                                                  //DepTime =string.IsNullOrEmpty(itinerary.DepTime)? (DateTime?) null :DateTime.ParseExact(itinerary.DepTime,"dd/mm/yyyy",null),
                                                  FlightNo = itinerary.FlightNo,
                                                  From = itinerary.From,
                                                  IsActive = true,
                                                  To = itinerary.To,
                                                  
                                              };
                    tempItinerary.Class = ItineraryClassEnum.Business;
                    tempItinerary.Status = ItineraryStatusEnum.Booked;
                    tempItinerary.BookingId = ticket.ID;
                    tempItinerary.DepTime = System.DateTime.Now;
                    tempItinerary.ArrTime = System.DateTime.Now;
                    tempItinerary.Date = System.DateTime.Now;

                    ticket.Itinerary.Add(tempItinerary);
                }


                // List<PassengerInfo> passengerInfo = new List<PassengerInfo>();
                foreach (PassengersDetailModel pass in model.Passengers)
                {
                    PassengerInfo tempPassenger = new PassengerInfo()
                                                  {
                                                      Id = 0,
                                                      AddedBy = User.Identity.Name, // Updated with user name
                                                      AddedTime = System.DateTime.Now,
                                                      FirstName = pass.FirstName,
                                                      IsActive = true,
                                                      Nationality = pass.Nationality,
                                                      PassportNo = pass.PassportNo,
                                                      SecondName = pass.SecondName,
                                                      BookingId = ticket.ID,
                                                      Type = pass.Type,
                                                  };
                    ticket.Passengers.Add(tempPassenger);
                }

                // if (!WebSecurity.Initialized)
                //    WebSecurity.InitializeDatabaseConnection("GulfRuby", "Account", "AccountId", "UserName", autoCreateTables: true);
                //if (!_securityAdapter.UserExists(model.UserName))
                //{
                //    _securityAdapter.Register(model.UserName, "GulfRubyPassword00971", null);

                var ticketResult = ticketRepository.Add(ticket);
                return ticketResult;
            }
            catch (NullReferenceException ex)
            {
                System.Console.WriteLine("Processor Usage" + ex.Message);
            }
            return null;
        }
    }
}
