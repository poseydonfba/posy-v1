using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class CategoriaConfig : EntityTypeConfiguration<Categoria>
    {
        public CategoriaConfig()
        {
            ToTable("Categoria");

            HasKey(x => x.CategoriaId);

            Property(x => x.Nome).IsRequired();
        }
    }
}
