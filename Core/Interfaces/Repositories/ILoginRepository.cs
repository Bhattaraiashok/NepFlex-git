using Core.Interfaces.Repositories;
using NepFlex.Core.Entities.ResourceModels;
using System.Threading.Tasks;

namespace NepFlex.Core.Interfaces.Repositories
{
    public interface ILoginRepository : IRepository<UserLoginResponse, int>
    {
        SignInStatusResponse UserLoginProcess(UserLoginRequest req);
        ResponseStatus UserRegistrationProcess(UserRegisterRequest req);
        ResponseStatus UpdateUserProcess(UserUpdateRequest req);
        SignInStatusResponse ValidateUserLogin(string usernameOrEmail, string password);
    }
}
