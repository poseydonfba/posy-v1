using CommonServiceLocator;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using POSY.App.Service;
using POSY.Domain.Interfaces;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Infra.CrossCutting.Identity.Configuration;
using POSY.Infra.CrossCutting.Identity.Context;
using POSY.Infra.CrossCutting.Identity.Model;
using POSY.Infra.Data.Context;
using POSY.Infra.Data.Interfaces;
using POSY.Infra.Data.Repository;
using POSY.Infra.Data.UnitOfWork;
using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.ServiceLocation;

namespace POSY.Infra.CrossCutting.IoC
{
    public static class Bootstrapper
    {
        private static readonly IUnityContainer _container = new UnityContainer();

        public static void Initialize()
        {
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(_container));

            _container.RegisterType<IDatabaseContext, DatabaseContext>(new HierarchicalLifetimeManager());
            _container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            _container.RegisterType<IUsuarioRepository, UsuarioRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IUsuarioService, UsuarioService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IAmizadeRepository, AmizadeRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IAmizadeService, AmizadeService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPerfilRepository, PerfilRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPerfilService, PerfilService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IVisitantePerfilRepository, VisitantePerfilRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IVisitantePerfilService, VisitantePerfilService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPostPerfilRepository, PostPerfilRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPostPerfilService, PostPerfilService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPostPerfilBloqueadoRepository, PostPerfilBloqueadoRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPostPerfilBloqueadoService, PostPerfilBloqueadoService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPostOcultoRepository, PostOcultoRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPostOcultoService, PostOcultoService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPostPerfilComentarioRepository, PostPerfilComentarioRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPostPerfilComentarioService, PostPerfilComentarioService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPrivacidadeRepository, PrivacidadeRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IPrivacidadeService, PrivacidadeService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IRecadoRepository, RecadoRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IRecadoService, RecadoService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IRecadoComentarioRepository, RecadoComentarioRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IRecadoComentarioService, RecadoComentarioService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IVideoRepository, VideoRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IVideoComentarioRepository, VideoComentarioRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IVideoService, VideoService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IVideoComentarioService, VideoComentarioService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IDepoimentoRepository, DepoimentoRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IDepoimentoService, DepoimentoService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IComunidadeRepository, ComunidadeRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IComunidadeService, ComunidadeService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IMembroRepository, MembroRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IMembroService, MembroService>(new HierarchicalLifetimeManager());
            _container.RegisterType<IModeradorRepository, ModeradorRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<IModeradorService, ModeradorService>(new HierarchicalLifetimeManager());
            _container.RegisterType<ITopicoRepository, TopicoRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<ITopicoService, TopicoService>(new HierarchicalLifetimeManager());
            _container.RegisterType<ITopicoPostRepository, TopicoPostRepository>(new HierarchicalLifetimeManager());
            _container.RegisterType<ITopicoPostService, TopicoPostService>(new HierarchicalLifetimeManager());

            var accountInjectionConstructor = new InjectionConstructor(new ApplicationDbContext());
            _container.RegisterType<IUserStore<ApplicationUser, Guid>, UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>>(accountInjectionConstructor);
            _container.RegisterType<IRoleStore<ApplicationRole, Guid>, RoleStore<ApplicationRole, Guid, ApplicationUserRole>>(accountInjectionConstructor);

            //_container.RegisterType<IUserStore<ApplicationUser, Guid>, UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>>(new HierarchicalLifetimeManager());
            //_container.RegisterType<IRoleStore<ApplicationRole, Guid>, RoleStore<ApplicationRole, Guid, ApplicationUserRole>>(new HierarchicalLifetimeManager());

            //container.RegisterPerWebRequest<IUserStore<ApplicationUser, Guid>>(
            //    () => new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(new ApplicationDbContext()));
            ////container.RegisterPerWebRequest<IRoleStore<ApplicationRole, Guid>>(() => new ApplicationRoleStore());
            //container.RegisterPerWebRequest<IRoleStore<ApplicationRole, Guid>>(
            //    () => new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(new ApplicationDbContext()));

            //_container.RegisterType<IUserStore<ApplicationUser, Guid>>(new HierarchicalLifetimeManager());
            //_container.RegisterType<IRoleStore<ApplicationRole, Guid>>(new HierarchicalLifetimeManager());
            _container.RegisterType<ApplicationRoleManager>(new HierarchicalLifetimeManager());
            _container.RegisterType<ApplicationUserManager>(new HierarchicalLifetimeManager());
            _container.RegisterType<ApplicationSignInManager>(new HierarchicalLifetimeManager());
        }

        public static void TearDown()
        {
            _container.Dispose();
        }
    }
}
