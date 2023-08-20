using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static JGM.Game.LocalizationService;
using Random = UnityEngine.Random;

namespace JGM.Game
{
    public class TopHeaderView : MonoBehaviour
    {
        [SerializeField] private TextMeshProAnimatedBinder m_movesAmountText;
        [SerializeField] private Button m_languageButton;
        [SerializeField] private Button m_bombButton;

        [Inject] private ILocalizationService m_localizationService;
        [Inject] private IAudioService m_audioService;

        private int m_movesAmount;

        public void Initialize(int initialMovesAmount)
        {
            m_movesAmount = initialMovesAmount;
            m_movesAmountText.SetValue(initialMovesAmount);
            m_languageButton.onClick.RemoveAllListeners();
            m_languageButton.onClick.AddListener(ChangeLanguageToRandom);
            m_bombButton.onClick.RemoveAllListeners();
            m_bombButton.onClick.AddListener(() => m_audioService.Play(AudioFileNames.ButtonClickSfx));
        }

        private void ChangeLanguageToRandom()
        {
            Language currentLanguage = m_localizationService.currentLanguage;
            Language randomLanguage;

            do
            {
                randomLanguage = (Language)Random.Range(0, (int)Language.Count);
            }
            while (randomLanguage == currentLanguage);

            m_localizationService.SetLanguage(randomLanguage);
            m_audioService.Play(AudioFileNames.ButtonClickSfx);
        }

        public void IncreaseMovesAmount()
        {
            m_movesAmount++;
            m_movesAmountText.SetValueAnimated(m_movesAmount);
        }
    }
}