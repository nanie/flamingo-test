using Flamingo.Board;
using System;
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
        Container.BindInterfacesAndSelfTo<BoardLoader>().AsSingle().WithArguments(_jsonTest.text).NonLazy();
        Container.BindFactory<Tile.TileSettings, Tile, Tile.Factory>().FromMethod(BuildTile);
    }

    private Tile BuildTile(DiContainer container, Tile.TileSettings settings)
    {
        var minigamePrefab = settings.hasMinigame && settings.index < _tilePrefabs.Count ? _tilePrefabs[settings.index] : _basicTilePrefab;
        var instance = container.InstantiatePrefabForComponent<Tile>(minigamePrefab);
        instance.transform.position = settings.position;
        return instance;
    }
}