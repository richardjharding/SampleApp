using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprydon.Data.Context
{
    internal class Initializer: MigrateDatabaseToLatestVersion<Context, Migrations.Configuration>
    {

    }
}
