using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TransactionBalance
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<Bootstrapper>();
            var bootstrapper = container.Resolve<Bootstrapper>();
            bootstrapper.Initialize(GlobalConfiguration.Configuration);
            //GlobalConfiguration.Configure(bootstrapper.Initialize);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
