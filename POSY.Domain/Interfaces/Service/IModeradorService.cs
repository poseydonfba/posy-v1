using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IModeradorService : IDisposable
    {
        Moderador AdicionarModerador(Guid comunidadeId, Guid usuarioModeradorId, Guid usuarioOperacaoId);
        void ExcluirModerador(Guid comunidadeId, Guid usuarioId, Guid usuarioIdExclusao);
        Moderador Obter(Guid comunidadeId, Guid usuarioId);
        IEnumerable<Moderador> ObterModeradores(Guid comunidadeId);
        IEnumerable<Moderador> ObterModeradores(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords);
        IEnumerable<Moderador> ObterComunidades(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
