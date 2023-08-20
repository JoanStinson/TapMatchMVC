using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JGM.Game
{
    public sealed partial class LocalizationService : ILocalizationService
    {
        public class LanguageChangedEvent : UnityEvent<Language, Language> { }
        public LanguageChangedEvent onLanguageChanged { get; set; } = new LanguageChangedEvent();
        public Language currentLanguage { get; private set; } = Language.Count;

        private const Language m_defaultLanguage = Language.English;
        private const string m_dataFolder = "Localization/";
        private const string m_configFilePath = "Data/localization_data";

        private LanguageData m_currentLanguageData => m_languages[currentLanguage];
        private Dictionary<Language, LanguageData> m_languages;

        public LocalizationService()
        {
            var localizationText = Resources.Load<TextAsset>(m_configFilePath);
            var localizationJson = JSONNode.Parse(localizationText.text);

            m_languages = new Dictionary<Language, LanguageData>();
            for (Language language = Language.English; language < Language.Count; language++)
            {
                var newLanguageData = new LanguageData();
                newLanguageData.language = language;
                m_languages[language] = newLanguageData;

                string langKey = language.ToString().ToLowerInvariant();
                if (localizationJson.HasKey(langKey))
                {
                    ParseLanguageData(ref newLanguageData, localizationJson[langKey]);
                }
            }

            SetLanguage(m_defaultLanguage);
        }

        public void SetLanguage(Language language)
        {
            if (language == Language.Count)
            {
                return;
            }

            var previousLanguage = currentLanguage;
            currentLanguage = language;
            onLanguageChanged?.Invoke(previousLanguage, currentLanguage);
        }

        public string Localize(string textId)
        {
            if (m_currentLanguageData.library.TryGetValue(textId, out string value))
            {
                return value;
            }

            return textId;
        }

        public string GetFontNameForLanguage(Language language)
        {
            if (m_languages.TryGetValue(language, out LanguageData value))
            {
                return value.fontName;
            }

            return null;
        }

        private void ParseLanguageData(ref LanguageData languageData, JSONNode jsonData)
        {
            if (jsonData.HasKey("isoCode"))
            {
                languageData.isoCode = jsonData["isoCode"];
            }

            if (jsonData.HasKey("fontName"))
            {
                languageData.fontName = jsonData["fontName"];
            }

            var languageText = Resources.Load<TextAsset>(m_dataFolder + languageData.isoCode);
            if (languageText != null)
            {
                string[] lines = languageText.text.Split('\n');
                char[] separator = { '=' };
                for (int i = 0; i < lines.Length; i++)
                {
                    // Parse line: make sure it has the exact expected format (key=value)
                    string[] split = lines[i].Split(separator, 2, System.StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length == 2)
                    {
                        // Remove spaces at the end of the line for both keys and values
                        string key = split[0].Trim();
                        string value = split[1].Trim().Replace("\\n", "\n");
                        languageData.library[key] = value;
                    }
                }
            }
        }
    }
}