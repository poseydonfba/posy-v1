using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;

namespace POSY.App.Service
{
    public class PostPerfilBloqueadoService : IPostPerfilBloqueadoService
    {
        private IPostPerfilBloqueadoRepository _repository;

        public PostPerfilBloqueadoService(IPostPerfilBloqueadoRepository repository)
        {
            _repository = repository;
        }

        public void BloquearPostPerfil(Guid usuarioId, Guid usuarioIdBloquear)
        {
            var perfilBloquear = new PostPerfilBloqueado(usuarioId, usuarioIdBloquear);

            _repository.Insert(perfilBloquear);
        }

        public PostPerfilBloqueado ObterPerfilBloqueado(Guid usuarioId, Guid usuarioIdBloqueado)
        {
            return _repository.Get(usuarioId, usuarioIdBloqueado);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
