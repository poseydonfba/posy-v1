using System;
using System.Collections.Generic;
using POSY.Domain.Entities;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IDisposable
    {
        Usuario GetById(Guid id);
        IEnumerable<Usuario> GetAll();
        void DesativarLock(string id);
    }
}