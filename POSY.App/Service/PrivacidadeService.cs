using POSY.Infra.CrossCutting.Common.Resources;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;

namespace POSY.App.Service
{
    public class PrivacidadeService : IPrivacidadeService
    {
        private IPrivacidadeRepository _repository;

        public PrivacidadeService(IPrivacidadeRepository repository)
        {
            _repository = repository;
        }

        public void Inserir(Privacidade privacidade)
        {
            _repository.Insert(privacidade);
        }

        public void Alterar(Privacidade privacidade)
        {
            _repository.Update(privacidade);
        }

        public void SalvarPrivacidade(Guid usuarioId, bool verRecado, bool escreverRecado)
        {
            var privacidade = Obter(usuarioId);
            privacidade.Edit(verRecado, escreverRecado);

            _repository.Update(privacidade);
        }

        public Privacidade Obter(Guid usuarioId)
        {
            var privacidade = _repository.GetByUsuario(usuarioId);

            if (privacidade == null)
                throw new Exception(Errors.UsuarioInvalido);

            return privacidade;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
