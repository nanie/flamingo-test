using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Flamingo.SceneLoader
{
    public class SceneLoadAnimation : MonoBehaviour
    {
        [Inject] private ILoadSceneService _loadSceneService;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private float _slideDuration = 0.5f;
        [SerializeField] private Ease _easing;
        private float _startPosition;
        private void Awake()
        {
            _startPosition = _rect.localPosition.x;
            DontDestroyOnLoad(gameObject);
        }
        private void Start()
        {
            _loadSceneService.KeepWaitingForAnimation(true);
        }
        private void OnEnable()
        {
            _loadSceneService.OnSceneStartLoad += OnSceneStartLoading;
            _loadSceneService.OnSceneLoaded += OnSceneLoaded;
        }
        private void OnDisable()
        {
            _loadSceneService.OnSceneStartLoad -= OnSceneStartLoading;
            _loadSceneService.OnSceneLoaded -= OnSceneLoaded;
        }
        public void OnSceneStartLoading()
        {
            _rect.DOAnchorPosX(0f, _slideDuration).SetEase(_easing).OnComplete(() =>
            {
                _loadSceneService.OnAnimationFinish();
            });
        }
        public void OnSceneLoaded()
        {
            _rect.DOAnchorPosX(_startPosition * -1, _slideDuration).SetEase(_easing).OnComplete(() =>
            {
                Destroy(gameObject, 1);
            });
        }
    }
}
