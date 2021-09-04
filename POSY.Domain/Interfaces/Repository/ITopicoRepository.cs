using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface ITopicoRepository : IDisposable
    {
        void Insert(Topico topico);
        void Update(Topico topico);
        void Remove(Topico topico);
        void RemovePermanente(Topico topico);
        Topico Get(Guid topicoId);
        IEnumerable<Topico> GetTopicos(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
