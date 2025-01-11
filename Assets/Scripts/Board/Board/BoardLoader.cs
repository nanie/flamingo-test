using UnityEngine;
using Zenject;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

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
                    index = item.Minigame.HasValue ? item.Minigame.Value : -1
                }));
                last = _tiles[_tiles.Count - 1];
            }
        }

        private Vector3 GetPosition(Tile last, Board.Direction previous)
        {
            if (last == null) { return Vector3.zero; }
            switch (previous)
            {
                case Board.Direction.Left:
                    return last.transform.position + new Vector3(0, 0, TILE_DISTANCE);
                case Board.Direction.Right:
                    return last.transform.position + new Vector3(0, 0, -TILE_DISTANCE);
                case Board.Direction.Front:
                    return last.transform.position + new Vector3(-TILE_DISTANCE, 0, 0);
                case Board.Direction.Back:
                    return last.transform.position + new Vector3(TILE_DISTANCE, 0, 0);
            }
            return Vector3.zero;
        }
    }
}