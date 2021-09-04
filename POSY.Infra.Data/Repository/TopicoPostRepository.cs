using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class TopicoPostRepository : ITopicoPostRepository
    {
        private readonly IDatabaseContext _db;

        public TopicoPostRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(TopicoPost post)
        {
            _db.TopicosPosts.Add(post);            
        }

        public void Remove(TopicoPost post)
        {
            _db.Entry(post).State = EntityState.Modified;            
        }

        public void RemovePermanente(TopicoPost post)
        {
            _db.Entry(post).State = EntityState.Modified;            
        }

        public TopicoPost Get(Guid postId)
        {
            return _db.TopicosPosts.Where(x => x.TopicoPostId == postId).FirstOrDefault();
        }

        public IEnumerable<TopicoPost> GetPosts(Guid topicoId, int paginaAtual, int itensPagina, out int totalRecords, out TopicoPost ultimoPost)
        {
            totalRecords = _db.TopicosPosts.Where(x => x.TopicoId == topicoId && x.Der == null).Count();

            ultimoPost = _db.TopicosPosts.Where(x => x.TopicoId == topicoId && x.Der == null).OrderByDescending(x => x.DataPost).FirstOrDefault();

            var posts = _db.TopicosPosts
                            .Include("Usuario.Perfil")
                            .Where(x => x.TopicoId == topicoId && x.Der == null)
                            .OrderBy(x => x.DataPost)
                            .Skip((paginaAtual - 1) * itensPagina)
                            .Take(itensPagina)
                            .ToList();

            return posts;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
