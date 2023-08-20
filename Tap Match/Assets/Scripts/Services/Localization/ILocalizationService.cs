using static JGM.Game.LocalizationService;

namespace JGM.Game
{
    public interface ILocalizationService
    {
        public Language currentLanguage { get; }
        public LanguageChangedEvent onLanguageChanged { get; set; }

        void SetLanguage(Language language);
        string Localize(string textId);
        string GetFontNameForLanguage(Language language);
    }
}