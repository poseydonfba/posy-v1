using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class PostPerfilConfig : EntityTypeConfiguration<PostPerfil>
    {
        public PostPerfilConfig()
        {
            ToTable("PostPerfil");

            HasKey(x => x.PostPerfilId);

            Property(x => x.DescricaoPost).IsRequired();

            Ignore(x => x.PostHtml);
            Ignore(x => x.IsOculto);

            HasRequired(x => x.Usuario);
        }
    }
}
