using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GulfRuby.Data.DataRepository;

namespace GulfRuby.Business.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            //add items to catalog here
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(AirlineRepository).Assembly));

            CompositionContainer container = new CompositionContainer(catalog);
            return container;
        }
    }
}
