using NepFlex.Core.Entities.ResourceModels;

namespace NepFlex.Core.Interfaces.Services
{
    public interface ILoginService
    {
        ResponseStatus UserLoginProcess(UserLogin req, ApplicationUser req2);
        ResponseStatus UserRegistrationProcess(UserRegister req, ApplicationUser req2);
        ResponseStatus UpdateUser(UserRegister req, ApplicationUser req2);
    }
}
