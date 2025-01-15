using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using DG.Tweening;

namespace Flamingo.GameLoop
{
    public class GameUIView : MonoBehaviour
    {
        [Inject] private IGameLoop controller;
        [SerializeField] private Button _startTurnButton;
        [SerializeField] private TextMeshProUGUI _rollText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _scoreAnimationText;

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

        private void OnTurnEnded(int score, int totalScore)
        {
            _startTurnButton.interactable = true;
            _rollText.text = "";
            AddScore(score, totalScore);
        }
        public void AddScore(int score, int total)
        {
            _scoreAnimationText.text = $"+{score}";
            _scoreAnimationText.color = new Color(_scoreAnimationText.color.r, _scoreAnimationText.color.g, _scoreAnimationText.color.b, 1);
            _scoreAnimationText.transform.localPosition = Vector3.zero;
            Sequence scoreSequence = DOTween.Sequence();

            scoreSequence
                .Append(_scoreAnimationText.transform.DOLocalMoveY(50f, 0.8f).SetEase(Ease.OutQuad)) // Move up
                .Join(_scoreAnimationText.DOFade(0f, 0.8f)) // Fade out
                .OnComplete(() =>
                {
                    _scoreText.text = total.ToString();
                });
        }
        private void OnButtonClicked()
        {
            controller.StartNewTurn();
            _startTurnButton.interactable = false;
        }
    }
}
