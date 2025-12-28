using AllocateQ3_ConsoleInteraction.Interfaces;
using System;
using System.Collections.Generic;

namespace AllocateQ3_ConsoleInteraction
{
    public class OldMcDonaldVerseFormatter : ISongVerseFormatter
    {
        private readonly ILocalizer _localizer;

        public OldMcDonaldVerseFormatter(ILocalizer localizer)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        }

        public IEnumerable<string> FormatSongVerse(string type, string sound)
        {
            var translatedType = _localizer.GetString(type.ToLowerInvariant());

            yield return _localizer.GetString("OldMacDonaldHadAFarm");
            yield return $"{_localizer.GetString("AndOnThatFarmHeHadA")} {translatedType}, E I E I O.";
            yield return string.Format(_localizer.GetString("WithASoundHereAndThere"), sound);
            yield return string.Format(_localizer.GetString("HereASoundThereASound"), sound);
            yield return _localizer.GetString("OldMacDonaldHadAFarm");
        }
    }
}
