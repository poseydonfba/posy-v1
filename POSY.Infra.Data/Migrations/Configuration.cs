using POSY.Infra.Data.Context;
using POSY.Infra.Data.Interfaces;
using System.Data.Entity.Migrations;

namespace POSY.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DatabaseContext context)
        {

        }
    }
}

//  Add-Migration Initial -IgnoreChanges