using SimpleInjector;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.Rs.Api.Context;
using Boma.RedeSocial.Infrastructure.Data;
using Boma.RedeSocial.Infrastructure.Auditing;
using Boma.RedeSocial.Crosscut.Auditing;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories;
using Boma.RedeSocial.AppService.Users.Interfaces;
using Boma.RedeSocial.AppService.Users.Services;
using Boma.RedeSocial.Domain.Interfaces.Repositories;
using SimpleInjector.Integration.WebApi;
using Microsoft.AspNet.Identity;
using Boma.Rs.Api.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using System.Web;
using Owin;
using System.Web.Http;
using Boma.Rs.Api.Controllers;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Boma.Rs.Api.StartupConfigurations
{
    public static class StartupContainer
    {

        public static void ConfigureContainer(this IAppBuilder app, HttpConfiguration httpConfig)
        {
            var container = RegisterNewContainer(new ExecutionContextScopeLifestyle());
            //RegisterContext(container);
            //RegisterInfrastructure(container);
            //RegisterUserScope(container);

            //container.Register(() => HttpContext.Current.GetOwinContext(), Lifestyle.Scoped);
            httpConfig.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterUserScope(Container container)
        {
            container.Register<IUserRepository, UserRepository>();
            container.Register<IUserAspNetRepository, UserAspNetRepository>();
            container.Register<IUserAppService, UserAppService>();
        }

        private static void RegisterInfrastructure(Container container)
        {
            container.Register<ISexMoveUnitOfWork, SexMoveUnitOfWork>(Lifestyle.Scoped);
            container.Register<IBomaAuditing, SexMoveAuditing>(Lifestyle.Scoped);
        }

        private static void RegisterContext(Container container)
        {
            
            container.Register(() => container, Lifestyle.Singleton);
            container.Register<ISexMoveContext>(() => new SexMoveContext(),Lifestyle.Scoped);
            container.Register(SexMoveContext.ResolveConnection, Lifestyle.Scoped);
        }

        public static Container RegisterNewContainer(ScopedLifestyle scope)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<DbConnection>(() => new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString), scope);
            container.Register(SexMoveContext.ResolveUser, scope);

            //container.Register<ISexMoveUnitOfWork, SexMoveUnitOfWork>();
            container.Register<IUserRepository, UserRepository>();
            container.Register<IUserAspNetRepository, UserAspNetRepository>();
            container.Register<IUserAppService, UserAppService>();
            container.Register<ISexMoveContext,SexMoveContext>();

            //container.Register(SexMoveContext.ResolveConnection, Lifestyle.Scoped);

            
            return container;
        } 


        
            
    
    }
}