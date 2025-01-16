using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using System;

namespace Flamingo.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private bool _autoInitialize = true;
        [Inject] private ILocalizationService _localizationService;
        private string _currentId;
        private void Start()
        {
            _localizationService.OnLanguageUpdate += UpdateValue;

            if (!_autoInitialize)
            {
                return;
            }

            if(_text == null)
            {
                _text = GetComponent<TextMeshProUGUI>();
            }
            _currentId = _text.text;
            _text.text = _localizationService.GetMessage(_currentId);
        }
        private void UpdateValue()
        {
            _text.text = _localizationService.GetMessage(_currentId);
        }
    }
}
