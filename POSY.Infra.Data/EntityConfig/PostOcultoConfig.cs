using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class PostOcultoConfig : EntityTypeConfiguration<PostOculto>
    {
        public PostOcultoConfig()
        {
            ToTable("PostOculto");

            HasKey(k => new { k.UsuarioId, k.PostPerfilId, k.Data });

            HasRequired(x => x.Usuario);
            HasRequired(x => x.PostPerfil);
        }
    }
}
