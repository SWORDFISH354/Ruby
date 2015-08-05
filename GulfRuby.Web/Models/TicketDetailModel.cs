using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;

namespace GulfRuby.Web.Models
{

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TicketDetailModel
    {
        public int ID { set; get; }
        public int? InvoiceNumber { set; get; }
        public string IssueDate { get; set; }
        public int CustomerType { set; get; }
        public string CareOf { set; get; }
        public string CorporateClient { set; get; }
        public string CustomerName { set; get; }
        public string ContactNumber { set; get; }
        public string Email { set; get; }
        public virtual ICollection<ItineraryDetailModel> Itineraries { set; get; }
        public virtual ICollection<PassengersDetailModel> Passengers { set; get; }
        public decimal BaseFare { set; get; }
        public decimal Tax { set; get; }
        public decimal QuotedFare { set; get; }
        public int Status { set; get; }
        public int ModeOfIssue { set; get; }
        public string DueDate { set; get; }
        public string AddedTime { set; get; }
        public string LastModifiedBy { set; get; }
        public int IsActive { set; get; }
    }

    public class ItineraryDetailModel
    {

        public int Id { get; set; }
        public int BookingId { set; get; }
        public string Date { set; get; }
        public bool DateIsOpen { set; get; }

        public string From { set; get; }
        public string To { set; get; }
        public string Carrier { set; get; }
        public string FlightNo { set; get; }
        public string DepTime { set; get; }
        public string ArrTime { set; get; }
        public int Status { set; get; } 
        public int Class { set; get; }
        public bool IsActive { set; get; }

      
    }

    public class PassengersDetailModel  
    {
       
        public int Id { get; set; }

        public int BookingId { get; set; }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Type { get; set; }
        public string PassportNo { get; set; }
        public string Nationality { get; set; }
        public string AddedTime { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }

      
    }
}
