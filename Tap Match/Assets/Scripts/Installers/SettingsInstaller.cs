﻿using UnityEngine;
using Zenject;

namespace JGM.Game
{
    [CreateAssetMenu(fileName = "New Settings Installer", menuName = "Installers/Settings Installer")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField] private AudioLibrary m_audioLibraryInstance;

        public override void InstallBindings()
        {
            Container.Bind<AudioLibrary>().FromInstance(m_audioLibraryInstance);
        }
    }
}