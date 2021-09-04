using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace POSY.Infra.Data.Repository
{
    public class ComunidadeRepository : IComunidadeRepository
    {
        private readonly IDatabaseContext _db;

        public ComunidadeRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(Comunidade comunidade)
        {
            _db.Comunidades.Add(comunidade);            
        }

        public void Update(Comunidade comunidade)
        {
            _db.Entry(comunidade).State = EntityState.Modified;            
        }

        public void Remove(Comunidade comunidade)
        {
            _db.Entry(comunidade).State = EntityState.Modified;            
        }

        public Comunidade Get(Guid comunidadeId)
        {
            return _db.Comunidades
                .Include("Categoria")
                .Include("Usuario")
                .Include("Usuario.Perfil")
                .Where(x => x.ComunidadeId == comunidadeId)
                .FirstOrDefault();
        }

        public Comunidade Get(string alias)
        {
            return _db.Comunidades
                .Include("Categoria")
                .Include("Usuario")
                .Include("Usuario.Perfil")
                .Where(x => x.Alias.ToLower() == alias.ToLower())
                .FirstOrDefault();
        }

        public IEnumerable<Comunidade> Get(int paginaAtual, int itensPagina, out int totalRecords)
        {
            totalRecords = _db.Comunidades.Count();

            return _db.Comunidades
                .Include("Usuario")
                .Include("Usuario.Perfil")
                .OrderByDescending(x => x.Dir)
                .Skip((paginaAtual - 1) * itensPagina)
                .Take(itensPagina)
                .ToList();
        }

        public TopicoPost GetUltimoPost(Guid comunidadeId)
        {
            var query = (from c in _db.Topicos
                         from d in _db.TopicosPosts
                             .Where(m => m.TopicoId == c.TopicoId).DefaultIfEmpty()
                         where c.ComunidadeId == comunidadeId
                         && c.Der == null
                         select d)
                    .OrderByDescending(x => x.DataPost)
                    .FirstOrDefault(); 

            return query;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
