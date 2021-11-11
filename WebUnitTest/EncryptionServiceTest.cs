//using System;
//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using NepFlex.Core.Entities.ResourceModels.Security;
//using NepFlex.Core.Interfaces.Security;

//namespace WebUnitTest
//{
//    [TestClass]
//    public class EncryptionServiceTest
//    {
//        private IEncryptionService _encryptionService;
//        //private SecuritySettings _securitySettings = null;
//        //private string _defaultEncryptionKey = null;

//        [TestMethod]
//        public void CanHashSha1()
//        {
//            var password = "MyLittleSecret";
//            var saltKey = "salt1";
//            var pType = "SHA1";
//            var hashedPassword = _encryptionService.CreatePasswordHash(password, saltKey, pType);
//            Assert.AreEqual(hashedPassword, "A07A9638CCE93E48E3F26B37EF7BDF979B8124D6");
//        }

//        [TestMethod]
//        public void CanHashSha512()
//        {
//            var password = "MyLittleSecret";
//            var saltKey = "salt1";
//            var hashedPassword = _encryptionService.CreatePasswordHash(password, saltKey, "SHA512");
//            Assert.AreEqual(hashedPassword, "4506D65FDB6F3A8CF97278AB7C5C62DEC35EADD474BE1E6243776691D56E1B27F71C1D9085B26BD7513BED89822204D6B8FCBD6E665D46558C48F56D21B2A293");
//        }

//        [TestMethod]
//        public void CanEncryptAndDecrypt()
//        {
//            var password = "MyLittleSecret";
//            var encryptedPassword = _encryptionService.EncryptText(password);
//            var decryptedPassword = _encryptionService.DecryptText(encryptedPassword);
//            Assert.AreEqual(decryptedPassword, password);
//        }
//    }
//}
