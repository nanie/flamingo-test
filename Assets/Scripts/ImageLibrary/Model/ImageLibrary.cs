using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flamingo.ImageProvider
{
    [CreateAssetMenu]
    public class ImageLibrary : ScriptableObject
    {
        [Serializable]
        public class IdentifyedImage
        {
            public string Id;
            public Sprite image;
        }

        public Sprite notFoundImage;
        public IdentifyedImage[] identifyedImages;

        public Dictionary<string, Sprite> GetImageDictionary()
        {
            Dictionary<string, Sprite> dic = new Dictionary<string, Sprite>();
            foreach (var item in identifyedImages)
            {
                if(dic.ContainsKey(item.Id))
                {
                    Debug.LogError($"Duplicated ID {item.Id} ID in library {name}");
                    continue;
                }
                dic.Add(item.Id, item.image);
            }
            return dic;
        }
    }
}
