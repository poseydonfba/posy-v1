using POSY.Domain.Enums;
using POSY.Infra.CrossCutting.Common;
using POSY.Infra.CrossCutting.Common.Resources;
using POSY.Infra.CrossCutting.Common.Validations;
using System;

namespace POSY.Domain.Entities
{
    public class PostOculto
    {
        public Guid UsuarioId { get; private set; }
        public Guid PostPerfilId { get; private set; }
        public DateTime Data { get; private set; }
        public StatusPostOculto StatusPost { get; private set; }

        public virtual Usuario Usuario { get; set; }
        public virtual PostPerfil PostPerfil { get; set; }

        protected PostOculto()
        {
            Data = ConfigurationBase.DataAtual;
        }

        public PostOculto(Guid usuarioId, Guid postPerfilId, StatusPostOculto statusPost)
        {
            UsuarioId = usuarioId;
            PostPerfilId = postPerfilId;
            StatusPost = statusPost;
            Data = ConfigurationBase.DataAtual;

            Validate();
        }

        public void SetOcultar()
        {
            StatusPost = StatusPostOculto.Oculto;
        }

        public void Validate()
        {
            Valid.AssertArgumentNotNull(UsuarioId, Errors.UsuarioInvalido);
            Valid.AssertArgumentNotNull(PostPerfilId, Errors.PostInvalido);
        }
    }
}
