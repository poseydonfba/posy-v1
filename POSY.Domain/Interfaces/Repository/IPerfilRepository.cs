using POSY.Domain.Entities;
using System;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IPerfilRepository : IDisposable
    {
        Perfil GetByUsuario(Guid usuarioId);
        Perfil GetByAlias(string alias);
        void Insert(Perfil obj);
        void Update(Perfil obj);
    }
}
