using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.App.Service
{
    public class PostPerfilComentarioService : IPostPerfilComentarioService
    {
        private IPostPerfilComentarioRepository _repository;

        public PostPerfilComentarioService(IPostPerfilComentarioRepository repository)
        {
            _repository = repository;
        }

        public PostPerfilComentario Comentar(Guid postId, Guid usuarioId, string comentario)
        {
            var coment = new PostPerfilComentario(postId, usuarioId, comentario);

            _repository.Insert(coment);

            return coment;
        }

        public void ExcluirComentario(Guid comentarioId, Guid usuarioIdExclusao)
        {
            var comentario = _repository.Get(comentarioId);
            comentario.Delete(usuarioIdExclusao);

            _repository.Remove(comentario);
        }

        public PostPerfilComentario ObterComentario(Guid comentarioId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostPerfilComentario> ObterComentarios(Guid postId, int paginaAtual, int itensPagina)
        {
            return _repository.Get(postId, paginaAtual, itensPagina);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
