using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using EFarming.Common.Filters;
using EFarming.Integration.Dependency;
using EFarming.Integration.Models;
using EFarming.Integration.Filters;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http.Validation;

[assembly: OwinStartup(typeof(EFarming.Integration.Startup))]
namespace EFarming.Integration
{
    public class Startup
    {
        private readonly IWindsorContainer container;

        public Startup()
        {
            this.container = new WindsorContainer();
        }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureWindsor(config);
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            config.Filters.Add(new UnhandledExceptionFilter());
        }

        public void ConfigureWindsor(HttpConfiguration config)
        {
            this.container.Install(new DependencyConventions());

            var dependencyResolver = new WindsorDependencyResolver(this.container);
            config.DependencyResolver = dependencyResolver;
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(5),
                //AccessTokenExpireTimeSpan = TimeSpan.MaxValue,
                Provider = this.container.Resolve<SimpleAuthorizationServerProvider>()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}