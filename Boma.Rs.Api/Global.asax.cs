using Boma.RedeSocial.AppService.Users.Interfaces;
using Boma.RedeSocial.AppService.Users.Services;
using Boma.RedeSocial.Crosscut.Auditing;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Interfaces.Repositories;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Domain.Users.Services;
using Boma.RedeSocial.Infrastructure.Auditing;
using Boma.RedeSocial.Infrastructure.Data;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories;
using Boma.Rs.Api.Context;
using Boma.Rs.Api.StartupConfigurations;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Boma.Rs.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // Register your types, for instance using the scoped lifestyle:
            container.Register<DbConnection>(() => new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString), new ExecutionContextScopeLifestyle());
            //container.Register(SexMoveContext.ResolveUser, scope);

            container.Register<ISexMoveUnitOfWork, SexMoveUnitOfWork>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IUserAspNetRepository, UserAspNetRepository>(Lifestyle.Scoped);
            container.Register<IUserAppService, UserAppService>(Lifestyle.Scoped);
            container.Register<ISexMoveContext, SexMoveContext>(Lifestyle.Scoped);
            container.Register<IBomaAuditing, SexMoveAuditing>(Lifestyle.Scoped);
            container.Register<ISexMoveIdentityStore, SexMoveIdentityStore>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);


            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            

        }
        
    }
}
