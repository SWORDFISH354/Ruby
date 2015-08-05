using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using Core.Common.Core;
//using System.ComponentModel.DataAnnotations.Schema;

namespace GulRuby.Business.Entities
{
    public enum ItineraryClassEnum
    {

        First,Business
    }
    
    public enum ItineraryStatusEnum
    {
        Booked,NotBooked
    }
    public class Itinerary //: ObjectBase//, IIdentifiableEntity
    {
        //[NotNavigable]
        //public int EntityId
        //{
        //    get { return Id; }
        //    set { Id = value; }
        //}
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }         
        public int BookingId{set;get;}
        public DateTime? Date{set;get;}
        public bool DateIsOpen {set;get;}    
        
        public string From {set;get;}
        public string To {set;get;}
        public string Carrier {set;get;}
        public string FlightNo {set;get;}
        public DateTime? DepTime {set;get;}
        public DateTime? ArrTime {set;get;}
        public ItineraryStatusEnum Status {set;get;}
        public ItineraryClassEnum Class{set;get;}
        public bool IsActive { set; get; }

        public virtual Booking Ticket { set; get; }
    }
}
