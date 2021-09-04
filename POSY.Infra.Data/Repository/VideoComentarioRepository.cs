using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class VideoComentarioRepository : IVideoComentarioRepository
    {
        private readonly IDatabaseContext _db;

        public VideoComentarioRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(VideoComentario comentario)
        {
            _db.VideoComentarios.Add(comentario);            
        }

        public void Remove(VideoComentario comentario)
        {
            _db.Entry(comentario).State = EntityState.Modified;            
        }

        public VideoComentario Get(Guid comentarioId)
        {
            return _db.VideoComentarios.Where(x => x.VideoComentarioId == comentarioId).FirstOrDefault();
        }

        public IEnumerable<VideoComentario> Get(Guid videoId, int paginaAtual, int itensPagina)
        {
            if (itensPagina == 0)
                return _db.VideoComentarios
                        .Where(x => x.VideoId == videoId && x.Der == null)
                        .OrderByDescending(x => x.DataComentario)
                        .Include("Usuario")
                        .Include("Usuario.Perfil")
                        .ToList();
            else
                return _db.VideoComentarios
                        .Where(x => x.VideoId == videoId && x.Der == null)
                        .OrderByDescending(x => x.DataComentario)
                        .Skip((paginaAtual - 1) * itensPagina)
                        .Take(itensPagina)
                        .Include("Usuario")
                        .Include("Usuario.Perfil")
                        .ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
