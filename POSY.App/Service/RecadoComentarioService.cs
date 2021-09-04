using POSY.Infra.CrossCutting.Common.Resources;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.App.Service
{
    public class RecadoComentarioService : IRecadoComentarioService
    {
        private IRecadoComentarioRepository _repository;

        public RecadoComentarioService(IRecadoComentarioRepository repository)
        {
            _repository = repository;
        }

        public RecadoComentario Comentar(Guid recadoId, Guid usuarioId, string comentario)
        {
            var coment = new RecadoComentario(recadoId, usuarioId, comentario);
            _repository.Insert(coment);

            return coment;
        }

        public void ExcluirComentario(Guid comentarioId, Guid usuarioIdExclusao)
        {
            var comentario = ObterComentario(comentarioId);
            comentario.Delete(usuarioIdExclusao);

            _repository.Remove(comentario);
        }

        public RecadoComentario ObterComentario(Guid comentarioId)
        {
            var coment = _repository.Get(comentarioId);

            if (coment == null)
                throw new Exception(Errors.ComentarioNaoEncontrado);

            return coment;
        }

        public IEnumerable<RecadoComentario> ObterComentarios(Guid recadoId, int paginaAtual, int itensPagina)
        {
            return _repository.Get(recadoId, paginaAtual, itensPagina);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
