using NepFlex.Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nepflex.ServiceAPI.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        public IEncryptionService _encryptionService;

        public CustomerController(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        [HttpGet]
        [Route("phash")]
        public IHttpActionResult PHashObject()
        {
            try
            {
                var password = "ohMySexyPassword";
                var saltKey = "salt1";
                var _result1 = _encryptionService.CreatePasswordHash(password, saltKey, "SHA1");
                var _result2 = _encryptionService.CreatePasswordHash(password, saltKey, "SHA512");

                var encryptedPassword = _encryptionService.EncryptText(password);
                //var decryptedPassword = _encryptionService.DecryptText(encryptedPassword);
                var lastResult = _result1 + "\n" + _result2 + "\n" + encryptedPassword;
                return Ok(lastResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
