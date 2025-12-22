using System.Collections.Generic;

namespace AllocateQ3_ConsoleInteraction.Interfaces
{
    public interface ISongVerseFormatter
    {
        IEnumerable<string> FormatSongVerse(string type, string sound);
    }
}
