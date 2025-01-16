using Flamingo.GameState;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameStateInstaller", menuName = "Installers/GameStateInstaller")]
public class GameStateInstaller : ScriptableObjectInstaller<GameStateInstaller>
{
    [SerializeField] private TextAsset _defaultBoardConfig;
    [SerializeField] private LevelData[] _levels;
    public override void InstallBindings()
    {
        Container.Bind<IGameStateService>().To<GameStateService>().AsSingle().WithArguments(_defaultBoardConfig, _levels).NonLazy();
    }
}