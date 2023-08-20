using UnityEngine;
using Zenject;

namespace JGM.Game
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private AudioService m_audioServiceInstance;

        public override void InstallBindings()
        {
            Container.Bind<IAudioService>().FromInstance(m_audioServiceInstance);

        }
    }
}