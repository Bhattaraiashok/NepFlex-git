using DataAccess.Repositories;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Interfaces.Repositories;
using NepFlex.DataAccess.Context;
using NepFlex.DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace NepFlex.DataAccess.Repositories
{
    public class LoginRepository : Repository<UserLoginResponse, int>, ILoginRepository
    {
        private readonly IOnlinePasalContext _context;

        public LoginRepository(IOnlinePasalContext context) : base(context)
        {
            _context = context;
        }
        public UserLoginResponse UserLoginProcess(UserLogin login, ApplicationUser identityVerifiedUser)
        {
            login.UserPSWD = identityVerifiedUser.PasswordHash;
            login.UserID = identityVerifiedUser.UserName;

            var _login = new UserLoginResponse();

            ValidateUserReturnModel _login2 = new ValidateUserReturnModel();

            _login2 = _context.ValidateUser(login.UserID, login.UserPSWD, login.UI);

            if (_login2.ResultSet1.Count > 0 && _login2.ResultSet1[0].UserID != 0 && _login2.ResultSet1[0].Username != null)
            {
                _login.Email = _login2.ResultSet1.Select(x => x.Email).FirstOrDefault();
                _login.UserGuid = _login2.ResultSet1.Select(x => x.GUID).FirstOrDefault();
                _login._Auth = _login2.ResultSet1.Select(x => x.Lastname).FirstOrDefault(); // need to modify this
                _login.Firstname = _login2.ResultSet1.Select(x => x.Firstname).FirstOrDefault();
                SessionIDManager manager = new SessionIDManager();
                string newSessionId = manager.CreateSessionID(HttpContext.Current);
                _login.SessionID = newSessionId;

                //append status
                _login = Utility.AppendStatus<UserLoginResponse>(ConstList.RES_OBJ_CONST_SUCCESS, _login);
            }
            else
            {
                _login = Utility.AppendStatus<UserLoginResponse>(ConstList.USER_LOGIN_CONST_FAILURE, _login);
            }
            return _login;
        }



        public ResponseStatus UserRegistrationProcess(UserRegister req, ApplicationUser req2)
        {
            var result = new List<RegisterUserReturnModel>();
            req.UserDetail.PSWDHASH = req2.PasswordHash;
            if (req.UserDetail.IsUserSeller)
            {
                //saves user and company both
                result = _context.RegisterUser(
                   req.UserDetail.Username,
                   req.UserDetail.PSWDHASH,
                   req.UserDetail.PSWDSALT,
                   req.UserDetail.Firstname,
                   req.UserDetail.Middlename,
                   req.UserDetail.Lastname,
                   req.UserDetail.Email,
                   req.UserDetail.PhoneCountryCode,
                   req.UserDetail.PhoneNumber != null ? req.UserDetail.PhoneNumber.Replace("[^a-zA-Z0-9]", "") : req.UserDetail.PhoneNumber,
                   req.UserDetail.ShowPhonenumber,
                   "Yes",
                   req.CompanyDetails.CompanyName,
                   req.CompanyDetails.Address,
                   req.CompanyDetails.PhoneCountryCode,
                   req.CompanyDetails.PhoneNumber != null ? req.CompanyDetails.PhoneNumber.Replace("[^a-zA-Z0-9]", "") : req.CompanyDetails.PhoneNumber,
                   req.CompanyDetails.IsGOVRegisteredCompany,
                   req.CompanyDetails.CompanyEmailID,
                   req.CompanyDetails.ShowPhonenumber,
                   req.UserDetail.UI);
            }
            else
            {
                // this will only save user info but not company
                result = _context.RegisterUser(
                    req.UserDetail.Username,
                    req.UserDetail.PSWDHASH,
                    req.UserDetail.PSWDSALT,
                    req.UserDetail.Firstname,
                    req.UserDetail.Middlename,
                    req.UserDetail.Lastname,
                    req.UserDetail.Email,
                    req.UserDetail.PhoneCountryCode,
                    req.UserDetail.PhoneNumber != null ? req.UserDetail.PhoneNumber.Replace("[^a-zA-Z0-9]", "") : req.UserDetail.PhoneNumber,
                    req.UserDetail.ShowPhonenumber,
                   "No",
                   "N/A",
                   "N/A",
                   "N/A",
                   "N/A",
                   false,
                   "N/A",
                   false,
                   req.UserDetail.UI);
            }

            ResponseStatus _status = new ResponseStatus
            {
                StrMessage = new List<string>()
            };

            _status.IsSuccess = result[0].Ver_Status == CONSTResponse.CONST_SUCCESS;
            _status.StrMessage.Add(result[0].VER_Detail);

            return _status;
        }
    }
}
