using System.Data.Entity;
using POSY.Domain.Entities;
using POSY.Infra.Data.EntityConfig;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.Linq;
using System.Data.Entity.Validation;
using POSY.Infra.Data.Interfaces;
using POSY.Infra.CrossCutting.Common;

namespace POSY.Infra.Data.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false; // Traz os dados por parte
            Configuration.ProxyCreationEnabled = false; // Evita erro de serialização json
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Privacidade> Privacidades { get; set; }
        public DbSet<Amizade> Amizades { get; set; }
        public DbSet<VisitantePerfil> VisitantesPerfil { get; set; }
        public DbSet<PostPerfil> PostsPerfil { get; set; }
        public DbSet<PostPerfilBloqueado> PostsPerfilBloqueado { get; set; }
        public DbSet<PostOculto> PostsOculto { get; set; }
        public DbSet<PostPerfilComentario> PostsPerfilComentario { get; set; }
        public DbSet<Recado> Recados { get; set; }
        public DbSet<RecadoComentario> RecadoComentarios { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoComentario> VideoComentarios { get; set; }
        public DbSet<Depoimento> Depoimentos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Comunidade> Comunidades { get; set; }
        public DbSet<Membro> Membros { get; set; }
        public DbSet<Moderador> Moderadores { get; set; }
        public DbSet<Topico> Topicos { get; set; }
        public DbSet<TopicoPost> TopicosPosts { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // Não cria tabela no plural
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); // Evita deletar em cascata
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); // Evita deletar em cascata

            // Força a configurar quando uma propriedade for alguma coisa mais Id no final como chave primaria
            modelBuilder.Properties()
                .Where(x => x.Name == x.ReflectedType.Name + "Id")
                .Configure(x => x.IsKey());

            // Muda valor padrão de uma string para varchar(100)
            modelBuilder.Properties<string>()
                .Configure(x => x.HasColumnType("varchar"));

            modelBuilder.Properties<DateTime>()
                .Configure(x => x.HasColumnType("datetime2"));

            modelBuilder.Properties<string>()
                .Configure(x => x.HasMaxLength(100));

            //modelBuilder.Properties<byte[]>()
            //    .Configure(x => x.HasColumnType("varbinary(MAX)"));

            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new PerfilConfig());
            modelBuilder.Configurations.Add(new PrivacidadeConfig());
            modelBuilder.Configurations.Add(new AmizadeConfig());
            modelBuilder.Configurations.Add(new VisitantePerfilConfig());
            modelBuilder.Configurations.Add(new PostPerfilConfig());
            modelBuilder.Configurations.Add(new PostPerfilBloqueadoConfig());
            modelBuilder.Configurations.Add(new PostOcultoConfig());
            modelBuilder.Configurations.Add(new RecadoConfig());
            modelBuilder.Configurations.Add(new RecadoComentarioConfig());
            modelBuilder.Configurations.Add(new VideoConfig());
            modelBuilder.Configurations.Add(new VideoComentarioConfig());
            modelBuilder.Configurations.Add(new DepoimentoConfig());
            modelBuilder.Configurations.Add(new CategoriaConfig());
            modelBuilder.Configurations.Add(new ComunidadeConfig());
            modelBuilder.Configurations.Add(new MembroConfig());
            modelBuilder.Configurations.Add(new ModeradorConfig());
            modelBuilder.Configurations.Add(new TopicoConfig());
            modelBuilder.Configurations.Add(new TopicoPostConfig());
            modelBuilder.Configurations.Add(new ConnectionConfig());
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Dir") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("Dir").CurrentValue = ConfigurationBase.DataAtual;

                if (entry.State == EntityState.Modified)
                    entry.Property("Dir").IsModified = false;
            }

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}