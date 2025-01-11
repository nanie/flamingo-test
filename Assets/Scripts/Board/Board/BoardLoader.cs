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
        private const float TILE_DISTANCE = 0.5f;
        public BoardLoader(Tile.Factory tileFactory, string boardDefinitionJson)
        {
            _currentLoaded = JsonConvert.DeserializeObject<Board>(boardDefinitionJson);
            _tileFactory = tileFactory;
        }
        public void Initialize()
        {
            Tile last = null;
            foreach (var item in _currentLoaded.Tiles)
            {
                _tiles.Add(_tileFactory.Create(new Tile.TileSettings
                {
                    position = GetPosition(last, item.Previous),
                    hasMinigame = item.Minigame.HasValue,
                    index = item.Minigame ?? -1
                }));
                last = _tiles[^1];
            }
        }
        private Vector3 GetPosition(Tile last, Board.Direction previous)
        {
            return last == null ? Vector3.zero : previous switch
            {
                Board.Direction.Left => last.transform.position + new Vector3(0, 0, TILE_DISTANCE),
                Board.Direction.Right => last.transform.position + new Vector3(0, 0, -TILE_DISTANCE),
                Board.Direction.Front => last.transform.position + new Vector3(-TILE_DISTANCE, 0, 0),
                Board.Direction.Back => last.transform.position + new Vector3(TILE_DISTANCE, 0, 0),
                _ => Vector3.zero
            };
        }
    }
}