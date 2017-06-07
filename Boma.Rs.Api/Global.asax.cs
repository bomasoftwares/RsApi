using Boma.RedeSocial.AppService.Users.Interfaces;
using Boma.RedeSocial.AppService.Users.Services;
using Boma.RedeSocial.Crosscut.Auditing;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Domain.Interfaces.Repositories;
using Boma.RedeSocial.Domain.Profiles.Interfaces;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Domain.Users.Services;
using Boma.RedeSocial.Infrastructure.Auditing;
using Boma.RedeSocial.Infrastructure.Data;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Profiles;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Users;
using Boma.Rs.Api.Context;
using Boma.Rs.Api.Models;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
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
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();


            container.Register<DbConnection>(() => new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString), Lifestyle.Scoped);
            container.Register<ISexMoveUnitOfWork, SexMoveUnitOfWork>();
            container.Register<ISexMoveContext, SexMoveContext>();
            container.Register<ISexMoveIdentityStore, SexMoveIdentityStore>();
            container.Register<IBomaAuditing, SexMoveAuditing>();

            container.Register<IUserRepository, UserRepository>();
            container.Register<IUserAspNetRepository, UserAspNetRepository>();
            container.Register<IProfileRepository, ProfileRepository>();
            container.Register<IProfilePeopleConfigurationRepository, ProfilePeopleConfigurationRepository>();

            container.Register<IUserAppService, UserAppService>();
            container.Register<IUserService, UserService>();

            //container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

    }
}
