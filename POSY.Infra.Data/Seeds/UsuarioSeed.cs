using POSY.Domain.Entities;
using POSY.Infra.Data.Context;
using System.Data.Entity.Migrations;

namespace POSY.Infra.Data.Seeds
{
    public class UsuarioSeed
    {
        public static void Seed(DatabaseContext context)
        {
            //context.Usuarios.AddOrUpdate(x => x.UsuarioId,
            //                                  new Usuario("adminMaster", new Cpf("40914294830"), new Email("angeloocana@gmail.com"), "testeteste", "testeteste", endereco));
        }
    }
}
