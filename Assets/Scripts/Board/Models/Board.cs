using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.Board
{
    public struct Board
    {
        public enum Direction
        {
            Left, Right, Front, Back
        }
        public struct Tile
        {
            public int? Minigame { get; set; }
            public Direction Next { get; set; }
            public Direction Previous { get; set; }
        }
        public Tile[] Tiles { get; set; }
    }
}
