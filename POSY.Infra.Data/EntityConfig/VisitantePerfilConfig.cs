using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class VisitantePerfilConfig : EntityTypeConfiguration<VisitantePerfil>
    {
        public VisitantePerfilConfig()
        {
            ToTable("VisitantePerfil");

            HasKey(f => new { f.VisitanteId, f.VisitadoId, f.DataVisita });

            HasRequired(a => a.Visitante).WithMany().HasForeignKey(x => x.VisitanteId);
            HasRequired(a => a.Visitado).WithMany().HasForeignKey(x => x.VisitadoId);
        }
    }
}
