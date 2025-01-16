using System;

namespace Flamingo.Localization
{
    public interface ILocalizationService
    {
        public event Action OnLanguageUpdate;
        public string GetMessage(string textId);
    }
}
