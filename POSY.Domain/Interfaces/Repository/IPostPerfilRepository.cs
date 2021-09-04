using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IPostPerfilRepository : IDisposable
    {
        void Insert(PostPerfil post);
        void Update(PostPerfil post);
        void Remove(PostPerfil post);
        PostPerfil Get(Guid postId);
        IEnumerable<PostPerfil> Get(Guid usuarioId, int paginaAtual, int itensPagina);
    }
}
