﻿using DataAccess.Repositories;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Interfaces.Repositories;
using NepFlex.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public UserLoginResponse UserLoginProcess(UserLogin login)
        {
            var _login = new UserLoginResponse();

            ValidateUserReturnModel _login2 = new ValidateUserReturnModel();
            _login2 = _context.ValidateUser(login.UserID, login.UserPSWD, login.UI);

            _login.Email = _login2.ResultSet1.Select(x => x.Email).FirstOrDefault();
            //_login.UserID= _login2.ResultSet1.Select(x => x.Email).FirstOrDefault();
            _login.UserGuid = _login2.ResultSet1.Select(x => x.GUID).FirstOrDefault();
            _login._Auth = _login2.ResultSet1.Select(x => x.Lastname).FirstOrDefault(); // need to modify this
            _login.Firstname = _login2.ResultSet1.Select(x => x.Firstname).FirstOrDefault();
            SessionIDManager manager = new SessionIDManager();
            string newSessionId = manager.CreateSessionID(HttpContext.Current);
            _login.SessionID = newSessionId;
            return _login;
        }

        public bool UserRegistrationProcess(UserRegister req)
        {
            var _login = new UserRegisterResponse();
            var result = new RegisterUserReturnModel();
            if (req.UserDetail.IsUserSeller == true)
            {
                //saves user and company both
                result = _context.RegisterUser(
                   req.UserDetail.Username,
                   req.UserDetail.Firstname,
                   req.UserDetail.Middlename,
                   req.UserDetail.Lastname,
                   req.UserDetail.Password,
                   req.UserDetail.Email,
                   req.UserDetail.PhoneNumber,
                   req.UserDetail.ShowPhonenumber,
                   "Yes",
                   req.CompanyDetails.CompanyName,
                   req.CompanyDetails.Address,
                   req.CompanyDetails.PhoneCountryCode,
                   req.CompanyDetails.PhoneNumber,
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
                    req.UserDetail.Firstname,
                    req.UserDetail.Middlename,
                    req.UserDetail.Lastname,
                    req.UserDetail.Password,
                    req.UserDetail.Email,
                    req.UserDetail.PhoneNumber,
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

            _login.response = result.ResultSet5.ToString();

            var status = new RequestStatus();
            status = Utility.RequestStatus(_login.response);

            return status.IsSuccess;
        }
    }
}
