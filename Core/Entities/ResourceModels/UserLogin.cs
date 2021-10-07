using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.Core.Entities.ResourceModels
{
    public class UserLogin
    {
        public string UserID { get; set; }
        public string UserPSWD { get; set; }
        public string UI { get { return "craig"; } }
        public bool? IsUserSeller { get; set; } //on the time of regester let user to choose if the same id is regester as seller too.
    }

    public class UserLoginResponse
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
