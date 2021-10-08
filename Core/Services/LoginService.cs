using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Interfaces.Services;

namespace NepFlex.Core.Services
{
    public class LoginService : ILoginService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public UserLoginResponse UserLoginProcess(UserLogin login)
        {
            return _unitOfWork.LoginRepository.UserLoginProcess(login);
        }
        public bool UserRegistrationProcess(UserRegister req)
        {
            return _unitOfWork.LoginRepository.UserRegistrationProcess(req);
        }
    }
}
