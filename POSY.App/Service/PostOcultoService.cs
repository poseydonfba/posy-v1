using System;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Domain.Enums;

namespace POSY.App.Service
{
    public class PostOcultoService : IPostOcultoService
    {
        private IPostOcultoRepository _repository;

        public PostOcultoService(IPostOcultoRepository repository)
        {
            _repository = repository;
        }

        public void OcultarPost(Guid usuarioId, Guid postPerfilId)
        {
            var post = new PostOculto(usuarioId, postPerfilId, StatusPostOculto.Oculto);

            _repository.Insert(post);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
