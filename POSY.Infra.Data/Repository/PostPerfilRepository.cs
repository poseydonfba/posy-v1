using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Entities;
using POSY.Infra.Data.Interfaces;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using POSY.Domain.Enums;

namespace POSY.Infra.Data.Repository
{
    public class PostPerfilRepository : IPostPerfilRepository
    {
        private readonly IDatabaseContext _db;

        public PostPerfilRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Insert(PostPerfil post)
        {
            _db.PostsPerfil.Add(post);            
        }

        public void Update(PostPerfil post)
        {
            _db.Entry(post).State = EntityState.Modified;            
        }

        public void Remove(PostPerfil post)
        {
            //_db.PostsPerfil.Remove(post);
            post.Delete();

            _db.Entry(post).State = EntityState.Modified;            
        }

        public PostPerfil Get(Guid postId)
        {
            return _db.PostsPerfil.Where(x => x.PostPerfilId == postId).FirstOrDefault();
        }

        public IEnumerable<PostPerfil> Get(Guid usuarioId, int paginaAtual, int itensPagina)
        {
            #region SQL POR USUARIO
            //var usuario = _db.Usuarios // Verificar incluir 2 registros na solicitação de Amizade
            //    .Include("SolicitacaoAmizadeEnviada") // Para preencher os Amigos
            //    .Include("SolicitacaoAmizadeRecebida") // Para preencher os Amigos
            //    .Include("SolicitacaoAmizadeEnviada.SolicitadoPara")
            //    .Include("SolicitacaoAmizadeRecebida.SolicitadoPor")
            //    .Include("SolicitacaoAmizadeEnviada.SolicitadoPara.Perfil")
            //    .Include("SolicitacaoAmizadeRecebida.SolicitadoPor.Perfil")
            //    .Include("SolicitacaoAmizadeEnviada.SolicitadoPara.PostsPerfil")
            //    .Include("SolicitacaoAmizadeRecebida.SolicitadoPor.PostsPerfil")
            //    .Include("SolicitacaoAmizadeRecebida.SolicitadoPor.PostsPerfilBloqueado")
            //    .Include("PostsPerfil")
            //    .Include("SolicitacaoAmizadeEnviada.SolicitadoPara.PostsOculto")
            //    .Include("SolicitacaoAmizadeRecebida.SolicitadoPor.PostsOculto")
            //    .Include("SolicitacaoAmizadeEnviada.SolicitadoPara.PostsPerfil.PostsPerfilComentario")
            //    .Include("SolicitacaoAmizadeRecebida.SolicitadoPor.PostsPerfil.PostsPerfilComentario")
            //    .Include("SolicitacaoAmizadeRecebida.SolicitadoPara.PostsPerfil.PostsPerfilComentario")
            //    .Include("SolicitacaoAmizadeEnviada.SolicitadoPor.PostsPerfil.PostsPerfilComentario")
            //    .Include("PostsOculto")
            //    .Where(x => x.UsuarioId == usuarioId)
            //    .FirstOrDefault();
            #endregion

            #region QUERY SEPARADA

            //// Meus posts
            //var meusPosts = (from c in _db.PostsPerfil
            //                 where c.UsuarioId == usuarioId
            //                 && c.Der == null
            //                 select c)
            //                 .Include("Usuario").Include("Usuario.Perfil").ToList();

            //// Posts de amigos que eu adicionei
            //var postsDeAmigosSolicitados = (from a in _db.Usuarios
            //                                join b in _db.Amizades on a.UsuarioId equals b.SolicitadoParaId
            //                                join c in _db.PostsPerfil on b.SolicitadoParaId equals c.UsuarioId
            //                                where b.StatusSolicitacao == SolicitacaoAmizade.Aprovado
            //                                && b.SolicitadoPorId == usuarioId
            //                                && c.Der == null
            //                                select c)
            //                                .Include("Usuario").Include("Usuario.Perfil").ToList();

            //// Posts de amigos que me adicionaram
            //var postsDeAmigosSolicitantes = (from a in _db.Usuarios
            //                                 join b in _db.Amizades on a.UsuarioId equals b.SolicitadoPorId
            //                                 join c in _db.PostsPerfil on b.SolicitadoPorId equals c.UsuarioId
            //                                 where b.StatusSolicitacao == SolicitacaoAmizade.Aprovado
            //                                 && b.SolicitadoParaId == usuarioId
            //                                 && c.Der == null
            //                                 select c)
            //                                 .Include("Usuario").Include("Usuario.Perfil").ToList();

            #endregion

            #region QUERY JOIN

            var query = (from c in _db.PostsPerfil
                         from d in _db.PostsOculto
                             .Where(m => m.PostPerfilId == c.PostPerfilId).DefaultIfEmpty()
                         where c.UsuarioId == usuarioId
                         && c.Der == null
                         && (d.StatusPost == StatusPostOculto.Visivel || d == null)
                         select c)
                .Concat(from a in _db.Usuarios
                        join b in _db.Amizades on a.UsuarioId equals b.SolicitadoParaId
                        join c in _db.PostsPerfil on b.SolicitadoParaId equals c.UsuarioId
                        from d in _db.PostsOculto
                            .Where(m => m.PostPerfilId == c.PostPerfilId).DefaultIfEmpty()
                        where b.StatusSolicitacao == SolicitacaoAmizade.Aprovado
                        && b.SolicitadoPorId == usuarioId
                        && c.Der == null
                        && b.Der == null
                        && (d.StatusPost == StatusPostOculto.Visivel || d == null)
                        select c)
                .Concat(from a in _db.Usuarios
                        join b in _db.Amizades on a.UsuarioId equals b.SolicitadoPorId
                        join c in _db.PostsPerfil on b.SolicitadoPorId equals c.UsuarioId
                        from d in _db.PostsOculto
                            .Where(m => m.PostPerfilId == c.PostPerfilId).DefaultIfEmpty()
                        where b.StatusSolicitacao == SolicitacaoAmizade.Aprovado
                        && b.SolicitadoParaId == usuarioId
                        && c.Der == null
                        && b.Der == null
                        && (d.StatusPost == StatusPostOculto.Visivel || d == null)
                        select c)
                .OrderByDescending(x => x.DataPost)
                .Skip((paginaAtual - 1) * itensPagina)
                .Take(itensPagina)
                .Include("Usuario")
                .Include("Usuario.Perfil")
                .ToList();

            #endregion

            #region QUERY JOIN e LEFT JOIN

            //var query = (from c in _db.PostsPerfil
            //             where c.UsuarioId == usuarioId
            //             && c.Der == null
            //             select c)
            //    .Concat(from a in _db.Usuarios
            //            from b in _db.Amizades
            //                .Where(m => m.SolicitadoParaId == a.UsuarioId).DefaultIfEmpty()
            //            from c in _db.PostsPerfil
            //                .Where(m => m.UsuarioId == b.SolicitadoParaId).DefaultIfEmpty()
            //            from d in _db.PostsOculto
            //                .Where(m => m.PostPerfilId == c.PostPerfilId).DefaultIfEmpty()
            //            where b.StatusSolicitacao == SolicitacaoAmizade.Aprovado
            //            && b.SolicitadoPorId == usuarioId
            //            && c.Der == null
            //            && (d.StatusPost == StatusPostOculto.Visivel || d == null)
            //            select c)
            //    .Concat(from a in _db.Usuarios
            //            from b in _db.Amizades
            //                .Where(m => m.SolicitadoPorId == a.UsuarioId).DefaultIfEmpty()
            //            from c in _db.PostsPerfil
            //                .Where(m => m.UsuarioId == b.SolicitadoPorId).DefaultIfEmpty()
            //            from d in _db.PostsOculto
            //                .Where(m => m.PostPerfilId == c.PostPerfilId).DefaultIfEmpty()
            //            where b.StatusSolicitacao == SolicitacaoAmizade.Aprovado
            //            && b.SolicitadoParaId == usuarioId
            //            && c.Der == null
            //            && (d.StatusPost == StatusPostOculto.Visivel || d == null)
            //            select c)
            //    .OrderByDescending(x => x.DataPost)
            //    .Include("Usuario")
            //    .Include("Usuario.Perfil")
            //    .ToList();

            #endregion

            return query;
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
