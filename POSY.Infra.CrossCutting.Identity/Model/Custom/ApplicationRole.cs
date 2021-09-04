using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace POSY.Infra.CrossCutting.Identity.Model
{
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole>
    {
        public ApplicationRole() { }
        public ApplicationRole(string name) { Name = name; }
    }
}
