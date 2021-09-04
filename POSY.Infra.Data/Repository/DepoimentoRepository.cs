using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using POSY.Domain.Enums;

namespace POSY.Infra.Data.Repository
{
    public class DepoimentoRepository : IDepoimentoRepository
    {
        private readonly IDatabaseContext _db;

        public DepoimentoRepository(IDatabaseContext db)
        {
            _db = db;
        }
        public void Insert(Depoimento depoimento)
        {
            _db.Depoimentos.Add(depoimento);            
        }

        public void Update(Depoimento depoimento)
        {
            _db.Entry(depoimento).State = EntityState.Modified;            
        }

        public void Remove(Depoimento depoimento)
        {
            _db.Entry(depoimento).State = EntityState.Modified;            
        }

        public Depoimento Get(Guid depoimentoId)
        {
            return _db.Depoimentos.Where(x => x.DepoimentoId == depoimentoId && x.Der == null).FirstOrDefault();
        }

        public IEnumerable<Depoimento> GetEnviados(Guid usuarioId, int paginaAtual, int itensPagina, StatusDepoimento flag, out int totalRecords)
        {
            var depoimentosEnviados = _db.Depoimentos
                .Include("EnviadoPor")
                .Include("EnviadoPor.Perfil")
                .Include("EnviadoPara")
                .Include("EnviadoPara.Perfil")
                .Where(DepoimentosEnviados(usuarioId, flag));

            totalRecords = depoimentosEnviados.Count();
            var depoimentos = depoimentosEnviados.OrderByDescending(x => x.DataDepoimento).Skip((paginaAtual - 1) * itensPagina).Take(itensPagina).ToList();

            return depoimentos;
        }

        public IEnumerable<Depoimento> GetRecebidos(Guid usuarioId, int paginaAtual, int itensPagina, StatusDepoimento flag, out int totalRecords)
        {
            var depoimentosRecebidos = _db.Depoimentos
                .Include("EnviadoPor")
                .Include("EnviadoPor.Perfil")
                .Include("EnviadoPara")
                .Include("EnviadoPara.Perfil")
                .Where(DepoimentosRecebidos(usuarioId, flag));

            totalRecords = depoimentosRecebidos.Count();
            var depoimentos = depoimentosRecebidos.OrderByDescending(x => x.DataDepoimento).Skip((paginaAtual - 1) * itensPagina).Take(itensPagina).ToList();

            return depoimentos;
        }

        public IEnumerable<Depoimento> GetEnviadosERecebidos(Guid usuarioId, int paginaAtual, int itensPagina, StatusDepoimento flag, out int totalRecords)
        {
            var depoimentosEnviados = _db.Depoimentos
                .Include("EnviadoPor")
                .Include("EnviadoPor.Perfil")
                .Include("EnviadoPara")
                .Include("EnviadoPara.Perfil")
                .Where(DepoimentosEnviados(usuarioId, flag));

            var depoimentosRecebidos = _db.Depoimentos
                .Include("EnviadoPor")
                .Include("EnviadoPor.Perfil")
                .Include("EnviadoPara")
                .Include("EnviadoPara.Perfil")
                .Where(DepoimentosRecebidos(usuarioId, flag));

            totalRecords = depoimentosRecebidos.Concat(depoimentosEnviados).Count();

            var depoimentos = depoimentosRecebidos.Concat(depoimentosEnviados)
                .OrderByDescending(x => x.DataDepoimento)
                .Skip((paginaAtual - 1) * itensPagina)
                .Take(itensPagina)
                .ToList();

            return depoimentos;
        }

        #region Expression

        private Expression<Func<Depoimento, bool>> DepoimentosEnviados(Guid usuarioId, StatusDepoimento flag)
        {
            if (flag == StatusDepoimento.Todos)
                return x => x.EnviadoPorId == usuarioId && x.Der == null && x.StatusDepoimento != StatusDepoimento.NaoAceito;
            else 
                return x => x.EnviadoPorId == usuarioId && x.Der == null && x.StatusDepoimento == flag;
        }
        private Expression<Func<Depoimento, bool>> DepoimentosRecebidos(Guid usuarioId, StatusDepoimento flag)
        {
            if (flag == StatusDepoimento.Todos)
                return x => x.EnviadoParaId == usuarioId && x.Der == null && x.StatusDepoimento != StatusDepoimento.NaoAceito;
            else
                return x => x.EnviadoParaId == usuarioId && x.Der == null && x.StatusDepoimento == flag;
        }

        #endregion

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
