using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Boma.Rs.Api.Models;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Identity.Manager;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;

namespace Boma.Rs.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                context.Request.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                var userManager = new SexMoveIdentityStore();

                var identityUser = userManager.GetIdentityUserByUserNameAndPassword(context.UserName, context.Password);


                if (identityUser == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                var user = ToApplicationUser(identityUser);
                var claimsIdentity = CreateClaimIdentity(user, OAuthDefaults.AuthenticationType);
                var properties = CreateProperties(user.UserName);
                var ticket = new AuthenticationTicket(claimsIdentity, properties);
                context.Validated(ticket);
                
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string id, secret;

            if (!context.TryGetBasicCredentials(out id, out secret))
                context.TryGetFormCredentials(out id, out secret);

            Guid clientId;
            Guid.TryParse(id, out clientId);

            context.OwinContext.Set("as:client_id", id);
            context.OwinContext.Set("as:clientAllowedOrigin", true);
            context.Validated();
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

        public static ClaimsIdentity CreateClaimIdentity(ApplicationUser user, string authenticationType)
        {
            var identity = new ClaimsIdentity(authenticationType);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("sexMove:id",user.Id.ToString()),
                new Claim("sexMove:email",user.Email),
                new Claim("sexMove:userName", user.UserName)
            };

            identity.AddClaims(claims);

            return identity;
        }

        public ApplicationUser ToApplicationUser(IdentityUser user)
        {
            return new ApplicationUser()
            {
                Id = user.Id,
                Email = user.Email,
                AccessFailedCount = user.AccessFailedCount,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEndDateUtc = user.LockoutEndDateUtc,
                PasswordHash = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                SecurityStamp = user.SecurityStamp,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName
            };

           
        }

    }
}