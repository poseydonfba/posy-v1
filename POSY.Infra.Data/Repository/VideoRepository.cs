using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class VideoRepository : IVideoRepository
    {
        private readonly IDatabaseContext _db;

        public VideoRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(Video video)
        {
            _db.Videos.Add(video);            
        }

        public void Remove(Video video)
        {
            _db.Entry(video).State = EntityState.Modified;            
        }

        public Video Get(Guid videoId)
        {
            return _db.Videos.Where(x => x.VideoId == videoId).FirstOrDefault();
        }

        public IEnumerable<Video> Get(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            var videos = _db.Videos.Include("Usuario").Include("Usuario.Perfil").Where(x => x.UsuarioId == usuarioId && x.Der == null);

            totalRecords = videos.Count();

            return videos
                    .OrderByDescending(x => x.DataVideo)
                    .Skip((paginaAtual - 1) * itensPagina)
                    .Take(itensPagina)
                    .ToList();
        }
        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
