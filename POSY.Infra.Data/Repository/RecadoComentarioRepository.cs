using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class RecadoComentarioRepository : IRecadoComentarioRepository
    {
        private readonly IDatabaseContext _db;

        public RecadoComentarioRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(RecadoComentario comentario)
        {
            _db.RecadoComentarios.Add(comentario);            
        }

        public void Remove(RecadoComentario comentario)
        {
            _db.Entry(comentario).State = EntityState.Modified;            
        }

        public RecadoComentario Get(Guid comentarioId)
        {
            return _db.RecadoComentarios.Where(x => x.RecadoComentarioId == comentarioId).FirstOrDefault();
        }

        public IEnumerable<RecadoComentario> Get(Guid recadoId, int paginaAtual, int itensPagina)
        {
            if (itensPagina == 0)
                return _db.RecadoComentarios
                        .Where(x => x.RecadoId == recadoId && x.Der == null)
                        .OrderByDescending(x => x.DataComentario)
                        .Include("Usuario")
                        .Include("Usuario.Perfil")
                        .ToList();
            else
                return _db.RecadoComentarios
                        .Where(x => x.RecadoId == recadoId && x.Der == null)
                        .OrderByDescending(x => x.DataComentario)
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
