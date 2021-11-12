using PlatformCommon.Service;
using PlatformTypes.NepFlexTypes.Password;
using System;
using System.Web.Http;

namespace Nepflex.ServiceAPI.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        [HttpGet]
        [Route("phash")]
        public IHttpActionResult PHashObject()
        {
            try
            {
                var password = "ohMySexyPassword";

                UserPassword userPassword = new UserPassword();

                var saltKey = EncryptionService.CreateSaltKey(5);
                userPassword.PasswordSalt = saltKey;
                userPassword.Password = EncryptionService.CreatePasswordHash(password, saltKey, "SHA512");

                var lastResult = PasswordsMatch(userPassword, password);

                return Ok(userPassword);
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

            savedPassword = EncryptionService.CreatePasswordHash(plainText, customerPassword.PasswordSalt, "SHA512");

            if (customerPassword.Password == null)
                return false;

            return customerPassword.Password.Equals(savedPassword);
        }

    }
}
