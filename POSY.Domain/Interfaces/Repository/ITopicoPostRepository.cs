using POSY.Domain.Entities;
using System;
using System.Collections.Generic;

namespace POSY.Domain.Interfaces.Repository
{
    public interface ITopicoPostRepository : IDisposable
    {
        void Insert(TopicoPost post);
        void Remove(TopicoPost post);
        void RemovePermanente(TopicoPost post);
        TopicoPost Get(Guid postId);
        IEnumerable<TopicoPost> GetPosts(Guid topicoId, int paginaAtual, int itensPagina, out int totalRecords, out TopicoPost ultimoPost);
    }
}
