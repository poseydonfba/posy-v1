using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class PostPerfilBloqueadoRepository : IPostPerfilBloqueadoRepository
    {
        private readonly IDatabaseContext _db;

        public PostPerfilBloqueadoRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(PostPerfilBloqueado perfilBloqueado)
        {
            _db.PostsPerfilBloqueado.Add(perfilBloqueado);            
        }

        public PostPerfilBloqueado Get(Guid usuarioId, Guid usuarioIdBloqueado)
        {
            return _db.PostsPerfilBloqueado.Where(x => x.UsuarioId == usuarioId && x.UsuarioIdBloqueado == usuarioIdBloqueado).FirstOrDefault();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
