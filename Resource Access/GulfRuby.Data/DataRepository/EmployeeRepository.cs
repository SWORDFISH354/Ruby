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
    [Export(typeof(IEmployeeRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EmployeeRepository : DataRepositoryBase<Employee>, IEmployeeRepository
    {
        protected override Employee AddEntity(GulfRubyContext entityContext, Employee entity)
        {
            return entityContext.EmployeeSet.Add(entity);
        }

        protected override Employee UpdateEntity(GulfRubyContext entityContext, Employee entity)
        {
            return (from e in entityContext.EmployeeSet
                    where e.Id == entity.Id
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<Employee> GetEntities(GulfRubyContext entityContext)
        {
            return from e in entityContext.EmployeeSet
                   select e;
        }

        protected override Employee GetEntity(GulfRubyContext entityContext, int id)
        {
            var query = (from e in entityContext.EmployeeSet
                         where e.Id == id
                         select e);
            var results = query.FirstOrDefault();
            return results;
        }

    }
}
