using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Flamingo.Minigame
{
    public class FlagMinigameView : MinigameView
    {
        [Serializable]
        public class AnswerButton
        {
            public Button button;
            public Image image;
        }
        [SerializeField] private PanelSlideAnimation _panelAnimation;
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private AnswerButton[] _answerButtons;
     
        public override void Initialize()
        {
            for (int i = 0; i < _answerButtons.Length; i++)
            {
                int buttonIndex = i;
                _answerButtons[i].button.onClick.AddListener(delegate { OnAnswerClick(buttonIndex); });
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
            for (int i = 0; i < _minigame.Data.Answers.Count; i++)
            {
                _answerButtons[i].image.sprite = _minigame.GetSpriteFromCode(_minigame.Data.Answers[i].ImageID);
            }
        }
        private void OnAnswerClick(int answerIndex)
        {
            _minigame.AnswerQuestion(answerIndex);
        }
    }
}
