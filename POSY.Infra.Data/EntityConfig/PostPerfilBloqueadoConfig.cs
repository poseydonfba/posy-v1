using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class PostPerfilBloqueadoConfig : EntityTypeConfiguration<PostPerfilBloqueado>
    {
        public PostPerfilBloqueadoConfig()
        {
            ToTable("PostPerfilBloqueado");

            HasKey(f => new { f.UsuarioId, f.UsuarioIdBloqueado });

            HasRequired(x => x.Usuario);
        }
    }
}
