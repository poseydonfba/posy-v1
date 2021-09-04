using POSY.Domain.Entities;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Infra.CrossCutting.Common.Resources;
using System;

namespace POSY.App.Service
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        #region OLD

        //public Usuario Autenticar(string email, string senha)
        //{
        //    //var usuario = ObterComPerfil(email);
        //    var usuario = Obter(email);

        //    if (usuario.Senha != Criptografia.EncryptSenha(senha))
        //        throw new Exception(Errors.CredenciaisInvalidas);

        //    return usuario;
        //}

        //public Usuario Registrar(string email, string senha, string confirmarSenha, string nome, string sobrenome, DateTime dataNascimento, string sexo, string estadoCivil, string paisId)
        //{
        //    var usu = _repository.Get(email);

        //    if (usu != null)
        //        throw new Exception(Errors.EmailDuplicado);

        //    var usuario = new Usuario { Email = email };
        //    usuario.Perfil = new Perfil(usuario.UsuarioId, nome, sobrenome, usuario.UsuarioId.ToString(), dataNascimento, sexo, estadoCivil, paisId);
        //    usuario.Privacidade = new Privacidade(usuario.UsuarioId, 1, 1);
        //    usuario.Validate();
        //    usuario.SetSenha(senha, confirmarSenha);

        //    _repository.Insert(usuario);

        //    return usuario;
        //}

        //public void AlterarSenha(string email, string senha, string novaSenha, string confirmarNovaSenha)
        //{
        //    var usuario = Autenticar(email, senha);

        //    usuario.SetSenha(novaSenha, confirmarNovaSenha);
        //    usuario.Validate();

        //    _repository.Alterar(usuario);
        //}

        //public string ResetarSenha(string email)
        //{
        //    var usuario = Obter(email);

        //    if (usuario == null)
        //        throw new Exception(Errors.UsuarioNaoEncontrado);

        //    var senha = usuario.ResetSenha();
        //    usuario.Validate();

        //    _repository.Alterar(usuario);

        //    return senha;
        //}

        #endregion

        public Usuario Obter(Guid id)
        {
            var usuario = _repository.GetById(id);

            if (usuario == null)
                throw new Exception(Errors.UsuarioNaoEncontrado);

            return usuario;
        }

        //public Usuario Obter(string email)
        //{
        //    var usuario = _repository.Get(email);

        //    if (usuario == null)
        //        throw new Exception(Errors.UsuarioNaoEncontrado);

        //    return usuario;
        //}

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
