using Microsoft.VisualStudio.TestTools.UnitTesting;
using POSY.Infra.CrossCutting.Common.Validations;
using System;

namespace POSY.Infra.CrossCutting.Common.Tests.Validations
{
    [TestClass]
    public class ValidTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Valid_AssertArgumentEquals_Erro_Quando_Diferente()
        {
            Valid.AssertArgumentEquals("A", "", "Os valores não podem ser diferente");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Valid_AssertArgumentNotEmpty_Erro_Quando_Em_Branco()
        {
            Valid.AssertArgumentNotEmpty("", "Valor não pode ser vazio");
        }
    }
}
