using Sprydon.Data.Context;
using Sprydon.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;

namespace Sprydon.Api.Controllers
{
    [Authorize]
    public class UsersController : ODataController
    {
        private Context context = new Context();

        private bool UserExists(int key)
        {
            return context.Users.Any(u => u.Id == key);
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }

        [EnableQuery]
        public IQueryable<User> Get()
        {
            return context.Users;
        }

        public SingleResult<User> Get([FromODataUri] int key)
        {
            IQueryable<User> result = context.Users.Where(u => u.Id == key);
            return SingleResult.Create(result);
        }
    }
}
