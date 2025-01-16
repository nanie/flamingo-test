using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flamingo.Minigame
{
    public class AnswerAnimation : MonoBehaviour
    {
        [Serializable]
        public struct RectPair
        {
            public Transform rect;
            public Transform rectPosition;
            [HideInInspector] public Vector3 originalPosition;
        }

        [SerializeField] private Graphic[] _elementsToFadeOut;
        [SerializeField] private Graphic[] _elementsToFadeIn;
        [SerializeField] RectPair[] _elementsToMove;
        [SerializeField] private Graphic[] _correctAnswerGraphic;
        [SerializeField] private Graphic[] _wrongAnswerGraphic;
        [SerializeField] private CanvasGroup _correctAnswerGroup;
        [SerializeField] private CanvasGroup _content;
        [SerializeField] private Button _finishButton;

        public Action OnMinigameCloseClick;
        private void Awake()
        {
            for (int i = 0; i < _elementsToMove.Length; i++)
            {
                _elementsToMove[i].originalPosition = _elementsToMove[i].rect.localPosition;
            }
            _finishButton.onClick.AddListener(() => { OnMinigameCloseClick.Invoke(); });
        }

        public void AnimateAnswer(int correctIndex, int chosenIndex)
        {
            _correctAnswerGroup.gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.Pause();
            if (correctIndex != chosenIndex)
            {
                sequence.Join(_wrongAnswerGraphic[chosenIndex].DOFade(1, 0.3f)).SetEase(Ease.Linear);
            }
            sequence.Join(_correctAnswerGraphic[correctIndex].DOFade(1, 0.3f)).SetEase(Ease.Linear);
            foreach (var item in _elementsToFadeOut)
            {
                sequence.Join(item.DOFade(0, 0.8f)).SetEase(Ease.Linear);
            }
            sequence.AppendInterval(0.5f);
            foreach (var item in _elementsToMove)
            {
                sequence.Join(item.rect.DOLocalMove(item.rectPosition.transform.localPosition, 1f));
            }
            sequence.AppendInterval(0.5f);
            foreach (var item in _elementsToFadeIn)
            {
                sequence.Join(item.DOFade(1, 1f)).SetEase(Ease.Linear);
            }
            sequence.AppendInterval(1f);
            sequence.Append(_correctAnswerGroup.DOFade(1, 1f)).SetEase(Ease.InCubic);
            sequence.Join(_content.DOFade(0, 1f)).SetEase(Ease.InCubic);
            sequence.Play().OnComplete(() => { _finishButton.enabled = true; });
        }

        public void ResetState()
        {
            foreach (var item in _elementsToFadeOut)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, 1);
            }
            foreach (var item in _elementsToFadeIn)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, 0);
            }
            for (int i = 0; i < _elementsToMove.Length; i++)
            {
                _elementsToMove[i].rect.transform.localPosition = _elementsToMove[i].originalPosition;
            }
            foreach (var item in _correctAnswerGraphic)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, 0);
            }
            foreach (var item in _wrongAnswerGraphic)
            {
                item.color = new Color(item.color.r, item.color.g, item.color.b, 0);
            }
            _correctAnswerGroup.alpha = 0;
            _content.alpha = 1;
            _finishButton.enabled = false;
            _correctAnswerGroup.gameObject.SetActive(false);
        }
    }
}
