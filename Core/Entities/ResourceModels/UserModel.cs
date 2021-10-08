using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.Core.Entities.ResourceModels
{
    public class UserRegister
    {
        public UserRegisterRequest UserDetail { get; set; }
        public CompanyRegisterRequest CompanyDetails { get; set; }
    }

    public class UserRegisterRequest
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string UI { get; set; }
        public bool IsUserSeller { get; set; }
        public string PhoneNumber { get; set; }
        public bool? ShowPhonenumber { get; set; }
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
    public class UserRegisterResponse : RequestStatus
    {
        public string response { get; set; }
    }

    public class UserLogin
    {
        public string UserID { get; set; }
        public string UserPSWD { get; set; }
        public string UI { get { return "craig"; } }
        public string IsUserSeller { get; set; } //on the time of regester let user to choose if the same id is regester as seller too.
    }

    public class UserLoginResponse: RequestStatus
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
