using System;
using System.Globalization;

namespace AllocateQ3_ConsoleInteraction.Helpers
{
    public class CultureResolver
    {
        public static CultureInfo GetCurrentLanguage()
        {
            Console.WriteLine("Choose your language: English (en) or Macedonian (mk):");
            var input = Console.ReadLine();

            var cultureName = input?.Trim().ToLowerInvariant() switch
            {
                "mk" => "mk-MK",
                "en" => "en-GB",
                _ => "en-GB"
            };

            var culture = new CultureInfo(cultureName);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            return culture;
        }
    }
}
