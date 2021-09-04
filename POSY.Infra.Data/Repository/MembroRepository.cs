using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using POSY.Domain.Enums;

namespace POSY.Infra.Data.Repository
{
    public class MembroRepository : IMembroRepository
    {
        private readonly IDatabaseContext _db;

        public MembroRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(Membro membro)
        {
            _db.Membros.Add(membro);            
        }

        public void Update(Membro membro)
        {
            _db.Entry(membro).State = EntityState.Modified;            
        }

        public Membro Get(Guid comunidadeId, Guid usuarioId)
        {
            var membro = _db.Membros
                    .Include("UsuarioMembro.Perfil")
                    .Include("UsuarioResposta.Perfil")
                    .Where(x => x.ComunidadeId == comunidadeId && x.UsuarioMembroId == usuarioId && x.Der == null)
                    .OrderByDescending(x => x.DataSolicitacao)
                    .FirstOrDefault();

            return membro;
        }

        public IEnumerable<Membro> GetMembros(Guid comunidadeId, int paginaAtual, int itensPagina, StatusSolicitacaoMembroComunidade flag, out int totalRecords)
        {
            if (flag == StatusSolicitacaoMembroComunidade.Todos)
            {
                totalRecords = _db.Membros.Where(x => x.ComunidadeId == comunidadeId && x.Der == null).Count();

                var membros = _db.Membros
                                .Include("UsuarioMembro.Perfil")
                                .Include("UsuarioResposta.Perfil")
                                .Where(x => x.ComunidadeId == comunidadeId && x.Der == null)
                                .OrderByDescending(x => x.DataSolicitacao)
                                .Skip((paginaAtual - 1) * itensPagina)
                                .Take(itensPagina)
                                .ToList();

                return membros;
            }
            else
            {
                totalRecords = _db.Membros.Where(x => x.ComunidadeId == comunidadeId && x.StatusSolicitacao == flag && x.Der == null).Count();

                var membros = _db.Membros
                                .Include("UsuarioMembro.Perfil")
                                .Include("UsuarioResposta.Perfil")
                                .Where(x => x.ComunidadeId == comunidadeId && x.StatusSolicitacao == flag && x.Der == null)
                                .OrderByDescending(x => x.DataSolicitacao)
                                .Skip((paginaAtual - 1) * itensPagina)
                                .Take(itensPagina)
                                .ToList();

                return membros;
            }
        }

        public IEnumerable<Membro> GetComunidades(Guid usuarioId, int paginaAtual, int itensPagina, StatusSolicitacaoMembroComunidade flag, out int totalRecords)
        {
            if (flag == StatusSolicitacaoMembroComunidade.Todos)
            {
                totalRecords = _db.Membros.Where(x => x.UsuarioMembroId == usuarioId && x.Der == null).Count();

                var membros = _db.Membros
                                .Include("Comunidade")
                                .Include("Comunidade.Usuario.Perfil")
                                .Where(x => x.UsuarioMembroId == usuarioId && x.Der == null)
                                .OrderByDescending(x => x.DataSolicitacao)
                                .Skip((paginaAtual - 1) * itensPagina)
                                .Take(itensPagina)
                                .ToList();

                return membros;
            }
            else
            {
                totalRecords = _db.Membros.Where(x => x.UsuarioMembroId == usuarioId && x.StatusSolicitacao == flag && x.Der == null).Count();

                var membros = _db.Membros
                                .Include("Comunidade")
                                .Include("Comunidade.Usuario.Perfil")
                                .Where(x => x.UsuarioMembroId == usuarioId && x.StatusSolicitacao == flag && x.Der == null)
                                .OrderByDescending(x => x.DataSolicitacao)
                                .Skip((paginaAtual - 1) * itensPagina)
                                .Take(itensPagina)
                                .ToList();

                return membros;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
