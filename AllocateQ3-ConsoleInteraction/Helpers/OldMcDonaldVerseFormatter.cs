using AllocateQ3_ConsoleInteraction.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace AllocateQ3_ConsoleInteraction.Helpers
{
    public class OldMcDonaldVerseFormatter : ISongVerseFormatter
    {
        private readonly (ResourceManager ResourceManager, CultureInfo Culture) _languagePack;

        public OldMcDonaldVerseFormatter((ResourceManager ResourceManager, CultureInfo Culture) languagePack)
        {
            _languagePack = languagePack;
        }

        public IEnumerable<string> FormatSongVerse(string type, string sound)
        {
            string translatedType = LocalizationHelper.GetString(_languagePack, type.ToLowerInvariant()) ?? type;

            yield return LocalizationHelper.GetString(_languagePack, "OldMacDonaldHadAFarm");
            yield return $"{LocalizationHelper.GetString(_languagePack, "AndOnThatFarmHeHadA")} {translatedType}, E I E I O.";
            yield return string.Format(LocalizationHelper.GetString(_languagePack, "WithASoundHereAndThere"), sound);
            yield return string.Format(LocalizationHelper.GetString(_languagePack, "HereASoundThereASound"), sound);
            yield return LocalizationHelper.GetString(_languagePack, "OldMacDonaldHadAFarm");
        }
    }
}
