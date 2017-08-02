using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sprydon.Portal.clients
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientName = "Sprydon Portal",
                    ClientId = "sprydonportal",
                    RequireConsent = false,
                    Enabled = true,                   
                    Flow = Flows.Implicit,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44301/",
                        "http://localhost:59714/"
                    },

                    AllowAccessToAllScopes = true
                },
                new Client
            {
                ClientName = "MVC Client (service communication)",
                ClientId = "mvc_service",
                RequireConsent = false,
                Flow = Flows.ClientCredentials,

                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = new List<string>
                {
                    "sprydonApi"
                }
            }
            };
        }
    }
}