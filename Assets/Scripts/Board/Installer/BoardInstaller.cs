using Flamingo.Board;
using Flamingo.GameLoop.Signals;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BoardInstaller", menuName = "Installers/BoardInstaller")]
public class BoardInstaller : ScriptableObjectInstaller<BoardInstaller>
{
    [SerializeField] private GameObject _basicTilePrefab;
    [SerializeField] private List<GameObject> _tilePrefabs;
    public override void InstallBindings()
    {
        Container.DeclareSignal<BoardLoadedSignal>();
        Container.BindInterfacesAndSelfTo<BoardLoader>().AsSingle().WithArguments(_tilePrefabs, _basicTilePrefab).NonLazy();
        Container.BindFactory<Tile.TileSettings, Tile, Tile.Factory>();
    }
}