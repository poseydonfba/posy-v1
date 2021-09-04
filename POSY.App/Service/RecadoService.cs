using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;
using System.Collections.Generic;
using POSY.Domain.Enums;

namespace POSY.App.Service
{
    public class RecadoService : IRecadoService
    {
        private IRecadoRepository _repository;

        public RecadoService(IRecadoRepository repository)
        {
            _repository = repository;
        }

        public Recado EnviarRecado(Guid enviadoPorId, Guid enviadoParaId, string recado)
        {
            var rec = new Recado(enviadoPorId, enviadoParaId, recado);

            _repository.Insert(rec);

            return rec;
        }

        public void ExcluirRecado(Guid recadoId, Guid usuarioIdExclusao)
        {
            var recado = _repository.Get(recadoId);
            recado.Delete(usuarioIdExclusao);

            _repository.Remove(recado);
        }

        public void MarcarComoLido(Guid recadoId)
        {
            var recado = _repository.Get(recadoId);

            if (recado.StatusRecado == StatusRecado.NaoLido)
            {
                recado.SetLeitura(StatusRecado.Lido);

                _repository.Update(recado);
            }
        }

        public Recado ObterRecado(Guid recadoId)
        {
            return _repository.Get(recadoId);
        }

        public IEnumerable<Recado> ObterRecadosEnviadosERecebidos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetEnviadosERecebidos(usuarioId, paginaAtual, itensPagina, StatusRecado.Todos, out totalRecords);
        }

        public IEnumerable<Recado> ObterRecadosRecebidosNaoLidos(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetRecebidos(usuarioId, paginaAtual, itensPagina, StatusRecado.NaoLido, out totalRecords);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
