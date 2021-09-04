using System;
using POSY.Infra.CrossCutting.Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
    
namespace POSY.Infra.CrossCutting.Identity.Context
{
    public class ApplicationDbContext : 
        IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IDisposable
    {
        public ApplicationDbContext()
            : base("DefaultConnection"/*, throwIfV1Schema: false*/)
        {
        }

        public DbSet<UsuarioCliente> UsuarioCliente { get; set; }

        public DbSet<Claims> Claims { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<IdentityUser>().ToTable("Usuario").Property(p => p.Id).HasColumnName("UsuarioId");
            //modelBuilder.Entity<ApplicationUser>().ToTable("Usuario").Property(p => p.Id).HasColumnName("UsuarioId");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("UsuarioFuncao").Property(p => p.UserId).HasColumnName("UsuarioId");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("UsuarioFuncao").Property(p => p.RoleId).HasColumnName("FuncaoId");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("UsuarioLogin").Property(p => p.UserId).HasColumnName("UsuarioId");
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("UsuarioClaim").Property(p => p.Id).HasColumnName("UsuarioClaimId");
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("UsuarioClaim").Property(p => p.UserId).HasColumnName("UsuarioId");
            //modelBuilder.Entity<IdentityRole>().ToTable("Funcao").Property(p => p.Id).HasColumnName("FuncaoId");
            //modelBuilder.Entity<UsuarioCliente>().HasRequired(x => x.Usuario);

            modelBuilder.Entity<ApplicationUser>().ToTable("Usuario").Property(p => p.Id).HasColumnName("UsuarioId");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UsuarioFuncao").Property(p => p.UserId).HasColumnName("UsuarioId");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UsuarioFuncao").Property(p => p.RoleId).HasColumnName("FuncaoId");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UsuarioLogin").Property(p => p.UserId).HasColumnName("UsuarioId");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UsuarioClaim").Property(p => p.Id).HasColumnName("UsuarioClaimId");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UsuarioClaim").Property(p => p.UserId).HasColumnName("UsuarioId");
            modelBuilder.Entity<ApplicationRole>().ToTable("Funcao").Property(p => p.Id).HasColumnName("FuncaoId");
            //modelBuilder.Entity<UsuarioCliente>().HasRequired(x => x.Usuario);

            //modelBuilder.Entity<ApplicationUserLogin>().Map(c =>
            //{
            //    c.ToTable("UserLogin");
            //    c.Properties(p => new
            //    {
            //        p.UserId,
            //        p.LoginProvider,
            //        p.ProviderKey
            //    });
            //}).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

            //// Mapping for ApiRole
            //modelBuilder.Entity<ApplicationRole>().Map(c =>
            //{
            //    c.ToTable("Role");
            //    c.Property(p => p.Id).HasColumnName("RoleId");
            //    c.Properties(p => new
            //    {
            //        p.Name
            //    });
            //}).HasKey(p => p.Id);
            //modelBuilder.Entity<ApplicationRole>().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);

            //modelBuilder.Entity<ApplicationUser>().Map(c =>
            //{
            //    c.ToTable("User");
            //    c.Property(p => p.Id).HasColumnName("UserId");
            //    c.Properties(p => new
            //    {
            //        p.AccessFailedCount,
            //        p.Email,
            //        p.EmailConfirmed,
            //        p.PasswordHash,
            //        p.PhoneNumber,
            //        p.PhoneNumberConfirmed,
            //        p.TwoFactorEnabled,
            //        p.SecurityStamp,
            //        p.LockoutEnabled,
            //        p.LockoutEndDateUtc,
            //        p.UserName
            //    });
            //}).HasKey(c => c.Id);
            //modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            //modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            //modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);

            //modelBuilder.Entity<ApplicationUserRole>().Map(c =>
            //{
            //    c.ToTable("UserRole");
            //    c.Properties(p => new
            //    {
            //        p.UserId,
            //        p.RoleId
            //    });
            //})
            //.HasKey(c => new { c.UserId, c.RoleId });

            //modelBuilder.Entity<ApplicationUserClaim>().Map(c =>
            //{
            //    c.ToTable("UserClaim");
            //    c.Property(p => p.Id).HasColumnName("UserClaimId");
            //    c.Properties(p => new
            //    {
            //        p.UserId,
            //        p.ClaimValue,
            //        p.ClaimType
            //    });
            //}).HasKey(c => c.Id);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
