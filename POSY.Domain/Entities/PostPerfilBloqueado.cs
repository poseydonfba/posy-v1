using POSY.Infra.CrossCutting.Common;
using POSY.Infra.CrossCutting.Common.Resources;
using POSY.Infra.CrossCutting.Common.Validations;
using System;

namespace POSY.Domain.Entities
{
    public class PostPerfilBloqueado
    {
        public Guid UsuarioId { get; private set; }
        public Guid UsuarioIdBloqueado { get; private set; }
        public DateTime DataBloqueio { get; private set; }
        public Guid? Uer { get; private set; }
        public DateTime? Der { get; private set; }

        public virtual Usuario Usuario { get; set; }

        protected PostPerfilBloqueado()
        {
            DataBloqueio = ConfigurationBase.DataAtual;
        }

        public PostPerfilBloqueado(Guid usuarioId, Guid usuarioIdBloqueado)
        {
            UsuarioId = usuarioId;
            UsuarioIdBloqueado = usuarioIdBloqueado;

            DataBloqueio = ConfigurationBase.DataAtual;

            Validate();
        }

        public void Validate()
        {
            Valid.AssertArgumentNotNull(UsuarioId, Errors.UsuarioInvalido);
            Valid.AssertArgumentNotNull(UsuarioIdBloqueado, Errors.UsuarioInvalido);
        }
    }
}
