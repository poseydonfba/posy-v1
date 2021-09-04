using POSY.Domain.Entities;
using POSY.Domain.Interfaces.Repository;
using POSY.Infra.Data.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly IDatabaseContext _db;

        public PerfilRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(Perfil perfil)
        {
            _db.Perfis.Add(perfil);            
        }

        public void Update(Perfil perfil)
        {
            _db.Entry(perfil).State = EntityState.Modified;
        }

        public Perfil GetByUsuario(Guid usuarioId)
        {
            return _db.Perfis.FirstOrDefault(x => x.UsuarioId == usuarioId);
        }

        public Perfil GetByAlias(string alias)
        {
            return _db.Perfis.FirstOrDefault(x => x.Alias == alias);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
