using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class MainSceneMonoInstaller : MonoInstaller
    {
        [SerializeField] private AudioService m_audioServiceInstance;
        [SerializeField] private CellView m_cellViewPrefab;
        [SerializeField] private Transform m_cellSlotPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IAudioService>().FromInstance(m_audioServiceInstance);
            Container.BindFactory<CellView, CellView.Factory>().FromComponentInNewPrefab(m_cellViewPrefab);
            Container.BindFactory<Transform, CellView.SlotFactory>().FromComponentInNewPrefab(m_cellSlotPrefab);
        }
    }
}