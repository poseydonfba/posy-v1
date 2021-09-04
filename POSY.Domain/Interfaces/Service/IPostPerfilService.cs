using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IPostPerfilService : IDisposable
    {
        PostPerfil Postar(Guid usuarioId, string descricaoPost);
        void ExcluirPost(Guid postId);
        PostPerfil ObterPost(Guid postId);
        IEnumerable<PostPerfil> ObterPosts(Guid usuarioId, int paginaAtual, int itensPagina);
    }
}
