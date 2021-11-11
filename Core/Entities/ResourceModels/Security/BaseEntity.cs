using System.Configuration;
using System.Web.Configuration;

namespace NepFlex.Core.Entities.ResourceModels.Security
{
    public static class BaseEntity
    {
        public static string UID { get; set; }
        //public static string HashedPasswordFormat { get; set; }
        public static int PasswordSaltKeySize { get { var objResult = PlatformCommon.Helper.ReadSetting("CURRENTSALTKEYSIZE"); return int.Parse(objResult); } }
        public static string DefaultHashedPasswordFormat { get { return PlatformCommon.Helper.ReadSetting("CURRENTSUPPORTEDHASHALGO"); } }
        public static string DBEncryptionKey { get { return PlatformCommon.Helper.ReadSetting("DBEncryptKey"); } }
        public static string MaxPasswordAttemptAllowed { get { return PlatformCommon.Helper.ReadSetting("MaxPasswordAttemptAllowed"); } }


        //private static string ReadSetting(string key)
        //{
        //    try
        //    {
        //        var appSettings = ConfigurationManager.AppSettings;
        //        string result = appSettings[key] ?? "Not Found";
        //        return result;
        //    }
        //    catch (ConfigurationErrorsException)
        //    {
        //        //log
        //        return null;
        //    }
        //}

        //private static bool AddUpdateAppSettings(string key, string value)
        //{
        //    try
        //    {
        //        var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //        var settings = configFile.AppSettings.Settings;
        //        if (settings[key] == null)
        //        {
        //            settings.Add(key, value);
        //        }
        //        else
        //        {
        //            settings[key].Value = value;
        //        }
        //        configFile.Save(ConfigurationSaveMode.Modified);
        //        ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

        //        //verify:
        //        string result = ReadSetting(key);
        //        return (!string.IsNullOrWhiteSpace(result));
        //    }
        //    catch (ConfigurationErrorsException)
        //    {
        //        //log
        //        return false;
        //    }
        //}

        //public static bool RenderFileFromXMLPath(string pathaname)
        //{
        //    var response = new System.Collections.Generic.List<string>();
        //    var filePath = ConfigurationManager.AppSettings["NepFlexXML"] + pathaname;
        //    var serializer = new System.Xml.Serialization.XmlSerializer(typeof(System.Collections.Generic.List<string>));
        //    using (System.IO.TextReader reader = new System.IO.StreamReader(filePath))
        //    {
        //        response = (System.Collections.Generic.List<string>)serializer.Deserialize(reader);
        //    }

        //    return response != null && response.Count > 0;
        //}
    }
}
