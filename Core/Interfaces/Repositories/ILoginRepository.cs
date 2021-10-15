using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;
using NepFlex.Core.Entities.ResourceModels;

namespace NepFlex.Core.Interfaces.Repositories
{
    public interface ILoginRepository : IRepository<UserLoginResponse, int>
    {
        UserLoginResponse UserLoginProcess(UserLogin req, ApplicationUser req2);
        ResponseStatus UserRegistrationProcess(UserRegister req, ApplicationUser req2);
    }
}
