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
using SimpleInjector;
using System;

namespace POSY.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
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

            //container.RegisterPerWebRequest<DatabaseContext>();
            container.RegisterPerWebRequest<IUserStore<ApplicationUser, Guid>>(
                () => new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(new ApplicationDbContext()));
            //container.RegisterPerWebRequest<IRoleStore<ApplicationRole, Guid>>(() => new ApplicationRoleStore());
            container.RegisterPerWebRequest<IRoleStore<ApplicationRole, Guid>>(
                () => new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(new ApplicationDbContext()));
            container.RegisterPerWebRequest<ApplicationRoleManager>();
            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();
        }
    }
}
