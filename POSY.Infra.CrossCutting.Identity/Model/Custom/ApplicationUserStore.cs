using Microsoft.AspNet.Identity.EntityFramework;
using POSY.Infra.CrossCutting.Identity.Context;
using System;

namespace POSY.Infra.CrossCutting.Identity.Model.Custom
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
