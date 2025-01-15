using Flamingo.GameLoop;
using Flamingo.GameLoop.Signals;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameLoopInstaller", menuName = "Installers/GameLoopInstaller")]
public class GameLoopInstaller : ScriptableObjectInstaller<GameLoopInstaller>
{
    [SerializeField] private bool isMultiplayer;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<BoardLoadedSignal>();
        Container.DeclareSignal<TurnStartedSignal>();
        Container.DeclareSignal<TurnEndedSignal>();
        Container.DeclareSignal<PlayerMovedSignal>();
        Container.DeclareSignal<MinigameRequestedSignal>();
        Container.DeclareSignal<MinigameCompletedSignal>();
        Container.Bind<IGameLoop>().To<GameLoopController>().AsSingle().NonLazy();
        Container.BindSignal<BoardLoadedSignal>().ToMethod<IGameLoop>(x => x.OnBoardLoaded).FromResolve();
        Container.BindSignal<PlayerMovedSignal>().ToMethod<IGameLoop>(x => x.OnPlayerMoved).FromResolve();
        Container.BindSignal<MinigameCompletedSignal>().ToMethod<IGameLoop>(x => x.OnMinigameCompleted).FromResolve();
    }
}