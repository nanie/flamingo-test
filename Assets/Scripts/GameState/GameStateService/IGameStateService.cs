using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.GameState
{
    public interface IGameStateService
    {
        public string BoardConfig { get; }
        public LevelData[] Levels { get; }
        void LoadLevel(LevelData level);
    }
}