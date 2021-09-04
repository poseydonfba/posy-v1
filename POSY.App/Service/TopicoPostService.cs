using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.App.Service
{
    public class TopicoPostService : ITopicoPostService
    {
        private ITopicoPostRepository _repository;

        public TopicoPostService(ITopicoPostRepository repository)
        {
            _repository = repository;
        }

        public TopicoPost SalvarPost(Guid topicoId, Guid usuarioId, string descricao)
        {
            var post = new TopicoPost(topicoId, usuarioId, descricao);

            _repository.Insert(post);

            return post;
        }

        public void ExcluirTopicoPost(Guid postId, Guid usuarioIdExclusao)
        {
            var post = Obter(postId);
            post.Delete(usuarioIdExclusao);

            _repository.Remove(post);
        }

        public void ExcluirTopicoPostPermanente(Guid postId, Guid usuarioIdExclusao)
        {
            var post = Obter(postId);
            post.DeletePermanente(usuarioIdExclusao);

            _repository.RemovePermanente(post);
        }

        public TopicoPost Obter(Guid postId)
        {
            var post = _repository.Get(postId);

            return post;
        }

        public IEnumerable<TopicoPost> ObterPosts(Guid topicoId, int paginaAtual, int itensPagina, out int totalRecords, out TopicoPost ultimoPost)
        {
            return _repository.GetPosts(topicoId, paginaAtual, itensPagina, out totalRecords, out ultimoPost);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
