using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class TopicoPostConfig : EntityTypeConfiguration<TopicoPost>
    {
        public TopicoPostConfig()
        {
            ToTable("TopicoPost");

            HasKey(k => k.TopicoPostId);

            Property(x => x.Descricao).IsRequired();

            Ignore(x => x.DescricaoHtml);

            HasRequired(x => x.Topico).WithMany().HasForeignKey(x => x.TopicoId);
            HasRequired(x => x.Usuario).WithMany().HasForeignKey(x => x.UsuarioId);
        }
    }
}
