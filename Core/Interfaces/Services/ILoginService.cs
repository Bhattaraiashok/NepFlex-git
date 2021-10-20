using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NepFlex.Core.Entities.ResourceModels;

namespace NepFlex.Core.Interfaces.Services
{
    public interface ILoginService
    {
        UserLoginResponse UserLoginProcess(UserLogin req, ApplicationUser req2);
        ResponseStatus UserRegistrationProcess(UserRegister req, ApplicationUser req2);
        ResponseStatus UpdateUser(UserRegister req, ApplicationUser req2);
    }
}
