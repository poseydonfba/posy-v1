using System;

namespace POSY.Domain.Interfaces.Service
{
    public interface IPostOcultoService : IDisposable
    {
        void OcultarPost(Guid usuarioId, Guid postPerfilId);

        //void MostrarPost(Guid usuarioId, Guid postPerfilId);
    }
}
