using POSY.Infra.CrossCutting.Common.Resources;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.App.Service
{
    public class VideoService : IVideoService
    {
        private IVideoRepository _repository;

        public VideoService(IVideoRepository repository)
        {
            _repository = repository;
        }

        public Video SalvarVideo(Guid usuarioId, string url, string nomeVideo)
        {
            var video = new Video(usuarioId, url, nomeVideo);

            _repository.Insert(video);

            return video;
        }

        public void ExcluirVideo(Guid videoId)
        {
            var video = ObterVideo(videoId);
            video.Delete();

            _repository.Remove(video);
        }

        public Video ObterVideo(Guid videoId)
        {
            var video = _repository.Get(videoId);

            if (video == null)
                throw new Exception(Errors.VideoNaoEncontrado);

            return video;
        }

        public IEnumerable<Video> ObterVideos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.Get(usuarioId, paginaAtual, itensPagina, out totalRecords);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
