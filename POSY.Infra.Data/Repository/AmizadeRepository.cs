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
    public class AmizadeRepository : IAmizadeRepository
    {
        private readonly IDatabaseContext _db;

        public AmizadeRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(Amizade amizade)
        {
            _db.Amizades.Add(amizade);            
        }

        public void Update(Amizade amizade)
        {
            _db.Entry(amizade).State = EntityState.Modified;            
        }

        public void Remove(Amizade amizade)
        {
            _db.Entry(amizade).State = EntityState.Modified;            
        }

        public Amizade Get(Guid usuarioId1, Guid usuarioId2)
        {
            var amigosSolicitados = (from t in _db.Amizades
                                     where t.SolicitadoPorId == usuarioId1
                                     && t.SolicitadoParaId == usuarioId2
                                     && t.Der == null
                                     select t);

            var amizade = amigosSolicitados
                    .Include("SolicitadoPor")
                    .Include("SolicitadoPor.Perfil")
                    .Include("SolicitadoPara")
                    .Include("SolicitadoPara.Perfil")
                    .OrderByDescending(x => x.DataSolicitacao)
                    .FirstOrDefault();

            if (amizade == null)
            {
                var amigosSolicitantes = (from t in _db.Amizades
                                          where t.SolicitadoPorId == usuarioId2
                                          && t.SolicitadoParaId == usuarioId1
                                          && t.Der == null
                                          select t);

                amizade = amigosSolicitantes
                    .Include("SolicitadoPor")
                    .Include("SolicitadoPor.Perfil")
                    .Include("SolicitadoPara")
                    .Include("SolicitadoPara.Perfil")
                    .OrderByDescending(x => x.DataSolicitacao)
                    .FirstOrDefault();
            }

            return amizade;
        }

        public Amizade GetSolicitacao(Guid usuarioIdSolicitante, Guid usuarioIdSolicitado)
        {
            var amizadeSolicitacao = (from t in _db.Amizades
                                      where t.SolicitadoPorId == usuarioIdSolicitante
                                      && t.SolicitadoParaId == usuarioIdSolicitado
                                      && t.Der == null
                                      select t);

            var amizade = amizadeSolicitacao
                .OrderByDescending(x => x.DataSolicitacao)
                .FirstOrDefault();

            return amizade;
        }

        /// <summary>
        /// Retorna todas as solicitações recebidas de um usuario
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="paginaAtual"></param>
        /// <param name="itensPagina"></param>
        /// <param name="flag"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public IEnumerable<Amizade> Get(Guid usuarioId, int paginaAtual, int itensPagina, SolicitacaoAmizade flag, out int totalRecords)
        {
            if (flag == SolicitacaoAmizade.Todos)
            {
                totalRecords = _db.Amizades.Where(x => x.SolicitadoParaId == usuarioId).Count();

                var amizades = _db.Amizades
                                .Include("SolicitadoPor")
                                .Include("SolicitadoPor.Perfil")
                                .Include("SolicitadoPara")
                                .Include("SolicitadoPara.Perfil")
                                .Where(x => x.SolicitadoParaId == usuarioId)
                                .OrderByDescending(x => x.DataSolicitacao)
                                .Skip((paginaAtual - 1) * itensPagina)
                                .Take(itensPagina)
                                .ToList();

                return amizades;
            }
            else
            {
                totalRecords = _db.Amizades.Where(x => x.SolicitadoParaId == usuarioId && x.StatusSolicitacao == flag).Count();

                var amizades = _db.Amizades
                                .Include("SolicitadoPor")
                                .Include("SolicitadoPor.Perfil")
                                .Include("SolicitadoPara")
                                .Include("SolicitadoPara.Perfil")
                                .Where(x => x.SolicitadoParaId == usuarioId && x.StatusSolicitacao == flag)
                                .OrderByDescending(x => x.DataSolicitacao)
                                .Skip((paginaAtual - 1) * itensPagina)
                                .Take(itensPagina)
                                .ToList();

                return amizades;
            }
        }

        /// <summary>
        /// Retorna todos os amigos de um usuario
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="paginaAtual"></param>
        /// <param name="itensPagina"></param>
        /// <returns></returns>
        public IEnumerable<Usuario> GetAmigos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            var amigosSolicitados = (from t in _db.Amizades
                                     where t.SolicitadoPorId == usuarioId
                                     && t.StatusSolicitacao == SolicitacaoAmizade.Aprovado
                                     && t.Der == null
                                     select t.SolicitadoPara);

            var amigosSolicitantes = (from t in _db.Amizades
                                      where t.SolicitadoParaId == usuarioId
                                      && t.StatusSolicitacao == SolicitacaoAmizade.Aprovado
                                      && t.Der == null
                                      select t.SolicitadoPor);

            totalRecords = amigosSolicitados.Concat(amigosSolicitantes).Count();

            var amigos = amigosSolicitados.Concat(amigosSolicitantes)
                                        .Include("Perfil")
                                        .OrderByDescending(x => x.Dir)
                                        .Skip((paginaAtual - 1) * itensPagina)
                                        .Take(itensPagina)
                                        .ToList();

            return amigos;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
