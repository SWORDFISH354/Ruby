using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Common.Contracts;
using Core.Common.Core;
using Core.Common.Extensions;
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

                var bookingList = ticketRepository.GetAllPendingBookings();
                var miniBookingList = from ticket in bookingList
                                     select new
                                     {
                                         ticket.ID,
                                         ticket.Status,
                                         ticket.IssueDate,
                                         ticket.InvoiceNumber
                                     };
                return request.CreateResponse(HttpStatusCode.OK, miniBookingList);
                //  return request.CreateResponse(HttpStatusCode.OK,ticketList);
            });

        }




        // Added By Jobin :- Start
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public HttpResponseMessage GetBookingById(HttpRequestMessage request, string id)
        {

            return GetHttpResponse(request, () =>
            {
                ITicketRepository ticketRepository = _dataRepositoryFactory.GetDataRepository<ITicketRepository>();
                var booking = ticketRepository.GetBookingAllById(Convert.ToInt32(id));
                var ticketDetailModel = ConvertBookingToTicketDetailModel(booking);
                return request.CreateResponse(HttpStatusCode.OK, ticketDetailModel);
                //  return request.CreateResponse(HttpStatusCode.OK,ticketList);
            });

        }

        [HttpPost]
        [Route("updatebookingstatus")]
        [Authorize]
        public HttpResponseMessage UpdateBookingStatus(HttpRequestMessage request, int  bookingid)
        {
            HttpResponseMessage response;
            return null;
        }

        [HttpPost]
        [Route("save")]
        [Authorize]
        public HttpResponseMessage Save(HttpRequestMessage request, [FromBody]TicketDetailModel model)
        {
            HttpResponseMessage response;
            ITicketRepository ticketRepository = _dataRepositoryFactory.GetDataRepository<ITicketRepository>();
            var booking = SetupBookingEntityFromRequest(model);
            booking.Status = TicketStatusEnum.NotBooked;
            if (model.ID == 0)
            {
                booking = AddNewBooking(booking, ticketRepository);
                var ticketDetailModel = ConvertBookingToTicketDetailModel(booking);
                return request.CreateResponse(HttpStatusCode.OK, ticketDetailModel);
            }
            else
            {

                // upDATE
                booking = UpdateNewBooking(booking, ticketRepository);
                var ticketDetailModel = ConvertBookingToTicketDetailModel(booking);
                response = request.CreateResponse(HttpStatusCode.OK, ticketDetailModel);
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

        private TicketDetailModel ConvertBookingToTicketDetailModel(Booking booking)
        {
            var ticketDetailModel = ClientObjectBase.Container.GetExportedValue<TicketDetailModel>();

            ticketDetailModel.ID = booking.ID;
            ticketDetailModel.InvoiceNumber = booking.InvoiceNumber;
            ticketDetailModel.CustomerType = (int)booking.CustomerType;
            ticketDetailModel.IssueDate = booking.IssueDate.ToString();
            ticketDetailModel.CareOf = booking.CareOf;
            ticketDetailModel.CorporateClient = booking.CorporateClient;
            ticketDetailModel.CustomerName = booking.CustomerName;
            ticketDetailModel.ContactNumber = booking.ContactNumber;
            ticketDetailModel.Email = booking.Email;
            ticketDetailModel.BaseFare = booking.BaseFare;
            ticketDetailModel.Tax = booking.Tax;
            ticketDetailModel.QuotedFare = booking.QuotedFare;
            ticketDetailModel.Status = (int)booking.Status;
            ticketDetailModel.ModeOfIssue = (int)booking.ModeOfIssue;
            ticketDetailModel.DueDate = booking.DueDate.ToString("dd/MM/yyyy");

            foreach (var bookingItinerary in booking.Itinerary)
            {
                var itinerary = ClientObjectBase.Container.GetExportedValue<ItineraryDetailModel>();
                itinerary.ID = bookingItinerary.ID;
                itinerary.BookingId = bookingItinerary.BookingId;
                itinerary.Date = bookingItinerary.Date.ToString();
                itinerary.DateIsOpen = bookingItinerary.DateIsOpen;
                itinerary.From = bookingItinerary.From;
                itinerary.To = bookingItinerary.To;
                itinerary.Carrier = bookingItinerary.Carrier;
                itinerary.FlightNo = bookingItinerary.FlightNo;
                itinerary.DepTime = bookingItinerary.DepTime;
                itinerary.ArrTime = bookingItinerary.ArrTime;
                itinerary.Status = (int)bookingItinerary.Status;
                itinerary.Class = (int)bookingItinerary.Class;
                itinerary.IsActive = bookingItinerary.IsActive;
                ticketDetailModel.Itineraries.Add(itinerary);
            }
            foreach (var bookingPassenger in booking.Passengers)
            {
                var passenger = ClientObjectBase.Container.GetExportedValue<PassengersDetailModel>();
                passenger.ID = bookingPassenger.ID;
                passenger.BookingId = bookingPassenger.BookingId;
                passenger.FirstName = bookingPassenger.FirstName;
                passenger.SecondName = bookingPassenger.SecondName;
                passenger.Type = bookingPassenger.Type;
                passenger.PassportNo = bookingPassenger.PassportNo;
                passenger.Nationality = bookingPassenger.Nationality;
                passenger.IsActive = bookingPassenger.IsActive;
                ticketDetailModel.Passengers.Add(passenger);
            }



            return ticketDetailModel;
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
                                              ID = itinerary.ID,
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
                                                  ID = pass.ID,
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
