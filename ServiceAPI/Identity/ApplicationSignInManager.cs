﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Nepflex.ServiceAPI.Models;

namespace Nepflex.ServiceAPI.Identity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }
    }
}