using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Contracts;
using GulRuby.Business.Entities;

namespace GulRuby.Data.Contracts.Repository_Interfaces
{
   public interface ITicketRepository : IDataRepository<Booking>
    {
       void AddIteniaryToTicket(Booking t, Itinerary i);   
    }
}
    