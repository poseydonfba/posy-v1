using POSY.Domain.Entities;
using POSY.Domain.Enums;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IAmizadeRepository : IDisposable
    {
        void Insert(Amizade amizade);
        void Update(Amizade amizade);
        void Remove(Amizade amizade);
        Amizade Get(Guid usuarioId1, Guid usuarioId2);
        Amizade GetSolicitacao(Guid usuarioIdSolicitante, Guid usuarioIdSolicitado);
        IEnumerable<Amizade> Get(Guid usuarioId, int paginaAtual, int itensPagina, SolicitacaoAmizade solicitacaoAmizade, out int totalRecords);
        IEnumerable<Usuario> GetAmigos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
