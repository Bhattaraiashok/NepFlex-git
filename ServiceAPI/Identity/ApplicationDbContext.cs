﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Nepflex.ServiceAPI.Models;
using NepFlex.Core.Entities.ResourceModels;

namespace Nepflex.ServiceAPI.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("OnlinePasalContext", throwIfV1Schema: false)
        {
        }
    }
}