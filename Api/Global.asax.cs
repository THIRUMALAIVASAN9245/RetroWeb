using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Retrospective.Application.API.Installers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Retrospective.Application.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ConfigureWindsor(GlobalConfiguration.Configuration);
        }

        public static void ConfigureWindsor(HttpConfiguration configuration)
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));

            var dependencyResolver = new WindsorDependencyResolver(container);

            configuration.DependencyResolver = dependencyResolver;

        }
    }
}
