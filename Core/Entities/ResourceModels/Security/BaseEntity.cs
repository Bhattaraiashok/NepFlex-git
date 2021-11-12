//using System.Configuration;
//using System.Web.Configuration;

//namespace NepFlex.Core.Entities.ResourceModels.Security
//{
//    public static class BaseEntity
//    {
//        public static string UID { get; set; }
//        //public static string HashedPasswordFormat { get; set; }
//        public static int PasswordSaltKeySize { get { var objResult = PlatformCommon.Helper.ReadSetting("CURRENTSALTKEYSIZE"); return int.Parse(objResult); } }
//        public static string DefaultHashedPasswordFormat { get { return PlatformCommon.Helper.ReadSetting("CURRENTSUPPORTEDHASHALGO"); } }
//        public static string DBEncryptionKey { get { return PlatformCommon.Helper.ReadSetting("DBEncryptKey"); } }
//        public static string MaxPasswordAttemptAllowed { get { return PlatformCommon.Helper.ReadSetting("MaxPasswordAttemptAllowed"); } }
//    }
//}
