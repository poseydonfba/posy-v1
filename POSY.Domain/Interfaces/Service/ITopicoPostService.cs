using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Service
{
    public interface ITopicoPostService : IDisposable
    {
        TopicoPost SalvarPost(Guid topicoId, Guid usuarioId, string descricao);
        void ExcluirTopicoPost(Guid postId, Guid usuarioIdExclusao);
        void ExcluirTopicoPostPermanente(Guid postId, Guid usuarioIdExclusao);
        TopicoPost Obter(Guid postId);
        IEnumerable<TopicoPost> ObterPosts(Guid topicoId, int paginaAtual, int itensPagina, out int totalRecords, out TopicoPost ultimoPost);
    }
}
