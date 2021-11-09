using Core.Interfaces;
using Nepflex.ServiceAPI.Identity;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Interfaces.Services;
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
                // authenticationManager.SignOut();
                _loginOut = Utility.AppendStatus<UserLoginResponse>(ConstList.REQ_OBJ_CONST_SUCCESS, _loginOut);
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
                ResponseStatus _status = new ResponseStatus
                {
                    IsSuccess = false,
                    StrMessage = new List<string>()
                };
                //pre-check
                var validationCheckFailed = (login == null || string.IsNullOrWhiteSpace(login.UserName?.Trim()) || string.IsNullOrWhiteSpace(login.UserPSWD));
                if (validationCheckFailed)
                {
                    _status = Utility.AppendStatus(ConstList.USER_LOGIN_CONST_FAILURE, _status);
                    return Ok(_status);
                }

                //put if else to check userID if not use email or phone number, phn number are unique on the table too.
                var processResult = _loginService.UserLoginProcess(login);
                switch (processResult.SignInStatus)
                {
                    case SignInStatus.SUCCESS:
                        if (processResult.IsSuccess == false)
                        {
                            //authenticationManager.SignOut();
                        }
                        return Ok(processResult);
                    case SignInStatus.INACTIVE:
                        foreach (var item in processResult.StrMessage)
                        {
                            _status.StrMessage.Add(item);
                        }
                        _status.IsSuccess = false;
                        return Ok(_status);
                    case SignInStatus.LOCKEDOUT:
                        foreach (var item in processResult.StrMessage)
                        {
                            _status.StrMessage.Add(item);
                        }
                        _status.IsSuccess = false;
                        return Ok(_status);
                    case SignInStatus.REQUIRESVERIFICATIONS:
                        foreach (var item in processResult.StrMessage)
                        {
                            _status.StrMessage.Add(item);
                        }
                        return Ok(_status);
                    // return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.FAILURE:

                        foreach (var item in processResult.StrMessage)
                        {
                            _status.StrMessage.Add(item);
                        }
                        _status.IsSuccess = false;
                        return Ok(_status);
                    default:
                        foreach (var item in processResult.StrMessage)
                        {
                            _status.StrMessage.Add(item);
                        }
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
                ResponseStatus _status = new ResponseStatus
                {
                    IsSuccess = false,
                    StrMessage = new List<string>()
                };
                //pre-check
                var validtionFailed = req == null || (string.IsNullOrWhiteSpace(req.Username))
                    || (string.IsNullOrWhiteSpace(req.Email)) || (string.IsNullOrWhiteSpace(req.EnteredPassword));

                if (validtionFailed)
                {
                    _status = Utility.AppendStatus(ConstList.USER_REGISTER_CONST_FAILURE, _status);
                    return Ok(_status);
                }

                var user = req.UserRegisterRequestValidation();
                //pre-check II
                if (!user)
                {
                    _status = Utility.AppendStatus(ConstList.USER_REGISTER_CONST_FAILURE, _status);
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
                ResponseStatus _status = new ResponseStatus
                {
                    IsSuccess = false,
                    StrMessage = new List<string>()
                };

                //pre-check
                if (reqUpdate == null || string.IsNullOrWhiteSpace(reqUpdate.UserEmail) || string.IsNullOrWhiteSpace(reqUpdate.UID))
                {
                    _status = Utility.AppendStatus(ConstList.USER_UPDATE_CONST_FAILURE, _status);
                    return Ok(_status);
                }

                if (!string.IsNullOrEmpty(reqUpdate.UserEmail))
                {
                    _status = _loginService.UpdateUserProcess(reqUpdate);
                    return Ok(_status);
                }
                else
                {
                    Utility.AppendStatus(ConstList.USER_LOGIN_CONST_FAILURE, _status);
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
