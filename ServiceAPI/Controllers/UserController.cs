using Core.Interfaces;
using Nepflex.ServiceAPI.Identity;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Interfaces.Services;
using NepFlex.DataAccess.Repositories.UserSetting;
using PlatformCommon;
using PlatformTypes.NepFlexTypes.Base;
using PlatformTypes.NepFlexTypes.Constant;
using PlatformTypes.NepFlexTypes.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Nepflex.ServiceAPI.Controllers
{
    [RoutePrefix("api/user")]
    //[Authorize]
    public class UserController : ApiController
    {
        public ILoginService _loginService;
        private IUnitOfWork _unitOfWork { get; set; }

        public UserController(
            ILoginService loginService,
           ApplicationUserManager userManager,
           ApplicationSignInManager signInManager
            )
        {
            _loginService = loginService;
        }
        [HttpPost]
        [Route("logoff")]
        //[ValidateAntiForgeryToken]
        public IHttpActionResult LogOff()
        {
            try
            {
                var _loginOut = new UserLoginResponse
                {
                    IsSuccess = false,
                    StrMessage = new List<string>()
                };
                UserSession userCntx = UserSessionContext.Current;
                // authenticationManager.SignOut();
                _loginOut = Helper.AppendStatus(ConstList.REQ_OBJ_CONST_SUCCESS, _loginOut);
                return Ok(_loginOut);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult AuthenticateUserLogin([FromBody] UserLoginRequest login)
        {
            Console.WriteLine("came here in login");
            try
            {
                SignInStatusResponse _status = new SignInStatusResponse
                {
                    IsSuccess = false,
                    StrMessage = new List<string>()
                };
                //pre-check
                var validationCheckFailed = (login == null || string.IsNullOrWhiteSpace(login.UserName?.Trim()) || string.IsNullOrWhiteSpace(login.UserPSWD));
                if (validationCheckFailed)
                {
                    _status = Helper.AppendStatus(ConstList.USER_LOGIN_CONST_FAILURE, _status);
                    return Ok(_status);
                }

                //put if else to check userID if not use email or phone number, phn number are unique on the table too.
                _status = _loginService.UserLoginProcess(login);
                switch (_status.SignInStatus)
                {
                    case SignInStatus.SUCCESS:
                        return Ok(_status);
                    case SignInStatus.INACTIVE:
                        return Ok(_status);
                    case SignInStatus.LOCKEDOUT:
                        return Ok(_status);
                    case SignInStatus.REQUIRESVERIFICATIONS:
                        return Ok(_status);
                    // return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.FAILURE:
                        return Ok(_status);
                    default:
                        return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("register")]
        [HttpPost]
        public IHttpActionResult UserRegistration([FromBody] UserRegisterRequest req)
        {
            try
            {
                ResponseBase _status = new ResponseBase
                {
                    IsSuccess = false,
                    StrMessage = new List<string>()
                };
                //pre-check
                var validtionFailed = req == null || (string.IsNullOrWhiteSpace(req.Username))
                    || (string.IsNullOrWhiteSpace(req.Email)) || (string.IsNullOrWhiteSpace(req.EnteredPassword));

                if (validtionFailed)
                {
                    _status = Helper.AppendStatus(ConstList.USER_REGISTER_CONST_FAILURE, _status);
                    return Ok(_status);
                }

                var user = req.UserRegisterRequestValidation();
                //pre-check II
                if (!user)
                {
                    _status = Helper.AppendStatus(ConstList.USER_REGISTER_CONST_FAILURE, _status);
                    return Ok(_status);
                }
                //if here...continue
                _status = _loginService.UserRegistrationProcess(req);
                if (_status.IsSuccess)
                {
                    //login here
                    //_status = _loginService.UserLoginProcess(req);
                    return Ok(_status);
                }

                //if here..something failed
                return Ok(_status);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("update")]
        [HttpPost]
        public IHttpActionResult UserUpdate([FromBody] UserUpdateRequest reqUpdate)
        {
            Console.WriteLine("came here in login");
            try
            {
                ResponseBase _status = new ResponseBase
                {
                    IsSuccess = false,
                    StrMessage = new List<string>()
                };

                //pre-check
                if (reqUpdate == null || string.IsNullOrWhiteSpace(reqUpdate.UserEmail) || string.IsNullOrWhiteSpace(reqUpdate.UID))
                {
                    _status = Helper.AppendStatus(ConstList.USER_UPDATE_CONST_FAILURE, _status);
                    return Ok(_status);
                }

                if (!string.IsNullOrEmpty(reqUpdate.UserEmail))
                {
                    _status = _loginService.UpdateUserProcess(reqUpdate);
                    return Ok(_status);
                }
                else
                {
                    Helper.AppendStatus(ConstList.USER_LOGIN_CONST_FAILURE, _status);
                    return Ok(_status);
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #region Helpers
        //still not added
        #endregion
    }
}
