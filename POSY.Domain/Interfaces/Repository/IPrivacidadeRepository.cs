using POSY.Domain.Entities;
using System;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IPrivacidadeRepository : IDisposable
    {
        void Insert(Privacidade privacidade);
        void Update(Privacidade privacidade);
        Privacidade GetByUsuario(Guid usuarioId);
    }
}
