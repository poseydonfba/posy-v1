using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface IRecadoComentarioService : IDisposable
    {
        RecadoComentario Comentar(Guid recadoId, Guid usuarioId, string comentario);
        void ExcluirComentario(Guid comentarioId, Guid usuarioIdExclusao);
        RecadoComentario ObterComentario(Guid comentarioId);
        IEnumerable<RecadoComentario> ObterComentarios(Guid recadoId, int paginaAtual, int itensPagina);
    }
}
