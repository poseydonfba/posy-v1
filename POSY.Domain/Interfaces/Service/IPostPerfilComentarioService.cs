using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IPostPerfilComentarioService : IDisposable
    {
        PostPerfilComentario Comentar(Guid postId, Guid usuarioId, string comentario);
        void ExcluirComentario(Guid comentarioId, Guid usuarioIdExclusao);
        PostPerfilComentario ObterComentario(Guid comentarioId);
        IEnumerable<PostPerfilComentario> ObterComentarios(Guid postId, int paginaAtual, int itensPagina);
    }
}
