using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.Board
{
    public class TileView : MonoBehaviour
    {
        private Tile _tile;
        public void Initialize(Tile tile)
        {
            _tile = tile;
            _tile.OnDispose += OnTileDispose;
            transform.position = tile.Settings.position;
        }
        private void OnTileDispose()
        {
            _tile.OnDispose -= OnTileDispose;
            Destroy(gameObject);          
        }
    }
}
