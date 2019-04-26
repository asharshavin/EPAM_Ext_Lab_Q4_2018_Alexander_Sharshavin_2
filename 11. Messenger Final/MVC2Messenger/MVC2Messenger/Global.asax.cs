using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DAL2Messenger;
using DAL2Messenger.Interfaces;
using Ninject;

namespace MVC2Messenger
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
    }
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
        // Ki. надо бы еще пример получения объекта используя контейнер зависимостей. 
        this.kernel.Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("connectionStringItem", ConfigurationManager.ConnectionStrings[messengerConnectionString]);
    }
}
