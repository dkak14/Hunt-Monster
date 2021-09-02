using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class MyInstaller : MonoInstaller
{
    [SerializeField] MinimapIcon minimapIcon;
    [SerializeField] SoundSetting soundSetting;
    [SerializeField] SoundPlay soundPlay;
    [SerializeField] ParticleSpawner particleSpawner;
    public Actor actor;
    IPlaySound iPlaySound;
    public override void InstallBindings() {
        Container.Bind<MinimapIcon>().FromInstance(minimapIcon).AsSingle();
        Container.Bind<SoundSetting>().FromInstance(soundSetting).AsSingle();
        Container.Bind<IPlaySound>().FromMethod(GetResourceManager);
        Container.Bind<ParticleSpawner>().FromInstance(particleSpawner);
        //Container.BindFactory<Actor, Actor.ActorFactory>().FromComponentInNewPrefab(actor);
    }

    IPlaySound GetResourceManager(InjectContext context) {
        return soundPlay;

    }
}
public class Actor : MonoBehaviour {
    [System.Obsolete]
    public class Factory : Factory<Actor> {
        }
}

public class ActorSpawner {
    readonly Actor.Factory actorSpawner;
}
