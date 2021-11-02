using Microsoft.AspNet.Identity.EntityFramework;
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