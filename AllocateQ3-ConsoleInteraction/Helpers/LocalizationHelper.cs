using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace AllocateQ3_ConsoleInteraction.Helpers
{
    public class LocalizationHelper
    {
        public static (ResourceManager ResourceManager, CultureInfo Culture) GetCurrentLanguage(string? languageCode)
        {
            string cultureString = languageCode?.ToLower() switch
            {
                "en" => "en-GB",
                "mk" => "mk-MK",
                _ => "en-GB"
            };

            var culture = new CultureInfo(cultureString);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var resourceManager = new ResourceManager(
                "AllocateQ3_ConsoleInteraction.Properties.Resources",
                Assembly.GetExecutingAssembly()
            );

            return (resourceManager, culture);
        }

        public static string GetString((ResourceManager ResourceManager, CultureInfo Culture) languagePack, string key, params object[] args)
        {
            var value = languagePack.ResourceManager.GetString(key, languagePack.Culture) ?? key;
            return args.Length > 0 ? string.Format(value, args) : value;
        }
    }
}
