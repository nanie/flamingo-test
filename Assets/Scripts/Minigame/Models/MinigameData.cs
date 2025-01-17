using UnityEngine;

namespace Flamingo.Minigame
{
    [CreateAssetMenu]
    public class MinigameData : ScriptableObject
    {
        public MinigameView minigameView;
        public TextAsset minigameContent;
        public int score;
    }
}
