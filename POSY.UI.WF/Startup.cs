using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POSY.UI.WF.Startup))]
namespace POSY.UI.WF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
