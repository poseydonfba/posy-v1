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
    public class RecadoRepository : IRecadoRepository
    {
        private readonly IDatabaseContext _db;

        public RecadoRepository(IDatabaseContext db)
        {
            _db = db;
        }
        public void Insert(Recado recado)
        {
            _db.Recados.Add(recado);
            
        }

        public void Update(Recado recado)
        {
            _db.Entry(recado).State = EntityState.Modified;            
        }

        public void Remove(Recado recado)
        {
            _db.Entry(recado).State = EntityState.Modified;            
        }

        public Recado Get(Guid recadoId)
        {
            return _db.Recados.Where(x => x.RecadoId == recadoId && x.Der == null).FirstOrDefault();
        }

        public IEnumerable<Recado> GetEnviados(Guid usuarioId, int paginaAtual, int itensPagina, StatusRecado statusRecado, out int totalRecords)
        {
            var recadosEnviados = _db.Recados
                .Include("EnviadoPor")
                .Include("EnviadoPor.Perfil")
                .Include("EnviadoPara")
                .Include("EnviadoPara.Perfil")
                .Where(RecadosEnviados(usuarioId, statusRecado));

            totalRecords = recadosEnviados.Count();
            var recados = recadosEnviados.OrderByDescending(x => x.DataRecado).Skip((paginaAtual - 1) * itensPagina).Take(itensPagina).ToList();

            return recados;
        }

        public IEnumerable<Recado> GetRecebidos(Guid usuarioId, int paginaAtual, int itensPagina, StatusRecado statusRecado, out int totalRecords)
        {
            var recadosRecebidos = _db.Recados
                .Include("EnviadoPor")
                .Include("EnviadoPor.Perfil")
                .Include("EnviadoPara")
                .Include("EnviadoPara.Perfil")
                .Where(RecadosRecebidos(usuarioId, statusRecado));

            totalRecords = recadosRecebidos.Count();
            var recados = recadosRecebidos.OrderByDescending(x => x.DataRecado).Skip((paginaAtual - 1) * itensPagina).Take(itensPagina).ToList();

            return recados;
        }

        public IEnumerable<Recado> GetEnviadosERecebidos(Guid usuarioId, int paginaAtual, int itensPagina, StatusRecado statusRecado, out int totalRecords)
        {
            var recadosEnviados = _db.Recados
                .Include("EnviadoPor")
                .Include("EnviadoPor.Perfil")
                .Include("EnviadoPara")
                .Include("EnviadoPara.Perfil")
                .Where(RecadosEnviados(usuarioId, statusRecado));

            var recadosRecebidos = _db.Recados
                .Include("EnviadoPor")
                .Include("EnviadoPor.Perfil")
                .Include("EnviadoPara")
                .Include("EnviadoPara.Perfil")
                .Where(RecadosRecebidos(usuarioId, statusRecado));

            totalRecords = recadosRecebidos.Concat(recadosEnviados).Count();

            var recados = recadosRecebidos.Concat(recadosEnviados)
                .OrderByDescending(x => x.DataRecado)
                .Skip((paginaAtual - 1) * itensPagina)
                .Take(itensPagina)
                .ToList();

            return recados;
        }

        #region Expression

        private Expression<Func<Recado, bool>> RecadosEnviados(Guid usuarioId, StatusRecado statusRecado)
        {
            if (statusRecado == StatusRecado.Todos)
                return x => x.EnviadoPorId == usuarioId && x.Der == null;
            else
                return x => x.EnviadoPorId == usuarioId && x.Der == null && x.StatusRecado == statusRecado;
        }
        private Expression<Func<Recado, bool>> RecadosRecebidos(Guid usuarioId, StatusRecado statusRecado)
        {
            if (statusRecado == StatusRecado.Todos)
                return x => x.EnviadoParaId == usuarioId && x.Der == null;
            else
                return x => x.EnviadoParaId == usuarioId && x.Der == null && x.StatusRecado == statusRecado;
        }

        #endregion

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
