using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IVideoComentarioService : IDisposable
    {
        VideoComentario Comentar(Guid videoId, Guid usuarioId, string comentario);
        void ExcluirComentario(Guid comentarioId, Guid usuarioIdExclusao);
        VideoComentario ObterComentario(Guid comentarioId);
        IEnumerable<VideoComentario> ObterComentarios(Guid videoId, int paginaAtual, int itensPagina);
    }
}
