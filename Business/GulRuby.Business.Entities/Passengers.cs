using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
//using Core.Common.Core;
//using System.ComponentModel.DataAnnotations.Schema;

namespace GulRuby.Business.Entities
{
    public class PassengerInfo //: ObjectBase//, IIdentifiableEntity
    {
       //[NotNavigable]
       //public int EntityId
       //{
       //    get { return ID; }
       //    set { ID = value; }
       //}
      //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        public int BookingId{get;set;}
       
        public string FirstName  { get; set; }     
        public string SecondName { get; set; }     
        public string Type  { get; set; }     
        public string PassportNo  { get; set; }     
        public string Nationality { get; set; }     
        public DateTime AddedTime { get; set; }     
        public string AddedBy  { get; set; }
        public bool IsActive { get; set; }

        public virtual Booking Ticket { set; get; }
    }
}
