using System.Collections.Generic;

namespace JGM.Game
{
    public sealed partial class LocalizationService
    {
        public class LanguageData
        {
            public Language language = Language.Count;
            public string isoCode;
            public string fontName;
            public Dictionary<string, string> library = new Dictionary<string, string>();
        }
    }
}