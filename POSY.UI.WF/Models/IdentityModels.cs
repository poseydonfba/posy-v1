﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using POSY.UI.WF.Models;

namespace POSY.UI.WF.Models
{
    // You can add User data for the user by adding more properties to your User class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public ClaimsIdentity GenerateUserIdentity(ApplicationUserManager manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }

    //    public Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
    //    {
    //        return Task.FromResult(GenerateUserIdentity(manager));
    //    }
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}

//#region Helpers
//namespace POSY.UI.WF
//{
//    public static class IdentityHelper
//    {
//    }
//}
//#endregion
