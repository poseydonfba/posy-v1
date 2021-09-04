using POSY.Domain.Entities;
using POSY.Infra.CrossCutting.Common.Enums;
using System;

namespace POSY.Domain.Interfaces.Service
{
    public interface IPerfilService : IDisposable
    {
        void Inserir(Perfil perfil);
        void Alterar(Perfil perfil);
        Perfil Obter(Guid usuarioId);
        Perfil Obter(string alias);
        Perfil EditarPerfil(Guid usuarioId, string nome, string sobrenome, string alias, DateTime dataNascimento, Sexo sexo, EstadoCivil estadoCivil, string frasePerfil, string descricaoPerfil, string paisId);
    }
}