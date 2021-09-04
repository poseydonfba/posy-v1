using POSY.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace POSY.Infra.Data.EntityConfig
{
    public class MembroConfig : EntityTypeConfiguration<Membro>
    {
        public MembroConfig()
        {
            ToTable("Membro");

            HasKey(k => new { k.ComunidadeId, k.UsuarioMembroId, k.DataSolicitacao });

            HasRequired(x => x.Comunidade);
            HasRequired(x => x.UsuarioMembro).WithMany().HasForeignKey(x => x.UsuarioMembroId);
            HasOptional(x => x.UsuarioResposta).WithMany().HasForeignKey(x => x.UsuarioRespostaId);
        }
    }
}
