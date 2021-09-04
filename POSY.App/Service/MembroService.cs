using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using POSY.Domain.Enums;

namespace POSY.App.Service
{
    public class MembroService : IMembroService
    {
        private IMembroRepository _repository;

        public MembroService(IMembroRepository repository)
        {
            _repository = repository;
        }

        public Membro SolicitarParticipacao(Guid comunidadeId, Guid usuarioId)
        {
            var membro = new Membro(comunidadeId, usuarioId);

            _repository.Insert(membro);

            return membro;
        }

        public void AdicionarMembro(Guid comunidadeId, Guid usuarioMembroId)
        {
            var membro = new Membro(comunidadeId, usuarioMembroId);
            membro.AdicionarMembro();

            _repository.Insert(membro);
        }

        public void AceitarMembro(Guid comunidadeId, Guid usuarioId, Guid usuarioIdResposta)
        {
            var membro = Obter(comunidadeId, usuarioId);
            membro.SetResposta(StatusSolicitacaoMembroComunidade.Aprovado, usuarioIdResposta);

            _repository.Update(membro);
        }

        public void RecusarMembro(Guid comunidadeId, Guid usuarioId, Guid usuarioIdResposta)
        {
            var membro = Obter(comunidadeId, usuarioId);
            membro.SetResposta(StatusSolicitacaoMembroComunidade.Rejeitado, usuarioIdResposta);

            _repository.Update(membro);
        }

        public Membro Obter(Guid comunidadeId, Guid usuarioId)
        {
            return _repository.Get(comunidadeId, usuarioId);
        }

        public IEnumerable<Membro> ObterMembros(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetMembros(comunidadeId, paginaAtual, itensPagina, StatusSolicitacaoMembroComunidade.Aprovado, out totalRecords).ToList();
        }

        public IEnumerable<Membro> ObterMembrosPendentes(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetMembros(comunidadeId, paginaAtual, itensPagina, StatusSolicitacaoMembroComunidade.Pendente, out totalRecords).ToList();
        }

        public IEnumerable<Membro> ObterComunidades(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetComunidades(usuarioId, paginaAtual, itensPagina, StatusSolicitacaoMembroComunidade.Aprovado, out totalRecords).ToList();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
