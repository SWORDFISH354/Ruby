using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Core.Common.Core;
using GulfRuby.Business.Bootstrapper;
using GulfRuby.Client.Bootstrapper;
using GulfRuby.Web.Core;

namespace GulfRuby.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            CompositionContainer container = MefLoader.Init(catalog.Catalogs);
            ObjectBase.Container = MEFLoader.Init();
            DependencyResolver.SetResolver(new MefDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new MefAPIDependencyResolver(container);
        }

      
    }
}