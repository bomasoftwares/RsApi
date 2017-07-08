using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Boma.Rs.Api.Providers;
using Boma.Rs.Api.Models;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using Boma.RedeSocial.Domain.Context.Interfaces;
using Boma.RedeSocial.Crosscut.Auditing;
using Boma.RedeSocial.Infrastructure.Data;
using Boma.RedeSocial.Infrastructure.Auditing;
using Boma.RedeSocial.Domain.Users.Interfaces;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Users;
using Boma.RedeSocial.Domain.Interfaces.Repositories;
using Boma.RedeSocial.AppService.Users.Interfaces;
using Boma.RedeSocial.AppService.Users.Services;
using Boma.Rs.Api.Context;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Profiles;
using Boma.RedeSocial.Domain.Profiles.Interfaces;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager;
using Boma.RedeSocial.Domain.Users.Services;
using SimpleInjector.Integration.WebApi;

namespace Boma.Rs.Api
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        private Container OAuthContainer { get; set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            //ConfigureOwinDI(app);

            // Configure the db context and user manager to use a single instance per request
           
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true,


            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}
