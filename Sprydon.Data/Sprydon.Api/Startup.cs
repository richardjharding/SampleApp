using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using IdentityServer3.Core.Configuration;
using System.Security.Cryptography.X509Certificates;
using IdentityServer3.Core.Models;
using IdentityServer3.AccessTokenValidation;

[assembly: OwinStartup(typeof(Sprydon.Api.Startup))]

namespace Sprydon.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "http://localhost:59714/identity",
                RequiredScopes = new[] { "sprydonApi" }
            });

        }

        
    }
}
