using Flamingo.GameLoop.Signals;
using Flamingo.GameState;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Flamingo.Board
{
    public class BoardLoader : IInitializable
    {
        [Inject] IGameStateService gameStateService;
        private Board _currentLoaded;
        private List<Tile> _tiles = new List<Tile>();
        private Tile.Factory _tileFactory;
        private const float TILE_DISTANCE = 0.8f;
        private List<GameObject> _tilePrefabs;
        private GameObject _baseTile;
        private readonly SignalBus _signalBus;
        public BoardLoader(SignalBus signalBus, Tile.Factory tileFactory, List<GameObject> specialTilePrefabs, GameObject baseTile)
        {
            _signalBus = signalBus;
            _tileFactory = tileFactory;            
            _tilePrefabs = specialTilePrefabs;
            _baseTile = baseTile;
        }
        public void Initialize()
        {
            _currentLoaded = JsonConvert.DeserializeObject<Board>(gameStateService.BoardConfig);
            var anchor = new GameObject("BoardAnchor");
            List<Vector3> positions = new List<Vector3>();
            Vector3 position = Vector3.zero;
            foreach (var item in _currentLoaded.Tiles)
            {
                positions.Add(position);
                CreateTile(position, item, anchor.transform);
                position = GetPosition(position, item.Next);
            }
            _signalBus.Fire(CreateSignalData(positions));
        }

        private BoardLoadedSignal CreateSignalData(List<Vector3> positions)
        {
            List<BoardLoadedSignal.Tile> tiles = new List<BoardLoadedSignal.Tile>();

            for (int i = 0; i < _currentLoaded.Tiles.Length; i++)
            {
                tiles.Add(new BoardLoadedSignal.Tile
                {
                    Minigame = _currentLoaded.Tiles[i].Minigame,
                    Position = positions[i]
                });
            }
            return new BoardLoadedSignal { Tiles = tiles.ToArray() };
        }

        private void CreateTile(Vector3 position, Board.Tile item, Transform parent)
        {
            Tile newTile = _tileFactory.Create(new Tile.TileSettings
            {
                position = position,
                hasMinigame = item.Minigame.HasValue,
                index = item.Minigame ?? -1
            });

            _tiles.Add(newTile);

            TileView newTileView = GameObject.Instantiate(item.Minigame.HasValue ? _tilePrefabs[item.Minigame ?? 0] : _baseTile, parent).AddComponent<TileView>();
            newTileView.Initialize(newTile);
        }

        private Vector3 GetPosition(Vector3 lastPosition, Board.Direction direction)
        {
            return direction switch
            {
                Board.Direction.Left => lastPosition + new Vector3(0, 0, -TILE_DISTANCE),
                Board.Direction.Right => lastPosition + new Vector3(0, 0, TILE_DISTANCE),
                Board.Direction.Front => lastPosition + new Vector3(TILE_DISTANCE, 0, 0),
                Board.Direction.Back => lastPosition + new Vector3(-TILE_DISTANCE, 0, 0),
                _ => Vector3.zero
            };
        }
    }
}