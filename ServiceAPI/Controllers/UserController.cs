﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Interfaces;
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
        public UserController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [Route("login")]
        [HttpPost]
        public IHttpActionResult AuthenticateUserLogin([FromBody] UserLogin login)
        {
            Console.WriteLine("came here in login");
            try
            {
                var results = _loginService.UserLoginProcess(login);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("register")]
        [HttpPost]
        public IHttpActionResult UserRegistration([FromBody] UserRegister req)
        {
            Console.WriteLine("came here in login");
            try
            {
                var results = _loginService.UserRegistrationProcess(req);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}