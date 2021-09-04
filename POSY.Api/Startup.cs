using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using POSY.Api;
using POSY.Api.App_Start;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace POSY.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //HttpConfiguration config = new HttpConfiguration();

            //SimpleInjectorInitializer.Initialize();
            //WebApiConfig.Register(config);
            ConfigureAuth(app);

            //app.UseCors(CorsOptions.AllowAll);
            //app.UseWebApi(config);
        }
    }
}
