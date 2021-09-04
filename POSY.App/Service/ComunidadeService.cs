using POSY.Domain.Entities;
using POSY.Domain.Enums;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Infra.CrossCutting.Common.Resources;
using System;
using System.Collections.Generic;

namespace POSY.App.Service
{
    public class ComunidadeService : IComunidadeService
    {
        private IComunidadeRepository _repository;

        public ComunidadeService(IComunidadeRepository repository)
        {
            _repository = repository;
        }

        public Comunidade CriarComunidade(Guid usuarioId, string nome, int categoriaId, string descricaoPerfil)
        {
            var comunidade = new Comunidade(usuarioId, nome, categoriaId, descricaoPerfil);

            _repository.Insert(comunidade);

            return comunidade;
        }

        public Comunidade EditarComunidadePerfil(Guid comunidadeId, string alias, string nome, int categoriaId, string descricaoPerfil, Guid uar)
        {
            var comunidade = Obter(comunidadeId);
            comunidade.Edit(alias, nome, categoriaId, descricaoPerfil, uar);

            _repository.Update(comunidade);

            return comunidade;
        }

        public Comunidade EditarComunidadePrivacidade(Guid comunidadeId, TipoComunidade tipo, Guid uar)
        {
            var comunidade = Obter(comunidadeId);
            comunidade.SetPrivacidade(tipo, uar);

            _repository.Update(comunidade);

            return comunidade;
        }

        public void ExcluirComunidade(Guid comunidadeId, Guid usuarioIdExclusao)
        {
            var comunidade = Obter(comunidadeId);
            comunidade.Delete(usuarioIdExclusao);

            _repository.Remove(comunidade);
        }

        public Comunidade Obter(string alias)
        {
            var comunidade = _repository.Get(alias);

            if (comunidade == null)
                throw new Exception(Errors.ComunidadeNaoEncontrada);

            return comunidade;
        }

        public Comunidade Obter(Guid comunidadeId)
        {
            var comunidade = _repository.Get(comunidadeId);

            if (comunidade == null)
                throw new Exception(Errors.ComunidadeNaoEncontrada);

            return comunidade;
        }

        public IEnumerable<Comunidade> Obter(int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.Get(paginaAtual, itensPagina, out totalRecords);
        }

        public TopicoPost ObterUltimoPost(Guid comunidadeId)
        {
            return _repository.GetUltimoPost(comunidadeId);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
