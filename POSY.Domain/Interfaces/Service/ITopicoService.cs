using POSY.Domain.Entities;
using POSY.Domain.Enums;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface ITopicoService : IDisposable
    {
        Topico SalvarTopico(Guid comunidadeId, Guid usuarioId, string titulo, string descricao, TipoTopico fixo);
        void ExcluirTopico(Guid topicoId, Guid usuarioIdExclusao);
        void ExcluirTopicoPermanente(Guid topicoId, Guid usuarioIdExclusao);
        Topico Obter(Guid topicoId);
        IEnumerable<Topico> ObterTopicos(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords);
    }
}
