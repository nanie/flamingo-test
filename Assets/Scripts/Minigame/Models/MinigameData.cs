using UnityEngine;

namespace Flamingo.Minigame
{
    [CreateAssetMenu]
    public class MinigameData : ScriptableObject
    {
        public int id;
        public MinigameView minigameView;
        public TextAsset minigameContent;
    }
}
