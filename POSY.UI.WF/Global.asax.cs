using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using POSY.App.Service;
using POSY.Domain.Interfaces;
using POSY.Domain.Interfaces.Repository;
using POSY.Domain.Interfaces.Service;
using POSY.Infra.CrossCutting.Identity.Configuration;
using POSY.Infra.CrossCutting.Identity.Context;
using POSY.Infra.CrossCutting.Identity.Model;
using POSY.Infra.CrossCutting.Identity.Model.Custom;
using POSY.Infra.Data.Context;
using POSY.Infra.Data.Interfaces;
using POSY.Infra.Data.Repository;
using POSY.Infra.Data.UnitOfWork;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Diagnostics;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;

namespace POSY.UI.WF
{
    public class Global : HttpApplication
    {
        public static Container container;

        public static void InitializeHandler(IHttpHandler handler)
        {
            container.GetRegistration(handler.GetType(), true).Registration
                .InitializeInstance(handler);
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrap();
        }

        private static void Bootstrap()
        {
            // 1. Create a new Simple Injector container.
            var container = new Container();

            // Register a custom PropertySelectionBehavior to enable property injection.
            container.Options.PropertySelectionBehavior =
                new ImportAttributePropertySelectionBehavior();

            // 2. Configure the container (register)
            //container.Register(() =>
            //{
            //    if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null && container.IsVerifying())
            //    {
            //        return new OwinContext();
            //    }
            //    return HttpContext.Current.GetOwinContext();
            //});

            #region REGISTER

            container.RegisterPerWebRequest<IDatabaseContext, DatabaseContext>();
            container.RegisterPerWebRequest<IUnitOfWork, UnitOfWork>();

            container.RegisterPerWebRequest<IUsuarioRepository, UsuarioRepository>();
            container.RegisterPerWebRequest<IUsuarioService, UsuarioService>();
            container.RegisterPerWebRequest<IAmizadeRepository, AmizadeRepository>();
            container.RegisterPerWebRequest<IAmizadeService, AmizadeService>();
            container.RegisterPerWebRequest<IPerfilRepository, PerfilRepository>();
            container.RegisterPerWebRequest<IPerfilService, PerfilService>();
            container.RegisterPerWebRequest<IVisitantePerfilRepository, VisitantePerfilRepository>();
            container.RegisterPerWebRequest<IVisitantePerfilService, VisitantePerfilService>();
            container.RegisterPerWebRequest<IPostPerfilRepository, PostPerfilRepository>();
            container.RegisterPerWebRequest<IPostPerfilService, PostPerfilService>();
            container.RegisterPerWebRequest<IPostPerfilBloqueadoRepository, PostPerfilBloqueadoRepository>();
            container.RegisterPerWebRequest<IPostPerfilBloqueadoService, PostPerfilBloqueadoService>();
            container.RegisterPerWebRequest<IPostOcultoRepository, PostOcultoRepository>();
            container.RegisterPerWebRequest<IPostOcultoService, PostOcultoService>();
            container.RegisterPerWebRequest<IPostPerfilComentarioRepository, PostPerfilComentarioRepository>();
            container.RegisterPerWebRequest<IPostPerfilComentarioService, PostPerfilComentarioService>();
            container.RegisterPerWebRequest<IPrivacidadeRepository, PrivacidadeRepository>();
            container.RegisterPerWebRequest<IPrivacidadeService, PrivacidadeService>();
            container.RegisterPerWebRequest<IRecadoRepository, RecadoRepository>();
            container.RegisterPerWebRequest<IRecadoService, RecadoService>();
            container.RegisterPerWebRequest<IRecadoComentarioRepository, RecadoComentarioRepository>();
            container.RegisterPerWebRequest<IRecadoComentarioService, RecadoComentarioService>();
            container.RegisterPerWebRequest<IVideoRepository, VideoRepository>();
            container.RegisterPerWebRequest<IVideoComentarioRepository, VideoComentarioRepository>();
            container.RegisterPerWebRequest<IVideoService, VideoService>();
            container.RegisterPerWebRequest<IVideoComentarioService, VideoComentarioService>();
            container.RegisterPerWebRequest<IDepoimentoRepository, DepoimentoRepository>();
            container.RegisterPerWebRequest<IDepoimentoService, DepoimentoService>();
            container.RegisterPerWebRequest<IComunidadeRepository, ComunidadeRepository>();
            container.RegisterPerWebRequest<IComunidadeService, ComunidadeService>();
            container.RegisterPerWebRequest<IMembroRepository, MembroRepository>();
            container.RegisterPerWebRequest<IMembroService, MembroService>();
            container.RegisterPerWebRequest<IModeradorRepository, ModeradorRepository>();
            container.RegisterPerWebRequest<IModeradorService, ModeradorService>();
            container.RegisterPerWebRequest<ITopicoRepository, TopicoRepository>();
            container.RegisterPerWebRequest<ITopicoService, TopicoService>();
            container.RegisterPerWebRequest<ITopicoPostRepository, TopicoPostRepository>();
            container.RegisterPerWebRequest<ITopicoPostService, TopicoPostService>();

            container.RegisterPerWebRequest<ApplicationDbContext>();
            //container.RegisterPerWebRequest<ApplicationUserStore>(() => new ApplicationUserStore(container.GetInstance<ApplicationDbContext>()));
            //container.RegisterPerWebRequest<ApplicationRoleStore>(() => new ApplicationRoleStore(container.GetInstance<ApplicationDbContext>()));

            container.RegisterPerWebRequest<IUserStore<ApplicationUser, Guid>>(() => new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(
                container.GetInstance<ApplicationDbContext>()));

            container.RegisterPerWebRequest<IRoleStore<ApplicationRole, Guid>>(() => new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(
                container.GetInstance<ApplicationDbContext>()));

            container.RegisterPerWebRequest<ApplicationSignInManager>(() => new ApplicationSignInManager(
                container.GetInstance<ApplicationUserManager>(),
                container.GetInstance<IAuthenticationManager>()));

            container.RegisterPerWebRequest<ApplicationUserManager>(() => new ApplicationUserManager(
                container.GetInstance<IUserStore<ApplicationUser, Guid>>()));
            //container.RegisterPerWebRequest<ApplicationUserManager>(() => new ApplicationUserManager(container.GetInstance<ApplicationUserStore>()));

            container.RegisterPerWebRequest<IAuthenticationManager>(() => HttpContext.Current.GetOwinContext().Authentication);

            ////container.RegisterPerWebRequest<DatabaseContext>();
            //container.RegisterPerWebRequest<IUserStore<ApplicationUser, Guid>>(
            //    () => new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(new ApplicationDbContext()));
            ////container.RegisterPerWebRequest<IRoleStore<ApplicationRole, Guid>>(() => new ApplicationRoleStore());
            //container.RegisterPerWebRequest<IRoleStore<ApplicationRole, Guid>>(
            //    () => new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(new ApplicationDbContext()));
            //container.RegisterPerWebRequest<ApplicationRoleManager>();
            //container.RegisterPerWebRequest<ApplicationUserManager>();
            //container.RegisterPerWebRequest<ApplicationSignInManager>();

            #endregion

            // Register your Page classes to allow them to be verified and diagnosed.
            RegisterWebPages(container);

            // 3. Store the container for use by Page classes.
            Global.container = container;

            // 3. Verify the container's configuration.
            //container.Verify();
        }

        private static void RegisterWebPages(Container container)
        {
            var pageTypes =
                from assembly in BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                where !assembly.IsDynamic
                where !assembly.GlobalAssemblyCache
                from type in assembly.GetExportedTypes()
                where type.IsSubclassOf(typeof(Page))
                where !type.IsAbstract && !type.IsGenericType
                select type;

            foreach (Type type in pageTypes)
            {
                var reg = Lifestyle.Transient.CreateRegistration(type, container);
                reg.SuppressDiagnosticWarning(
                    DiagnosticType.DisposableTransientComponent,
                    "ASP.NET creates and disposes page classes for us.");
                container.AddRegistration(type, reg);
            }
        }

        class ImportAttributePropertySelectionBehavior : IPropertySelectionBehavior
        {
            public bool SelectProperty(Type serviceType, PropertyInfo propertyInfo)
            {
                // Makes use of the System.ComponentModel.Composition assembly
                return typeof(Page).IsAssignableFrom(serviceType) &&
                    propertyInfo.GetCustomAttributes<ImportAttribute>().Any();
            }
        }
    }
}