using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IUsuarioService : IDisposable
    {
        //Usuario Autenticar(string email, string senha);
        //Usuario Registrar(string email, string senha, string confirmarSenha, string nome, string sobrenome, DateTime dataNascimento, string sexo, string estadoCivil, string paisId);
        //void AlterarSenha(string email, string senha, string novaSenha, string confirmarNovaSenha);
        //string ResetarSenha(string email);
        Usuario Obter(Guid id);
        //Usuario Obter(string email);
    }
}
