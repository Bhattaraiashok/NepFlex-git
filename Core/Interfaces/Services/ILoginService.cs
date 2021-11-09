using NepFlex.Core.Entities.ResourceModels;
using System.Threading.Tasks;

namespace NepFlex.Core.Interfaces.Services
{
    public interface ILoginService
    {
        SignInStatusResponse UserLoginProcess(UserLoginRequest req);
        ResponseStatus UserRegistrationProcess(UserRegisterRequest req);
        ResponseStatus UpdateUserProcess(UserUpdateRequest req);
        SignInStatusResponse ValidateUserLogin(string usernameOrEmail, string password);
    }
}
