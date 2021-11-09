using Core.Interfaces;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Entities.ResourceModels.Security;
using NepFlex.Core.Entities.Utility;
using NepFlex.Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NepFlex.Core.Services.Security
{
    public class EncryptionService : IEncryptionService
    {
        #region Fields
        private readonly SecuritySettings _securitySettings;
        #endregion
        #region Ctor
        public EncryptionService(SecuritySettings securitySettings)
        {
            _securitySettings = securitySettings;
        }
        #endregion

        #region Utilities

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                var toEncrypt = Encoding.Unicode.GetBytes(data);
                cs.Write(toEncrypt, 0, toEncrypt.Length);
                cs.FlushFinalBlock();
            }

            return ms.ToArray();
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream(data);
            var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read);
            var sr = new StreamReader(cs, Encoding.Unicode);
            return sr.ReadToEnd();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Create salt key
        /// </summary>
        /// <param name="size">Key size</param>
        /// <returns>Salt key</returns>
        public string CreateSaltKey(int size)
        {
            //generate a cryptographic random number
            var provider = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            provider.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Create a password hash
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="saltkey">Salk key</param>
        /// <param name="passwordFormat">Password format (hash algorithm)</param>
        /// <returns>Password hash</returns>
        public string CreatePasswordHash(string password, string saltKey, string passwordFormat)
        {
            return HashHelper.CreateHash(Encoding.UTF8.GetBytes(string.Concat(password, saltKey)), passwordFormat);
        }

        /// <summary>
        /// Encrypt text
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Encrypted text</returns>
        public string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (string.IsNullOrEmpty(encryptionPrivateKey))
            {
                var newNumberGenerate = Helper.GenerateRandomDigitCode(16); // just a number
                _securitySettings.EncryptionKey = newNumberGenerate;
                encryptionPrivateKey = _securitySettings.EncryptionKey;
            }

            var provider = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16)),
                IV = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8))
            };

            var encryptedBinary = EncryptTextToMemory(plainText, provider.Key, provider.IV);
            var result = Convert.ToBase64String(encryptedBinary);
            return result;
        }

        /// <summary>
        /// Decrypt text
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Decrypted text</returns>
        public string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            if (string.IsNullOrEmpty(encryptionPrivateKey))
            {
                var newNumberGenerate = Helper.GenerateRandomDigitCode(16); // just a number
                _securitySettings.EncryptionKey = newNumberGenerate;
                encryptionPrivateKey = _securitySettings.EncryptionKey;
            }

            var provider = new TripleDESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16)),
                IV = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8))
            };

            var buffer = Convert.FromBase64String(cipherText);
            var result = DecryptTextFromMemory(buffer, provider.Key, provider.IV);
            return result;
        }

        #endregion
    }
}
