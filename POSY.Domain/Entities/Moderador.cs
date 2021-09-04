using POSY.Infra.CrossCutting.Common;
using System;

namespace POSY.Domain.Entities
{
    public class Moderador
    {
        public Guid ComunidadeId { get; private set; }
        public Guid UsuarioModeradorId { get; private set; }
        public Guid UsuarioOperacaoId { get; private set; }
        public DateTime DataOperacao { get; private set; }
        public Guid? Uer { get; private set; }
        public DateTime? Der { get; private set; }

        public virtual Comunidade Comunidade { get; set; }
        public virtual Usuario UsuarioModerador { get; set; }
        public virtual Usuario UsuarioOperacao { get; set; }

        protected Moderador() { }

        public Moderador(Guid comunidadeId, Guid usuarioModeradorId, Guid usuarioOperacaoId)
        {
            ComunidadeId = comunidadeId;
            UsuarioModeradorId = usuarioModeradorId;
            UsuarioOperacaoId = usuarioOperacaoId;
            DataOperacao = ConfigurationBase.DataAtual;
        }

        public void Delete(Guid usuarioIdExclusao)
        {
            Uer = usuarioIdExclusao;
            Der = ConfigurationBase.DataAtual;
        }
    }
}
