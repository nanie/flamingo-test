using System;
using UnityEngine;
using Zenject;

namespace Flamingo.Board
{
    public class Tile : IDisposable
    {
        public struct TileSettings
        {
            public Vector3 position;
            public bool hasMinigame;
            public int index;
        }

        public TileSettings Settings => _tileSettings;

        private TileSettings _tileSettings;
        internal event Action OnDispose;

        public Tile(TileSettings tileSettings)
        {
            _tileSettings = tileSettings;
        }

        public class Factory : PlaceholderFactory<TileSettings, Tile>
        {

        }
        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}