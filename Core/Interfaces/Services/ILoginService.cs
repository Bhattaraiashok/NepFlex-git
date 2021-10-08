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
        UserLoginResponse UserLoginProcess(UserLogin req);
        UserRegisterResponse UserRegistrationProcess(UserRegister req);
    }
}
