using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class VisitantePerfilRepository : IVisitantePerfilRepository
    {
        private readonly IDatabaseContext _db;

        public VisitantePerfilRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(VisitantePerfil visitante)
        {
            _db.VisitantesPerfil.Add(visitante);            
        }

        public IEnumerable<Perfil> GetVisitados(Guid usuarioId)
        {
            return _db.VisitantesPerfil
                .Include("Visitado.Perfil")
                .Where(x => x.VisitanteId == usuarioId)
                .OrderByDescending(x => x.DataVisita)
                .Select(y => y.Visitante.Perfil)
                .Distinct()
                .ToList();
        }

        public IEnumerable<Perfil> GetVisitados(Guid usuarioId, int take)
        {
            return _db.VisitantesPerfil
                .Include("Visitado.Perfil")
                .Where(x => x.VisitanteId == usuarioId)
                .OrderByDescending(x => x.DataVisita)
                .Select(y => y.Visitante.Perfil)
                .Distinct()
                .Take(take)
                .ToList();
        }

        public IEnumerable<Perfil> GetVisitantes(Guid usuarioId)
        {
            return _db.VisitantesPerfil
                .Include("Visitante.Perfil")
                .Where(x => x.VisitadoId == usuarioId)
                .OrderByDescending(x => x.DataVisita)
                .Select(y => y.Visitante.Perfil)
                .Distinct()
                .ToList();
        }

        public IEnumerable<Perfil> GetVisitantes(Guid usuarioId, int take)
        {
            return _db.VisitantesPerfil
                .Include("Visitante.Perfil")
                .Where(x => x.VisitadoId == usuarioId)
                .OrderByDescending(x => x.DataVisita)
                .Select(y => y.Visitante.Perfil)
                .Distinct()
                .Take(take)
                .ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
