using Core.Interfaces;
using NepFlex.Core.Entities.ResourceModels;
using NepFlex.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace NepFlex.Core.Services
{
    public class LoginService : ILoginService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public SignInStatusResponse UserLoginProcess(UserLoginRequest login)
        {
            return _unitOfWork.LoginRepository.UserLoginProcess(login);
        }
        public ResponseStatus UserRegistrationProcess(UserRegisterRequest req)
        {
            return _unitOfWork.LoginRepository.UserRegistrationProcess(req);
        }
        public ResponseStatus UpdateUserProcess(UserUpdateRequest req)
        {
            return _unitOfWork.LoginRepository.UpdateUserProcess(req);
        }
        public SignInStatusResponse ValidateUserLogin(string usernameOrEmail, string password)
        {
            return _unitOfWork.LoginRepository.ValidateUserLogin(usernameOrEmail, password);
        }
    }
}
