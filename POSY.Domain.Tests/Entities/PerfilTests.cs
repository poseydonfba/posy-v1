using Microsoft.VisualStudio.TestTools.UnitTesting;
using POSY.Domain.Entities;
using POSY.Infra.CrossCutting.Common.Enums;
using System;

namespace POSY.Domain.Tests.Entities
{
    [TestClass]
    public class PerfilTests
    {
        private Guid UsuarioId { get; set; }
        private string Nome { get; set; }
        private string Sobrenome { get; set; }
        private string Alias { get; set; }
        private DateTime DataNascimento { get; set; }
        private Sexo Sexo { get; set; }
        private EstadoCivil EstadoCivil { get; set; }
        private string PaisId { get; set; }

        public PerfilTests()
        {
            UsuarioId = Guid.NewGuid();
            Nome = "Admin";
            Sobrenome = "Teste";
            Alias = "admin";
            DataNascimento = new DateTime(1985, 12, 12);
            Sexo = Sexo.MASCULINO;
            EstadoCivil = EstadoCivil.SOLTEIRO;
            PaisId = "pt-BR";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Perfil_New_Nome_Obrigatorio()
        {
            new Perfil(UsuarioId, "", Sobrenome, Alias, DataNascimento, Sexo, EstadoCivil, PaisId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Perfil_New_Nome_MaxLength()
        {
            var nome = Nome;
            while (nome.Length < Perfil.NomeMaxLength + 1)
                nome += Nome;

            new Perfil(UsuarioId, nome, Sobrenome, Alias, DataNascimento, Sexo, EstadoCivil, PaisId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Perfil_New_Sobrenome_Obrigatorio()
        {
            new Perfil(UsuarioId, Nome, "", Alias, DataNascimento, Sexo, EstadoCivil, PaisId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Perfil_New_Sobrenome_MaxLength()
        {
            var sobrenome = Sobrenome;
            while (sobrenome.Length < Perfil.SobrenomeMaxLength + 1)
                sobrenome += Nome;

            new Perfil(UsuarioId, Nome, sobrenome, Alias, DataNascimento, Sexo, EstadoCivil, PaisId);
        }
    }
}
