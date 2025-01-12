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

        private void OnButtonClicked()
        {
            controller.StartNewTurn();
        }
    }
}
