﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Core.Interfaces.Services;
using System.Threading.Tasks;
using System.Diagnostics;
using Core.Interfaces;

namespace Nepflex.ServiceAPI.Controllers
{
    [RoutePrefix("api/menuTopContainer")]
    //[Authorize]
    public class MenuTopController : ApiController
    {
        public IMenuTopService _menuTopService;
        private IUnitOfWork _unitOfWork { get; set; }

        public MenuTopController(IMenuTopService menuTopService)
        {
            _menuTopService = menuTopService;
            Console.WriteLine("came in controller");
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            Console.WriteLine("came here in get");
            try
            {
                var menuTopList = _menuTopService.GetMenuTopItems();
                    return Ok(menuTopList);
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
