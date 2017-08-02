using Sprydon.Data.Entities;
using Sprydon.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprydon.Data.Context
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }

        public Context(): base("SprydonTalent")
        {            
            Database.SetInitializer(new Initializer());            
        }

        public Context(DbConnection connection) { }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMapping());
        }        
    }

    
}
