using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IAmizadeService : IDisposable
    {
        void AdicionarSolicitacaoAmizade(Guid usuarioIdSolicitante, Guid usuarioIdSolicitado);
        void ExcluirAmigo(Guid usuarioIdExclusao, Guid usuarioIdParaExcluir);
        void AceitarSolicitacaoAmizade(Guid usuarioId, Guid usuarioIdAceitar);
        void RecusarSolicitacaoAmizade(Guid usuarioId, Guid usuarioIdRecusar);
        Amizade Obter(Guid usuarioId1, Guid usuarioId2);
        IEnumerable<Amizade> SolicitacoesRecebidasPendentes(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
        IEnumerable<Usuario> ObterAmigos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
