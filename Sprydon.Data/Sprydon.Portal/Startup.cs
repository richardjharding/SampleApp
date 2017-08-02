[assembly: Microsoft.Owin.OwinStartup(typeof(Sprydon.Portal.Startup))]

namespace Sprydon.Portal
{
    using System.Web.Mvc;
    using NWebsec.Owin;
    using Owin;
    using Microsoft.Owin.Security.OpenIdConnect;
    using System;
    using System.Security.Cryptography.X509Certificates;
    using IdentityServer3.Core.Configuration;
    using IdentityServer3.Core.Models;
    using System.Security.Claims;
    using Microsoft.Owin.Security;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Web.Helpers;
    using IdentityModel.Client;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Strict-Transport-Security - Adds the Strict-Transport-Security HTTP header to responses.
            //      This HTTP header is only relevant if you are using TLS. It ensures that content is loaded over 
            //      HTTPS and refuses to connect in case of certificate errors and warnings. NWebSec currently does 
            //      not support an MVC filter that can be applied globally. Instead we can use Owin (Using the 
            //      added NWebSec.Owin NuGet package) to apply it.
            //      Note: Including subdomains and a minimum maxage of 18 weeks is required for preloading.
            //      Note: You can view preloaded HSTS domains in Chrome here: chrome://net-internals/#hsts
            //      https://developer.mozilla.org/en-US/docs/Web/Security/HTTP_strict_transport_security
            //      http://www.troyhunt.com/2015/06/understanding-http-strict-transport.html
            // app.UseHsts(options => options.MaxAge(days: 18 * 7).IncludeSubdomains().Preload());

            // Public-Key-Pins - Adds the Public-Key-Pins HTTP header to responses.
            //      This HTTP header is only relevant if you are using TLS. It stops man-in-the-middle attacks by 
            //      telling browsers exactly which TLS certificate you expect.
            //      Note: The current specification requires including a second pin for a backup key which isn't yet 
            //      used in production. This allows for changing the server's public key without breaking accessibility 
            //      for clients that have already noted the pins. This is important for example when the former key 
            //      gets compromised. 
            //      Note: You can use the ReportUri option to provide browsers a URL to post JSON violations of the 
            //      HPKP policy. Note that the report URI must not be this site as a violation would mean that the site
            //      is blocked. You must use a separate domain using HTTPS to report to. Consider using this service:
            //      https://report-uri.io/ for this purpose.
            //      Note: You can change UseHpkp to UseHpkpReportOnly to stop browsers blocking anything but continue
            //      reporting any violations.
            //      See https://developer.mozilla.org/en-US/docs/Web/Security/Public_Key_Pinning
            //      and https://scotthelme.co.uk/hpkp-http-public-key-pinning/
            // app.UseHpkp(options => options
            //     .Sha256Pins(
            //         "Base64 encoded SHA-256 hash of your first certificate e.g. cUPcTAZWKaASuYWhhneDttWpY3oBAkE3h2+soZS7sWs=",
            //         "Base64 encoded SHA-256 hash of your second backup certificate e.g. M8HztCzM3elUxkcjR2S5P4hhyBNf6lHkmjAHKhpGPWE=")
            //     .MaxAge(days: 18 * 7)
            //     .IncludeSubdomains());

            // Content-Security-Policy:upgrade-insecure-requests - Adds the 'upgrade-insecure-requests' directive to
            //      the Content-Security-Policy HTTP header. This is only relevant if you are using HTTPS. Any objects
            //      on the page using HTTP is automatically upgraded to HTTPS.
            //      See https://scotthelme.co.uk/migrating-from-http-to-https-ease-the-pain-with-csp-and-hsts/
            //      and http://www.w3.org/TR/upgrade-insecure-requests/
            // app.UseCsp(x => x.UpgradeInsecureRequests());

            app.Map("/identity", idsvrApp =>
            {
                idsvrApp.UseIdentityServer(new IdentityServerOptions
                {
                    RequireSsl = false,
                    SiteName = "Sprydon API Identity Server",
                    SigningCertificate = LoadCertificate(),
                    Factory = new IdentityServerServiceFactory()
                    .UseInMemoryUsers(users.Users.Get())
                    .UseInMemoryClients(clients.Clients.Get())
                    .UseInMemoryScopes(scopes.Scopes.Get())

                });
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "http://localhost:59714/identity",
                ClientId = "sprydonportal",
                RedirectUri = "http://localhost:59714/",
                ResponseType = "id_token",
                Scope = "openid profile roles",
                SignInAsAuthenticationType = "Cookies",
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated =  n =>
                    {
                        var id = n.AuthenticationTicket.Identity;

                        // we want to keep first name, last name, subject and roles
                        var givenName = id.FindFirst(IdentityServer3.Core.Constants.ClaimTypes.GivenName);
                        var familyName = id.FindFirst(IdentityServer3.Core.Constants.ClaimTypes.FamilyName);
                        var sub = id.FindFirst(IdentityServer3.Core.Constants.ClaimTypes.Subject);
                        var roles = id.FindAll(IdentityServer3.Core.Constants.ClaimTypes.Role);

                        // create new identity and set name and role claim type
                        var nid = new ClaimsIdentity(
                            id.AuthenticationType,
                            IdentityServer3.Core.Constants.ClaimTypes.GivenName,
                            IdentityServer3.Core.Constants.ClaimTypes.Role);



                        nid.AddClaim(givenName);
                        nid.AddClaim(familyName);
                        nid.AddClaim(sub);
                        nid.AddClaims(roles);

                        // add some other app specific claim
                        nid.AddClaim(new Claim("app_specific", "some data"));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            nid,
                            n.AuthenticationTicket.Properties);

                        return Task.FromResult(0);
                    }
                }
            });

            ConfigureContainer(app);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(string.Format(@"{0}\bin\certificate\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }
    }
}
