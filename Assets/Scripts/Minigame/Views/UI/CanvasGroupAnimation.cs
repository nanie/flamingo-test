using DG.Tweening;
using UnityEngine;

namespace Flamingo.Minigame
{
    public class CanvasGroupAnimation : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.5f;

        private void Awake()
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
            }
            _canvasGroup.alpha = 0;
        }
        private void OnEnable()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1f, _fadeDuration).SetEase(Ease.OutQuad);
        }
        private void OnDisable()
        {
            _canvasGroup.alpha = 0;
        }
    }
}
