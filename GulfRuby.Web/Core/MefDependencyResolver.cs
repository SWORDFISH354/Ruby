using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Web.Mvc;
using Core.Common.Extensions;

namespace GulfRuby.Web.Core
{
    public class MefDependencyResolver : IDependencyResolver
    {
        public MefDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        CompositionContainer _container;

        public object GetService(Type serviceType)
        {
            return _container.GetExportedValueByType(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetExportedValuesByType(serviceType);
        }
    }
}
