using POSY.Domain.Entities;
using POSY.Domain.Enums;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IRecadoRepository : IDisposable
    {
        void Insert(Recado recado);
        void Update(Recado recado);
        void Remove(Recado recado);
        Recado Get(Guid recadoId);
        IEnumerable<Recado> GetEnviados(Guid usuarioId, int paginaAtual, int itensPagina, StatusRecado statusRecado, out int totalRecords);
        IEnumerable<Recado> GetRecebidos(Guid usuarioId, int paginaAtual, int itensPagina, StatusRecado statusRecado, out int totalRecords);
        IEnumerable<Recado> GetEnviadosERecebidos(Guid usuarioId, int paginaAtual, int itensPagina, StatusRecado statusRecado, out int totalRecords);
    }
}
