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
        public BoardLoader(Tile.Factory tileFactory, string boardDefinitionJson)
        {
            _currentLoaded = JsonConvert.DeserializeObject<Board>(boardDefinitionJson);
            _tileFactory = tileFactory;
        }
        public void Initialize()
        {
            Vector3 position = Vector3.zero;
            foreach (var item in _currentLoaded.Tiles)
            {
                _tiles.Add(_tileFactory.Create(new Tile.TileSettings
                {
                    position = position,
                    hasMinigame = item.Minigame.HasValue,
                    index = item.Minigame ?? -1
                }));
                position = GetPosition(position, item.Next);
            }
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