using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class PrivacidadeConfig : EntityTypeConfiguration<Privacidade>
    {
        public PrivacidadeConfig()
        {
            ToTable("Privacidade");

            HasKey(x => x.UsuarioId);

            Property(x => x.VerRecado).IsRequired();
            Property(x => x.EscreverRecado).IsRequired();
        }
    }
}
