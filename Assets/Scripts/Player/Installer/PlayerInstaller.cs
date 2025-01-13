using Flamingo.GameLoop;
using Flamingo.GameLoop.Signals;
using Flamingo.Player;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.DeclareSignal<TurnStartedSignal>();
        Container.DeclareSignal<PlayerMovedSignal>();
        Container.DeclareSignal<TurnEndedSignal>();
        Container.Bind<PlayerMovement>().AsSingle().NonLazy();
        Container.BindSignal<TurnStartedSignal>().ToMethod<PlayerMovement>(x => x.OnTurnStarted).FromResolve();
        Container.BindSignal<TurnEndedSignal>().ToMethod<PlayerMovement>(x => x.OnTurnEnded).FromResolve();
    }
}