using POSY.Infra.CrossCutting.Common.Resources;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;
using System.Collections.Generic;
using POSY.Domain.Enums;

namespace POSY.App.Service
{
    public class DepoimentoService : IDepoimentoService
    {
        private IDepoimentoRepository _repository;

        public DepoimentoService(IDepoimentoRepository repository)
        {
            _repository = repository;
        }

        public Depoimento EnviarDepoimento(Guid enviadoPorId, Guid enviadoParaId, string depoimento)
        {
            var dep = new Depoimento(enviadoPorId, enviadoParaId, depoimento);

            _repository.Insert(dep);

            return dep;
        }

        public void ExcluirDepoimento(Guid recadoId, Guid usuarioIdExclusao)
        {
            var recado = _repository.Get(recadoId);
            recado.Delete(usuarioIdExclusao);

            _repository.Remove(recado);
        }

        public void AceitarDepoimento(Guid depoimentoId)
        {
            var depoimento = ObterDepoimento(depoimentoId);
            depoimento.SetResposta(StatusDepoimento.Aceito);

            _repository.Update(depoimento);
        }

        public void RecusarDepoimento(Guid depoimentoId)
        {
            var depoimento = ObterDepoimento(depoimentoId);
            depoimento.SetResposta(StatusDepoimento.NaoAceito);

            _repository.Update(depoimento);
        }

        public Depoimento ObterDepoimento(Guid depoimentoId)
        {
            var depoimento = _repository.Get(depoimentoId);

            if (depoimento == null)
                throw new Exception(Errors.DepoimentoNaoEncontrado);

            return depoimento;
        }

        public IEnumerable<Depoimento> ObterDepoimentosEnviados(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetEnviados(usuarioId, paginaAtual, itensPagina, StatusDepoimento.Todos, out totalRecords);
        }

        public IEnumerable<Depoimento> ObterDepoimentosRecebidos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetRecebidos(usuarioId, paginaAtual, itensPagina, StatusDepoimento.Todos, out totalRecords);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
