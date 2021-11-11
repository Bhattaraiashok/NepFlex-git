using PlatformCommon;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace NepFlex.DataAccess.Repositories.UserSetting
{
    [Serializable]
    public class UserSession
    {
        public string UserID { get; set; }
        public bool UserIsLoggedIn { get; set; }
        public bool IsAuthenticated { get; set; }
        public string IPFound { get; set; }
        public string LocationFound { get; set; }
        public string FE_SessionID { get; set; }
        public string BE_SessionID { get; set; }
        public List<string> RestrictPages { get; set; }
        public string AssignedAuthToken { get; set; }
        public string UserHasSellerAC { get; set; }
        public string UserRole { get; set; }
        public DateTime? CurrentTimeStamp { get { return DateTime.Now; } }
    }

    public class UserSessionContext
    {
        public static UserSession Current
        {
            get
            {
                if (SessionCollections == null)
                {
                    UserSession _obj = new UserSession();
                    Set("UserSessionContext", _obj);
                }
                var obj = Get<UserSession>("UserSessionContext");

                return obj;
                //var obj = ContextManager<UserSession>.Get("UserSessionContext");
                //return obj;
            }
            set
            {
                //ContextManager<UserSession>.Set("UserSessionContext", value);
                Set("UserSessionContext", value);
            }
        }

        public static HttpSessionState SessionCollections
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new ApplicationException("No Http Context, No Session to Get!");

                return HttpContext.Current.Session;
            }
        }

        public static T Get<T>(string key)
        {
            if (SessionCollections[key] == null)
                return default(T);
            else
                return (T)SessionCollections[key];
        }

        public static void Set<T>(string key, T value)
        {
            SessionCollections[key] = value;
        }

        public static string GetString(string key)
        {
            string s = Get<string>(key);
            return s ?? string.Empty;
        }

        public static void SetString(string key, string value)
        {
            Set(key, value);
        }
    }
}
