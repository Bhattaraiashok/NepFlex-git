using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace NepFlex.DataAccess.Repositories.UserSetting
{
    //[Serializable]
    //public class LocalObjectStore
    //{
    //    public string UserID { get; set; }
    //    public string UserGuid { get; set; }
    //    public bool UserIsLoggedIn { get; set; }
    //    public bool IsAuthenticated { get; set; }
    //    public string IPFound { get; set; }
    //    public string LocationFound { get; set; }
    //    public string FE_SessionID { get; set; }
    //    public string BE_SessionID { get; set; }
    //    public string SessionID { get; set; }
    //    public List<string> RestrictPages { get; set; }
    //    public string AssignedAuthToken { get; set; }
    //    public string UserHasSellerAC { get; set; }
    //    public string UserRole { get; set; }
    //    public DateTime? CurrentTimeStamp { get { return DateTime.Now; } }
    //}

    //public interface IUnityPerSessionLifetimeManager
    //{
    //    LocalObjectStore GetValue();
    //    void RemoveValue();
    //    void SetValue(LocalObjectStore newValue);
    //}

    //public class UnityPerSessionLifetimeManager: IUnityPerSessionLifetimeManager
    //{
    //    public static string _sessionKey;

    //    public UnityPerSessionLifetimeManager(string sessionKey = "UserSessionContext")
    //    {
    //        _sessionKey = sessionKey;
    //    }

    //    public LocalObjectStore GetValue()
    //    {
    //        var got = PlatformCommon.ContextManager<LocalObjectStore>.Get("UserSessionContext");
    //        return got;
    //        //return (LocalObjectStore)HttpContext.Current.Session["UserSessionContext"];
    //    }

    //    public void RemoveValue()
    //    {
    //        HttpContext.Current.Session.Remove(_sessionKey);
    //    }

    //    public void SetValue(LocalObjectStore newValue)
    //    {
    //        PlatformCommon.ContextManager<LocalObjectStore>.Set("UserSessionContext", newValue);
    //        //HttpContext.Current.Session["UserSessionContext"] = newValue;
    //    }
    //}

    //public static class GetUserSessionContext
    //{
    //    public static LocalObjectStore GetCurrentUser
    //    {
    //        get
    //        {
    //            var got = PlatformCommon.ContextManager<LocalObjectStore>.Get("UserSessionContext");
    //            return got;
    //        }
    //    }
    //}
    //public static class SetUserSessionContext
    //{
    //    public static LocalObjectStore SetCurrentUser
    //    {
    //        set
    //        {
    //            PlatformCommon.ContextManager<LocalObjectStore>.Set("UserSessionContext", value);
    //        }
    //    }
    //}


    ////public static HttpSessionState SessionCollections
    ////{
    ////    get
    ////    {
    ////        if (HttpContext.Current == null)
    ////            throw new ApplicationException("No Http Context, No Session to Get!");

    ////        return HttpContext.Current.Session;
    ////    }
    ////}

    ////public static T Get<T>(string key)
    ////{
    ////    if (SessionCollections[key] == null)
    ////        return default(T);
    ////    else
    ////        return (T)SessionCollections[key];
    ////}

    ////public static void Set<T>(string key, T value)
    ////{
    ////    SessionCollections[key] = value;
    ////}

    ////public static string GetString(string key)
    ////{
    ////    string s = Get<string>(key);
    ////    return s ?? string.Empty;
    ////}

    ////public static void SetString(string key, string value)
    ////{
    ////    Set(key, value);
    ////}

}
