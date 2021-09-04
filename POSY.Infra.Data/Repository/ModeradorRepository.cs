using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace POSY.Infra.Data.Repository
{
    public class ModeradorRepository : IModeradorRepository
    {
        private readonly IDatabaseContext _db;

        public ModeradorRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(Moderador moderador)
        {
            _db.Moderadores.Add(moderador);            
        }

        public void Remove(Moderador moderador)
        {
            _db.Entry(moderador).State = EntityState.Modified;            
        }

        public Moderador Get(Guid comunidadeId, Guid usuarioId)
        {
            var moderador = _db.Moderadores
                    .Include("UsuarioModerador.Perfil")
                    .Include("UsuarioOperacao.Perfil")
                    .Where(x => x.ComunidadeId == comunidadeId && x.UsuarioModeradorId == usuarioId && x.Der == null)
                    .FirstOrDefault();

            return moderador;
        }

        public IEnumerable<Moderador> GetComunidades(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            totalRecords = _db.Moderadores.Where(x => x.UsuarioModeradorId == usuarioId && x.Der == null).Count();

            var moderadores = _db.Moderadores
                            .Include("Comunidade")
                            .Where(x => x.UsuarioModeradorId == usuarioId && x.Der == null)
                            .OrderByDescending(x => x.DataOperacao)
                            .Skip((paginaAtual - 1) * itensPagina)
                            .Take(itensPagina)
                            .ToList();

            return moderadores;
        }

        public IEnumerable<Moderador> GetModeradores(Guid comunidadeId)
        {
            var moderadores = _db.Moderadores
                            .Include("UsuarioModerador.Perfil")
                            .Include("UsuarioOperacao.Perfil")
                            .Where(x => x.ComunidadeId == comunidadeId && x.Der == null)
                            .OrderByDescending(x => x.DataOperacao)
                            .ToList();

            return moderadores;
        }

        public IEnumerable<Moderador> GetModeradores(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            totalRecords = _db.Moderadores.Where(x => x.ComunidadeId == comunidadeId && x.Der == null).Count();

            var moderadores = _db.Moderadores
                            .Include("UsuarioModerador.Perfil")
                            .Include("UsuarioOperacao.Perfil")
                            .Where(x => x.ComunidadeId == comunidadeId && x.Der == null)
                            .OrderByDescending(x => x.DataOperacao)
                            .Skip((paginaAtual - 1) * itensPagina)
                            .Take(itensPagina)
                            .ToList();

            return moderadores;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
