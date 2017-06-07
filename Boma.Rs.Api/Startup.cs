﻿using Boma.RedeSocial.AppService.Users.Interfaces;
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
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net.Http.Formatting;
using System.Web.Http;

//[assembly: OwinStartup(typeof(Boma.Rs.Api.Startup))]

namespace Boma.Rs.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = StartupHttpConfiguration.CreateHttpConfiguration(app);

            // Autenticação
            ConfigureAuth(app);

            // Cross-Domain
            app.UseCors(CorsOptions.AllowAll);


            //var container = new Container();
            //container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();


            //container.Register<DbConnection>(() => new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString), Lifestyle.Scoped);
            //container.Register<ISexMoveUnitOfWork, SexMoveUnitOfWork>(Lifestyle.Scoped);
            //container.Register<ISexMoveContext, SexMoveContext>(Lifestyle.Scoped);

            //container.Register<ISexMoveIdentityStore, SexMoveIdentityStore>(Lifestyle.Scoped);
            //container.Register<IBomaAuditing, SexMoveAuditing>(Lifestyle.Scoped);

            //container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            //container.Register<IUserAspNetRepository, UserAspNetRepository>(Lifestyle.Scoped);
            //container.Register<IProfileRepository, ProfileRepository>(Lifestyle.Scoped);
            //container.Register<IProfilePeopleConfigurationRepository, ProfilePeopleConfigurationRepository>(Lifestyle.Scoped);

            //container.Register<IUserAppService, UserAppService>(Lifestyle.Scoped);
            //container.Register<IUserService, UserService>(Lifestyle.Scoped);

            //container.Verify();

            //app.Use(async (context, next) =>
            //{
            //    using (AsyncScopedLifestyle.BeginScope(container))
            //    {
            //        await next();
            //    }
            //});
        }

       
    }

    public static partial class StartupHttpConfiguration
    {
        public static HttpConfiguration CreateHttpConfiguration(this IAppBuilder appBuilder)
        {

            var config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };


            var formatters = config.Formatters;
            formatters.Add(new BsonMediaTypeFormatter());

            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Mapea Rotas
            config.MapHttpAttributeRoutes();

            //// adiciona os filters ao pipeline
            config.Filters.Add(new AuthorizeAttribute());


            return config;
        }
    }
}
