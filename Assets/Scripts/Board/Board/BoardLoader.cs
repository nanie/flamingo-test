using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Flamingo.Board
{
    public class BoardLoader : IInitializable
    {
        private Board _currentLoaded;
        private List<Tile> _tiles = new List<Tile>();
        private Tile.Factory _tileFactory;
        private const float TILE_DISTANCE = 0.8f;
        private List<GameObject> _tilePrefabs;
        private GameObject _baseTile;

        public BoardLoader(Tile.Factory tileFactory, string boardDefinitionJson, List<GameObject> specialTilePrefabs, GameObject baseTile)
        {
            _tileFactory = tileFactory;
            _currentLoaded = JsonConvert.DeserializeObject<Board>(boardDefinitionJson);
            _tilePrefabs = specialTilePrefabs;    
            _baseTile = baseTile;
        }
        public void Initialize()
        {
            Vector3 position = Vector3.zero;
            foreach (var item in _currentLoaded.Tiles)
            {
                CreateTile(position, item);
                position = GetPosition(position, item.Next);
            }
        }

        private void CreateTile(Vector3 position, Board.Tile item)
        {
            Tile newTile = _tileFactory.Create(new Tile.TileSettings
            {
                position = position,
                hasMinigame = item.Minigame.HasValue,
                index = item.Minigame ?? -1
            });

            _tiles.Add(newTile);

            TileView newTileView = GameObject.Instantiate(item.Minigame.HasValue ? _tilePrefabs[item.Minigame ?? 0] : _baseTile).AddComponent<TileView>();
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