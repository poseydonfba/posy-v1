using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IRecadoComentarioRepository : IDisposable
    {
        void Insert(RecadoComentario comentario);
        void Remove(RecadoComentario comentario);
        RecadoComentario Get(Guid comentarioId);
        IEnumerable<RecadoComentario> Get(Guid recadoId, int paginaAtual, int itensPagina);
    }
}
