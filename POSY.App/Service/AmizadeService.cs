using POSY.Domain.Entities;
using POSY.Domain.Enums;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Infra.CrossCutting.Common.Resources;
using System;
using System.Collections.Generic;

namespace POSY.App.Service
{
    public class AmizadeService : IAmizadeService
    {
        private IAmizadeRepository _repository;

        public AmizadeService(IAmizadeRepository repository)
        {
            _repository = repository;
        }

        public void AdicionarSolicitacaoAmizade(Guid usuarioIdSolicitante, Guid usuarioIdSolicitado)
        {
            var solicitacao = Obter(usuarioIdSolicitante, usuarioIdSolicitado);

            if (solicitacao != null && solicitacao.StatusSolicitacao == SolicitacaoAmizade.Pendente)
                throw new Exception(Errors.SolicitacaoAmizadeJaEnviada);

            var amizade = new Amizade(usuarioIdSolicitante, usuarioIdSolicitado);

            _repository.Insert(amizade);
        }

        public void ExcluirAmigo(Guid usuarioIdExclusao, Guid usuarioIdParaExcluir)
        {
            var amizade = Obter(usuarioIdExclusao, usuarioIdParaExcluir);

            if (amizade == null)
                throw new Exception(Errors.ExclusaoInvalida);

            amizade.Delete(usuarioIdExclusao);

            _repository.Remove(amizade);
        }

        public void AceitarSolicitacaoAmizade(Guid usuarioId, Guid usuarioIdAceitar)
        {
            var solicitacaoAmizade = Obter(usuarioId, usuarioIdAceitar);

            if(solicitacaoAmizade == null)
                throw new Exception(Errors.SolicitacaoAmizadeInvalida);

            solicitacaoAmizade.SetResposta(SolicitacaoAmizade.Aprovado);

            _repository.Update(solicitacaoAmizade);
        }

        public void RecusarSolicitacaoAmizade(Guid usuarioId, Guid usuarioIdRecusar)
        {
            var solicitacaoAmizade = Obter(usuarioIdRecusar, usuarioId);

            if (solicitacaoAmizade == null)
                throw new Exception(Errors.SolicitacaoAmizadeInvalida);

            solicitacaoAmizade.SetResposta(SolicitacaoAmizade.Rejeitado);

            _repository.Update(solicitacaoAmizade);
        }

        public Amizade Obter(Guid usuarioId1, Guid usuarioId2)
        {
            if (usuarioId1 == usuarioId2)
                throw new Exception(Errors.AmizadeInvalida);

            return _repository.Get(usuarioId1, usuarioId2);
        }

        public IEnumerable<Amizade> SolicitacoesRecebidasPendentes(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.Get(usuarioId, paginaAtual, itensPagina, SolicitacaoAmizade.Pendente, out totalRecords);
        }

        public IEnumerable<Usuario> ObterAmigos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetAmigos(usuarioId, paginaAtual, itensPagina, out totalRecords);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
