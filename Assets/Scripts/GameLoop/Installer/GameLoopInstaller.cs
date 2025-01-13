using Flamingo.GameLoop;
using Flamingo.GameLoop.Signals;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameLoopInstaller", menuName = "Installers/GameLoopInstaller")]
public class GameLoopInstaller : ScriptableObjectInstaller<GameLoopInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<BoardLoadedSignal>();
        Container.DeclareSignal<TurnStartedSignal>();
        Container.DeclareSignal<TurnEndedSignal>();
        Container.DeclareSignal<PlayerMovedSignal>();
        Container.DeclareSignal<MinigameRequestedSignal>();
        Container.DeclareSignal<MinigameCompletedSignal>();
        Container.Bind<GameLoopController>().AsSingle().NonLazy();
        Container.BindSignal<BoardLoadedSignal>().ToMethod<GameLoopController>(x => x.OnBoardLoaded).FromResolve();
        Container.BindSignal<PlayerMovedSignal>().ToMethod<GameLoopController>(x => x.OnPlayerMoved).FromResolve();
        Container.BindSignal<MinigameCompletedSignal>().ToMethod<GameLoopController>(x => x.OnMinigameCompleted).FromResolve();
    }
}