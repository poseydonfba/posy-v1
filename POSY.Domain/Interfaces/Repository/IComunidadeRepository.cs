using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IComunidadeRepository : IDisposable
    {
        void Insert(Comunidade comunidade);
        void Update(Comunidade comunidade);
        void Remove(Comunidade comunidade);
        Comunidade Get(Guid comunidadeId);
        Comunidade Get(string alias);
        IEnumerable<Comunidade> Get(int paginaAtual, int itensPagina, out int totalRecords);
        TopicoPost GetUltimoPost(Guid comunidadeId);
    }
}
