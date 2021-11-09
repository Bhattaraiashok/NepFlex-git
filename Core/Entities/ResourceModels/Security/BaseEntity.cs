using System.Configuration;
using System.Web.Configuration;

namespace NepFlex.Core.Entities.ResourceModels.Security
{
    public static class BaseEntity
    {
        public static string UID { get; set; }
        //public static string HashedPasswordFormat { get; set; }
        public static int PasswordSaltKeySize { get { var objResult = ReadSetting("CURRENTSALTKEYSIZE"); return int.Parse(objResult); } }
        public static string DefaultHashedPasswordFormat { get { return ReadSetting("CURRENTSUPPORTEDHASHALGO"); } }
        public static string DBEncryptionKey { get { return ReadSetting("DBEncryptKey"); } }
        public static string MaxPasswordAttemptAllowed { get { return ReadSetting("MaxPasswordAttemptAllowed"); } }


        private static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                //log
                return null;
            }
        }

        private static bool AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);

                //verify:
                string result = ReadSetting(key);
                return (!string.IsNullOrWhiteSpace(result));
            }
            catch (ConfigurationErrorsException)
            {
                //log
                return false;
            }
        }
    }
}
