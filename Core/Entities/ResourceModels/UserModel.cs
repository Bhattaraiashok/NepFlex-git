using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace NepFlex.Core.Entities.ResourceModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    [Serializable]
    public class LocalObjectStore : User
    {
    }

    [Serializable]
    public class User
    {
        public string UserID { get; set; }
        public string UserGuid { get; set; } //use guid instead of emailid
        public string EmailId { get; set; }
        public bool UserIsLoggedIn { get; set; }
        public bool IsAuthenticated { get; set; }
        public string IPFound { get; set; }
        public string LocationFound { get; set; }
        public string FE_SessionID { get; set; }
        public string BE_SessionID { get; set; }
        public string SessionID { get; set; }
        public List<string> RestrictPages { get; set; }
        public string AssignedAuthToken { get; set; }
        public string UserHasSellerAC { get; set; }
        public string UserRole { get; set; }
        public DateTime? CurrentTimeStamp { get { return DateTime.Now; } }
    }

    public interface ISessionManager
    {
        T Get<T>(string key);
        void Set<T>(string key, T entry);
    }

    public class SessionManager : ISessionManager
    {
        public T Get<T>(string key)
        {
            string _sessionId = string.Empty;
            HttpCookie cookie = null;
            cookie = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"];
            _sessionId = cookie.Value;

            HttpContext currentContext = GetSessionContext();

            return (T)currentContext.Session[key];
        }

        public void Set<T>(string key, T entry)
        {
            HttpContext currentContext = GetSessionContext();
            currentContext.Session[key] = entry;
        }
        private static HttpContext GetSessionContext()
        {
            HttpContext currentContext = HttpContext.Current;

            if (currentContext == null)
            {
                throw new InvalidOperationException();
            }
            return currentContext;
        }
    }

    public static class GetUserSessionContext
    {
        public static LocalObjectStore GetCurrentUser
        {
            get
            {
                var got = PlatformCommon.ContextManager<LocalObjectStore>.Get("UserSessionContext");
                return got;
            }
        }
    }
    public static class SetUserSessionContext
    {
        public static LocalObjectStore SetCurrentUser
        {
            set
            {
                PlatformCommon.ContextManager<LocalObjectStore>.Set("UserSessionContext", value);
            }
        }
    }


    //public static HttpSessionState SessionCollections
    //{
    //    get
    //    {
    //        if (HttpContext.Current == null)
    //            throw new ApplicationException("No Http Context, No Session to Get!");

    //        return HttpContext.Current.Session;
    //    }
    //}

    //public static T Get<T>(string key)
    //{
    //    if (SessionCollections[key] == null)
    //        return default(T);
    //    else
    //        return (T)SessionCollections[key];
    //}

    //public static void Set<T>(string key, T value)
    //{
    //    SessionCollections[key] = value;
    //}

    //public static string GetString(string key)
    //{
    //    string s = Get<string>(key);
    //    return s ?? string.Empty;
    //}

    //public static void SetString(string key, string value)
    //{
    //    Set(key, value);
    //}
}
