using POSY.Domain.Entities;
using System;

namespace POSY.Domain.Interfaces.Service
{
    public interface IPostPerfilBloqueadoService : IDisposable
    {
        void BloquearPostPerfil(Guid usuarioId, Guid usuarioIdBloquear);
        PostPerfilBloqueado ObterPerfilBloqueado(Guid usuarioId, Guid usuarioIdBloqueado);
    }
}
