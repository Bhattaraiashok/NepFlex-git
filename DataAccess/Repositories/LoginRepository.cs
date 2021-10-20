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

            if (_login2.ResultSet1.Count > 0 && _login2.ResultSet1[0].UserID != null && _login2.ResultSet1[0].Username != null)
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

            //saves user initial registration
            result = _context.RegisterUser(
               req.UserDetail.Email,
               req.UserDetail.Username,
               req.UserDetail.PhoneNumber,
               req.UserDetail.PSWDHASH,
               req.UserDetail.PSWDSALT,
               req.UserDetail.UI);


            ResponseStatus _status = new ResponseStatus
            {
                StrMessage = new List<string>()
            };

            _status.IsSuccess = result[0].Ver_Status == CONSTResponse.CONST_SUCCESS;
            _status.StrMessage.Add(result[0].VER_Detail);

            return _status;
        }

        public ResponseStatus UpdateUser(UserRegister req, ApplicationUser req2)
        {
            ResponseStatus _status = new ResponseStatus
            {
                IsSuccess = false,
                StrMessage = new List<string>()
            };

            var _usrInfo = (from _usr in _context.Users
                            where _usr.UserName == req.UserDetail.Username &&
                             _usr.Email == req.UserDetail.Email && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr).FirstOrDefault();

            req.CompanyDetails.UserID = _usrInfo.UserId;

            if (_usrInfo != null)
            {
                //user
                if (_usrInfo.FirstName != req.UserDetail.Firstname)
                {
                    _usrInfo.FirstName = req.UserDetail.Firstname;
                }
                if (_usrInfo.MiddleName != req.UserDetail.Middlename)
                {
                    _usrInfo.MiddleName = req.UserDetail.Middlename;
                }
                if (_usrInfo.LastName != req.UserDetail.Lastname)
                {
                    _usrInfo.LastName = req.UserDetail.Lastname;
                }
                if (_usrInfo.PhNumber != req.UserDetail.PhoneNumber)
                {
                    _usrInfo.PhNumber = req.UserDetail.PhoneNumber;
                }
                if (_usrInfo.Email != req.UserDetail.Email)
                {
                    _usrInfo.Email = req.UserDetail.Email;
                }
                if (_usrInfo.ShowPhNumber != req.UserDetail.ShowPhonenumber)
                {
                    _usrInfo.ShowPhNumber = req.UserDetail.ShowPhonenumber;
                }
                if (_usrInfo.UserPhnCode != req.UserDetail.PhoneCountryCode)
                {
                    _usrInfo.UserPhnCode = req.UserDetail.PhoneCountryCode;
                }

                _usrInfo.UpdatedDate = DateTime.Now;


                var IsSeller = req.UserDetail.IsUserSeller == true ? "yes" : "no";
                if (_usrInfo.IsUserSeller != IsSeller)
                {
                    _usrInfo.IsUserSeller = IsSeller;
                }

                var returnRes = _context.SaveChanges();

                //company info
                if (_usrInfo.IsUserSeller == "yes")
                {
                    UpdateCompany(req, _usrInfo);
                }

                if (returnRes == CONSTResponse.INT_CONST_SUCCESS)
                {
                    _status = Utility.AppendStatus<ResponseStatus>(ConstList.USER_UPDATE_CONST_SUCCESS, _status);
                }
            }
            else
            {
                _status = Utility.AppendStatus<ResponseStatus>(ConstList.USER_PROFILE_CONST_FAILURE, _status);
            }

            return _status;
        }

        public ResponseStatus UpdateCompany(UserRegister req, User _userInfo)
        {
            var _companyInfo = (from _comp in _context.MasterCompanies
                                where _comp.UserId == req.CompanyDetails.UserID
                                select _comp).FirstOrDefault();

            ResponseStatus _status = new ResponseStatus
            {
                IsSuccess = false,
                StrMessage = new List<string>()
            };

            int returnResults = 0; //failure

            if (_companyInfo != null)
            {
                if (_companyInfo.CompanyName != req.CompanyDetails.CompanyName)
                {
                    _companyInfo.CompanyName = req.CompanyDetails.CompanyName;
                }
                if (_companyInfo.EmailId != req.CompanyDetails.CompanyEmailID)
                {
                    //_companyInfo.EmailId = req.CompanyDetails.CompanyEmailID; //do not do this since on modify user has to look up using existing email
                }
                if (_companyInfo.IsActive != req.CompanyDetails.IsCompanyActive)
                {
                    _companyInfo.IsActive = req.CompanyDetails.IsCompanyActive;
                }
                if (_companyInfo.IsGovRegistered != req.CompanyDetails.IsGOVRegisteredCompany)
                {
                    _companyInfo.IsGovRegistered = req.CompanyDetails.IsGOVRegisteredCompany;
                }
                if (_companyInfo.PhnCountryCode != req.CompanyDetails.PhoneCountryCode)
                {
                    _companyInfo.PhnCountryCode = req.CompanyDetails.PhoneCountryCode;
                }
                if (_companyInfo.PhNumber != req.CompanyDetails.PhoneNumber)
                {
                    _companyInfo.PhNumber = req.CompanyDetails.PhoneNumber;
                }

                if (_companyInfo.ShowPhNumber != req.CompanyDetails.ShowPhonenumber)
                {
                    _companyInfo.ShowPhNumber = req.CompanyDetails.ShowPhonenumber == true ? true : false;
                }

                _companyInfo.UpdatedDate = DateTime.Now;

                // have to insert through DB due to primary keys on various fields.
                var result = new List<UpdateCompanyReturnModel>();
                result = _context.UpdateCompany(
                                  _companyInfo.EmailId,
                                  _companyInfo.UserId,
                                  "yes",
                                  _companyInfo.CompanyName,
                                  _companyInfo.Address,
                                  _companyInfo.PhnCountryCode,
                                  _companyInfo.PhNumber,
                                  _companyInfo.IsGovRegistered,
                                  _companyInfo.IsActive,
                                   req.CompanyDetails.CompanyEmailID,
                                  _companyInfo.ShowPhNumber
                                );

                _status.IsSuccess = result[0].Ver_Status == CONSTResponse.CONST_SUCCESS;
                _status.StrMessage.Add(result[0].VER_Detail);
            }
            else
            {
                if (req.UserDetail.IsUserSeller == true)
                {
                    _companyInfo = new MasterCompany
                    {
                        User = _userInfo,
                        UserId = _userInfo.UserId,
                        CompanyName = req.CompanyDetails.CompanyName,
                        EmailId = req.CompanyDetails.CompanyEmailID,
                        Address = req.CompanyDetails.Address,
                        IsActive = req.CompanyDetails.IsCompanyActive,
                        IsGovRegistered = req.CompanyDetails.IsGOVRegisteredCompany,
                        PhnCountryCode = req.CompanyDetails.PhoneCountryCode,
                        PhNumber = req.CompanyDetails.PhoneNumber,
                        ShowPhNumber = req.CompanyDetails.ShowPhonenumber == true ? true : false,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                }
                _context.MasterCompanies.Add(_companyInfo);
                returnResults = _context.SaveChanges();


                if (returnResults == CONSTResponse.INT_CONST_SUCCESS)
                {
                    _status = Utility.AppendStatus<ResponseStatus>(ConstList.USER_UPDATE_CONST_SUCCESS, _status);
                }
                else
                {
                    _status = Utility.AppendStatus<ResponseStatus>(ConstList.USER_UPDATE_CONST_FAILURE, _status);
                }
            }

            return _status;
        }
    }
}
