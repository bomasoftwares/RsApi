﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Boma.RedeSocial.Api.Models;
using Boma.RedeSocial.Infrastructure.Data;
using Boma.RedeSocial.Infrastructure.Data.EntityFramework.Repositories.Users;
using Boma.RedeSocial.Domain.Users.Entities;
using Boma.RedeSocial.Crosscut.Services;

namespace Boma.RedeSocial.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        public UserRepository UserRepository { get; }

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;

            UserRepository = new UserRepository(new SexMoveContext());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*"});
            
            var authenticatedUser = UserRepository.Authenticate(context.UserName, SecurityService.Encrypt(context.Password));

            if (authenticatedUser == null)
            {
                context.SetError("invalid_grant", "Usuário ou senha inválidos");
                return;
            }

            var claimsIdentity = CreateClaimIdentityByUser(context.Options.AuthenticationType, authenticatedUser);
            AuthenticationProperties properties = CreateProperties(authenticatedUser.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(claimsIdentity, properties);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
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

        public static ClaimsIdentity CreateClaimIdentityByUser(string athenticationType, User user)
        {
            var identity = new ClaimsIdentity(athenticationType);

            var claimList = new List<Claim>();

            claimList.Add(new Claim(ClaimTypes.Name, user.Email));
            claimList.Add(new Claim("sexmoveuser:id", user.Id.ToString()));

            identity.AddClaims(claimList);

            return identity;
        }
    }
}