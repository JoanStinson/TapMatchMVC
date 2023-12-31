﻿using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class MainSceneMonoInstaller : MonoInstaller
    {
        [Header("Services")]
        [SerializeField] private AudioService m_audioServiceInstance;
        [SerializeField] private CoroutineService m_coroutineServiceInstance;

        [Header("Prefabs")]
        [SerializeField] private CellView m_cellViewPrefab;
        [SerializeField] private Transform m_cellSlotPrefab;

        public override void InstallBindings()
        {
            BindServices();
            BindPrefabs();
        }

        private void BindServices()
        {
            Container.Bind<IAudioService>().FromInstance(m_audioServiceInstance);
            Container.Bind<ICoroutineService>().FromInstance(m_coroutineServiceInstance);
            Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();
        }

        private void BindPrefabs()
        {
            Container.BindFactory<CellView, CellView.Factory>().FromComponentInNewPrefab(m_cellViewPrefab);
            Container.BindFactory<Transform, CellView.SlotFactory>().FromComponentInNewPrefab(m_cellSlotPrefab);
        }
    }
}