using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Sprydon.Portal.users
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "bob",
                    Password = "secret",
                    Subject = "1",
                    Claims = new[]
                {
                    new Claim(IdentityServer3.Core.Constants.ClaimTypes.GivenName, "Bob"),
                    new Claim(IdentityServer3.Core.Constants.ClaimTypes.FamilyName, "Smith"),
                    new Claim(IdentityServer3.Core.Constants.ClaimTypes.Role, "Admin")
                }

                }
            };
        }
    }
}