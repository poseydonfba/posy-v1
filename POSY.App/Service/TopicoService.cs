using POSY.Domain.Entities;
using POSY.Domain.Enums;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace POSY.App.Service
{
    public class TopicoService : ITopicoService
    {
        private ITopicoRepository _repository;

        public TopicoService(ITopicoRepository repository)
        {
            _repository = repository;
        }

        public Topico SalvarTopico(Guid comunidadeId, Guid usuarioId, string titulo, string descricao, TipoTopico fixo)
        {
            var topico = new Topico(comunidadeId, usuarioId, titulo, descricao, fixo);

            _repository.Insert(topico);

            return topico;
        }

        public void ExcluirTopico(Guid topicoId, Guid usuarioIdExclusao)
        {
            var topico = Obter(topicoId);
            topico.Delete(usuarioIdExclusao);

            _repository.Remove(topico);
        }

        public void ExcluirTopicoPermanente(Guid topicoId, Guid usuarioIdExclusao)
        {
            var topico = Obter(topicoId);
            topico.DeletePermanente(usuarioIdExclusao);

            _repository.RemovePermanente(topico);
        }

        public Topico Obter(Guid topicoId)
        {
            var topico = _repository.Get(topicoId);

            return topico;
        }

        public IEnumerable<Topico> ObterTopicos(Guid comunidadeId, int paginaAtual, int itensPagina, out int totalRecords)
        {
            return _repository.GetTopicos(comunidadeId, paginaAtual, itensPagina, out totalRecords);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
