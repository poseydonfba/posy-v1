using POSY.Domain.Entities;
using System;

namespace POSY.Domain.Interfaces.Service
{
    public interface IPrivacidadeService : IDisposable
    {
        void Inserir(Privacidade privacidade);
        void Alterar(Privacidade privacidade);
        void SalvarPrivacidade(Guid usuarioId, bool verRecado, bool escreverRecado);
        Privacidade Obter(Guid usuarioId);
    }
}
