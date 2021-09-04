using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class TopicoRepository : ITopicoRepository
    {
        private readonly IDatabaseContext _db;

        public TopicoRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(Topico topico)
        {
            _db.Topicos.Add(topico);            
        }

        public void Update(Topico topico)
        {
            _db.Entry(topico).State = EntityState.Modified;            
        }

        public void Remove(Topico topico)
        {
            _db.Entry(topico).State = EntityState.Modified;            
        }

        public void RemovePermanente(Topico topico)
        {
            _db.Entry(topico).State = EntityState.Modified;            
        }

        public Topico Get(Guid topicoId)
        {
            return _db.Topicos.Where(x => x.TopicoId == topicoId).FirstOrDefault();
        }

        public IEnumerable<Topico> GetTopicos(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            totalRecords = _db.Topicos.Where(x => x.ComunidadeId == comunidadeId && x.Der == null).Count();

            var topicos = _db.Topicos
                            .Include("Usuario.Perfil")
                            .Where(x => x.ComunidadeId == comunidadeId && x.Der == null)
                            .OrderByDescending(x => x.DataTopico)
                            .Skip((paginaAtual - 1) * itensPagina)
                            .Take(itensPagina)
                            .ToList();

            return topicos;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
