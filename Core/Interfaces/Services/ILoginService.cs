using NepFlex.Core.Entities.ResourceModels;
using PlatformTypes.NepFlexTypes.Base;
using PlatformTypes.NepFlexTypes.User;
using System.Threading.Tasks;

namespace NepFlex.Core.Interfaces.Services
{
    public interface ILoginService
    {
        LocalObjectStore GetSession();
        SignInStatusResponse UserLoginProcess(UserLoginRequest req);
        ResponseBase UserRegistrationProcess(UserRegisterRequest req);
        ResponseBase UpdateUserProcess(UserUpdateRequest req);
        SignInStatusResponse ValidateUserLogin(string usernameOrEmail, string password);
    }
}
