using POSY.Infra.CrossCutting.Common.Resources;
using POSY.Infra.CrossCutting.Common.Validations;
using System;

namespace POSY.Domain.Entities
{
    public class Privacidade
    {
        public Guid UsuarioId { get; private set; }

        public bool VerRecado { get; private set; }
        public bool EscreverRecado { get; private set; }

        public virtual Usuario Usuario { get; set; }

        protected Privacidade()
        {

        }

        public Privacidade(Guid usuarioId, bool verRecado, bool escreverRecado)
        {
            UsuarioId = usuarioId;
            VerRecado = verRecado;
            EscreverRecado = escreverRecado;

            Validate();
        }

        public void Edit(bool verRecado, bool escreverRecado)
        {
            VerRecado = verRecado;
            EscreverRecado = escreverRecado;

            Validate();
        }

        public void Validate()
        {
            Valid.AssertArgumentNotNull(VerRecado, Errors.ErroConfiguracaoPropriedade);
            Valid.AssertArgumentNotNull(EscreverRecado, Errors.ErroConfiguracaoPropriedade);
        }
    }
}
