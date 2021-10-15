using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.Owin.Security;
using Nepflex.ServiceAPI.Identity;
using Nepflex.ServiceAPI.Models;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Interfaces.Services;

namespace Nepflex.ServiceAPI.Controllers
{
    [RoutePrefix("api/user")]
    //[Authorize]
    public class UserController : ApiController
    {
        public ILoginService _loginService;
        private IUnitOfWork _unitOfWork { get; set; }
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationSignInManager signInManager;
        private readonly IAuthenticationManager authenticationManager;

        public UserController(
            ILoginService loginService,
           ApplicationUserManager userManager,
           ApplicationSignInManager signInManager,
           IAuthenticationManager authenticationManager
            )
        {
            _loginService = loginService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IHttpActionResult> AuthenticateUserLogin([FromBody] UserLogin login)
        {
            Console.WriteLine("came here in login");
            try
            {
                var results = new UserLoginResponse
                {
                    IsSuccess = false,
                    StrMessage = new List<string>(),
                    IsAuthenticated = false
                };
                //put if else to check userID if not use email or phone number, phn number are unique on the table too.
                var processResult = await signInManager.PasswordSignInAsync(login.UserID, login.UserPSWD, login.IsRememberMe, shouldLockout: false);
                switch (processResult)
                {
                    case SignInStatus.Success:
                        var _userDetail = signInManager.UserManager.Users.Where(x => x.UserName == login.UserID).FirstOrDefault();
                        results = _loginService.UserLoginProcess(login, _userDetail);
                        return Ok(results);
                    case SignInStatus.LockedOut:
                        results.StrMessage.Add("Opps! you are locked out.");
                        results.IsSuccess = false;
                        return Ok(results);
                    case SignInStatus.RequiresVerification:
                        return Ok(results);
                    // return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                        results.StrMessage.Add("Login failed. please enter username and password correctly.");
                        results.IsSuccess = false;
                        return Ok(results);
                    default:
                        results.StrMessage.Add("Invalid Login Attempts...");
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
        public async Task<IHttpActionResult> UserRegistration([FromBody] UserRegister req)
        {
            Console.WriteLine("came here in login");
            try
            {
                var user = new ApplicationUser { UserName = req.UserDetail.Username, Email = req.UserDetail.UserEmail };
                var result = await userManager.CreateAsync(user, req.UserDetail.PSWDHASH);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    var results = _loginService.UserRegistrationProcess(req, user);
                    return Ok(results);
                }
                else
                {
                    ResponseStatus _status = new ResponseStatus
                    {
                        StrMessage = new List<string>()
                    };

                    _status.IsSuccess = false;
                    foreach (var item in result.Errors)
                    {
                        _status.StrMessage.Add(item);
                    }
                    return Ok(_status);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
