using POSY.Domain.Entities;
using POSY.Domain.Interfaces.Repository;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        //private readonly ApplicationDbContext _db;
        private readonly IDatabaseContext _db;

        //public UsuarioRepository()
        public UsuarioRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public Usuario GetById(Guid id)
        {
            return _db.Usuarios.Find(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _db.Usuarios.ToList();
        }
        public void DesativarLock(string id)
        {
            _db.Usuarios.Find(id).LockoutEnabled = false;
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}