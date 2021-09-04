using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IMembroService : IDisposable
    {
        Membro SolicitarParticipacao(Guid comunidadeId, Guid usuarioId);
        void AdicionarMembro(Guid comunidadeId, Guid usuarioMembroId);
        void AceitarMembro(Guid comunidadeId, Guid usuarioId, Guid usuarioIdResposta);
        void RecusarMembro(Guid comunidadeId, Guid usuarioId, Guid usuarioIdResposta);
        Membro Obter(Guid comunidadeId, Guid usuarioId);
        IEnumerable<Membro> ObterMembros(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords);
        IEnumerable<Membro> ObterMembrosPendentes(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords);
        IEnumerable<Membro> ObterComunidades(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
