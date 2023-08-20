using UnityEngine;
using Zenject;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "New Scriptable Object Installer", menuName = "Installers/Scriptable Object Installer")]
    public class MainSceneSOInstaller : ScriptableObjectInstaller<MainSceneSOInstaller>
    {
        [SerializeField] private AudioLibrary m_audioLibraryInstance;
        [SerializeField] private GameSettings m_gameSettingsInstance;

        public override void InstallBindings()
        {
            Container.Bind<AudioLibrary>().FromInstance(m_audioLibraryInstance);
            Container.Bind<GameSettings>().FromInstance(m_gameSettingsInstance);
        }
    }
}