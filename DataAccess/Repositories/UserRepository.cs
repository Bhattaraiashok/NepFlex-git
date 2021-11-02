using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace NepFlex.DataAccess.Repositories
{
    public class UserRepository : Repository<UserLoginResponse, int>, ILoginRepository
    {
        private readonly IOnlinePasalContext _context;
        public LoginRepository(IOnlinePasalContext context) : base(context)
        {
            _context = context;
        }
        public UserLoginResponse UserLoginProcess(UserLogin login)
        {
            var _login = new UserLoginResponse();

            ValidateUserReturnModel _login2 = new ValidateUserReturnModel();
            _login2 = _context.ValidateUser(login.UserID, login.UserPSWD, login.UI);

            _login.Email = _login2.ResultSet1.Select(x => x.Email).FirstOrDefault();
            //_login.UserID= _login2.ResultSet1.Select(x => x.Email).FirstOrDefault();
            _login.UserGuid = _login2.ResultSet1.Select(x => x.GUID).FirstOrDefault();
            _login._Auth = _login2.ResultSet1.Select(x => x.Lastname).FirstOrDefault(); // need to modify this
            _login.Firstname = _login2.ResultSet1.Select(x => x.Firstname).FirstOrDefault();
            SessionIDManager manager = new SessionIDManager();
            string newSessionId = manager.CreateSessionID(HttpContext.Current);
            _login.SessionID = newSessionId;
            return _login;
        }
    }
}
