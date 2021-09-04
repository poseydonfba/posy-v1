using POSY.Domain.Entities;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POSY.App.Service
{
    public class ModeradorService : IModeradorService
    {
        private IModeradorRepository _repository;

        public ModeradorService(IModeradorRepository repository)
        {
            _repository = repository;
        }

        public Moderador AdicionarModerador(Guid comunidadeId, Guid usuarioModeradorId, Guid usuarioOperacaoId)
        {
            var moderador = new Moderador(comunidadeId, usuarioModeradorId, usuarioOperacaoId);

            _repository.Insert(moderador);

            return moderador;
        }

        public void ExcluirModerador(Guid comunidadeId, Guid usuarioId, Guid usuarioIdExclusao)
        {
            var moderador = Obter(comunidadeId, usuarioId);
            moderador.Delete(usuarioIdExclusao);

            _repository.Remove(moderador);
        }

        public Moderador Obter(Guid comunidadeId, Guid usuarioId)
        {
            return _repository.Get(comunidadeId, usuarioId);
        }

        public IEnumerable<Moderador> ObterModeradores(Guid comunidadeId)
        {
            return _repository.GetModeradores(comunidadeId).ToList();
        }

        public IEnumerable<Moderador> ObterModeradores(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetModeradores(comunidadeId, paginaAtual, itensPagina, out totalRecords).ToList();
        }

        public IEnumerable<Moderador> ObterComunidades(Guid usuarioId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetComunidades(usuarioId, paginaAtual, itensPagina, out totalRecords).ToList();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
