using POSY.Domain.Entities;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Infra.CrossCutting.Common.Enums;
using POSY.Infra.CrossCutting.Common.Resources;
using POSY.Infra.CrossCutting.Common.Sanitizer;
using System;

namespace POSY.App.Service
{
    public class PerfilService : IPerfilService
    {
        private IPerfilRepository _repository;

        public PerfilService(IPerfilRepository repository)
        {
            _repository = repository;
        }

        public void Inserir(Perfil perfil)
        {
            _repository.Insert(perfil);
        }

        public void Alterar(Perfil perfil)
        {
            _repository.Update(perfil);
        }

        public Perfil EditarPerfil(Guid usuarioId, string nome, string sobrenome, string alias, DateTime dataNascimento, Sexo sexo, EstadoCivil estadoCivil, string frasePerfil, string descricaoPerfil, string paisId)
        {
            var perfil = _repository.GetByUsuario(usuarioId);

            
            //User.Identity.GetUserId();

            if (perfil == null)
                throw new Exception(Errors.PerfilNaoEncontrado);

            perfil.Edit(nome, sobrenome, alias, dataNascimento, sexo, estadoCivil, frasePerfil, HtmlSanitizer.SanitizeHtml(descricaoPerfil), paisId);
            perfil.Validate();

            _repository.Update(perfil);

            return perfil;
        }

        public Perfil Obter(Guid usuarioId)
        {
            var perfil = _repository.GetByUsuario(usuarioId);

            //if (perfil == null)
            //    throw new Exception(Errors.PerfilNaoEncontrado);

            return perfil;
        }

        public Perfil Obter(string alias)
        {
            var perfil = _repository.GetByAlias(alias);

            if (perfil == null)
                throw new Exception(Errors.PerfilNaoEncontrado);

            return perfil;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
