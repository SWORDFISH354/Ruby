using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Common.Contracts;
using Core.Common.Core;
using GulfRuby.Web.Core;
using GulfRuby.Web.Models;
using GulRuby.Business.Entities;
using GulRuby.Business.Entities.Enums;
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


        [HttpGet]
        [Route("test")]
        public HttpResponseMessage Save(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            var obj = ObjectBase.Container.GetExportedValueOrDefault<Booking>();
            var obj1 = ClientObjectBase.Container.GetExportedValueOrDefault<TicketDetailModel>();
            return response;
        }


        [HttpPost]
        [Route("save")]
        [Authorize]
        public HttpResponseMessage Save(HttpRequestMessage request, [FromBody]TicketDetailModel model)
        {
            HttpResponseMessage response;
            ITicketRepository ticketRepository = _dataRepositoryFactory.GetDataRepository<ITicketRepository>();
            var booking = SetupBookingEntityFromRequest(model);
            if (model.ID == 0)
            {
                booking = AddNewBooking(booking, ticketRepository);

                return request.CreateResponse(HttpStatusCode.OK, booking.ID);
            }
            else
            {
                
                // upDATE
                //
                response = request.CreateResponse(HttpStatusCode.OK);
            }
            return response;
            /*if (model.ID == 0)
            {
              
             }*/

        }

        private Booking AddNewBooking(Booking booking, ITicketRepository ticketRepository)
        {
            try
            {
                var ticketResult = ticketRepository.Add(booking);
                return ticketResult;
            }
            catch (NullReferenceException ex)
            {
                System.Console.WriteLine("Processor Usage" + ex.Message);
            }
            return null;
        }



        private Booking UpdateNewBooking(Booking booking, ITicketRepository ticketRepository)
        {
            try
            {
                var ticketResult = ticketRepository.Update(booking);
                return ticketResult;
            }
            catch (NullReferenceException ex)
            {
                System.Console.WriteLine("Processor Usage" + ex.Message);
            }
            return null;
        }




        private Booking SetupBookingEntityFromRequest(TicketDetailModel model)
        {
            var ticket = new Booking
                         {
                             ID = model.ID,
                             BaseFare = model.BaseFare,
                             CareOf = model.CareOf,
                             ContactNumber = model.ContactNumber,
                             CorporateClient = model.CorporateClient,
                             CustomerName = model.CustomerName,
                             CustomerType = (CustomerTypeEnum)model.CustomerType,
                             Email = model.Email,
                             InvoiceNumber = model.InvoiceNumber,
                             IsActive = true, //ToDo: need it to come from UI since this function is used for both Create and update
                             LastModifiedBy = User.Identity.Name,
                             QuotedFare = model.QuotedFare,
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
                                              ID = itinerary.Id,
                                              ArrTime = itinerary.ArrTime,
                                              Carrier = itinerary.Carrier,
                                              DateIsOpen = itinerary.DateIsOpen,
                                              DepTime = itinerary.DepTime,
                                              FlightNo = itinerary.FlightNo,
                                              From = itinerary.From,
                                              IsActive = true, //ToDo: need it to come from UI since this function is used for both Create and update
                                              To = itinerary.To,
                                              Class = ItineraryClassEnum.Business,
                                              Status = ItineraryStatusEnum.Booked,
                                              Date = String.IsNullOrEmpty(itinerary.Date) ? (DateTime?)null : DateTime.ParseExact(itinerary.Date, "dd/MM/yyyy", null),
                                          };
                ticket.Itinerary.Add(tempItinerary);
            }


            // List<PassengerInfo> passengerInfo = new List<PassengerInfo>();
            foreach (PassengersDetailModel pass in model.Passengers)
            {
                PassengerInfo tempPassenger = new PassengerInfo()
                                              {
                                                  ID = pass.Id,
                                                  AddedBy = User.Identity.Name, // Updated with user name
                                                  AddedTime = System.DateTime.UtcNow,
                                                  FirstName = pass.FirstName,
                                                  IsActive = true, //ToDo: need it to come from UI since this function is used for both Create and update
                                                  Nationality = pass.Nationality,
                                                  PassportNo = pass.PassportNo,
                                                  SecondName = pass.SecondName,
                                                  BookingId = ticket.ID,
                                                  Type = pass.Type,
                                              };
                ticket.Passengers.Add(tempPassenger);
            }
            return ticket;
        }
    }
}
