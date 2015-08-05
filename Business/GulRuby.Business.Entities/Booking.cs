using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Core;
using Core.Common.Contracts;
//using System.Data.Entity;
//using System.ComponentModel.DataAnnotations.Schema;



namespace GulRuby.Business.Entities
{
    public enum TicketStatusEnum
    {
        Booked,NotBooked
    }

    public enum CustomerTypeEnum
    {
        WalkIn = 1,
        Corporate 
    }
    public enum ModeOfIssueEnum
    {
        Cash, CreditNote
    }
    public class Booking:ObjectBase,IIdentifiableEntity
    {
        public Booking()
        {
            Itinerary = new List<Itinerary>();
            Passengers = new List<PassengerInfo>();
        }


        [NotNavigable]
        public int EntityId
        {
            get { return ID; }
            set { ID = value; }
        }

       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]     
        public int ID { get; set; }
        public int ? InvoiceNumber {set;get;}

        
        public DateTime?IssueDate {get;set;}
        public CustomerTypeEnum CustomerType{set;get;}        
        public string CareOf {set;get;}
        public string CorporateClient{set;get;}
        public string CustomerName{set;get;}
        public string ContactNumber{set;get;}
        public string Email{set;get;}       
        public virtual ICollection<Itinerary> Itinerary {set;get;}
        public virtual ICollection<PassengerInfo> Passengers { set; get; }        
        public decimal? BaseFare { set; get; }
        public decimal Tax { set; get; }
        public decimal QuotedFare { set; get; }
        public TicketStatusEnum Status {set;get;}
        public   ModeOfIssueEnum ModeOfIssue {set;get;}
        public DateTime DueDate {set;get;}
        public DateTime AddedTime {set;get;}
        public string LastModifiedBy {set;get;}
        public bool IsActive {set;get;}
    }
}
