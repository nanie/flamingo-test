using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.GameState
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        public TextAsset board;
        public Sprite boardIcon;
        public string levelName;
    }
}
