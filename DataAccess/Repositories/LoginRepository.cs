using DataAccess.Repositories;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Entities.ResourceModels.Security;
using NepFlex.Core.Interfaces.Repositories;
using NepFlex.DataAccess.Context;
using NepFlex.DataAccess.Repositories.UserSetting;
using PlatformCommon;
using PlatformCommon.Service;
using PlatformTypes.NepFlexTypes.Base;
using PlatformTypes.NepFlexTypes.Company;
using PlatformTypes.NepFlexTypes.Constant;
using PlatformTypes.NepFlexTypes.Password;
using PlatformTypes.NepFlexTypes.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NepFlex.DataAccess.Repositories
{

    public class LoginRepository : Repository<UserLoginResponse, int>, ILoginRepository
    {
        private readonly IOnlinePasalContext _context;
        // private readonly IEncryptionService _encryptionService;

        public UserInformationSettings _userInformationSettings { get { return new UserInformationSettings(_context); } }

        public LoginRepository(IOnlinePasalContext context) : base(context)
        {
            _context = context;
        }

        public SignInStatusResponse UserLoginProcess(UserLoginRequest login)
        {
            SignInStatusResponse _signInStatus = new SignInStatusResponse()
            {
                IsSuccess = false,
                StrMessage = new List<string>(),
                SignInStatus = SignInStatus.FAILURE
            };

            if (login == null || string.IsNullOrWhiteSpace(login.UserPSWD) || string.IsNullOrWhiteSpace(login.UserName))
            {
                _signInStatus = Helper.AppendStatus(ConstList.USER_LOGIN_CONST_FAILURE, _signInStatus);
                return _signInStatus;
            }

            _signInStatus = ValidateUserLogin(login.UserName, login.UserPSWD);

            return _signInStatus;
        }

        public ResponseBase UserRegistrationProcess(UserRegisterRequest req)
        {
            ResponseBase _status = new ResponseBase
            {
                IsSuccess = false,
                StrMessage = new List<string>()
            };

            //check
            if (req == null || string.IsNullOrWhiteSpace(req.EnteredPassword)
                || string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace((req.Username)))
            {
                _status = Helper.AppendStatus<ResponseBase>(ConstList.USER_REGISTER_CONST_FAILURE, _status);
                return _status;
            }

            var result = new List<SpRegisterUserReturnModel>();

            //req.PasswordHash = _encryptionService.CreatePasswordHash(req.EnteredPassword, "", PasswordFormat.HASHED.ToString());
            UserPassword userpassword = new UserPassword();
            userpassword = _userInformationSettings.SetPasswordHash(req.EnteredPassword);

            //saves user initial registration
            result = _context.SpRegisterUser(
               req.Email,
               req.Username,
               userpassword.PasswordHash,
               userpassword.PasswordSalt,
               userpassword.PasswordFormat.ToString(), //passwordtype,
               BaseEntity.DefaultHashedPasswordFormat, //pswdalgorithm
               BaseEntity.DBEncryptionKey,//dbkey
               PlatformTypes.NepFlexTypes.Base.SiteName.Site);


            _status.IsSuccess = result[0].Ver_Status == CONSTResponse.CONST_SUCCESS;
            _status.StrMessage.Add(result[0].VER_Detail);

            return _status;
        }

        public ResponseBase UpdateUserProcess(UserUpdateRequest req)
        {
            bool _dataUserAltered = false;

            ResponseBase _status = new ResponseBase
            {
                IsSuccess = false,
                StrMessage = new List<string>()
            };

            UserSession userCntx = UserSessionContext.Current;

            //first check
            if (req == null || req?.UID == null || req?.UserEmail == null)
            {
                _status = Helper.AppendStatus<ResponseBase>(ConstList.USER_UPDATE_CONST_FAILURE, _status);
                return _status;
            }

            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.UserId == req.UID &&
                             _usr.Email == req.UserEmail && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr).FirstOrDefault();

            //second checks
            if (_usrInfo == null || string.IsNullOrEmpty(_usrInfo?.UserId))
            {
                _status = Helper.AppendStatus(ConstList.USER_PROFILE_CONST_FAILURE, _status);
                return _status;
            }

            //user
            if (_usrInfo.FirstName != req.Firstname && (!string.IsNullOrWhiteSpace(req.Firstname)) && req.FieldUpdateRequest == CONST_Update_FormControlName.firstname)
            {
                _usrInfo.FirstName = req.Firstname;
                _dataUserAltered = true;
            }
            if (_usrInfo.MiddleName != req.Middlename && (!string.IsNullOrWhiteSpace(req.Middlename)) && req.FieldUpdateRequest == CONST_Update_FormControlName.middlename)
            {
                _usrInfo.MiddleName = req.Middlename;
                _dataUserAltered = true;
            }
            if (_usrInfo.LastName != req.Lastname && (!string.IsNullOrWhiteSpace(req.Lastname)) && req.FieldUpdateRequest == CONST_Update_FormControlName.lastname)
            {
                _usrInfo.LastName = req.Lastname;
                _dataUserAltered = true;
            }
            if (_usrInfo.PhNumber != req.PhoneNumber && (!string.IsNullOrWhiteSpace(req.PhoneNumber)) && req.FieldUpdateRequest == CONST_Update_FormControlName.userphonenumber)
            {
                _usrInfo.PhNumber = req.PhoneNumber;
                _dataUserAltered = true;
            }
            if (_usrInfo.Email != req.UserEmail && (!string.IsNullOrWhiteSpace(req.UserEmail)) && req.FieldUpdateRequest == CONST_Update_FormControlName.useremail)
            {
                _usrInfo.Email = req.UserEmail;
                _dataUserAltered = true;
            }
            if (_usrInfo.ShowPhNumber != req.ShowPhonenumber && req.ShowPhonenumber != null && req.FieldUpdateRequest == CONST_Update_FormControlName.showOrHideUserPhonenumber)
            {
                _usrInfo.ShowPhNumber = req.ShowPhonenumber;
                _dataUserAltered = true;
            }

            //ToDO:change to country
            if (_usrInfo.Country != req.Country && (!string.IsNullOrWhiteSpace(req.Country)) && req.FieldUpdateRequest == CONST_Update_FormControlName.usercountryCode)
            {
                _usrInfo.Country = req.Country;
                _dataUserAltered = true;
            }

            if (_usrInfo.ProfilePicture != req.ProfileImage && (!string.IsNullOrWhiteSpace(req.ProfileImage)) && req.FieldUpdateRequest == CONST_Update_FormControlName.profilephoto)
            {
                _usrInfo.ProfilePicture = req.ProfileImage;
                _dataUserAltered = true;
            }

            if (req.IsUserSeller.Equals(true))
            {
                var IsSeller = req.IsUserSeller == true ? 1 : 0;
                if (_usrInfo.IsUserSeller != IsSeller && req.FieldUpdateRequest == CONST_Update_FormControlName.userIsSeller)
                {
                    _usrInfo.IsUserSeller = IsSeller;
                    _dataUserAltered = true;
                }
            }

            var returnRes = 0;

            if (_dataUserAltered)
            {
                _usrInfo.UpdatedDate = DateTime.Now;

                returnRes = _context.SaveChanges();
            }

            //company info
            if (_usrInfo.IsUserSeller == 1)
            {
                CompanyUpdateRequest _compRequest = new CompanyUpdateRequest()
                {
                    UID = req.UID
                };

                UpdateCompany(_compRequest);
            }

            if (returnRes == CONSTResponse.INT_CONST_SUCCESS)
            {
                _status = Helper.AppendStatus<ResponseBase>(ConstList.USER_UPDATE_CONST_SUCCESS, _status);
            }

            return _status;

        }

        public ResponseBase RegisterCompanyProcess(CompanyRegisterRequest req)
        {
            ResponseBase _status = new ResponseBase
            {
                IsSuccess = false,
                StrMessage = new List<string>()
            };

            //ToDo: validate company if already exist on time of registration


            int returnResults = -1; //failure

            MasterCompany _companyInfo = new MasterCompany
            {
                UserId = req.UID,
                CompanyName = req.CompanyName,
                EmailId = req.CompanyEmailID,
                Address = req.Address,
                IsActive = req.IsCompanyActive,
                IsGovRegistered = req.IsGOVRegisteredCompany,
                PhnCountryCode = req.PhoneCountryCode,
                PhNumber = req.PhoneNumber,
                ShowPhNumber = req.ShowPhonenumber == true ? true : false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            _context.MasterCompanies.Add(_companyInfo);
            returnResults = _context.SaveChanges();


            if (returnResults == CONSTResponse.INT_CONST_SUCCESS)
            {
                _status = Helper.AppendStatus<ResponseBase>(ConstList.COMPANY_REGISTER_CONST_SUCCESS, _status);
            }
            else
            {
                _status = Helper.AppendStatus<ResponseBase>(ConstList.COMPANY_REGISTER_CONST_FAILURE, _status);
            }

            return _status;
        }

        public ResponseBase UpdateCompany(CompanyUpdateRequest req)
        {
            ResponseBase _status = new ResponseBase
            {
                IsSuccess = false,
                StrMessage = new List<string>()
            };


            if (req == null || string.IsNullOrWhiteSpace(req?.UID))
            {
                _status = Helper.AppendStatus<ResponseBase>(ConstList.COMPANY_UPDATE_CONST_FAILURE, _status);
                return _status;
            }

            bool _dataCompanyAltered = false;

            var _companyInfo = (from _comp in _context.MasterCompanies
                                where _comp.UserId == req.UID
                                select _comp).FirstOrDefault();

            UserSession userCntx = UserSessionContext.Current;

            int returnResults = -1; //failure

            if (_companyInfo != null)
            {
                if (_companyInfo.CompanyName != req.CompanyName && (!string.IsNullOrWhiteSpace(req.CompanyName)) && req.FieldUpdateRequest == CONST_Update_FormControlName.companyname)
                {
                    _companyInfo.CompanyName = req.CompanyName;
                    _dataCompanyAltered = true;
                }
                if (_companyInfo.EmailId != req.CompanyEmailID && req.FieldUpdateRequest == CONST_Update_FormControlName.companyemail)
                {
                    //_companyInfo.EmailId = req.CompanyDetails.CompanyEmailID; //do not do this since on modify user has to look up using existing email
                }
                if (_companyInfo.IsActive != req.IsCompanyActive && req.IsCompanyActive != null && req.FieldUpdateRequest == CONST_Update_FormControlName.isCompanyRegistered) //TODO: need to change
                {
                    _companyInfo.IsActive = req.IsCompanyActive;
                    _dataCompanyAltered = true;
                }
                if (_companyInfo.IsGovRegistered != req.IsGOVRegisteredCompany && req.IsGOVRegisteredCompany != null && req.FieldUpdateRequest == CONST_Update_FormControlName.isCompanyRegistered)
                {
                    _companyInfo.IsGovRegistered = req.IsGOVRegisteredCompany;
                    _dataCompanyAltered = true;
                }
                if (_companyInfo.PhnCountryCode != req.PhoneCountryCode && (!string.IsNullOrWhiteSpace(req.PhoneCountryCode)) && req.FieldUpdateRequest == CONST_Update_FormControlName.companycountryCode)
                {
                    _companyInfo.PhnCountryCode = req.PhoneCountryCode;
                    _dataCompanyAltered = true;
                }
                if (_companyInfo.PhNumber != req.PhoneNumber && (!string.IsNullOrWhiteSpace(req.PhoneNumber)) && req.FieldUpdateRequest == CONST_Update_FormControlName.companyphonenumber)
                {
                    _companyInfo.PhNumber = req.PhoneNumber;
                    _dataCompanyAltered = true;
                }

                if (_companyInfo.ShowPhNumber != req.ShowPhonenumber && req.ShowPhonenumber != null && req.FieldUpdateRequest == CONST_Update_FormControlName.showOrHideCompanyPhonenumber)
                {
                    _companyInfo.ShowPhNumber = req.ShowPhonenumber == true ? true : false;
                    _dataCompanyAltered = true;
                }

                if (_dataCompanyAltered)
                {
                    _companyInfo.UpdatedDate = DateTime.Now;

                    // have to insert through DB due to primary keys on various fields.
                    var result = new List<SpUpdateCompanyReturnModel>();
                    result = _context.SpUpdateCompany(
                                      _companyInfo.EmailId,
                                      _companyInfo.UserId,
                                      "yes",
                                      _companyInfo.CompanyName,
                                      _companyInfo.Address,
                                      _companyInfo.PhnCountryCode,
                                      _companyInfo.PhNumber,
                                      _companyInfo.IsGovRegistered,
                                      _companyInfo.IsActive,
                                       req.CompanyEmailID,
                                      _companyInfo.ShowPhNumber
                                    );

                    _status.IsSuccess = result[0].Ver_Status == CONSTResponse.CONST_SUCCESS;
                    _status.StrMessage.Add(result[0].VER_Detail);

                    return _status;
                }
            }

            return _status;
        }

        public SignInStatusResponse ValidateUserLogin(string usernameOrEmail, string password)
        {
            SignInStatusResponse _signInStatus = new SignInStatusResponse()
            {
                IsSuccess = false,
                StrMessage = new List<string>(),
                SignInStatus = SignInStatus.FAILURE
            };

            MasterUser customer = new MasterUser();
            var selectionUsingEmail = usernameOrEmail.Contains("@");
            if (!selectionUsingEmail)
            {
                customer = _userInformationSettings.GetUserByUserName(usernameOrEmail);
            }
            else
            {
                customer = _userInformationSettings.GetUserByEmail(usernameOrEmail);
            }

            //pre-check
            if (customer == null)
            {
                _signInStatus.SignInStatus = SignInStatus.FAILURE;
                _signInStatus = Helper.AppendStatus(ConstList.USER_INVALID_FAILURE, _signInStatus);
                return _signInStatus;
            }
            if (!customer.IsActiveAccount)
            {
                _signInStatus.SignInStatus = SignInStatus.INACTIVE;
                _signInStatus = Helper.AppendStatus(ConstList.USER_INACTIVE_FAILURE, _signInStatus);
                return _signInStatus;
            }

            //var getUserPasswordsAsync = GetUserPassword(customerPassword);
            UserPassword uPassword = new UserPassword();
            if (!_userInformationSettings.PasswordsMatch(customer.UserId, password))
            {
                uPassword.PasswordWrongAttemptPerSession++;
                if (uPassword.PasswordWrongAttemptPerSession >= int.Parse(BaseEntity.MaxPasswordAttemptAllowed))
                {
                    // retun msg- saying left over attempts
                    _signInStatus = Helper.AppendStatus(ConstList.MULTIPLE_PASSWORD_ATTEMPT_FAILURE, _signInStatus);
                }
                return _signInStatus;
            }

            //if (result.RequiresVerification)
            //    _signInStatus.SignInStatus = SignInStatus.RequiresVerification;

            UserSession userCntx = new UserSession();
            userCntx.UserID = customer.UserId;
            userCntx.IsAuthenticated = true;
            userCntx.FE_SessionID = Guid.NewGuid().ToString();
            userCntx.BE_SessionID = Guid.NewGuid().ToString();
            var salt = EncryptionService.CreateSaltKey(2);
            userCntx.AssignedAuthToken = EncryptionService.CreatePasswordHash("CurrentXAuthYTokenUSERZ", salt, "SHA1");
            UserSessionContext.Current = userCntx;

            _signInStatus = Helper.AppendStatus(ConstList.USER_VALID_SUCCESS, _signInStatus);
            _signInStatus.SignInStatus = SignInStatus.SUCCESS;

            return _signInStatus;
        }

        public ResponseBase ValidateUserRegestration(UserRegisterRequest userRegisterObject)
        {
            ResponseBase _status = new ResponseBase
            {
                IsSuccess = false,
                StrMessage = new List<string>()
            };

            if (userRegisterObject == null)
            {
                return _status;
            }
            else if (userRegisterObject == null)
            {
                return _status;
            }

            MasterUser customer = new MasterUser();

            //negative check
            var validationCheckFailed = (
                   (string.IsNullOrWhiteSpace(userRegisterObject.Username))
                || (string.IsNullOrWhiteSpace(userRegisterObject.Email))
                || (string.IsNullOrWhiteSpace(userRegisterObject.EnteredPassword))
                || (userRegisterObject.IsUserAgreementChecked == false)
                );

            if (validationCheckFailed)
            {
                return _status;
            }

            //check if email already registered
            bool registetrationUsingEmail = _userInformationSettings.ValidateEmailExist(userRegisterObject.Email);

            if (registetrationUsingEmail)
            {
                _status = Helper.AppendStatus(ConstList.RES_USER_EMAIL_EXISTS_CONST_FAILURE, _status);
                return _status;
            }

            //check if usrname already taken
            bool registetrationUsingUserName = _userInformationSettings.ValidateUsernameExist(userRegisterObject.Username);


            if (registetrationUsingUserName)
            {
                _status = Helper.AppendStatus(ConstList.RES_USERNAME_EXISTS_CONST_FAILURE, _status);
                return _status;
            }

            _status = Helper.AppendStatus(ConstList.USER_VALID_SUCCESS, _status);
            return _status;
        }

        //protected bool PasswordsMatch(UserPassword customerPassword, string enteredPassword)
        //{
        //    if (customerPassword == null || string.IsNullOrEmpty(enteredPassword))
        //        return false;

        //    var savedPassword = string.Empty;
        //    switch (customerPassword.PasswordFormat)
        //    {
        //        case PasswordFormat.CLEAR:
        //            savedPassword = enteredPassword;
        //            break;
        //        case PasswordFormat.ENCRYPTED:
        //            savedPassword = _encryptionService.EncryptText(enteredPassword);
        //            break;
        //        case PasswordFormat.HASHED:
        //            savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, customerPassword.PasswordSalt, BaseEntity.DefaultHashedPasswordFormat);
        //            break;
        //    }

        //    if (customerPassword.Password == null)
        //        return false;

        //    return customerPassword.Password.Equals(savedPassword);
        //}

        //public UserPassword GetUserPassword(UserPassword currentUserInfo)
        //{
        //    if (currentUserInfo == null)
        //    {
        //        return null;
        //    }

        //    UserPassword _localBind = new UserPassword();
        //    //filter by customer
        //    if (!string.IsNullOrWhiteSpace(currentUserInfo.UserID))
        //    {
        //        //var query = (from _pswd in _context.MasterSaltyPasswords
        //        //             where _pswd.Usrid == currentUserInfo.UserID
        //        //             select _pswd).FirstOrDefault();
        //        var query=_userInformationSettings.get

        //        //_localBind = new UserPassword()
        //        //{
        //        //    PasswordHash = query.Pswdhash,
        //        //    PasswordFormat = query.Passwordtype.ToLower() == "en" ? PasswordFormat.Encrypted : PasswordFormat.Hashed,
        //        //    PasswordSalt = query.Pswdsalt,
        //        //    CreatedOn = Convert.ToDateTime(query.Pswdchangedon),
        //        //    PasswordExpiresOn = Convert.ToDateTime(query.Pswdexpireson),
        //        //    PasswordIsActive = query.Pswdisactive,
        //        //    PasswordSaltKey = query.Pswdsaltkey,
        //        //    PasswordIsCompromised = query.Pswdcompromised == true ? true : false,
        //        //    PasswordWrongAttemptPerSession = query.Pswdwrongattempt > 0 ? query.Pswdwrongattempt.Value : 0, //never sending back to db unless session expires. circulate and count from code itself
        //        //    UserID = query.Usrid
        //        //};

        //        currentUserInfo = _localBind;

        //        return currentUserInfo;
        //    }

        //    return _localBind;
        //}

        public UserLoginResponse GetCustomerByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = (from c in _context.MasterUsers
                         where c.UserName == username
                         select c).FirstOrDefault();

            if (query != null)
            {
                UserLoginResponse userModel = new UserLoginResponse()
                {
                    Email = query.Email,
                    Firstname = query.FirstName,
                    Middlename = query.MiddleName,
                    Lastname = query.LastName,
                    ProfilePicture = query.ProfilePicture,
                    DateJoined = query.CreatedDate,
                    UserGuid = query.Guid
                };



                return userModel;
            }

            return null;
        }
    }
}
