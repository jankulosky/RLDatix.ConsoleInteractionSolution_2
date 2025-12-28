using AllocateQ3_ConsoleInteraction.Interfaces;
using System.Globalization;

namespace AllocateQ3_ConsoleInteraction.Helpers
{
    public class ResourceLocalizer : ILocalizer
    {
        private readonly CultureInfo _culture;

        public ResourceLocalizer(CultureInfo culture)
        {
            _culture = culture;
        }

        public string GetString(string key, params object[] args)
        {
            var value = Properties.Resources.ResourceManager.GetString(key, _culture)
                        ?? key;

            return args.Length > 0
                ? string.Format(_culture, value, args)
                : value;
        }
    }
}
