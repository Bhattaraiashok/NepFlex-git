using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NepFlex.Core.Entities.ResourceModels.Security;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NepFlex.Core.Entities.ResourceModels
{
    [Serializable]
    public class UserRegisterRequest : RequestBase
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string EnteredPassword { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool? IsUserAgreementChecked { get; set; }
        public string UI { get { return CONSTUINAME.UI_NAME; } }
        public bool UserRegisterRequestValidation()
        {
            if (string.IsNullOrWhiteSpace(Email) && string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(EnteredPassword))
            {
                return false;
            }
            return true;
        }
    }

    [Serializable]
    public class UserUpdateRequest : RequestBase
    {
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string ProfileImage { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public bool? ShowPhonenumber { get; set; }
        public bool? IsUserSeller { get; set; }
        public string FieldUpdateRequest { get; set; }
    }

    [Serializable]
    public class CompanyRegisterRequest : RequestBase
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

    [Serializable]
    public class CompanyUpdateRequest : RequestBase
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneCountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public bool? ShowPhonenumber { get; set; }
        public bool? IsGOVRegisteredCompany { get; set; }
        public bool? IsCompanyActive { get; set; }
        public string CompanyEmailID { get; set; }
        public string FieldUpdateRequest { get; set; }
    }

    [Serializable]
    public class UserLoginRequest : RequestBase
    {
        public string UserName { get; set; }
        public string UserPSWD { get; set; }
        public string UI { get { return CONSTUINAME.UI_NAME; } }
        public bool IsRememberMe { get; set; }
        public string IsUserSeller { get; set; } //on the time of regester let user to choose if the same id is regester as seller too.
        public bool UserNameIsEmail
        {
            get
            {
                var selectionUsingEmail = UserName.Contains("@");
                return selectionUsingEmail;
            }
        }
    }

    [Serializable]
    public class UserLoginResponse : UserUtilityModal
    {
        public string UserID { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime DateJoined { get; set; }
        public string UserGuid { get; set; }
    }

    [Serializable]
    public class SignInStatusResponse : ResponseStatus
    {
        public SignInStatus SignInStatus { get; set; }
    }

    [Serializable]
    public class UserPassword
    {
        // UserPassword object: do not feed into any other models
        public string UserID { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool PasswordIsActive { get; set; }
        public string PasswordSaltKey { get; set; }
        public DateTime PasswordExpiresOn { get; set; }
        public bool PasswordIsCompromised { get; set; }
        public int PasswordWrongAttemptPerSession { get; set; }
        public PasswordFormat PasswordFormat { get; set; }

    }

    [Serializable]
    public class UID : UserUtilityModal
    {
        public string UserID { get; set; }
    }

    [Serializable]
    public class RequestBase
    {
        public string UserEmail { get; set; }
        public string UID { get; set; }
        public string Page { get; set; }
        public string TraceID { get; set; }
        public string SessionID { get; set; }
        public string Component { get; set; }
        public string DeviceType { get; set; }
        public bool? IsLog { get; set; }
    }

    [Serializable]
    public class UserUtilityModal : ResponseStatus
    {
        public bool UserIsLoggedIn { get; set; }
        public bool IsAuthenticated { get; set; }
        public string IPFound { get; set; }
        public string LocationFound { get; set; }
        public string FE_SessionID { get; set; }
        public string BE_SessionID { get; set; }
        public List<string> RestrictPages { get; set; }
        public string AssignedAuthToken { get; set; }
        public string UserHasSellerAC { get; set; }
        public string UserRole { get; set; }
        public DateTime? CurrentTimeStamp { get { return DateTime.Now; } }
    }


    [Serializable]
    public class CONST_Update_FormControlName
    {
        public readonly static string firstname = "firstname";
        public readonly static string middlename = "middlename";
        public readonly static string lastname = "lastname";
        public readonly static string useremail = "useremail";
        public readonly static string usercountryCode = "usercountryCode";
        public readonly static string userphonenumber = "userphonenumber";
        public readonly static string username = "username";
        public readonly static string password = "password";
        public readonly static string userIsSeller = "userIsSeller";
        public readonly static string showOrHideUserPhonenumber = "showOrHideUserPhonenumber";
        public readonly static string isUserAgreementChecked = "isUserAgreementChecked";
        public readonly static string companyname = "companyname";
        public readonly static string address = "address";
        public readonly static string address2 = "address2";
        public readonly static string city = "city";
        public readonly static string state = "state";
        public readonly static string zipcode = "zipcode";
        public readonly static string companyemail = "companyemail";
        public readonly static string companycountryCode = "companycountryCode";
        public readonly static string companyphonenumber = "companyphonenumber";
        public readonly static string isCompanyRegistered = "isCompanyRegistered";
        public readonly static string showOrHideCompanyPhonenumber = "showOrHideCompanyPhonenumber";
        public readonly static string profilephoto = "profilephoto";
    }


    [Serializable]
    public enum PasswordFormat
    {
        CLEAR = 0,
        HASHED = 1,
        ENCRYPTED = 2,
        NULL = 3

    }

    public enum SignInStatus
    {
        SUCCESS = 0,
        LOCKEDOUT = 1,
        REQUIRESVERIFICATIONS = 2,
        FAILURE = 3,
        INACTIVE = 4
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
}
