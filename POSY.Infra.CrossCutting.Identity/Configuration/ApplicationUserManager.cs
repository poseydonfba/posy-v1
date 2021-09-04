using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using POSY.Infra.CrossCutting.Identity.Model;
using POSY.Infra.CrossCutting.Identity.Context;
using Microsoft.Owin.Security.DataProtection;

namespace POSY.Infra.CrossCutting.Identity.Configuration
{
    // Configuração do UserManager Customizado
    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, Guid> store)
            : base(store)
        {
            // SE FOR UM PROJETO MVC MANTER ESSE CÓDIGO NO CONSTRUTOR
            // SE FOR UM PROJETO WEBFORM NÃO MANTER ESSE CÓDIGO NO CONSTRUTOR
            #region UTILIZADO NA APLICAÇÂO MVC

            //var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

            //var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("YourAppName");
            //manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));

            // Configurando validator para nome de usuario
            UserValidator = new UserValidator<ApplicationUser, Guid>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Logica de validação e complexidade de senha
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Configuração de Lockout
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;

            // Registrando os providers para Two Factor.
            RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<ApplicationUser, Guid>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<ApplicationUser, Guid>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            // Definindo a classe de serviço de e-mail
            EmailService = new EmailService();

            // Definindo a classe de serviço de SMS
            SmsService = new SmsService();

            var provider = new DpapiDataProtectionProvider("POSY");
            var dataProtector = provider.Create("ASP.NET Identity");

            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, Guid>(dataProtector);

            //var dataProtectionProvider = options.DataProtectionProvider;

            //if (dataProtectionProvider != null)
            //{
            //    manager.UserTokenProvider =
            //        new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            //}
            
            #endregion
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            // SE FOR UM PROJETO MVC NÃO MANTER ESSE CÓDIGO NO CONSTRUTOR
            // SE FOR UM PROJETO WEBFORM MANTER ESSE CÓDIGO NO CONSTRUTOR
            #region UTILIZADO NA APLICACAO WEBFORMS

            var manager = new ApplicationUserManager(
                new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context.Get<ApplicationDbContext>()));

            //var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("YourAppName");
            //manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));

            // Configurando validator para nome de usuario
            manager.UserValidator = new UserValidator<ApplicationUser, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Logica de validação e complexidade de senha
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Configuração de Lockout
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Registrando os providers para Two Factor.
            manager.RegisterTwoFactorProvider("Código via SMS", new PhoneNumberTokenProvider<ApplicationUser, Guid>
            {
                MessageFormat = "Seu código de segurança é: {0}"
            });

            manager.RegisterTwoFactorProvider("Código via E-mail", new EmailTokenProvider<ApplicationUser, Guid>
            {
                Subject = "Código de Segurança",
                BodyFormat = "Seu código de segurança é: {0}"
            });

            // Definindo a classe de serviço de e-mail
            manager.EmailService = new EmailService();

            // Definindo a classe de serviço de SMS
            manager.SmsService = new SmsService();

            var provider = new DpapiDataProtectionProvider("POSY");
            var dataProtector = provider.Create("ASP.NET Identity");

            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, Guid>(dataProtector);

            //var dataProtectionProvider = options.DataProtectionProvider;

            //if (dataProtectionProvider != null)
            //{
            //    manager.UserTokenProvider =
            //        new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            //}

            return manager;

            #endregion
        }

        #region ASYNC

        public async Task<IdentityResult> SignInClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException("clientKey");
            }

            var client = user.UsuarioClientes.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client == null)
            {
                client = new UsuarioCliente() { ClientKey = clientKey };
                user.UsuarioClientes.Add(client);
            }

            var result = await UpdateAsync(user);
            user.CurrentClientId = client.UsuarioClienteId.ToString();
            return result;
        }

        // Metodo para login async que remove os dados Client conectado
        public async Task<IdentityResult> SignOutClientAsync(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException("clientKey");
            }

            var client = user.UsuarioClientes.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client != null)
            {
                user.UsuarioClientes.Remove(client);
            }

            user.CurrentClientId = null;
            return await UpdateAsync(user);
        }

        #endregion

        #region NOT ASYNC

        public IdentityResult SignInClient(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException("clientKey");
            }

            var client = user.UsuarioClientes.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client == null)
            {
                client = new UsuarioCliente() { ClientKey = clientKey };
                user.UsuarioClientes.Add(client);
            }

            IdentityResult result = this.Update(user);

            user.CurrentClientId = client.UsuarioClienteId.ToString();
            return result;
        }

        public IdentityResult SignOutClient(ApplicationUser user, string clientKey)
        {
            if (string.IsNullOrEmpty(clientKey))
            {
                throw new ArgumentNullException("clientKey");
            }

            var client = user.UsuarioClientes.SingleOrDefault(c => c.ClientKey == clientKey);
            if (client != null)
            {
                user.UsuarioClientes.Remove(client);
            }

            user.CurrentClientId = null;
            return this.Update(user);
        }

        #endregion
    }
}