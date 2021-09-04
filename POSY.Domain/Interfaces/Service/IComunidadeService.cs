using POSY.Domain.Entities;
using POSY.Domain.Enums;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IComunidadeService : IDisposable
    {
        Comunidade Obter(Guid comunidadeId);
        Comunidade Obter(string alias);
        IEnumerable<Comunidade> Obter(int paginaAtual, int itensPagina, out int totalRecords);
        Comunidade CriarComunidade(Guid usuarioId, string nome, int categoriaId, string descricaoPerfil);
        Comunidade EditarComunidadePerfil(Guid comunidadeId, string alias, string nome, int categoriaId, string descricaoPerfil, Guid uar);
        Comunidade EditarComunidadePrivacidade(Guid comunidadeId, TipoComunidade tipo, Guid uar);
        void ExcluirComunidade(Guid comunidadeId, Guid usuarioIdExclusao);
        TopicoPost ObterUltimoPost(Guid comunidadeId);
    }
}
