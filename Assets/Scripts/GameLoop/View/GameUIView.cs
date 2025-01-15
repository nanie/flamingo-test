using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace Flamingo.GameLoop
{
    public class GameUIView : MonoBehaviour
    {
        [Inject] private IGameLoop controller;
        [SerializeField] private Button _startTurnButton;
        [SerializeField] private TextMeshProUGUI _rollText;

        private void Awake()
        {
            _startTurnButton.onClick.AddListener(OnButtonClicked);     
        }

        private void OnEnable()
        {
            controller.OnTurnEnded += OnTurnEnded;
            controller.OnPlayerRoll += PlayerRoll;
        }

        private void OnDisable()
        {
            controller.OnTurnEnded -= OnTurnEnded;
            controller.OnPlayerRoll -= PlayerRoll;
        }

        private void PlayerRoll(int obj)
        {
            _rollText.text = obj.ToString();
        }

        private void OnTurnEnded()
        {
            _startTurnButton.interactable = true;
            _rollText.text = "";
        }

        private void OnButtonClicked()
        {
            controller.StartNewTurn();
            _startTurnButton.interactable = false;
        }
    }
}
