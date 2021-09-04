using Microsoft.Owin;
using System.Web;
using System.Web.Mvc;

namespace POSY.Infra.CrossCutting.Identity.Configuration
{
    public class UserLogged
    {
        public static void GetUser()
        {
            //var username = System.Web.HttpContext.Current.GetOwinContext()
            //        .GetUserManager<ApplicationUserManager>();
            //var user = System.Web.HttpContext.Current.GetOwinContext().Authentication;
        }
    }
}