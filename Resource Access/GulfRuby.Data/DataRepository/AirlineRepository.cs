
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Sockets;
using GulRuby.Business.Entities;
using Core.Common.Data;
using GulRuby.Data.Contracts.Repository_Interfaces;

namespace GulfRuby.Data.DataRepository
{
    [Export(typeof(IAirlineRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AirlineRepository : DataRepositoryBase<Airline>, IAirlineRepository
    {
        protected override Airline AddEntity(GulfRubyContext entityContext, Airline entity)
        {
            return entityContext.AirlineSet.Add(entity);
        }

        protected override Airline UpdateEntity(GulfRubyContext entityContext, Airline entity)
        {
            return (from e in entityContext.AirlineSet
                where e.ID == entity.ID
                select e).FirstOrDefault();
        }

        protected override IEnumerable<Airline> GetEntities(GulfRubyContext entityContext)
        {
            return from e in entityContext.AirlineSet
                select e;
        }

        protected override Airline GetEntity(GulfRubyContext entityContext, int id)
        {
            var query = (from e in entityContext.AirlineSet
                where e.ID == id
                select e);
            var results = query.FirstOrDefault();
            return results;
        }
    }
}
