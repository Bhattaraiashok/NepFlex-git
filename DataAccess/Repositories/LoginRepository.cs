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
                if (_usrInfo.FirstName != req.UserDetail.Firstname && req.FieldUpdateRequest == CONST_Update_FormControlName.firstname)
                {
                    _usrInfo.FirstName = !string.IsNullOrWhiteSpace(req.UserDetail.Firstname) ? req.UserDetail.Firstname : _usrInfo.FirstName;
                }
                if (_usrInfo.MiddleName != req.UserDetail.Middlename && req.FieldUpdateRequest == CONST_Update_FormControlName.middlename)
                {
                    _usrInfo.MiddleName = !string.IsNullOrWhiteSpace(req.UserDetail.Middlename) ? req.UserDetail.Middlename : _usrInfo.MiddleName;
                }
                if (_usrInfo.LastName != req.UserDetail.Lastname && req.FieldUpdateRequest == CONST_Update_FormControlName.lastname)
                {
                    _usrInfo.LastName = !string.IsNullOrWhiteSpace(req.UserDetail.Lastname) ? req.UserDetail.Lastname : _usrInfo.LastName;
                }
                if (_usrInfo.PhNumber != req.UserDetail.PhoneNumber && req.FieldUpdateRequest == CONST_Update_FormControlName.userphonenumber)
                {
                    _usrInfo.PhNumber = !string.IsNullOrWhiteSpace(req.UserDetail.PhoneNumber) ? req.UserDetail.PhoneNumber : _usrInfo.PhNumber;
                }
                if (_usrInfo.Email != req.UserDetail.Email && req.FieldUpdateRequest == CONST_Update_FormControlName.useremail)
                {
                    _usrInfo.Email = !string.IsNullOrWhiteSpace(req.UserDetail.Email) ? req.UserDetail.Email : _usrInfo.Email;
                }
                if (_usrInfo.ShowPhNumber != req.UserDetail.ShowPhonenumber && req.FieldUpdateRequest == CONST_Update_FormControlName.showOrHideUserPhonenumber)
                {
                    _usrInfo.ShowPhNumber = req.UserDetail.ShowPhonenumber;
                }
                if (_usrInfo.UserPhnCode != req.UserDetail.PhoneCountryCode && req.FieldUpdateRequest == CONST_Update_FormControlName.usercountryCode)
                {
                    _usrInfo.UserPhnCode = !string.IsNullOrWhiteSpace(req.UserDetail.PhoneCountryCode) ? req.UserDetail.PhoneCountryCode : _usrInfo.UserPhnCode;
                }

                _usrInfo.UpdatedDate = DateTime.Now;


                var IsSeller = req.UserDetail.IsUserSeller == true ? "yes" : "no";
                if (_usrInfo.IsUserSeller != IsSeller && req.FieldUpdateRequest == CONST_Update_FormControlName.userIsSeller)
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
                if (_companyInfo.CompanyName != req.CompanyDetails.CompanyName && req.FieldUpdateRequest == CONST_Update_FormControlName.companyname)
                {
                    _companyInfo.CompanyName = !string.IsNullOrWhiteSpace(req.CompanyDetails.CompanyName) ? req.CompanyDetails.CompanyName : _companyInfo.CompanyName;
                }
                if (_companyInfo.EmailId != req.CompanyDetails.CompanyEmailID && req.FieldUpdateRequest == CONST_Update_FormControlName.companyemail)
                {
                    //_companyInfo.EmailId = req.CompanyDetails.CompanyEmailID; //do not do this since on modify user has to look up using existing email
                }
                if (_companyInfo.IsActive != req.CompanyDetails.IsCompanyActive && req.FieldUpdateRequest == CONST_Update_FormControlName.isCompanyRegistered) //TODO: need to change
                {
                    _companyInfo.IsActive = req.CompanyDetails.IsCompanyActive;
                }
                if (_companyInfo.IsGovRegistered != req.CompanyDetails.IsGOVRegisteredCompany && req.FieldUpdateRequest == CONST_Update_FormControlName.isCompanyRegistered)
                {
                    _companyInfo.IsGovRegistered = req.CompanyDetails.IsGOVRegisteredCompany;
                }
                if (_companyInfo.PhnCountryCode != req.CompanyDetails.PhoneCountryCode && req.FieldUpdateRequest == CONST_Update_FormControlName.companycountryCode)
                {
                    _companyInfo.PhnCountryCode = !string.IsNullOrWhiteSpace(req.CompanyDetails.PhoneCountryCode) ? req.CompanyDetails.PhoneCountryCode : _companyInfo.PhnCountryCode;
                }
                if (_companyInfo.PhNumber != req.CompanyDetails.PhoneNumber && req.FieldUpdateRequest == CONST_Update_FormControlName.companyphonenumber)
                {
                    _companyInfo.PhNumber = !string.IsNullOrWhiteSpace(req.CompanyDetails.PhoneNumber) ? req.CompanyDetails.PhoneNumber : _companyInfo.PhNumber;
                }

                if (_companyInfo.ShowPhNumber != req.CompanyDetails.ShowPhonenumber && req.FieldUpdateRequest == CONST_Update_FormControlName.showOrHideCompanyPhonenumber)
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
                _status.StrMessage.Add(req.FieldUpdateRequest + result[0].VER_Detail);
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
