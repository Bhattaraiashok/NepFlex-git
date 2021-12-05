using Core.Interfaces.Repositories;
using NepFlex.Core.Entities.ResourceModels;
using PlatformTypes.NepFlexTypes.Base;
using PlatformTypes.NepFlexTypes.User;

namespace NepFlex.Core.Interfaces.Repositories
{
    public interface ILoginRepository : IRepository<UserLoginResponse, int>
    {
        LocalObjectStore GetSession();
        SignInStatusResponse UserLoginProcess(UserLoginRequest req);
        ResponseBase UserRegistrationProcess(UserRegisterRequest req);
        ResponseBase UpdateUserProcess(UserUpdateRequest req);
        SignInStatusResponse ValidateUserLogin(string usernameOrEmail, string password);
    }
}
