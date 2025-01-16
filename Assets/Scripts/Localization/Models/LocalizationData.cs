using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.Localization
{
    [CreateAssetMenu]
    public class LocalizationData : ScriptableObject
    {
        public string language;
        public TextAsset textAsset;       
    }
}
