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
    [SerializeField] private TextAsset _jsonTest;
    public override void InstallBindings()
    {
        Container.DeclareSignal<BoardLoadedSignal>();
        Container.BindInterfacesAndSelfTo<BoardLoader>().AsSingle().WithArguments(_jsonTest.text, _tilePrefabs, _basicTilePrefab).NonLazy();
        Container.BindFactory<Tile.TileSettings, Tile, Tile.Factory>();
    }
}