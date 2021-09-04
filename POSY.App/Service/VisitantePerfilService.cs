using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.App.Service
{
    public class VisitantePerfilService : IVisitantePerfilService
    {
        private IVisitantePerfilRepository _repository;

        public VisitantePerfilService(IVisitantePerfilRepository repository)
        {
            _repository = repository;
        }

        public void SalvarVisita(Guid usuarioIdVisitante, Guid usuarioIdVisitado)
        {
            if (usuarioIdVisitante != usuarioIdVisitado)
            {
                var visita = new VisitantePerfil(usuarioIdVisitante, usuarioIdVisitado);

                _repository.Insert(visita);
            }
        }

        public IEnumerable<Perfil> ObterVisitados(Guid usuarioId)
        {
            return _repository.GetVisitados(usuarioId);
        }

        public IEnumerable<Perfil> ObterVisitados(Guid usuarioId, int take)
        {
            return _repository.GetVisitados(usuarioId, take);
        }

        public IEnumerable<Perfil> ObterVisitantes(Guid usuarioId)
        {
            return _repository.GetVisitantes(usuarioId);
        }

        public IEnumerable<Perfil> ObterVisitantes(Guid usuarioId, int take)
        {
            return _repository.GetVisitantes(usuarioId, take);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
