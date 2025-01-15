using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace Flamingo.Minigame
{
    public class PanelSlideAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private float _slideDuration = 0.5f;

        private void OnEnable()
        {
            _canvasGroup.alpha = 0;
            _rect.transform.localPosition = new Vector3(0, -1000f, 0);
            _rect.DOAnchorPosY(0, _slideDuration).SetEase(Ease.OutElastic);
            _canvasGroup.DOFade(1, _fadeDuration);
        }
        public void Hide(Action animationEnded)
        {
            _rect.DOAnchorPosY(-1000f, _slideDuration).SetEase(Ease.InElastic);
            _canvasGroup.DOFade(0, _fadeDuration).OnComplete(() => { animationEnded(); });
        }
    }
}
