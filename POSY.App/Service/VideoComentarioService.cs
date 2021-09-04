using System;
using System.Collections.Generic;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using POSY.Domain.Interfaces.Repository;
using POSY.Infra.CrossCutting.Common.Resources;

namespace POSY.App.Service
{
    public class VideoComentarioService : IVideoComentarioService
    {
        private IVideoComentarioRepository _repository;

        public VideoComentarioService(IVideoComentarioRepository repository)
        {
            _repository = repository;
        }

        public VideoComentario Comentar(Guid videoId, Guid usuarioId, string comentario)
        {
            var coment = new VideoComentario(videoId, usuarioId, comentario);

            _repository.Insert(coment);

            return coment;
        }

        public void ExcluirComentario(Guid comentarioId, Guid usuarioIdExclusao)
        {
            var coment = ObterComentario(comentarioId);
            coment.Delete(usuarioIdExclusao);

            _repository.Remove(coment);
        }

        public VideoComentario ObterComentario(Guid comentarioId)
        {
            var coment = _repository.Get(comentarioId);

            if (coment == null)
                throw new Exception(Errors.ComentarioNaoEncontrado);

            return coment;
        }

        public IEnumerable<VideoComentario> ObterComentarios(Guid videoId, int paginaAtual, int itensPagina)
        {
            return _repository.Get(videoId, paginaAtual, itensPagina);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
