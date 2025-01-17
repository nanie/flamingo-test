using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Flamingo.Minigame
{
    public class QuizMinigameView : MinigameView
    {
        [Serializable]
        public class AnswerButton
        {
            public Button button;
            public TextMeshProUGUI label;
        }
        [SerializeField] private Image _mainImage;
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private PanelSlideAnimation _panelAnimation;
        [SerializeField] private AnswerButton[] _answerButtons;
        [SerializeField] private AnswerAnimation _answerAnimation;
        [SerializeField] private TextMeshProUGUI _answerResultText;
        [SerializeField] private TextMeshProUGUI _answerText;

        private int _selectedIndex;
        public override void Initialize()
        {
            for (int i = 0; i < _answerButtons.Length; i++)
            {
                int buttonIndex = i;
                _answerButtons[i].button.onClick.AddListener( delegate { OnAnswerClick(buttonIndex); } );
            }
            _answerAnimation.OnMinigameCloseClick += CloseMinigame;
        }
        public override void OnMinigameDispose()
        {
            Destroy(gameObject);
        }
        public override void Show()
        {
            _answerAnimation.ResetState();
            gameObject.SetActive(true);
        }
        public override void Hide()
        {
            _panelAnimation.Hide(() => { gameObject.SetActive(false); });           
        }
        public override void LoadData()
        {
            _questionText.text = _minigame.Data.Question;
            _mainImage.sprite = _minigame.GetSpriteFromCode(_minigame.Data.CustomImageID);
            for (int i = 0; i < _minigame.Data.Answers.Count; i++)
            {
                _answerButtons[i].label.text = _minigame.Data.Answers[i].Text;
            }
        }
        private void OnAnswerClick(int answerIndex)
        {
            _selectedIndex = answerIndex;
            _answerResultText.text = _minigame.GetResultText(answerIndex);
            _answerText.text =_minigame.GetAnswer();
            _answerAnimation.AnimateAnswer(_minigame.CorrectAnswerIndex, answerIndex);
        }
        private void CloseMinigame()
        {
            _minigame.AnswerQuestion(_selectedIndex);
        }
    }
}
