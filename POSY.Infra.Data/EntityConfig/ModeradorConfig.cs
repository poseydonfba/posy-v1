using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class ModeradorConfig : EntityTypeConfiguration<Moderador>
    {
        public ModeradorConfig()
        {
            ToTable("Moderador");

            HasKey(k => new { k.ComunidadeId, k.UsuarioModeradorId, k.DataOperacao });

            HasRequired(x => x.Comunidade);
            HasRequired(x => x.UsuarioModerador).WithMany().HasForeignKey(x => x.UsuarioModeradorId);
            HasRequired(x => x.UsuarioOperacao).WithMany().HasForeignKey(x => x.UsuarioOperacaoId);
        }
    }
}
