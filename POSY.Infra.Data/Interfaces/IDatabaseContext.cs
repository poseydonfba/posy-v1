using POSY.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace POSY.Infra.Data.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Perfil> Perfis { get; set; }
        DbSet<Privacidade> Privacidades { get; set; }
        DbSet<Amizade> Amizades { get; set; }
        DbSet<VisitantePerfil> VisitantesPerfil { get; set; }
        DbSet<PostPerfil> PostsPerfil { get; set; }
        DbSet<PostPerfilBloqueado> PostsPerfilBloqueado { get; set; }
        DbSet<PostOculto> PostsOculto { get; set; }
        DbSet<PostPerfilComentario> PostsPerfilComentario { get; set; }
        DbSet<Recado> Recados { get; set; }
        DbSet<RecadoComentario> RecadoComentarios { get; set; }
        DbSet<Video> Videos { get; set; }
        DbSet<VideoComentario> VideoComentarios { get; set; }
        DbSet<Depoimento> Depoimentos { get; set; }
        DbSet<Categoria> Categorias { get; set; }
        DbSet<Comunidade> Comunidades { get; set; }
        DbSet<Membro> Membros { get; set; }
        DbSet<Moderador> Moderadores { get; set; }
        DbSet<Topico> Topicos { get; set; }
        DbSet<TopicoPost> TopicosPosts { get; set; }
        DbSet<Connection> Connections { get; set; }

        int SaveChanges();
        DbEntityEntry Entry(object entity);
        void Dispose();
    }
}
