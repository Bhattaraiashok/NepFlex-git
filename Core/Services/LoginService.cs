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
        public UserLoginResponse UserLoginProcess(UserLogin login, ApplicationUser req2)
        {
            return _unitOfWork.LoginRepository.UserLoginProcess(login, req2);
        }
        public ResponseStatus UserRegistrationProcess(UserRegister req, ApplicationUser req2)
        {
            return _unitOfWork.LoginRepository.UserRegistrationProcess(req, req2);
        }
        public ResponseStatus UpdateUser(UserRegister req, ApplicationUser req2)
        {
            return _unitOfWork.LoginRepository.UpdateUser(req, req2);
        }
    }
}
