using System;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System.Data.Entity;

namespace POSY.Infra.Data.Repository
{
    public class PostOcultoRepository : IPostOcultoRepository
    {
        private readonly IDatabaseContext _db;

        public PostOcultoRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(PostOculto post)
        {
            _db.PostsOculto.Add(post);            
        }

        public void Ocultar(PostOculto post)
        {
            post.SetOcultar();

            _db.Entry(post).State = EntityState.Modified;            
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
