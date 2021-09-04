using System;
using System.Collections.Generic;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System.Linq;
using System.Data.Entity;

namespace POSY.Infra.Data.Repository
{
    public class PostPerfilComentarioRepository : IPostPerfilComentarioRepository
    {
        private readonly IDatabaseContext _db;

        public PostPerfilComentarioRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(PostPerfilComentario comentario)
        {
            _db.PostsPerfilComentario.Add(comentario);            
        }

        public void Remove(PostPerfilComentario comentario)
        {
            _db.Entry(comentario).State = EntityState.Modified;            
        }

        public PostPerfilComentario Get(Guid comentarioId)
        {
            return _db.PostsPerfilComentario.Where(x => x.PostPerfilComentarioId == comentarioId).FirstOrDefault();
        }

        public IEnumerable<PostPerfilComentario> Get(Guid postId, int paginaAtual, int itensPagina)
        {
            if (itensPagina == 0)
                return _db.PostsPerfilComentario
                        .Where(x => x.PostPerfilId == postId && x.Der == null)
                        .OrderByDescending(x => x.Data)
                        .Include("Usuario")
                        .Include("Usuario.Perfil")
                        .ToList();
            else
                return _db.PostsPerfilComentario
                        .Where(x => x.PostPerfilId == postId && x.Der == null)
                        .OrderByDescending(x => x.Data)
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
