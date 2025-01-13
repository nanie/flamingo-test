using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Flamingo.GameLoop
{
    public class GameUIView : MonoBehaviour
    {
        [Inject] private GameLoopController controller;
        [SerializeField] private Button _startTurnButton;

        private void Awake()
        {
            _startTurnButton.onClick.AddListener(OnButtonClicked);     
        }

        private void OnEnable()
        {
            controller.OnTurnEnded += OnTurnEnded;
        }

        private void OnDisable()
        {
            controller.OnTurnEnded -= OnTurnEnded;
        }

        private void OnTurnEnded()
        {
            _startTurnButton.interactable = true;
        }

        private void OnButtonClicked()
        {
            controller.StartNewTurn();
            _startTurnButton.interactable = false;
        }
    }
}
