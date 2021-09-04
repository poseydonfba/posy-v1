using POSY.Domain.Entities;
using POSY.Domain.Enums;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IMembroRepository : IDisposable
    {
        void Insert(Membro membro);
        void Update(Membro membro);
        Membro Get(Guid comunidadeId, Guid usuarioId);
        IEnumerable<Membro> GetMembros(Guid comunidadeId, int paginaAtual, int itensPagina, StatusSolicitacaoMembroComunidade statusSolicitacaoMembroComunidade, out int totalRecords);
        IEnumerable<Membro> GetComunidades(Guid usuarioId, int paginaAtual, int itensPagina, StatusSolicitacaoMembroComunidade statusSolicitacaoMembroComunidade, out int totalRecords);
    }
}
