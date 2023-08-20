using TMPro;
using UnityEngine;
using Zenject;
using static JGM.Game.LocalizationService;

namespace JGM.Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField]
        private string m_localizedKey = "INSERT_KEY_HERE";

        [Inject] private ILocalizationService m_localizationService;
        [Inject] private FontLibrary m_fontLibrary;

        private TextMeshProUGUI m_text;

        private void Awake()
        {
            m_text = GetComponent<TextMeshProUGUI>();
            m_localizationService.onLanguageChanged.AddListener(OnLanguageChanged);
            string fontName = m_localizationService.GetFontNameForLanguage(m_localizationService.currentLanguage);
            m_text.font = m_fontLibrary.GetFontAsset(fontName);
            RefreshText();
        }

        private void OnLanguageChanged(Language previousLanguage, Language newLanguage)
        {
            if (newLanguage == previousLanguage)
            {
                return;
            }

            string fontName = m_localizationService.GetFontNameForLanguage(newLanguage);
            m_text.font = m_fontLibrary.GetFontAsset(fontName);
            RefreshText();
        }

        public void RefreshText()
        {
            m_text.text = m_localizationService.Localize(m_localizedKey);
        }
    }
}