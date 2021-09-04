using POSY.Domain.Entities;
using System;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IPostPerfilBloqueadoRepository : IDisposable
    {
        void Insert(PostPerfilBloqueado perfilBloqueado);
        PostPerfilBloqueado Get(Guid usuarioId, Guid usuarioIdBloqueado);
    }
}
