using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IModeradorRepository : IDisposable
    {
        void Insert(Moderador moderador);
        void Remove(Moderador moderador);
        Moderador Get(Guid comunidadeId, Guid usuarioId);
        IEnumerable<Moderador> GetModeradores(Guid comunidadeId);
        IEnumerable<Moderador> GetModeradores(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords);
        IEnumerable<Moderador> GetComunidades(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
