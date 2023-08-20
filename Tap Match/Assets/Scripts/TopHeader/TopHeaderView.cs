using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static JGM.Game.LocalizationService;
using Random = UnityEngine.Random;

namespace JGM.Game
{
    public class TopHeaderView : MonoBehaviour
    {
        public Action<int> onClickBombButton;

        [SerializeField] private TextMeshProAnimatedBinder m_movesAmountText;
        [SerializeField] private Button m_languageButton;
        [SerializeField] private Button m_bombButton;
        [SerializeField] private Image m_bombIcon;
        [SerializeField] private Animator m_bombAnimator;

        [Inject] private IAudioService m_audioService;
        [Inject] private ICoroutineService m_coroutineService;
        [Inject] private ILocalizationService m_localizationService;
        [Inject] private GameSettings m_gameSettings;

        private int m_movesAmount;
        private int m_currentBombCellType = -1;

        public void Initialize()
        {
            m_movesAmount = 0;
            m_movesAmountText.SetValue(m_movesAmount);
            m_languageButton.onClick.RemoveAllListeners();
            m_languageButton.onClick.AddListener(OnClickLanguageButton);
            m_bombButton.onClick.RemoveAllListeners();
            m_bombButton.onClick.AddListener(OnClickBombButton);
            RefreshBombButton();
        }

        private void OnClickLanguageButton()
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

        private void OnClickBombButton()
        {
            onClickBombButton?.Invoke(m_currentBombCellType);
            m_audioService.Play(AudioFileNames.ButtonClickSfx);
            m_bombButton.interactable = false;
            m_coroutineService.DelayedCall(RefreshBombButton, 1f);
        }

        private void RefreshBombButton()
        {
            int randomBombCellType;

            do
            {
                randomBombCellType = Random.Range(0, m_gameSettings.cellAssets.Length);
            }
            while (randomBombCellType == m_currentBombCellType);

            m_currentBombCellType = randomBombCellType;
            var bombCellAsset = m_gameSettings.cellAssets[m_currentBombCellType];
            m_bombIcon.sprite = bombCellAsset.sprite;
            m_bombAnimator.runtimeAnimatorController = bombCellAsset.overrideController;
            m_bombAnimator.SetTrigger("Idle");
            m_bombButton.interactable = true;
        }

        public void IncreaseMovesAmount()
        {
            m_movesAmount++;
            m_movesAmountText.SetValueAnimated(m_movesAmount);
        }
    }
}