using Flamingo.GameLoop.Signals;
using Flamingo.Minigame;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "MinigameInstaller", menuName = "Installers/MinigameInstaller")]
public class MinigameInstaller : ScriptableObjectInstaller<MinigameInstaller>
{
    [SerializeField] private List<MinigameData> _minigames;
    public override void InstallBindings()
    {
        Container.Bind<MinigameController>().AsSingle().WithArguments(_minigames).NonLazy();
        Container.DeclareSignal<MinigameRequestedSignal>();
        Container.DeclareSignal<MinigameCompletedSignal>();
        Container.BindMemoryPool<Minigame, Minigame.Pool>();
        Container.BindSignal<MinigameRequestedSignal>().ToMethod<MinigameController>(x => x.OnMinigameRequested).FromResolve();
        Container.BindSignal<MinigameCompletedSignal>().ToMethod<MinigameController>(x => x.OnMinigameEnd).FromResolve();
    }
}