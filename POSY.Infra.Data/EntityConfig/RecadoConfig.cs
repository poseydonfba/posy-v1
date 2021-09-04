using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class RecadoConfig : EntityTypeConfiguration<Recado>
    {
        public RecadoConfig()
        {
            ToTable("Recado");

            HasKey(x => x.RecadoId);

            Property(x => x.DescricaoRecado).IsRequired();

            Ignore(x => x.RecadoHtml);
            
            HasRequired(a => a.EnviadoPor).WithMany().HasForeignKey(c => c.EnviadoPorId);
            HasRequired(a => a.EnviadoPara).WithMany().HasForeignKey(c => c.EnviadoParaId);
        }
    }
}
