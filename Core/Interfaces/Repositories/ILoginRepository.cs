using Core.Interfaces.Repositories;
using NepFlex.Core.Entities.ResourceModels;

namespace NepFlex.Core.Interfaces.Repositories
{
    public interface ILoginRepository : IRepository<UserLoginResponse, int>
    {
        ResponseStatus UserLoginProcess(UserLogin req, ApplicationUser req2);
        ResponseStatus UserRegistrationProcess(UserRegister req, ApplicationUser req2);
        ResponseStatus UpdateUser(UserRegister req, ApplicationUser req2);
    }
}
