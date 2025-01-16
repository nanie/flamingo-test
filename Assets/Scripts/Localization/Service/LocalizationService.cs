using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Flamingo.Localization
{
    public class LocalizationService : ILocalizationService
    {
        public event Action OnLanguageUpdate;
        public class Localization
        {
            public class Message
            {
                public string Id { get; set; }
                public string Text { get; set; }
            }
            public Message[] messages;
        }

        private LocalizationData[] _localizedTexts;
        private Localization _localization;
        private Dictionary<string, string> _messages;
        public LocalizationService(LocalizationData[] localizedTexts)
        {
            _localizedTexts = localizedTexts;
            _localization = JsonConvert.DeserializeObject<Localization>(localizedTexts[0].textAsset.text);
            UpdateMessages();
        }

        private void UpdateMessages()
        {
            _messages = new Dictionary<string, string>();
            foreach (var item in _localization.messages)
            {
                if (_messages.ContainsKey(item.Id))
                {
                    Debug.LogWarning($"ID {item.Id} duplicated on localization file");
                    continue;
                }
                _messages.Add(item.Id, item.Text);
            }
        }

        public string GetMessage(string textId)
        {
            if (_messages.ContainsKey(textId))
            {
                return _messages[textId];
            }

            return textId;
        }

        public void SetLanguage(string language)
        {
            LocalizationData newLocalization = _localizedTexts.Where(q => q.language == language).FirstOrDefault();
            if(newLocalization == null)
            {
                return;
            }
            _localization = JsonConvert.DeserializeObject<Localization>(newLocalization.textAsset.text);
            UpdateMessages();
            OnLanguageUpdate?.Invoke();
        }
    }
}
