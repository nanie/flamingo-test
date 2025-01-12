using Flamingo.GameLoop.Signals;
using Flamingo.Player;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<TurnStartedSignal>();
        Container.DeclareSignal<PlayerMovedSignal>();
        Container.Bind<PlayerMovement>().AsSingle().NonLazy();
        Container.BindSignal<TurnStartedSignal>().ToMethod<PlayerMovement>(x => x.OnTurnStarted).FromResolve();
    }
}