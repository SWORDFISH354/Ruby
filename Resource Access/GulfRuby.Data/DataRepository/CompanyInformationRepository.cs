
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using GulRuby.Business.Entities;
using GulRuby.Data.Contracts.Repository_Interfaces;

namespace GulfRuby.Data.DataRepository
{
    [Export(typeof(ICompanyInformationRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CompanyInformationRepository : DataRepositoryBase<CompanyInformation>, ICompanyInformationRepository
    {
        protected override CompanyInformation AddEntity(GulfRubyContext entityContext, CompanyInformation entity)
        {
            return entityContext.CompanyInformationSet.Add(entity);
        }

        protected override CompanyInformation UpdateEntity(GulfRubyContext entityContext, CompanyInformation entity)
        {
            return (from e in entityContext.CompanyInformationSet
                    where e.ID == entity.ID
                    select e).FirstOrDefault();
        }

        protected override IEnumerable<CompanyInformation> GetEntities(GulfRubyContext entityContext)
        {
            return from e in entityContext.CompanyInformationSet
                   select e;
        }

        protected override CompanyInformation GetEntity(GulfRubyContext entityContext, int id)
        {
            var query = (from e in entityContext.CompanyInformationSet
                         where e.ID == id
                         select e);
            var results = query.FirstOrDefault();
            return results;
        }

    }
}
