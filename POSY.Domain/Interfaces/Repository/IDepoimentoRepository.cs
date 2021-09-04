using POSY.Domain.Entities;
using POSY.Domain.Enums;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IDepoimentoRepository : IDisposable
    {
        void Insert(Depoimento depoimento);
        void Update(Depoimento depoimento);
        void Remove(Depoimento depoimento);
        Depoimento Get(Guid depoimentoId);
        IEnumerable<Depoimento> GetEnviados(Guid usuarioId, int paginaAtual, int itensPagina, StatusDepoimento statusDepoimento, out int totalRecords);
        IEnumerable<Depoimento> GetRecebidos(Guid usuarioId, int paginaAtual, int itensPagina, StatusDepoimento statusDepoimento, out int totalRecords);
        IEnumerable<Depoimento> GetEnviadosERecebidos(Guid usuarioId, int paginaAtual, int itensPagina, StatusDepoimento statusDepoimento, out int totalRecords);
    }
}
