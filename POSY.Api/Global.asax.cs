using POSY.Api.App_Start;
using System.Web.Http;

namespace POSY.Api
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}