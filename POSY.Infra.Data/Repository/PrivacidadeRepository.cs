using POSY.Domain.Entities;
using POSY.Domain.Interfaces.Repository;
using POSY.Infra.Data.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class PrivacidadeRepository : IPrivacidadeRepository
    {
        private IDatabaseContext _db;

        public PrivacidadeRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(Privacidade privacidade)
        {
            _db.Privacidades.Add(privacidade);
        }

        public void Update(Privacidade privacidade)
        {
            _db.Entry(privacidade).State = EntityState.Modified;
        }

        public Privacidade GetByUsuario(Guid usuarioId)
        {
            return _db.Privacidades.FirstOrDefault(x => x.UsuarioId == usuarioId);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
