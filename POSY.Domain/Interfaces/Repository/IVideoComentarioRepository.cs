using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IVideoComentarioRepository : IDisposable
    {
        void Insert(VideoComentario comentario);
        void Remove(VideoComentario comentario);
        VideoComentario Get(Guid comentarioId);
        IEnumerable<VideoComentario> Get(Guid videoId, int paginaAtual, int itensPagina);
    }
}
