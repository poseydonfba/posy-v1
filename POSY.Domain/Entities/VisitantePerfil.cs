using POSY.Infra.CrossCutting.Common;
using POSY.Infra.CrossCutting.Common.Resources;
using POSY.Infra.CrossCutting.Common.Validations;
using System;

namespace POSY.Domain.Entities
{
    public class VisitantePerfil
    {
        public Guid VisitanteId { get; private set; }
        public Guid VisitadoId { get; private set; }
        public DateTime DataVisita { get; private set; }

        public virtual Usuario Visitante { get; set; }
        public virtual Usuario Visitado { get; set; }

        protected VisitantePerfil()
        {

        }

        public VisitantePerfil(Guid visitanteId, Guid visitadoId)
        {
            VisitanteId = visitanteId;
            VisitadoId = visitadoId;
            DataVisita = ConfigurationBase.DataAtual;

            Validate();
        }

        public void Validate()
        {
            Valid.AssertArgumentNotNull(VisitanteId,Errors.PerfilVisitanteInvalido);
            Valid.AssertArgumentNotNull(VisitadoId, Errors.PerfilVisitadoInvalido);
            Valid.AssertArgumentNotEquals(VisitanteId, Guid.Empty, Errors.PerfilVisitanteInvalido);
            Valid.AssertArgumentNotEquals(VisitadoId, Guid.Empty, Errors.PerfilVisitadoInvalido);
            Valid.AssertArgumentNotEquals(VisitanteId, VisitadoId, Errors.PerfilVisitanteIgualPerfilVisitado);
        }
    }
}
