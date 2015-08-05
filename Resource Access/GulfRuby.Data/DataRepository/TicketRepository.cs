using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GulRuby.Business.Entities;
using GulRuby.Data.Contracts.Repository_Interfaces;

namespace GulfRuby.Data.DataRepository
{
    [Export (typeof(ITicketRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class TicketRepository :DataRepositoryBase<Booking>, ITicketRepository
    {
        protected override Booking AddEntity(GulfRubyContext entityContext, Booking entity)
        {
            
            return entityContext.BookingSet.Add(entity);
            
        }

        protected override Booking UpdateEntity(GulfRubyContext entityContext, Booking entity)
        {
            return (from e in entityContext.BookingSet where e.ID == entity.ID select e).FirstOrDefault();
        }
      

        protected override IEnumerable<Booking> GetEntities(GulfRubyContext entityContext)
        {
            return from e in entityContext.BookingSet select e;
        }
        protected override Booking GetEntity(GulfRubyContext entityContext, int id)
        {
            return (from e in entityContext.BookingSet where e.ID == id select e).FirstOrDefault();
        }
        public void AddIteniaryToTicket(Booking t, Itinerary i)
        {

        }
       
    }
}


