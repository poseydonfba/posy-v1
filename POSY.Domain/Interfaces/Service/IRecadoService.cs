using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IRecadoService : IDisposable
    {
        Recado EnviarRecado(Guid enviadoPorId, Guid enviadoParaId, string recado);
        void ExcluirRecado(Guid recadoId, Guid usuarioIdExclusao);
        void MarcarComoLido(Guid recadoId);
        Recado ObterRecado(Guid recadoId);
        IEnumerable<Recado> ObterRecadosEnviadosERecebidos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
        IEnumerable<Recado> ObterRecadosRecebidosNaoLidos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
