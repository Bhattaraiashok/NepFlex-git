using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.Core.Entities.ResourceModels
{
    public class UserRegister
    {
        public UserRegisterRequest UserDetail { get; set; }
        public CompanyRegisterRequest CompanyDetails { get; set; }
    }

    public class UserRegisterRequest : IdentityUser
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string PSWDHASH { get; set; }
        public string PSWDSALT { get; set; }
        public string UserEmail { get; set; }
        public string Address { get; set; }
        public string UI { get { return "craig"; } }
        public bool IsUserSeller { get; set; }
        public string PhoneCountryCode { get; set; }
        public override string PhoneNumber { get; set; }
        public bool? ShowPhonenumber { get; set; }
        public bool? IsUserAgreementChecked { get; set; }

        public override string Email => UserEmail;
        //public string passwordHash => PSWDHASH;
        public override bool LockoutEnabled { get => base.LockoutEnabled; set => base.LockoutEnabled = true; }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class CompanyRegisterRequest
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneCountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool? ShowPhonenumber { get; set; }
        public bool? IsGOVRegisteredCompany { get; set; }
        public bool? IsCompanyActive { get; set; }
        public string CompanyEmailID { get; set; }
    }

    public class UserLogin
    {
        public string UserID { get; set; }
        public string UserPSWD { get; set; }
        public string UI { get { return "craig"; } }
        public bool IsRememberMe { get; set; }
        public string IsUserSeller { get; set; } //on the time of regester let user to choose if the same id is regester as seller too.
    }

    public class UserLoginResponse : ResponseStatus
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DateJoined { get; set; }

        public string UserGuid { get; set; }
        public string _Auth { get; set; }
        public bool? IsAuthenticated { get; set; } // this needs to be true in order to be successfully login
        public string SessionID { get; set; }
        public DateTime? TimeStamp { get; set; }
    }
}
