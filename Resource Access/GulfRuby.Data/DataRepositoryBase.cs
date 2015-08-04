
using System.Collections.Generic;
using Core.Common.Contracts;
using Core.Common.Data;

namespace GulfRuby.Data
{
    public abstract class DataRepositoryBase<T> : DataRepositoryBase<T, GulfRubyContext>
         where T : class, IIdentifiableEntity, new()
    {
       
    }
}
