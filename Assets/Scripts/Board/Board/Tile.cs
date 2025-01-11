using UnityEngine;
using Zenject;

namespace Flamingo.Board
{
    public class Tile : MonoBehaviour
    {
        public struct TileSettings
        {
            public Vector3 position;
            public bool hasMinigame;
            public int index;
        }
        public class Factory : PlaceholderFactory<TileSettings, Tile>
        {

        }
    }
}