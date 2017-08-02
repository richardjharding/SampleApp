using Effort;
using Sprydon.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class ContextTests
    {
        private readonly Context context;
        private readonly DbConnection dbConnection;

        public ContextTests()
        {
            this.dbConnection = DbConnectionFactory.CreateTransient();
            this.context = new Context(this.dbConnection);
        }
        [Fact]
        public void TestOne()
        {            
            var user = context.Users.FirstOrDefault();
            Assert.NotNull(user);
        }

        [Fact]
        public void FilterBySurname()
        {
            var poole = context.Users.Where(u => u.Surname == "Poole").SingleOrDefault();
            Assert.NotNull(poole);
        }
    }
}
