using POSY.Domain.Interfaces.Service;
using System;
using POSY.Domain.Entities;
using System.Collections.Generic;
using POSY.Domain.Interfaces.Repository;

namespace POSY.App.Service
{
    public class PostPerfilService : IPostPerfilService
    {
        private IPostPerfilRepository _repository;

        public PostPerfilService(IPostPerfilRepository repository)
        {
            _repository = repository;
        }

        public PostPerfil Postar(Guid usuarioId, string descricaoPost)
        {
            var post = new PostPerfil(usuarioId, descricaoPost);
            _repository.Insert(post);

            return post;
        }

        public void ExcluirPost(Guid postId)
        {
            var post = _repository.Get(postId);
            _repository.Remove(post);
        }

        public PostPerfil ObterPost(Guid postId)
        {
            return _repository.Get(postId);
        }

        public IEnumerable<PostPerfil> ObterPosts(Guid usuarioId, int paginaAtual, int itensPagina)
        {
            return _repository.Get(usuarioId, paginaAtual, itensPagina);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
