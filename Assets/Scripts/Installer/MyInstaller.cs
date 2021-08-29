using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class MyInstaller : MonoInstaller
{
    [SerializeField] MinimapIcon minimapIcon;
    [SerializeField] SoundSetting soundSetting;
    [SerializeField] SoundPlay soundPlay;
    IPlaySound iPlaySound;
    public override void InstallBindings() {
        Container.Bind<MinimapIcon>().FromInstance(minimapIcon);
        Container.Bind<SoundSetting>().FromInstance(soundSetting);
        Container.Bind<IPlaySound>().FromMethod(GetResourceManager);
    }

    IPlaySound GetResourceManager(InjectContext context) {
        return soundPlay;

    }
}
