using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sprydon.Portal.scopes
{
    public class Scopes
    {
        public static List<Scope> Get()
        {
            var scopes = new List<Scope>
        {
            new Scope
            {
                Enabled = true,
                Name = "roles",
                Type = ScopeType.Identity,
                Claims = new List<ScopeClaim>
                {
                    new ScopeClaim("role")
                }
            },
            new Scope
            {
                Enabled = true,
                DisplayName = "Sprydon API",
                Name = "sprydonApi",
                Description = "Access to a sample API",
                Type = ScopeType.Resource,
                Claims = new List<ScopeClaim>
                {
                    new ScopeClaim
                    {
                        Name = "ReadAPI",
                        AlwaysIncludeInIdToken = true
                    }
                }
                
            }
        };

            scopes.AddRange(StandardScopes.All);

            return scopes;
        }
    }
}