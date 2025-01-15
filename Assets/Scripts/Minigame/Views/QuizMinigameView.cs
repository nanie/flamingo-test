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
        public override void Initialize()
        {
            for (int i = 0; i < _answerButtons.Length; i++)
            {
                int buttonIndex = i;
                _answerButtons[i].button.onClick.AddListener( delegate { OnAnswerClick(buttonIndex); } );
            }
        }
        public override void OnMinigameDispose()
        {
            Destroy(gameObject);
        }
        public override void Show()
        {
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
            _minigame.AnswerQuestion(answerIndex);
        }
    }
}
