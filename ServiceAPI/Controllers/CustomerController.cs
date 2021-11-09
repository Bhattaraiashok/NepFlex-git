using NepFlex.Core.Entities.ResourceModels;
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
                var _staticsaltKey = "salt1";

                //var _dynamicsaltkey1 = _encryptionService.CreateSaltKey(5);

                //var _result1 = _encryptionService.CreatePasswordHash(password, _dynamicsaltkey1, "SHA1");

                //var _result2 = _encryptionService.CreatePasswordHash(password, password, "SHA1");


                //var _result3 = _encryptionService.CreatePasswordHash(password, _dynamicsaltkey1, "SHA512");

                //var encryptedPassword = _encryptionService.EncryptText(_result2);
                //var decryptedPassword = _encryptionService.DecryptText(encryptedPassword); 

                UserPassword userPassword = new UserPassword();

                var saltKey = _encryptionService.CreateSaltKey(5);
                userPassword.PasswordSalt = saltKey;
                userPassword.Password = _encryptionService.CreatePasswordHash(password, saltKey, "SHA512");

                var lastResult = PasswordsMatch(userPassword, password);

                return Ok(lastResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        protected bool PasswordsMatch(UserPassword customerPassword, string plainText)
        {
            if (customerPassword == null || string.IsNullOrEmpty(plainText))
                return false;

            var savedPassword = string.Empty;

            //savedPassword = _encryptionService.EncryptText(plainText);

            savedPassword = _encryptionService.CreatePasswordHash(plainText, customerPassword.PasswordSalt, "SHA512");

            if (customerPassword.Password == null)
                return false;

            return customerPassword.Password.Equals(savedPassword);
        }

    }
}
