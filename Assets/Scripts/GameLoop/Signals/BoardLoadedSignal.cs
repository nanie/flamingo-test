using UnityEngine;

namespace Flamingo.GameLoop.Signals
{
    public class BoardLoadedSignal
    {
        public struct Tile
        {
            public Vector3 Position { get; set; }
            public int? Minigame { get; set; }
        }

        public Tile[] Tiles { get; set; }
    }
}
