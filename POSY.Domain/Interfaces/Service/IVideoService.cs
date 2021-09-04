using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IVideoService : IDisposable
    {
        Video SalvarVideo(Guid usuarioId, string url, string nomeVideo);
        void ExcluirVideo(Guid videoId);
        Video ObterVideo(Guid videoId);
        IEnumerable<Video> ObterVideos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
