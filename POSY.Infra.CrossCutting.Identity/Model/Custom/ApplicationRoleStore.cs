using Microsoft.AspNet.Identity.EntityFramework;
using POSY.Infra.CrossCutting.Identity.Context;
using System;

namespace POSY.Infra.CrossCutting.Identity.Model.Custom
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, Guid, ApplicationUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context) 
            : base(context) 
        {
        }
    }
}
