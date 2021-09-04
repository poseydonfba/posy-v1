using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IVideoRepository : IDisposable
    {
        void Insert(Video video);
        void Remove(Video video);
        Video Get(Guid videoId);
        IEnumerable<Video> Get(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
