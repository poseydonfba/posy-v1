﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using POSY.Infra.CrossCutting.Identity.Configuration;
using POSY.Infra.CrossCutting.Identity.Model;
using POSY.MVC.Configuration;
using POSY.MVC.Helpers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace POSY.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await _userManager.FindAsync(model.Email, model.Password);
                    if (!user.EmailConfirmed)
                    {
                        TempData["AvisoEmail"] = "Usuário não confirmado, verifique seu e-mail.";
                    }
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Login ou Senha incorretos.");
                    return View(model);
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            var clientKey = Request.Browser.Type;
            await _userManager.SignInClientAsync(user, clientKey);

            // Zerando contador de logins errados.
            await _userManager.ResetAccessFailedCountAsync(user.Id);

            // Coletando Claims externos (se houver)
            ClaimsIdentity ext = await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn
                (
                    new AuthenticationProperties { IsPersistent = isPersistent },
                    // Criação da instancia do Identity e atribuição dos Claims
                    await user.GenerateUserIdentityAsync(_userManager, ext)
                );
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, Guid userId)
        {
            // Requer que o usuario já tenha feito um login por senha.
            if (!await _signInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(await _signInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "DEMO: Caso o código não chegue via " + provider + " o código é: ";
                ViewBag.CodigoAcesso = await _userManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, UserId = userId });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = _userManager.FindByIdAsync(model.UserId);
                    await SignInAsync(user.Result, false);
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Código Inválido.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    await _userManager.SendEmailAsync(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");
                    ViewBag.Link = callbackUrl;
                    return View("DisplayEmail");
                }
                AddErrors(result);
            }

            // No caso de falha, reexibir a view. 
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(Guid userId, string code)
        {
            if (userId == default(Guid) || code == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Não revelar se o usuario nao existe ou nao esta confirmado
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Esqueci minha senha", "Por favor altere sua senha clicando aqui: <a href='" + callbackUrl + "'></a>");
                ViewBag.Link = callbackUrl;
                ViewBag.Status = "DEMO: Caso o link não chegue: ";
                ViewBag.LinkAcesso = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }

            // No caso de falha, reexibir a view. 
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Não revelar se o usuario nao existe ou nao esta confirmado
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var userId = await _signInManager.GetVerifiedUserIdAsync();
            if (userId == default(Guid))
            {
                return View("Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, UserId = userId });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Gerar o token e enviar
            if (!await _signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, userId = model.UserId });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _userManager.FindAsync(loginInfo.Login);

            // Logar caso haja um login externo e já esteja logado neste provedor de login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);

            switch (result)
            {
                case SignInStatus.Success:
                    var userext = _userManager.FindByEmailAsync(user.Email);
                    await SignInAsync(userext.Result, false);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    // Se ele nao tem uma conta solicite que crie uma

                    if (loginInfo.Login.LoginProvider.ToLower() == "facebook")
                    {
                        var externalIdentity = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
                        var email = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:email").Value;
                        var firstName = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:first_name").Value;
                        var lastName = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "urn:facebook:last_name").Value;

                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email, LastName = lastName, Name = firstName });
                    }
                    else if (loginInfo.Login.LoginProvider.ToLower() == "google")
                    {
                        var externalIdentity = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
                        var email = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                        var firstName = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;
                        var lastName = externalIdentity.Result.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname").Value;

                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email, LastName = lastName, Name = firstName });
                    }
                    else if (loginInfo.Login.LoginProvider.ToLower() == "twitter")
                    {
                        var externalIdentity = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
                        var access_token = externalIdentity.Result.Claims.Where(x => x.Type == "urn:twitter:access_token").Select(x => x.Value).FirstOrDefault();
                        var access_secret = externalIdentity.Result.Claims.Where(x => x.Type == "urn:twitter:access_secret").Select(x => x.Value).FirstOrDefault();

                        var response = await TwitterHelper.TwitterLogin(access_token, access_secret, TwitterConfiguration.ConsumerKey, TwitterConfiguration.ConsumerSecret);

                        var email = response.email;
                        var firstName = response.name;
                        var lastName = string.Empty;

                        ViewBag.ReturnUrl = returnUrl;
                        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email, LastName = lastName, Name = firstName });
                    }
                    else
                    {
                        return View("Error");
                    }
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Pegar a informação do login externo.
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        var userext = _userManager.FindByEmailAsync(model.Email);
                        await SignInAsync(userext.Result, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            await SignOutAsync();
            //AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        private async Task SignOutAsync()
        {
            var clientKey = Request.Browser.Type;
            var user = _userManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            await _userManager.SignOutClientAsync(user, clientKey);
            AuthenticationManager.SignOut();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignOutEverywhere()
        {
            _userManager.UpdateSecurityStamp(Guid.Parse(User.Identity.GetUserId()));
            await SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOutClient(int clientId)
        {
            var user = _userManager.FindById(Guid.Parse(User.Identity.GetUserId()));
            var client = user.UsuarioClientes.SingleOrDefault(c => c.UsuarioClienteId == clientId);
            if (client != null)
            {
                user.UsuarioClientes.Remove(client);
            }
            _userManager.Update(user);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                //// this line fixed the problem with returing null
                //context.RequestContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;

                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}