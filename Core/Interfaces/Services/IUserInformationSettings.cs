using NepFlex.Core.Entities.OnlinePasal;
using NepFlex.Core.Entities.ResourceModels;
using System.Threading.Tasks;

namespace NepFlex.Core.Interfaces.Services
{
    public interface IUserInformationSettings<T>
    {
        void Dispose();
        T GetUserByEmail(string emailId);                  //GET'S THE USER INFO FROM USER TABLE USING EMAIL
        T GetUserById(string userId);                   //GET'S THE USER INFO FROM USER TABLE USING UID
        T GetUserByUserName(string userName);                 //GET'S THE USER INFO FROM USER TABLE USING USERNAME
        T GetUserbyGuid(string _guid);               //GET'S THE USER INFO FROM USER TABLE USING GUID
        string GetUsernamebyUID(string userId);                  //GET'S THE USERNAME FROM USER TABLE USING UID
        bool IsUserActiveCheck(string userId);                     //GET'S THE USER ACTIVE STATUS FROM USER TABLE USING UID
        Task<int> SetEmailOfUser(string userId, string email);          //SET'S THE USER EMAIL TO USER TABLE USING UID AND EMAIL
        bool CheckIfUserIsSeller(string userId);             //CHECK'S IF USER IS SELLER
        bool ValidateEmailExist(string email);          //CHECK'S IF INPUT EMAIL ALREADY IS IN THE USER TABLE
        bool ValidateUsernameExist(string _username);               //CHECK'S IF INPUT USERNAME ALREADY EXIST/TAKEN
        string GetPasswordHashOfUser(string userId);                   //GET'S THE USER HASED PASSWORD FROM USER TABLE USING UID
        string GetPasswordSaltOfUser(string userId);                  //GET'S THE USER SALTED PASSWORD FROM USER TABLE USING UID
        UserPassword SetPasswordHash(string _text);                  //HASHES THE USER ENTERED PASSWORD
        string EncryptUserProvidedPlainPassword(string _text);             //ENCRYPTS THE ENTERED TEXTS
        bool PasswordsMatch(string uId, string enteredPassword);
    }
}
