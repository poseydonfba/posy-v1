namespace POSY.Infra.CrossCutting.Identity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<POSY.Infra.CrossCutting.Identity.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(POSY.Infra.CrossCutting.Identity.Context.ApplicationDbContext context)
        {
            //string[] roles = new string[] { "Owner", "Administrador", "Colaborador", "Visitante" };

            //Task.Run(async () => { await SeedAsync(context); }).Wait();
        }
    }
}
