using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DAL2Messenger;
using Ninject;
using System.Configuration;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new NinjectDependencyResolver(new StandardKernel()));
        }

        public class NinjectDependencyResolver : IDependencyResolver
        {
            private IKernel kernel;

            public NinjectDependencyResolver(IKernel kernelParam)
            {
                this.kernel = kernelParam;
                this.AddBindings();
            }

            public object GetService(Type serviceType)
            {
                return this.kernel.TryGet(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return this.kernel.GetAll(serviceType);
            }

            private void AddBindings()
            {
                var messengerConnectionString = "MessengerConection";
                this.kernel.Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("connectionStringItem", ConfigurationManager.ConnectionStrings[messengerConnectionString]);
            }
        }
    }
}
