using POSY.Domain.Entities;
using System;

namespace POSY.Domain.Interfaces.Repository
{
    public interface IPostOcultoRepository : IDisposable
    {
        void Insert(PostOculto post);
        void Ocultar(PostOculto post);
    }
}
