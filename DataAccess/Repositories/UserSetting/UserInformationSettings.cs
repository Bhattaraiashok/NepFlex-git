using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Entities.ResourceModels.Security;
using NepFlex.DataAccess.Context;
using PlatformCommon.Service;
using PlatformTypes.NepFlexTypes.Constant;
using PlatformTypes.NepFlexTypes.Password;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NepFlex.DataAccess.Repositories.UserSetting
{
    public class UserInformationSettings
    {
        private readonly IOnlinePasalContext _context;

        public UserInformationSettings(IOnlinePasalContext context) //: base(context, encryptionService)
        {
            _context = context;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public MasterUser GetUserByEmail(string emailId)                    //GET'S THE USER INFO FROM USER TABLE USING EMAIL
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.Email == emailId && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr).FirstOrDefault();
            return _usrInfo;
        }

        public MasterUser GetUserById(string userId)                         //GET'S THE USER INFO FROM USER TABLE USING UID
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.UserId == userId && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr).FirstOrDefault();
            return _usrInfo;
        }

        public MasterUser GetUserByUserName(string userName)                 //GET'S THE USER INFO FROM USER TABLE USING USERNAME
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.UserName == userName && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr).FirstOrDefault();
            return _usrInfo;
        }

        public MasterUser GetUserbyGuid(string _guid)                      //GET'S THE USER INFO FROM USER TABLE USING GUID
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.Guid == _guid && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr).FirstOrDefault();
            return _usrInfo;
        }

        public string GetUsernamebyUID(string userId)                         //GET'S THE USERNAME FROM USER TABLE USING UID
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.UserId == userId
                            select _usr.UserName).FirstOrDefault();
            return _usrInfo;
        }

        public bool IsUserActiveCheck(string userId)                            //GET'S THE USER ACTIVE STATUS FROM USER TABLE USING UID
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.UserId == userId
                            select _usr.IsActiveAccount).FirstOrDefault();
            return _usrInfo;
        }

        public async Task<int> SetEmailOfUser(string userId, string email)          //SET'S THE USER EMAIL TO USER TABLE USING UID AND EMAIL
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.UserId == userId && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr).FirstOrDefault();
            _usrInfo.Email = email;

            var obj = await _context.SaveChangesAsync();

            return obj;
        }

        public bool CheckIfUserIsSeller(string userId)                 //CHECK'S IF USER IS SELLER
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.UserId == userId && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr.IsUserSeller).FirstOrDefault();
            var _result = _usrInfo.Equals(true);
            return _result;
        }

        public bool ValidateEmailExist(string email)                 //CHECK'S IF INPUT EMAIL ALREADY IS IN THE USER TABLE
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.Email == email && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr.Email).FirstOrDefault();
            var _result = _usrInfo.Equals(email);
            return _result;
        }

        public bool ValidateUsernameExist(string _username)                 //CHECK'S IF INPUT USERNAME ALREADY EXIST/TAKEN
        {
            var _usrInfo = (from _usr in _context.MasterUsers
                            where _usr.UserName == _username && _usr.Ui.ToLower() == CONSTUINAME.UI_NAME
                            select _usr.UserName).FirstOrDefault();
            var _result = _usrInfo.Equals(_username);
            return _result;
        }

        public string GetPasswordHashOfUser(string userId)                      //GET'S THE USER HASED PASSWORD FROM USER TABLE USING UID
        {
            var _usrInfo = (from _usr in _context.MasterSaltyPasswords
                            where _usr.Usrid == userId
                            select _usr.Pswdhash).FirstOrDefault();
            return _usrInfo;
        }

        public string GetPasswordSaltOfUser(string userId)                      //GET'S THE USER SALTED PASSWORD FROM USER TABLE USING UID
        {
            var _usrInfo = (from _usr in _context.MasterSaltyPasswords
                            where _usr.Usrid == userId
                            select _usr.Pswdsalt).FirstOrDefault();
            return _usrInfo;
        }

        public PasswordFormat GetPasswordFormatOfUser(string userId)                      //GET'S THE USER SALTED PASSWORD FROM USER TABLE USING UID
        {
            var _usrInfo = (from _usr in _context.MasterSaltyPasswords
                            where _usr.Usrid == userId
                            select _usr.Passwordtype).FirstOrDefault();

            if (_usrInfo.ToLower() == "ha")
                return PasswordFormat.HASHED;
            else if (_usrInfo.ToLower() == "en")
                return PasswordFormat.ENCRYPTED;
            else
                return PasswordFormat.NULL;
        }

        public UserPassword SetPasswordHash(string _text)                     //HASHES THE USER ENTERED PASSWORD
        {
            UserPassword userPassword = new UserPassword();
            var saltKey = EncryptionService.CreateSaltKey(BaseEntity.PasswordSaltKeySize);
            userPassword.PasswordSalt = saltKey;
            userPassword.PasswordFormat = PasswordFormat.HASHED;
            userPassword.PasswordHash = EncryptionService.CreatePasswordHash(_text, saltKey, BaseEntity.DefaultHashedPasswordFormat);
            return userPassword;
        }

        public string EncryptUserProvidedPlainPassword(string _text)              //ENCRYPTS THE ENTERED TEXTS
        {
            var strObj = EncryptionService.EncryptText(_text);
            return strObj;
        }

        public bool PasswordsMatch(string _UID, string enteredPassword)
        {
            var passwordFormat = GetPasswordFormatOfUser(_UID);

            if (passwordFormat == PasswordFormat.NULL || string.IsNullOrEmpty(enteredPassword))
                return false;

            string _userPassword = GetPasswordHashOfUser(_UID); //get user hash password

            if (_userPassword == null)
                return false;

            string _userPasswordSaltKey = GetPasswordSaltOfUser(_UID); // get salt key

            var newPasswordCreation = string.Empty;
            switch (passwordFormat)
            {
                case PasswordFormat.CLEAR:
                    newPasswordCreation = enteredPassword;
                    break;
                case PasswordFormat.ENCRYPTED:
                    newPasswordCreation = EncryptionService.EncryptText(enteredPassword);
                    break;
                case PasswordFormat.HASHED:
                    newPasswordCreation = EncryptionService.CreatePasswordHash(enteredPassword, _userPasswordSaltKey, BaseEntity.DefaultHashedPasswordFormat);
                    break;
            }

            return _userPassword.Equals(newPasswordCreation);
        }
    }
}
