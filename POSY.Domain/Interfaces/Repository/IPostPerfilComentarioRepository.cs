using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IPostPerfilComentarioRepository : IDisposable
    {
        void Insert(PostPerfilComentario comentario);
        void Remove(PostPerfilComentario comentario);
        PostPerfilComentario Get(Guid comentarioId);
        IEnumerable<PostPerfilComentario> Get(Guid postId, int paginaAtual, int itensPagina);
    }
}
