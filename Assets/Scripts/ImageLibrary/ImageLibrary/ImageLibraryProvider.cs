using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Flamingo.ImageProvider
{
    public class ImageLibraryProvider
    {
        private Dictionary<string, Sprite> _images;
        private Sprite _defaultSprite;

        public ImageLibraryProvider(ImageLibrary imageLibrary)
        {
            _images = imageLibrary.GetImageDictionary();
            _defaultSprite = imageLibrary.notFoundImage;
        }

        public Sprite GetSprite(string spriteId)
        {
            if(string.IsNullOrEmpty(spriteId))
            {
                return _defaultSprite;
            }
            if(_images.ContainsKey(spriteId))
            {
                return _images[spriteId];
            }
            return _defaultSprite;
        }
    }
}
